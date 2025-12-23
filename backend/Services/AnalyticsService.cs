using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SmartCampus.API.Data;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly ApplicationDbContext _context;
    private readonly IGradeCalculationService _gradeCalculationService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<AnalyticsService> _logger;
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);

    public AnalyticsService(
        ApplicationDbContext context,
        IGradeCalculationService gradeCalculationService,
        IMemoryCache cache,
        ILogger<AnalyticsService> logger)
    {
        _context = context;
        _gradeCalculationService = gradeCalculationService;
        _cache = cache;
        _logger = logger;
    }

    public async Task<object> GetDashboardMetricsAsync()
    {
        try
        {
            // Try to get from cache
            var cacheKey = "dashboard_metrics";
            if (_cache.TryGetValue(cacheKey, out object? cachedMetrics))
            {
                return cachedMetrics!;
            }

            var today = DateTime.UtcNow.Date;
            var todayStart = today;
            var todayEnd = today.AddDays(1);

            // Total users
            var totalUsers = await _context.Users.CountAsync();

            // Active users today (users who logged in today)
            var activeUsersToday = await _context.ActivityLogs
                .Where(log => log.Action == "Login" && 
                             log.CreatedAt >= todayStart && 
                             log.CreatedAt < todayEnd)
                .Select(log => log.UserId)
                .Distinct()
                .CountAsync();

            // Total courses
            var totalCourses = await _context.Courses
                .Where(c => c.IsActive)
                .CountAsync();

            // Total enrollments
            var totalEnrollments = await _context.Enrollments
                .Where(e => e.Status == EnrollmentStatus.Active)
                .CountAsync();

            // Attendance rate (last 30 days)
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            var totalSessions = await _context.AttendanceSessions
                .Where(s => s.Date >= thirtyDaysAgo)
                .CountAsync();

            var totalRecords = await _context.AttendanceRecords
                .Where(r => r.CreatedAt >= thirtyDaysAgo)
                .CountAsync();

            var attendanceRate = totalSessions > 0 
                ? Math.Round((double)totalRecords / (totalSessions * 50) * 100, 1) // Assuming ~50 students per session average
                : 0;

            // Meal reservations today
            var mealReservationsToday = await _context.MealReservations
                .Where(r => r.Date >= todayStart && r.Date < todayEnd)
                .CountAsync();

            // Upcoming events (next 7 days)
            var upcomingEvents = await _context.Events
                .Where(e => e.Date >= today && e.Date <= today.AddDays(7) && e.Status == EventStatus.Published)
                .CountAsync();

            // System health (simple check - can be enhanced)
            var systemHealth = "healthy"; // TODO: Add actual health checks

            var metrics = new
            {
                totalUsers,
                activeUsersToday,
                totalCourses,
                totalEnrollments,
                attendanceRate,
                mealReservationsToday,
                upcomingEvents,
                systemHealth
            };

            // Cache the result
            _cache.Set(cacheKey, metrics, CacheExpiration);

            return metrics;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting dashboard metrics");
            throw;
        }
    }

    public async Task<object> GetAcademicPerformanceAsync(string? semester = null, int? year = null)
    {
        try
        {
            var currentYear = year ?? DateTime.UtcNow.Year;
            var currentSemester = semester ?? (DateTime.UtcNow.Month >= 9 ? "Fall" : "Spring");

            // Get enrollments with grades
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                    .ThenInclude(s => s.Department)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Course)
                .Where(e => e.Section.Semester == currentSemester && 
                           e.Section.Year == currentYear &&
                           e.GradePoint.HasValue)
                .ToListAsync();

            // Average GPA by department
            var gpaByDepartment = enrollments
                .GroupBy(e => e.Student.Department.Name)
                .Select(g => new
                {
                    Department = g.Key,
                    AverageGPA = Math.Round(g.Average(e => e.GradePoint!.Value), 2),
                    StudentCount = g.Select(e => e.StudentId).Distinct().Count(),
                    CourseCount = g.Select(e => e.Section.CourseId).Distinct().Count()
                })
                .OrderByDescending(x => x.AverageGPA)
                .ToList();

            // Grade distribution
            var gradeDistribution = enrollments
                .GroupBy(e => e.LetterGrade ?? "No Grade")
                .Select(g => new
                {
                    Grade = g.Key,
                    Count = g.Count(),
                    Percentage = Math.Round((double)g.Count() / enrollments.Count * 100, 1)
                })
                .OrderByDescending(x => x.Grade)
                .ToList();

            // Pass/Fail rates
            var passingGrades = new[] { "AA", "BA", "BB", "CB", "CC", "DC", "DD" };
            var passed = enrollments.Count(e => e.LetterGrade != null && passingGrades.Contains(e.LetterGrade));
            var failed = enrollments.Count - passed;
            var passRate = enrollments.Count > 0 
                ? Math.Round((double)passed / enrollments.Count * 100, 1) 
                : 0;
            var failRate = enrollments.Count > 0 
                ? Math.Round((double)failed / enrollments.Count * 100, 1) 
                : 0;

            // Top performing students (GPA >= 3.5)
            var topStudents = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Department)
                .Where(s => s.CGPA >= 3.5m)
                .OrderByDescending(s => s.CGPA)
                .Take(10)
                .Select(s => new
                {
                    StudentId = s.Id,
                    StudentNumber = s.StudentNumber,
                    Name = $"{s.User.FirstName} {s.User.LastName}",
                    Department = s.Department.Name,
                    CGPA = s.CGPA
                })
                .ToListAsync();

            // At-risk students (GPA < 2.0)
            var atRiskStudents = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Department)
                .Where(s => s.CGPA > 0 && s.CGPA < 2.0m)
                .OrderBy(s => s.CGPA)
                .Take(20)
                .Select(s => new
                {
                    StudentId = s.Id,
                    StudentNumber = s.StudentNumber,
                    Name = $"{s.User.FirstName} {s.User.LastName}",
                    Department = s.Department.Name,
                    CGPA = s.CGPA
                })
                .ToListAsync();

            return new
            {
                semester = currentSemester,
                year = currentYear,
                gpaByDepartment,
                gradeDistribution,
                passFailRates = new
                {
                    passRate,
                    failRate,
                    passed,
                    failed,
                    total = enrollments.Count
                },
                topStudents,
                atRiskStudents = new
                {
                    count = atRiskStudents.Count,
                    students = atRiskStudents
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting academic performance analytics");
            throw;
        }
    }

    public async Task<object> GetAttendanceAnalyticsAsync(string? semester = null, int? year = null, Guid? courseId = null)
    {
        try
        {
            var currentYear = year ?? DateTime.UtcNow.Year;
            var currentSemester = semester ?? (DateTime.UtcNow.Month >= 9 ? "Fall" : "Spring");

            // Get sessions for the period
            var sessionsQuery = _context.AttendanceSessions
                .Include(s => s.Section)
                    .ThenInclude(sec => sec.Course)
                .Where(s => s.Section.Semester == currentSemester && 
                           s.Section.Year == currentYear);

            if (courseId.HasValue)
            {
                sessionsQuery = sessionsQuery.Where(s => s.Section.CourseId == courseId.Value);
            }

            var sessions = await sessionsQuery
                .Include(s => s.Section)
                    .ThenInclude(sec => sec.Enrollments)
                .ToListAsync();

            var sessionIds = sessions.Select(s => s.Id).ToList();

            // Get all attendance records for these sessions
            var records = await _context.AttendanceRecords
                .Where(r => sessionIds.Contains(r.SessionId))
                .ToListAsync();

            // Attendance rate by course
            var attendanceByCourse = sessions
                .GroupBy(s => new { s.Section.CourseId, s.Section.Course.Code, s.Section.Course.Name })
                .Select(g =>
                {
                    var courseSessions = g.ToList();
                    var courseSessionIds = courseSessions.Select(s => s.Id).ToList();
                    var totalRecords = records.Count(r => courseSessionIds.Contains(r.SessionId));
                    
                    // Calculate expected attendance (sum of enrolled students per session)
                    var totalExpected = courseSessions.Sum(s => s.Section.Enrollments.Count(e => e.Status == EnrollmentStatus.Active));
                    
                    var attendanceRate = totalExpected > 0 
                        ? Math.Round((double)totalRecords / totalExpected * 100, 1) 
                        : 0;

                    return new
                    {
                        CourseId = g.Key.CourseId,
                        CourseCode = g.Key.Code,
                        CourseName = g.Key.Name,
                        TotalSessions = courseSessions.Count,
                        TotalRecords = totalRecords,
                        AttendanceRate = attendanceRate
                    };
                })
                .OrderByDescending(x => x.AttendanceRate)
                .ToList();

            // Attendance trends over time (last 30 days)
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            var trendData = await _context.AttendanceSessions
                .Where(s => s.Date >= thirtyDaysAgo)
                .GroupBy(s => s.Date.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    SessionCount = g.Count(),
                    RecordCount = _context.AttendanceRecords
                        .Count(r => g.Select(s => s.Id).Contains(r.SessionId))
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            // Students with critical absence rates (< 70%)
            var studentAttendanceRates = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Department)
                .Select(s => new
                {
                    Student = s,
                    TotalSessions = _context.AttendanceSessions
                        .Count(sess => sess.Section.Semester == currentSemester && 
                                      sess.Section.Year == currentYear &&
                                      sess.Section.Enrollments.Any(e => e.StudentId == s.Id && e.Status == EnrollmentStatus.Active)),
                    AttendedSessions = _context.AttendanceRecords
                        .Count(r => r.StudentId == s.Id &&
                                   r.Session.Section.Semester == currentSemester &&
                                   r.Session.Section.Year == currentYear)
                })
                .Where(x => x.TotalSessions > 0)
                .Select(x => new
                {
                    StudentId = x.Student.Id,
                    StudentNumber = x.Student.StudentNumber,
                    Name = $"{x.Student.User.FirstName} {x.Student.User.LastName}",
                    Department = x.Student.Department.Name,
                    TotalSessions = x.TotalSessions,
                    AttendedSessions = x.AttendedSessions,
                    AttendanceRate = Math.Round((double)x.AttendedSessions / x.TotalSessions * 100, 1)
                })
                .Where(x => x.AttendanceRate < 70)
                .OrderBy(x => x.AttendanceRate)
                .Take(20)
                .ToListAsync();

            // Courses with low attendance (< 75%)
            var lowAttendanceCourses = attendanceByCourse
                .Where(c => c.AttendanceRate < 75)
                .ToList();

            return new
            {
                semester = currentSemester,
                year = currentYear,
                attendanceByCourse,
                trends = trendData,
                criticalStudents = new
                {
                    count = studentAttendanceRates.Count,
                    students = studentAttendanceRates
                },
                lowAttendanceCourses = new
                {
                    count = lowAttendanceCourses.Count,
                    courses = lowAttendanceCourses
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting attendance analytics");
            throw;
        }
    }

    public async Task<object> GetMealUsageAnalyticsAsync(DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        try
        {
            dateFrom ??= DateTime.UtcNow.AddDays(-30);
            dateTo ??= DateTime.UtcNow;

            // Daily meal counts
            var dailyMealCounts = await _context.MealReservations
                .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
                .GroupBy(r => r.Date.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            // Cafeteria utilization
            var cafeteriaUtilization = await _context.MealReservations
                .Include(r => r.Menu)
                    .ThenInclude(m => m.Cafeteria)
                .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
                .GroupBy(r => r.Menu.Cafeteria.Name)
                .Select(g => new
                {
                    Cafeteria = g.Key,
                    TotalReservations = g.Count(),
                    UniqueUsers = g.Select(r => r.UserId).Distinct().Count()
                })
                .OrderByDescending(x => x.TotalReservations)
                .ToListAsync();

            // Peak hours (by meal type)
            var peakHours = await _context.MealReservations
                .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
                .GroupBy(r => r.MealType)
                .Select(g => new
                {
                    MealType = g.Key.ToString(),
                    Count = g.Count(),
                    AverageTime = g.Average(r => r.CreatedAt.Hour)
                })
                .ToListAsync();

            return new
            {
                period = new { from = dateFrom, to = dateTo },
                dailyMealCounts,
                cafeteriaUtilization,
                peakHours
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting meal usage analytics");
            throw;
        }
    }

    public async Task<object> GetEventsAnalyticsAsync(DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        try
        {
            dateFrom ??= DateTime.UtcNow.AddDays(-30);
            dateTo ??= DateTime.UtcNow;

            var events = await _context.Events
                .Include(e => e.EventRegistrations)
                .Where(e => e.Date >= dateFrom && e.Date <= dateTo)
                .ToListAsync();

            // Most popular events
            var popularEvents = events
                .OrderByDescending(e => e.EventRegistrations.Count)
                .Take(10)
                .Select(e => new
                {
                    EventId = e.Id,
                    Title = e.Title,
                    Category = e.Category.ToString(),
                    Date = e.Date,
                    RegistrationCount = e.EventRegistrations.Count,
                    Capacity = e.Capacity
                })
                .ToList();

            // Registration rates
            var registrationRates = events
                .Where(e => e.Capacity > 0)
                .Select(e => new
                {
                    EventId = e.Id,
                    Title = e.Title,
                    Registrations = e.EventRegistrations.Count,
                    Capacity = e.Capacity,
                    RegistrationRate = Math.Round((double)e.EventRegistrations.Count / e.Capacity * 100, 1)
                })
                .OrderByDescending(x => x.RegistrationRate)
                .ToList();

            // Check-in rates (if check-in is implemented)
            var checkInRates = events
                .Select(e => new
                {
                    EventId = e.Id,
                    Title = e.Title,
                    Registrations = e.EventRegistrations.Count,
                    CheckIns = e.EventRegistrations.Count(r => r.CheckedInAt.HasValue),
                    CheckInRate = e.EventRegistrations.Count > 0 
                        ? Math.Round((double)e.EventRegistrations.Count(r => r.CheckedInAt.HasValue) / e.EventRegistrations.Count * 100, 1)
                        : 0
                })
                .OrderByDescending(x => x.CheckInRate)
                .ToList();

            // Category breakdown
            var categoryBreakdown = events
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key.ToString(),
                    Count = g.Count(),
                    TotalRegistrations = g.Sum(e => e.EventRegistrations.Count)
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            return new
            {
                period = new { from = dateFrom, to = dateTo },
                popularEvents,
                registrationRates,
                checkInRates,
                categoryBreakdown
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting events analytics");
            throw;
        }
    }
}

