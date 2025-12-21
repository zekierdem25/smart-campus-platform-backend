using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System.Security.Claims;
using System.Text;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/scheduling")]
[Authorize]
public class SchedulingController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ISchedulingService _schedulingService;
    private readonly ILogger<SchedulingController> _logger;

    public SchedulingController(
        ApplicationDbContext context,
        ISchedulingService schedulingService,
        ILogger<SchedulingController> logger)
    {
        _context = context;
        _schedulingService = schedulingService;
        _logger = logger;
    }

    /// <summary>
    /// Generate schedule automatically (Admin only)
    /// Uses CSP (Constraint Satisfaction Problem) with backtracking algorithm
    /// Includes hard constraints: classroom conflict, instructor conflict, student conflict, classroom features
    /// Includes soft constraints: instructor preferences, minimize gaps, even distribution, morning slots
    /// </summary>
    [HttpPost("generate")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GenerateSchedule([FromBody] GenerateScheduleDto dto)
    {
        _logger.LogInformation("Schedule generation requested for {Semester} {Year}", dto.Semester, dto.Year);

        // Build scheduling options
        var options = new SchedulingOptions
        {
            TimeoutMs = dto.TimeoutMs ?? 30000,
            UseHeuristics = dto.UseHeuristics ?? true,
            InstructorPreferences = dto.InstructorPreferences,
            InstructorPreferenceWeight = dto.InstructorPreferenceWeight ?? 10,
            GapMinimizationWeight = dto.GapMinimizationWeight ?? 5,
            EvenDistributionWeight = dto.EvenDistributionWeight ?? 5,
            MorningSlotWeight = dto.MorningSlotWeight ?? 8
        };

        var result = await _schedulingService.GenerateScheduleAsync(dto.Semester, dto.Year, dto.SectionIds, options);

        if (!result.Success)
        {
            return BadRequest(new
            {
                message = result.ErrorMessage ?? "Program oluşturulamadı",
                conflictMessages = result.ConflictMessages,
                unscheduledSections = result.UnscheduledSections,
                executionTimeMs = result.ExecutionTime.TotalMilliseconds
            });
        }

        // Don't save automatically - return the schedule for preview
        return Ok(new
        {
            message = "Program başarıyla oluşturuldu",
            schedule = result.Schedules.Select(s => new
            {
                s.Id,
                s.SectionId,
                s.DayOfWeek,
                s.StartTime,
                s.EndTime,
                s.ClassroomId,
                s.Semester,
                s.Year
            }).ToList(),
            scheduledSections = result.ScheduledSections,
            unscheduledSections = result.UnscheduledSections,
            totalSoftConstraintScore = result.TotalSoftConstraintScore,
            executionTimeMs = result.ExecutionTime.TotalMilliseconds,
            semester = dto.Semester,
            year = dto.Year
        });
    }

    /// <summary>
    /// Save a generated schedule
    /// </summary>
    [HttpPost("save")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> SaveSchedule([FromBody] SaveScheduleDto dto)
    {
        _logger.LogInformation("Schedule save requested for {Semester} {Year}", dto.Semester, dto.Year);

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Clear existing schedules for this semester/year
            var existingSchedules = await _context.Schedules
                .Where(s => s.Semester == dto.Semester && s.Year == dto.Year)
                .ToListAsync();

            _context.Schedules.RemoveRange(existingSchedules);

            // Add new schedules
            var schedules = dto.Schedules.Select(item => new Schedule
            {
                Id = Guid.NewGuid(),
                SectionId = item.SectionId,
                DayOfWeek = item.DayOfWeek,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                ClassroomId = item.ClassroomId,
                Semester = dto.Semester,
                Year = dto.Year,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }).ToList();

            _context.Schedules.AddRange(schedules);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new
            {
                message = "Program başarıyla kaydedildi",
                savedSchedules = schedules.Count,
                semester = dto.Semester,
                year = dto.Year
            });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error saving schedule");
            return BadRequest(new { message = "Program kaydedilirken hata oluştu", error = ex.Message });
        }
    }

    /// <summary>
    /// Get schedule by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetSchedule(Guid id)
    {
        var schedule = await _context.Schedules
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Instructor)
                    .ThenInclude(i => i.User)
            .Include(s => s.Classroom)
            .Where(s => s.Id == id)
            .Select(s => new
            {
                s.Id,
                s.DayOfWeek,
                s.StartTime,
                s.EndTime,
                s.Semester,
                s.Year,
                Section = new
                {
                    s.Section.Id,
                    s.Section.SectionNumber,
                    CourseName = s.Section.Course.Name,
                    CourseCode = s.Section.Course.Code,
                    InstructorName = $"{s.Section.Instructor.User.FirstName} {s.Section.Instructor.User.LastName}"
                },
                Classroom = new
                {
                    s.Classroom.Id,
                    s.Classroom.Building,
                    s.Classroom.RoomNumber,
                    s.Classroom.Capacity
                }
            })
            .FirstOrDefaultAsync();

        if (schedule == null)
            return NotFound(new { message = "Program bulunamadı" });

        return Ok(schedule);
    }

    /// <summary>
    /// Get current user's schedule
    /// </summary>
    [HttpGet("my-schedule")]
    public async Task<ActionResult<object>> GetMySchedule(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Kullanıcı kimliği bulunamadı" });
        }

        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        semester ??= "Fall";
        year ??= DateTime.Now.Year;

        IQueryable<Schedule> query;

        if (userRole == "Student")
        {
            // Get student's enrolled sections
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                return NotFound(new { message = "Öğrenci bilgisi bulunamadı" });

            var enrolledSectionIds = await _context.Enrollments
                .Where(e => e.StudentId == student.Id && 
                           e.Status == EnrollmentStatus.Active)
                .Select(e => e.SectionId)
                .ToListAsync();

            query = _context.Schedules
                .Where(s => enrolledSectionIds.Contains(s.SectionId) &&
                           s.Semester == semester &&
                           s.Year == year);
        }
        else if (userRole == "Faculty")
        {
            // Get faculty's teaching sections
            var faculty = await _context.Faculties.FirstOrDefaultAsync(f => f.UserId == userId);
            if (faculty == null)
                return NotFound(new { message = "Öğretim üyesi bilgisi bulunamadı" });

            var teachingSectionIds = await _context.CourseSections
                .Where(cs => cs.InstructorId == faculty.Id && cs.IsActive)
                .Select(cs => cs.Id)
                .ToListAsync();

            query = _context.Schedules
                .Where(s => teachingSectionIds.Contains(s.SectionId) &&
                           s.Semester == semester &&
                           s.Year == year);
        }
        else
        {
            // Admin sees all
            query = _context.Schedules
                .Where(s => s.Semester == semester && s.Year == year);
        }

        var schedules = await query
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Instructor)
                    .ThenInclude(i => i.User)
            .Include(s => s.Classroom)
            .Where(s => s.Section != null && s.Section.Course != null && 
                       s.Section.Instructor != null && s.Section.Instructor.User != null && 
                       s.Classroom != null)
            .OrderBy(s => s.DayOfWeek)
            .ThenBy(s => s.StartTime)
            .Select(s => new
            {
                s.Id,
                s.DayOfWeek,
                s.StartTime,
                s.EndTime,
                CourseCode = s.Section.Course.Code,
                CourseName = s.Section.Course.Name,
                SectionNumber = s.Section.SectionNumber,
                InstructorName = $"{s.Section.Instructor.User.FirstName} {s.Section.Instructor.User.LastName}",
                Building = s.Classroom.Building,
                Room = s.Classroom.RoomNumber
            })
            .ToListAsync();

        // Group by day for weekly view
        var weeklySchedule = schedules
            .GroupBy(s => s.DayOfWeek)
            .OrderBy(g => g.Key)
            .ToDictionary(
                g => g.Key.ToString(),
                g => g.OrderBy(s => s.StartTime).ToList()
            );

        return Ok(new
        {
            semester,
            year,
            weeklySchedule,
            totalCourses = schedules.Count
        });
    }

    /// <summary>
    /// Export schedule as iCal format
    /// </summary>
    [HttpGet("my-schedule/ical")]
    public async Task<IActionResult> ExportToICal(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Kullanıcı kimliği bulunamadı" });
        }

        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        semester ??= "Fall";
        year ??= DateTime.Now.Year;

        // Get schedules (similar logic as GetMySchedule)
        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        var faculty = await _context.Faculties.FirstOrDefaultAsync(f => f.UserId == userId);

        IQueryable<Schedule> query = _context.Schedules
            .Where(s => s.Semester == semester && s.Year == year);

        if (student != null)
        {
            var enrolledSectionIds = await _context.Enrollments
                .Where(e => e.StudentId == student.Id && e.Status == EnrollmentStatus.Active)
                .Select(e => e.SectionId)
                .ToListAsync();

            query = query.Where(s => enrolledSectionIds.Contains(s.SectionId));
        }
        else if (faculty != null && userRole != "Admin")
        {
            var teachingSectionIds = await _context.CourseSections
                .Where(cs => cs.InstructorId == faculty.Id && cs.IsActive)
                .Select(cs => cs.Id)
                .ToListAsync();

            query = query.Where(s => teachingSectionIds.Contains(s.SectionId));
        }

        var schedules = await query
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Include(s => s.Classroom)
            .Where(s => s.Section != null && s.Section.Course != null && s.Classroom != null)
            .ToListAsync();

        // Generate iCal content
        var ical = new StringBuilder();
        ical.AppendLine("BEGIN:VCALENDAR");
        ical.AppendLine("VERSION:2.0");
        ical.AppendLine("PRODID:-//Smart Campus//Schedule//TR");

        foreach (var schedule in schedules)
        {
            if (schedule.Section?.Course == null || schedule.Classroom == null)
                continue;

            ical.AppendLine("BEGIN:VEVENT");
            ical.AppendLine($"UID:{schedule.Id}@smartcampus.com");
            ical.AppendLine($"SUMMARY:{schedule.Section.Course.Code} - {schedule.Section.Course.Name}");
            ical.AppendLine($"LOCATION:{schedule.Classroom.Building} {schedule.Classroom.RoomNumber}");
            ical.AppendLine($"RRULE:FREQ=WEEKLY;BYDAY={GetDayAbbreviation(schedule.DayOfWeek)}");
            ical.AppendLine("END:VEVENT");
        }

        ical.AppendLine("END:VCALENDAR");

        var bytes = Encoding.UTF8.GetBytes(ical.ToString());
        return File(bytes, "text/calendar", "schedule.ics");
    }

    private static string GetDayAbbreviation(ScheduleDayOfWeek day) => day switch
    {
        ScheduleDayOfWeek.Monday => "MO",
        ScheduleDayOfWeek.Tuesday => "TU",
        ScheduleDayOfWeek.Wednesday => "WE",
        ScheduleDayOfWeek.Thursday => "TH",
        ScheduleDayOfWeek.Friday => "FR",
        _ => "MO"
    };

    /// <summary>
    /// Get all schedules for admin
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetAllSchedules(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null,
        [FromQuery] Guid? classroomId = null)
    {
        semester ??= "Fall";
        year ??= DateTime.Now.Year;

        var query = _context.Schedules
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Instructor)
                    .ThenInclude(i => i.User)
            .Include(s => s.Classroom)
            .Where(s => s.Semester == semester && s.Year == year)
            .AsQueryable();

        if (classroomId.HasValue)
            query = query.Where(s => s.ClassroomId == classroomId.Value);

        var schedules = await query
            .OrderBy(s => s.DayOfWeek)
            .ThenBy(s => s.StartTime)
            .Select(s => new
            {
                s.Id,
                s.DayOfWeek,
                s.StartTime,
                s.EndTime,
                s.Semester,
                s.Year,
                CourseCode = s.Section.Course.Code,
                CourseName = s.Section.Course.Name,
                SectionNumber = s.Section.SectionNumber,
                InstructorName = $"{s.Section.Instructor.User.FirstName} {s.Section.Instructor.User.LastName}",
                ClassroomId = s.ClassroomId,
                Building = s.Classroom.Building,
                Room = s.Classroom.RoomNumber
            })
            .ToListAsync();

        return Ok(schedules);
    }
}

// DTOs
public record GenerateScheduleDto(
    string Semester, 
    int Year,
    List<Guid>? SectionIds = null,
    int? TimeoutMs = null,
    bool? UseHeuristics = null,
    Dictionary<Guid, List<string>>? InstructorPreferences = null,
    int? InstructorPreferenceWeight = null,
    int? GapMinimizationWeight = null,
    int? EvenDistributionWeight = null,
    int? MorningSlotWeight = null
);

public record SaveScheduleDto(
    string Semester,
    int Year,
    List<ScheduleItemDto> Schedules
);

public record ScheduleItemDto(
    Guid SectionId,
    ScheduleDayOfWeek DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    Guid ClassroomId
);
