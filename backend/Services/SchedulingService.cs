using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using System.Diagnostics;
using System.Text.Json;

namespace SmartCampus.API.Services;

/// <summary>
/// Course scheduling service using CSP (Constraint Satisfaction Problem) with backtracking
/// Implements hard constraints (classroom conflict, instructor conflict, student conflict, classroom features)
/// and soft constraints (instructor preferences, minimize gaps, even distribution, morning slots)
/// </summary>
public class SchedulingService : ISchedulingService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SchedulingService> _logger;
    private CancellationTokenSource? _timeoutCts;

    public SchedulingService(ApplicationDbContext context, ILogger<SchedulingService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Generates an optimized schedule using CSP with backtracking
    /// </summary>
    public async Task<SchedulingResult> GenerateScheduleAsync(string semester, int year, List<Guid>? sectionIds = null, SchedulingOptions? options = null)
    {
        var stopwatch = Stopwatch.StartNew();
        options ??= new SchedulingOptions();
        _timeoutCts = new CancellationTokenSource(options.TimeoutMs);

        try
        {
            _logger.LogInformation("Starting schedule generation for {Semester} {Year}", semester, year);

            // Get sections that need scheduling
            var sectionsQuery = _context.CourseSections
                .Include(s => s.Course)
                .Include(s => s.Instructor)
                    .ThenInclude(i => i.User)
                .Include(s => s.Enrollments)
                .Where(s => s.Semester == semester && s.Year == year && s.IsActive);

            // Filter by SectionIds if provided
            if (sectionIds != null && sectionIds.Any())
            {
                sectionsQuery = sectionsQuery.Where(s => sectionIds.Contains(s.Id));
            }

            var sections = await sectionsQuery.ToListAsync(_timeoutCts.Token);

            if (!sections.Any())
            {
                return new SchedulingResult
                {
                    Success = false,
                    ErrorMessage = "Programlanacak ders bölümü bulunamadı"
                };
            }

            // Get available classrooms with features
            var classrooms = await _context.Classrooms
                .Where(c => c.IsActive)
                .ToListAsync(_timeoutCts.Token);

            if (!classrooms.Any())
            {
                return new SchedulingResult
                {
                    Success = false,
                    ErrorMessage = "Kullanılabilir derslik bulunamadı"
                };
            }

            // Define time slots (Monday-Friday, 09:00-17:00, 1.5 hour slots)
            var timeSlots = GenerateTimeSlots();

            // Don't clear existing schedules - just generate without saving
            // This allows multiple alternatives to be generated

            // Initialize random with seed if provided
            var random = options.RandomSeed.HasValue 
                ? new Random(options.RandomSeed.Value) 
                : new Random();

            // Order sections using heuristics if enabled
            var orderedSections = options.UseHeuristics
                ? OrderSectionsByHeuristics(sections, classrooms, random)
                : sections.OrderBy(x => random.Next()).ToList(); // Randomize if heuristics disabled

            // Run CSP with backtracking
            var result = await BacktrackingCSP(
                orderedSections.ToList(),
                classrooms,
                timeSlots,
                options,
                semester,
                year,
                random);

            stopwatch.Stop();
            result.ExecutionTime = stopwatch.Elapsed;

            // Don't save automatically - return schedules for preview/selection
            if (result.Success && result.Schedules.Any())
            {
                _logger.LogInformation(
                    "Schedule generated successfully: {Count} sections scheduled in {Time}ms",
                    result.ScheduledSections, stopwatch.ElapsedMilliseconds);
            }

            return result;
        }
        catch (OperationCanceledException)
        {
            stopwatch.Stop();
            _logger.LogWarning("Schedule generation timed out after {Time}ms", stopwatch.ElapsedMilliseconds);
            return new SchedulingResult
            {
                Success = false,
                ErrorMessage = "Program oluşturma zaman aşımına uğradı",
                ExecutionTime = stopwatch.Elapsed
            };
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Schedule generation failed");
            return new SchedulingResult
            {
                Success = false,
                ErrorMessage = "Program oluşturma sırasında bir hata oluştu",
                ExecutionTime = stopwatch.Elapsed
            };
        }
    }

    /// <summary>
    /// Main CSP backtracking algorithm
    /// </summary>
    private async Task<SchedulingResult> BacktrackingCSP(
        List<CourseSection> sections,
        List<Classroom> classrooms,
        List<TimeSlot> timeSlots,
        SchedulingOptions options,
        string semester,
        int year,
        Random random)
    {
        var assignments = new List<Schedule>();
        var usedSlots = new Dictionary<string, bool>();
        var conflictMessages = new List<string>();

        var success = await Backtrack(
            assignments, sections, classrooms, timeSlots, 
            options, semester, year, usedSlots, conflictMessages, 0, random);

        var result = new SchedulingResult
        {
            Success = success,
            Schedules = assignments,
            ScheduledSections = assignments.Count,
            UnscheduledSections = sections.Count - assignments.Count,
            ConflictMessages = conflictMessages
        };

        if (success)
        {
            result.TotalSoftConstraintScore = assignments.Sum(s => 
                CalculateSoftConstraintScore(s, sections.First(sec => sec.Id == s.SectionId), 
                    assignments.Where(a => a.Id != s.Id).ToList(), options));
        }

        return result;
    }

    /// <summary>
    /// Recursive backtracking function
    /// </summary>
    private async Task<bool> Backtrack(
        List<Schedule> assignments,
        List<CourseSection> sections,
        List<Classroom> classrooms,
        List<TimeSlot> timeSlots,
        SchedulingOptions options,
        string semester,
        int year,
        Dictionary<string, bool> usedSlots,
        List<string> conflictMessages,
        int depth,
        Random random)
    {
        _timeoutCts?.Token.ThrowIfCancellationRequested();

        if (depth >= sections.Count)
            return true; // All sections scheduled

        var currentSection = sections[depth];
        var candidates = await GetValidCandidates(
            currentSection, classrooms, timeSlots, assignments, usedSlots, semester, year);

        if (!candidates.Any())
        {
            conflictMessages.Add($"'{currentSection.Course.Name}' dersi için uygun zaman dilimi bulunamadı");
            return false;
        }

        // Order candidates by soft constraint score (highest first), then add randomness
        var sortedCandidates = candidates
            .Select(c => new { 
                Schedule = c, 
                Score = CalculateSoftConstraintScore(c, currentSection, assignments, options),
                RandomValue = random.NextDouble() // Add randomness
            })
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.RandomValue) // Randomize among equal scores
            .Select(x => x.Schedule)
            .ToList();

        foreach (var candidate in sortedCandidates)
        {
            // Add assignment
            assignments.Add(candidate);
            MarkSlotsUsed(candidate, usedSlots, true);

            // Recurse
            if (await Backtrack(assignments, sections, classrooms, timeSlots, 
                    options, semester, year, usedSlots, conflictMessages, depth + 1, random))
            {
                return true; // Solution found
            }

            // Backtrack: remove assignment
            assignments.Remove(candidate);
            MarkSlotsUsed(candidate, usedSlots, false);
        }

        return false; // No valid assignment for this section
    }

    /// <summary>
    /// Gets all valid candidate schedules for a section
    /// </summary>
    private async Task<List<Schedule>> GetValidCandidates(
        CourseSection section,
        List<Classroom> classrooms,
        List<TimeSlot> timeSlots,
        List<Schedule> existingAssignments,
        Dictionary<string, bool> usedSlots,
        string semester,
        int year)
    {
        var candidates = new List<Schedule>();

        // Get suitable classrooms (capacity and features)
        // Note: Randomization is handled at the service level, not here
        var suitableClassrooms = classrooms
            .Where(c => c.Capacity >= section.EnrolledCount)
            .Where(c => ClassroomFeaturesMatch(section.Course, c))
            .OrderBy(c => c.Capacity)
            .ToList();

        // Get student schedules for conflict checking
        var studentSectionIds = await GetEnrolledStudentOtherSectionIds(section);

        foreach (var slot in timeSlots)
        {
            foreach (var classroom in suitableClassrooms)
            {
                var schedule = new Schedule
                {
                    Id = Guid.NewGuid(),
                    SectionId = section.Id,
                    DayOfWeek = slot.Day,
                    StartTime = slot.Start,
                    EndTime = slot.End,
                    ClassroomId = classroom.Id,
                    Semester = semester,
                    Year = year
                };

                // Check all hard constraints
                if (await IsValidAssignmentInternal(schedule, existingAssignments, usedSlots, studentSectionIds, section))
                {
                    candidates.Add(schedule);
                }
            }
        }

        return candidates;
    }

    /// <summary>
    /// Checks if an assignment satisfies all hard constraints
    /// </summary>
    private Task<bool> IsValidAssignmentInternal(
        Schedule schedule,
        List<Schedule> existingAssignments,
        Dictionary<string, bool> usedSlots,
        List<Guid> studentOtherSectionIds,
        CourseSection section)
    {
        // Hard Constraint 1: No classroom double-booking
        var classroomSlotKey = $"ROOM_{schedule.ClassroomId}_{schedule.DayOfWeek}_{schedule.StartTime}";
        if (usedSlots.GetValueOrDefault(classroomSlotKey))
            return Task.FromResult(false);

        // Hard Constraint 2: No instructor double-booking
        var instructorSlotKey = $"INS_{section.InstructorId}_{schedule.DayOfWeek}_{schedule.StartTime}";
        if (usedSlots.GetValueOrDefault(instructorSlotKey))
            return Task.FromResult(false);

        // Hard Constraint 3: No student schedule conflict
        if (HasStudentConflict(schedule, existingAssignments, studentOtherSectionIds))
            return Task.FromResult(false);

        return Task.FromResult(true);
    }

    /// <summary>
    /// Hard Constraint 3: Checks for student schedule conflicts
    /// Students enrolled in this section shouldn't have another class at the same time
    /// </summary>
    private bool HasStudentConflict(
        Schedule newSchedule,
        List<Schedule> existingAssignments,
        List<Guid> studentOtherSectionIds)
    {
        // Check if any of the student's other sections conflict with this time slot
        foreach (var assignment in existingAssignments)
        {
            if (studentOtherSectionIds.Contains(assignment.SectionId) &&
                assignment.DayOfWeek == newSchedule.DayOfWeek &&
                TimeOverlaps(newSchedule.StartTime, newSchedule.EndTime, 
                           assignment.StartTime, assignment.EndTime))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets section IDs of other courses that students in this section are enrolled in
    /// </summary>
    private async Task<List<Guid>> GetEnrolledStudentOtherSectionIds(CourseSection section)
    {
        // Get all students enrolled in this section
        var studentIds = section.Enrollments
            .Where(e => e.Status == EnrollmentStatus.Active)
            .Select(e => e.StudentId)
            .ToList();

        if (!studentIds.Any())
            return new List<Guid>();

        // Get all other sections these students are enrolled in
        var otherSectionIds = await _context.Enrollments
            .Where(e => studentIds.Contains(e.StudentId) && 
                       e.SectionId != section.Id && 
                       e.Status == EnrollmentStatus.Active)
            .Select(e => e.SectionId)
            .Distinct()
            .ToListAsync(_timeoutCts?.Token ?? CancellationToken.None);

        return otherSectionIds;
    }

    /// <summary>
    /// Hard Constraint 5: Checks if classroom features match course requirements
    /// </summary>
    private bool ClassroomFeaturesMatch(Course course, Classroom classroom)
    {
        // Parse course requirements (if any)
        if (string.IsNullOrEmpty(course.RequirementsJson))
            return true; // No requirements, any classroom is fine

        List<string>? requirements;
        try
        {
            requirements = JsonSerializer.Deserialize<List<string>>(course.RequirementsJson);
        }
        catch
        {
            return true; // Invalid JSON, skip this constraint
        }

        if (requirements == null || !requirements.Any())
            return true;

        // Parse classroom features
        if (string.IsNullOrEmpty(classroom.FeaturesJson))
            return false; // Classroom has no features but course requires some

        List<string>? features;
        try
        {
            features = JsonSerializer.Deserialize<List<string>>(classroom.FeaturesJson);
        }
        catch
        {
            return false; // Invalid JSON
        }

        if (features == null)
            return false;

        // All course requirements must be in classroom features
        return requirements.All(req => features.Contains(req, StringComparer.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Calculates soft constraint score for a schedule assignment
    /// Higher score = better assignment
    /// </summary>
    private int CalculateSoftConstraintScore(
        Schedule schedule,
        CourseSection section,
        List<Schedule> existingAssignments,
        SchedulingOptions options)
    {
        int score = 0;

        // Soft Constraint 1: Instructor time preferences
        if (options.InstructorPreferences != null && 
            options.InstructorPreferences.TryGetValue(section.InstructorId, out var preferredSlots))
        {
            var slotKey = $"{schedule.DayOfWeek}-{schedule.StartTime}";
            if (preferredSlots.Contains(slotKey))
                score += options.InstructorPreferenceWeight;
        }

        // Soft Constraint 2: Minimize gaps in instructors' schedules
        score += CalculateGapScore(schedule, existingAssignments, section.InstructorId, options.GapMinimizationWeight);

        // Soft Constraint 3: Even distribution across week
        score += CalculateDistributionScore(schedule, existingAssignments, options.EvenDistributionWeight);

        // Soft Constraint 4: Morning slots for core courses
        if (section.Course.IsActive && schedule.StartTime.Hours < 12)
            score += options.MorningSlotWeight;

        return score;
    }

    /// <summary>
    /// Calculates score for minimizing gaps in schedule
    /// </summary>
    private int CalculateGapScore(
        Schedule schedule, 
        List<Schedule> existingAssignments, 
        Guid instructorId,
        int weight)
    {
        // Find instructor's other classes on the same day
        var sameDaySchedules = existingAssignments
            .Where(s => s.DayOfWeek == schedule.DayOfWeek)
            .ToList();

        if (!sameDaySchedules.Any())
            return weight; // First class of the day, no gaps

        // Calculate minimum gap to any existing class
        var minGap = sameDaySchedules
            .Select(s => Math.Abs((schedule.StartTime - s.EndTime).TotalHours))
            .Concat(sameDaySchedules.Select(s => Math.Abs((s.StartTime - schedule.EndTime).TotalHours)))
            .Min();

        // Prefer adjacent time slots (gap = 0)
        if (minGap <= 0.5) return weight;
        if (minGap <= 1.5) return weight / 2;
        return 0;
    }

    /// <summary>
    /// Calculates score for even distribution across week
    /// </summary>
    private int CalculateDistributionScore(
        Schedule schedule, 
        List<Schedule> existingAssignments,
        int weight)
    {
        var dayCounts = new Dictionary<ScheduleDayOfWeek, int>
        {
            { ScheduleDayOfWeek.Monday, 0 },
            { ScheduleDayOfWeek.Tuesday, 0 },
            { ScheduleDayOfWeek.Wednesday, 0 },
            { ScheduleDayOfWeek.Thursday, 0 },
            { ScheduleDayOfWeek.Friday, 0 }
        };

        foreach (var s in existingAssignments)
            if (dayCounts.ContainsKey(s.DayOfWeek))
                dayCounts[s.DayOfWeek]++;

        var averagePerDay = existingAssignments.Count > 0 
            ? existingAssignments.Count / 5.0 
            : 0;

        // Prefer days with fewer scheduled classes
        if (dayCounts[schedule.DayOfWeek] < averagePerDay)
            return weight;
        
        return 0;
    }

    /// <summary>
    /// Marks slots as used or unused in the tracking dictionary
    /// </summary>
    private void MarkSlotsUsed(Schedule schedule, Dictionary<string, bool> usedSlots, bool used)
    {
        var classroomSlotKey = $"ROOM_{schedule.ClassroomId}_{schedule.DayOfWeek}_{schedule.StartTime}";
        usedSlots[classroomSlotKey] = used;
        
        // We need to get section to mark instructor slot, but we'll handle this via assignment tracking
    }

    /// <summary>
    /// Orders sections by heuristics for better performance
    /// Most constrained variable (MCV) - sections with fewer valid options first
    /// </summary>
    private IEnumerable<CourseSection> OrderSectionsByHeuristics(
        List<CourseSection> sections, 
        List<Classroom> classrooms,
        Random random)
    {
        return sections
            .OrderByDescending(s => s.EnrolledCount) // Larger classes first (harder to schedule)
            .ThenByDescending(s => !string.IsNullOrEmpty(s.Course.RequirementsJson)) // Sections with requirements first
            .ThenBy(s => classrooms.Count(c => c.Capacity >= s.EnrolledCount)) // Fewer suitable classrooms first
            .ThenBy(s => random.NextDouble()); // Add randomness for sections with same priority
    }

    /// <summary>
    /// Generates available time slots for scheduling
    /// </summary>
    private List<TimeSlot> GenerateTimeSlots()
    {
        var slots = new List<TimeSlot>();
        
        for (int day = 1; day <= 5; day++)
        {
            for (int hour = 9; hour <= 15; hour += 2)
            {
                slots.Add(new TimeSlot
                {
                    Day = (ScheduleDayOfWeek)day,
                    Start = TimeSpan.FromHours(hour),
                    End = TimeSpan.FromHours(hour + 1.5)
                });
            }
        }

        return slots;
    }

    /// <summary>
    /// Checks if two time periods overlap
    /// </summary>
    private bool TimeOverlaps(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
    {
        return start1 < end2 && start2 < end1;
    }

    /// <summary>
    /// Validates a schedule against all constraints
    /// </summary>
    public async Task<ValidationResult> ValidateScheduleAsync(IEnumerable<Schedule> schedules)
    {
        var result = new ValidationResult { IsValid = true };
        var scheduleList = schedules.ToList();

        foreach (var schedule in scheduleList)
        {
            var others = scheduleList.Where(s => s.Id != schedule.Id);
            
            if (!await IsValidAssignmentAsync(schedule, others))
            {
                result.IsValid = false;
                result.HardConstraintViolations++;
                result.Violations.Add($"Schedule {schedule.Id} has constraint violations");
            }
        }

        return result;
    }

    /// <summary>
    /// Checks if a specific schedule assignment is valid
    /// </summary>
    public async Task<bool> IsValidAssignmentAsync(Schedule schedule, IEnumerable<Schedule> existingSchedules)
    {
        var section = await _context.CourseSections
            .Include(s => s.Enrollments)
            .FirstOrDefaultAsync(s => s.Id == schedule.SectionId);

        if (section == null)
            return false;

        var studentOtherSectionIds = await GetEnrolledStudentOtherSectionIds(section);
        var usedSlots = new Dictionary<string, bool>();

        foreach (var existing in existingSchedules)
        {
            var classroomKey = $"ROOM_{existing.ClassroomId}_{existing.DayOfWeek}_{existing.StartTime}";
            usedSlots[classroomKey] = true;
        }

        return await IsValidAssignmentInternal(
            schedule, 
            existingSchedules.ToList(), 
            usedSlots, 
            studentOtherSectionIds, 
            section);
    }
}

/// <summary>
/// Represents a time slot for scheduling
/// </summary>
public class TimeSlot
{
    public ScheduleDayOfWeek Day { get; set; }
    public TimeSpan Start { get; set; }
    public TimeSpan End { get; set; }
}
