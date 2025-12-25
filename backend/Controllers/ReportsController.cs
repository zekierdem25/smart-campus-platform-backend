using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;

namespace SmartCampus.API.Controllers;

/// <summary>
/// Controller for scheduling reports and analytics
/// </summary>
[ApiController]
[Route("api/v1/scheduling/reports")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ReportsController> _logger;

    // Weekly available hours (5 days x 8 hours per day)
    private const int WeeklyAvailableHours = 40;
    private const int DailyAvailableSlots = 4; // 4 time slots per day (9-11, 11-13, 13-15, 15-17)

    public ReportsController(ApplicationDbContext context, ILogger<ReportsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // ========== CLASSROOM USAGE REPORT ==========

    /// <summary>
    /// Get classroom usage report with utilization rates
    /// </summary>
    [HttpGet("classroom-usage")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> GetClassroomUsageReport(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null,
        [FromQuery] Guid? classroomId = null,
        [FromQuery] Guid? buildingId = null)
    {
        var query = _context.Schedules
            .Include(s => s.Classroom)
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .AsQueryable();

        if (!string.IsNullOrEmpty(semester))
            query = query.Where(s => s.Semester == semester);
        if (year.HasValue)
            query = query.Where(s => s.Year == year.Value);
        if (classroomId.HasValue)
            query = query.Where(s => s.ClassroomId == classroomId.Value);

        var schedules = await query.ToListAsync();

        // Group by classroom
        var classroomUsage = schedules
            .GroupBy(s => s.ClassroomId)
            .Select(g => 
            {
                var classroom = g.First().Classroom;
                var totalHours = g.Sum(s => (s.EndTime - s.StartTime).TotalHours);
                var totalSessions = g.Count();
                var uniqueDays = g.Select(s => s.DayOfWeek).Distinct().Count();
                
                return new
                {
                    ClassroomId = g.Key,
                    Building = classroom.Building,
                    RoomNumber = classroom.RoomNumber,
                    Capacity = classroom.Capacity,
                    TotalHours = Math.Round(totalHours, 1),
                    TotalSessions = totalSessions,
                    DaysUsed = uniqueDays,
                    UtilizationRate = Math.Round((double)totalSessions / (DailyAvailableSlots * 5) * 100, 1),
                    Courses = g.Select(s => new
                    {
                        Code = s.Section.Course.Code,
                        Name = s.Section.Course.Name
                    }).Distinct().ToList()
                };
            })
            .OrderByDescending(x => x.UtilizationRate)
            .ToList();

        return Ok(new
        {
            semester,
            year,
            totalClassrooms = classroomUsage.Count,
            averageUtilization = classroomUsage.Any() 
                ? Math.Round(classroomUsage.Average(c => c.UtilizationRate), 1) 
                : 0,
            classrooms = classroomUsage
        });
    }

    // ========== RESOURCE UTILIZATION REPORT ==========

    /// <summary>
    /// Get comprehensive resource utilization report
    /// </summary>
    [HttpGet("resource-utilization")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetResourceUtilizationReport(
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null)
    {
        dateFrom ??= DateTime.UtcNow.AddMonths(-1);
        dateTo ??= DateTime.UtcNow;

        // Classroom utilization summary
        var classrooms = await _context.Classrooms
            .Where(c => c.IsActive)
            .Select(c => new { c.Id, c.Building, c.RoomNumber, c.Capacity })
            .ToListAsync();

        var classroomScheduleCounts = await _context.Schedules
            .GroupBy(s => s.ClassroomId)
            .Select(g => new { ClassroomId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.ClassroomId, x => x.Count);

        var classroomReservationCounts = await _context.ClassroomReservations
            .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
            .GroupBy(r => r.ClassroomId)
            .Select(g => new { ClassroomId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.ClassroomId, x => x.Count);

        var classroomStats = classrooms.Select(c => new
        {
            c.Id,
            c.Building,
            c.RoomNumber,
            c.Capacity,
            ScheduleCount = classroomScheduleCounts.GetValueOrDefault(c.Id, 0),
            ReservationCount = classroomReservationCounts.GetValueOrDefault(c.Id, 0)
        }).ToList();

        // Equipment utilization
        var equipmentStats = await _context.EquipmentBorrowings
            .Where(b => b.BorrowDate >= dateFrom && b.BorrowDate <= dateTo)
            .GroupBy(b => b.Equipment.Type)
            .Select(g => new
            {
                EquipmentType = g.Key.ToString(),
                TotalBorrowings = g.Count(),
                ActiveBorrowings = g.Count(b => b.Status == BorrowingStatus.Borrowed || b.Status == BorrowingStatus.Overdue),
                ReturnedBorrowings = g.Count(b => b.Status == BorrowingStatus.Returned),
                AverageBorrowDays = g.Where(b => b.ActualReturnDate.HasValue)
                    .Average(b => (b.ActualReturnDate!.Value - b.BorrowDate).TotalDays)
            })
            .ToListAsync();

        // Event statistics
        var eventStats = await _context.Events
            .Where(e => e.Date >= dateFrom && e.Date <= dateTo)
            .GroupBy(e => e.Status)
            .Select(g => new
            {
                Status = g.Key.ToString(),
                Count = g.Count()
            })
            .ToListAsync();

        // Meal statistics
        var mealStats = await _context.MealReservations
            .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
            .GroupBy(r => r.Status)
            .Select(g => new
            {
                Status = g.Key.ToString(),
                Count = g.Count()
            })
            .ToListAsync();

        return Ok(new
        {
            period = new { from = dateFrom, to = dateTo },
            classrooms = new
            {
                total = classroomStats.Count,
                totalCapacity = classroomStats.Sum(c => c.Capacity),
                averageScheduledSessions = classroomStats.Any() 
                    ? Math.Round(classroomStats.Average(c => c.ScheduleCount), 1) 
                    : 0,
                mostUsed = classroomStats
                    .OrderByDescending(c => c.ScheduleCount + c.ReservationCount)
                    .Take(5)
                    .Select(c => new { c.Building, c.RoomNumber, UsageCount = c.ScheduleCount + c.ReservationCount })
            },
            equipment = equipmentStats,
            events = eventStats,
            meals = mealStats
        });
    }

    // ========== CONFLICT REPORT ==========

    /// <summary>
    /// Get scheduling conflicts report
    /// </summary>
    [HttpGet("conflicts")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetConflictsReport(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null)
    {
        var currentYear = year ?? DateTime.UtcNow.Year;
        var currentSemester = semester ?? (DateTime.UtcNow.Month >= 9 ? "Fall" : "Spring");

        var schedules = await _context.Schedules
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Instructor)
                    .ThenInclude(i => i.User)
            .Include(s => s.Classroom)
            .Where(s => s.Semester == currentSemester && s.Year == currentYear)
            .ToListAsync();

        var conflicts = new List<object>();

        // Classroom double-booking check
        var classroomGroups = schedules
            .GroupBy(s => new { s.ClassroomId, s.DayOfWeek, s.StartTime })
            .Where(g => g.Count() > 1);

        foreach (var group in classroomGroups)
        {
            var items = group.ToList();
            conflicts.Add(new
            {
                Type = "ClassroomDoubleBooking",
                Severity = "High",
                Classroom = $"{items.First().Classroom.Building} {items.First().Classroom.RoomNumber}",
                DayOfWeek = group.Key.DayOfWeek.ToString(),
                Time = $"{group.Key.StartTime:hh\\:mm}",
                ConflictingCourses = items.Select(i => new
                {
                    Code = i.Section.Course.Code,
                    Name = i.Section.Course.Name,
                    Section = i.Section.SectionNumber
                }).ToList()
            });
        }

        // Instructor double-booking check
        var instructorGroups = schedules
            .Where(s => s.Section != null)
            .GroupBy(s => new { s.Section.InstructorId, s.DayOfWeek, s.StartTime })
            .Where(g => g.Count() > 1);

        foreach (var group in instructorGroups)
        {
            var items = group.ToList();
            var instructor = items.First().Section.Instructor;
            conflicts.Add(new
            {
                Type = "InstructorDoubleBooking",
                Severity = "High",
                Instructor = instructor != null ? $"{instructor.User.FirstName} {instructor.User.LastName}" : "Unknown",
                DayOfWeek = group.Key.DayOfWeek.ToString(),
                Time = $"{group.Key.StartTime:hh\\:mm}",
                ConflictingCourses = items.Select(i => new
                {
                    Code = i.Section.Course.Code,
                    Name = i.Section.Course.Name,
                    Section = i.Section.SectionNumber,
                    Classroom = $"{i.Classroom.Building} {i.Classroom.RoomNumber}"
                }).ToList()
            });
        }

        // Capacity issues
        var capacityIssues = schedules
            .Where(s => s.Section.EnrolledCount > s.Classroom.Capacity)
            .Select(s => new
            {
                Type = "CapacityExceeded",
                Severity = "Medium",
                Course = $"{s.Section.Course.Code} - {s.Section.Course.Name}",
                Section = s.Section.SectionNumber,
                Classroom = $"{s.Classroom.Building} {s.Classroom.RoomNumber}",
                ClassroomCapacity = s.Classroom.Capacity,
                EnrolledStudents = s.Section.EnrolledCount,
                Overflow = s.Section.EnrolledCount - s.Classroom.Capacity
            })
            .ToList();

        conflicts.AddRange(capacityIssues.Cast<object>());

        return Ok(new
        {
            semester = currentSemester,
            year = currentYear,
            totalConflicts = conflicts.Count,
            highSeverity = conflicts.Count(c => ((dynamic)c).Severity == "High"),
            mediumSeverity = conflicts.Count(c => ((dynamic)c).Severity == "Medium"),
            conflicts = conflicts.OrderByDescending(c => ((dynamic)c).Severity)
        });
    }

    // ========== ANALYTICS ENDPOINT ==========

    /// <summary>
    /// Get comprehensive scheduling analytics
    /// </summary>
    [HttpGet("analytics")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetAnalytics(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null)
    {
        try
        {
            var currentYear = year ?? DateTime.UtcNow.Year;
            var currentSemester = semester ?? (DateTime.UtcNow.Month >= 9 ? "Fall" : "Spring");

            // Fetch ALL data needed for analytics in one query to avoid multiple round-trips 
            // and EF Core translation issues with TimeSpan calculations and deep grouping.
            var schedules = await _context.Schedules
                .Include(s => s.Classroom)
                .Include(s => s.Section)
                    .ThenInclude(sec => sec.Course)
                        .ThenInclude(c => c.Department)
                .Where(s => s.Semester == currentSemester && s.Year == currentYear)
                .ToListAsync();

            // 1. Basic counts
            var totalClassrooms = await _context.Classrooms.CountAsync();
            var activeClassrooms = await _context.Classrooms.Where(c => c.IsActive).CountAsync();
            var totalSchedules = schedules.Count;

            // 2. Peak hours analysis (In Memory)
            var peakHours = schedules
                .GroupBy(s => s.StartTime.Hours)
                .Select(g => new
                {
                    Hour = g.Key,
                    SessionCount = g.Count()
                })
                .OrderByDescending(x => x.SessionCount)
                .Take(5)
                .ToList();

            // 3. Day distribution (In Memory)
            var dayDistribution = schedules
                .GroupBy(s => s.DayOfWeek)
                .OrderBy(g => g.Key) // Sort by DayOfWeek enum value (Mon, Tue...) rather than string
                .Select(g => new
                {
                    Day = g.Key.ToString(),
                    SessionCount = g.Count()
                })
                .ToList();

            // 4. Department load (In Memory)
            var departmentLoad = schedules
                .Where(s => s.Section?.Course?.Department != null)
                .GroupBy(s => s.Section!.Course!.Department!.Name)
                .Select(g => new
                {
                    Department = g.Key,
                    TotalSessions = g.Count(),
                    TotalHours = Math.Round(g.Sum(s => (s.EndTime - s.StartTime).TotalHours), 1)
                })
                .OrderByDescending(x => x.TotalSessions)
                .ToList();

            // 5. Most used classrooms (In Memory)
            var mostUsedClassrooms = schedules
                .Where(s => s.Classroom != null)
                .GroupBy(s => s.ClassroomId)
                .Select(g => new
                {
                    ClassroomId = g.Key,
                    Building = g.First().Classroom!.Building,
                    RoomNumber = g.First().Classroom!.RoomNumber,
                    SessionCount = g.Count()
                })
                .OrderByDescending(x => x.SessionCount)
                .Take(10)
                .ToList();

            // 6. Underutilized classrooms (DB for unused ids)
            var usedClassroomIds = schedules.Select(s => s.ClassroomId).Distinct().ToList();

            var underutilizedClassrooms = await _context.Classrooms
                .Where(c => c.IsActive && !usedClassroomIds.Contains(c.Id))
                .Select(c => new
                {
                    c.Id,
                    c.Building,
                    c.RoomNumber,
                    c.Capacity
                })
                .Take(10)
                .ToListAsync();

            // Calculate average utilization
            var totalPossibleSlots = activeClassrooms * DailyAvailableSlots * 5; // 5 days
            var averageUtilization = totalPossibleSlots > 0 
                ? Math.Round((double)totalSchedules / totalPossibleSlots * 100, 1) 
                : 0;

            return Ok(new
            {
                semester = currentSemester,
                year = currentYear,
                summary = new
                {
                    totalClassrooms,
                    activeClassrooms,
                    totalSchedules,
                    averageUtilization
                },
                peakHours,
                dayDistribution,
                departmentLoad,
                mostUsedClassrooms,
                underutilizedClassrooms
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating scheduling analytics");
            var errorMessage = ex.InnerException != null ? $"{ex.Message} -> {ex.InnerException.Message}" : ex.Message;
            return StatusCode(500, new { message = "Analiz raporu oluşturulurken bir hata oluştu.", error = errorMessage });
        }
    }

    // ========== INSTRUCTOR WORKLOAD REPORT ==========

    /// <summary>
    /// Get instructor workload distribution report
    /// </summary>
    [HttpGet("instructor-workload")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> GetInstructorWorkloadReport(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null,
        [FromQuery] Guid? departmentId = null)
    {
        var currentYear = year ?? DateTime.UtcNow.Year;
        var currentSemester = semester ?? (DateTime.UtcNow.Month >= 9 ? "Fall" : "Spring");

        var query = _context.Schedules
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Instructor)
                    .ThenInclude(i => i.User)
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
                    .ThenInclude(c => c.Department)
            .Where(s => s.Semester == currentSemester && s.Year == currentYear);

        if (departmentId.HasValue)
            query = query.Where(s => s.Section.Course.DepartmentId == departmentId.Value);

        var schedules = await query.ToListAsync();

        var workload = schedules
            .Where(s => s.Section.Instructor != null)
            .GroupBy(s => s.Section.InstructorId)
            .Select(g =>
            {
                var instructor = g.First().Section.Instructor!;
                var totalHours = g.Sum(s => (s.EndTime - s.StartTime).TotalHours);
                var uniqueCourses = g.Select(s => s.Section.CourseId).Distinct().Count();
                var uniqueSections = g.Select(s => s.SectionId).Distinct().Count();

                return new
                {
                    InstructorId = g.Key,
                    Name = $"{instructor.User.FirstName} {instructor.User.LastName}",
                    Email = instructor.User.Email,
                    Department = g.First().Section.Course.Department.Name,
                    WeeklyHours = Math.Round(totalHours, 1),
                    TotalCourses = uniqueCourses,
                    TotalSections = uniqueSections,
                    SessionsPerWeek = g.Count(),
                    Days = g.Select(s => s.DayOfWeek.ToString()).Distinct().ToList()
                };
            })
            .OrderByDescending(x => x.WeeklyHours)
            .ToList();

        return Ok(new
        {
            semester = currentSemester,
            year = currentYear,
            totalInstructors = workload.Count,
            averageWeeklyHours = workload.Any() ? Math.Round(workload.Average(w => w.WeeklyHours), 1) : 0,
            maxWeeklyHours = workload.Any() ? workload.Max(w => w.WeeklyHours) : 0,
            minWeeklyHours = workload.Any() ? workload.Min(w => w.WeeklyHours) : 0,
            instructors = workload
        });
    }
}
