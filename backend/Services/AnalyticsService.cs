using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.DTOs;

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

    public async Task<DashboardMetricsDto> GetDashboardMetricsAsync()
    {
        try
        {
            // Try to get from cache
            var cacheKey = "dashboard_metrics";
            if (_cache.TryGetValue(cacheKey, out DashboardMetricsDto? cachedMetrics))
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

            var totalFaculty = await _context.Faculties.CountAsync();

            var metrics = new DashboardMetricsDto
            {
                TotalStudents = totalUsers, // Warning: This might include non-students, refining below
                TotalCourses = totalCourses,
                TotalFaculty = totalFaculty,
                ActiveEnrollments = totalEnrollments
            };
            
            // To be more precise with TotalStudents
            metrics.TotalStudents = await _context.Students.CountAsync();

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

    public async Task<AcademicPerformanceMetricsDto> GetAcademicPerformanceAsync(string? semester = null, int? year = null)
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
                .Where(e => e.Student?.Department != null)
                .GroupBy(e => e.Student.Department.Name)
                .ToDictionary(
                    g => g.Key,
                    g => Math.Round(g.Average(e => (double)e.GradePoint!.Value), 2)
                );

            // Grade distribution
            var gradeDistribution = enrollments
                .Where(e => e.LetterGrade != null)
                .GroupBy(e => e.LetterGrade!)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count()
                );
                
            var averageGpa = enrollments.Any() ? Math.Round(enrollments.Average(e => (double)e.GradePoint!.Value), 2) : 0;

            return new AcademicPerformanceMetricsDto
            {
                Term = $"{currentSemester} {currentYear}",
                AverageGpa = averageGpa,
                GpaByDepartment = gpaByDepartment,
                GradeDistribution = gradeDistribution
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting academic performance analytics");
            throw;
        }
    }

    public async Task<AttendanceAnalyticsDto> GetAttendanceAnalyticsAsync(string? semester = null, int? year = null, Guid? courseId = null)
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
            var lowAttendanceCourses = sessions
                .GroupBy(s => new { s.Section.CourseId, s.Section.Course.Name })
                .Select(g =>
                {
                    var courseSessionIds = g.Select(s => s.Id).ToList();
                    var totalRecords = records.Count(r => courseSessionIds.Contains(r.SessionId));
                    var totalExpected = g.Sum(s => s.Section.Enrollments.Count(e => e.Status == EnrollmentStatus.Active));
                    
                    var rate = totalExpected > 0 ? (double)totalRecords / totalExpected * 100 : 0;
                    
                    return new CourseAttendanceDto
                    {
                        CourseId = g.Key.CourseId,
                        CourseName = g.Key.Name,
                        AttendanceRate = Math.Round(rate, 1)
                    };
                })
                .Where(c => c.AttendanceRate < 75)
                .ToList();

             // Simplified for this phase - skipping explicit student risk calculation or using dummy for now to match DTO
             // In a real implementation we would fetch students logic here
             
             // Calculate overall rate
             var grandTotalExpected = sessions.Sum(s => s.Section.Enrollments.Count(e => e.Status == EnrollmentStatus.Active));
             var grandTotalRecords = records.Count;
             var overallRate = grandTotalExpected > 0 ? (double)grandTotalRecords / grandTotalExpected * 100 : 0;

            return new AttendanceAnalyticsDto
            {
                OverallAttendanceRate = Math.Round(overallRate, 1),
                LowAttendanceCourses = lowAttendanceCourses,
                AtRiskStudents = new List<StudentAttendanceRiskDto>() // Populated in full implementation
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting attendance analytics");
            throw;
        }
    }

    public async Task<MealUsageAnalyticsDto> GetMealUsageAnalyticsAsync(DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        try
        {
            dateFrom ??= DateTime.UtcNow.AddDays(-30);
            dateTo ??= DateTime.UtcNow;

            var reservations = await _context.MealReservations
                .Include(r => r.Menu)
                    .ThenInclude(m => m.Cafeteria)
                .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
                .ToListAsync();

            var usageByCafeteria = reservations
                .GroupBy(r => r.Menu.Cafeteria.Name)
                .ToDictionary(g => g.Key, g => g.Count());

            var peakHours = reservations
                .GroupBy(r => $"{r.MealType}")
                .ToDictionary(g => g.Key, g => g.Count());

            return new MealUsageAnalyticsDto
            {
                TotalMealsServed = reservations.Count,
                UsageByCafeteria = usageByCafeteria,
                PeakHours = peakHours
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting meal usage analytics");
            throw;
        }
    }

    public async Task<EventsAnalyticsDto> GetEventsAnalyticsAsync(DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        try
        {
            dateFrom ??= DateTime.UtcNow.AddDays(-30);
            dateTo ??= DateTime.UtcNow;

            var events = await _context.Events
                .Include(e => e.EventRegistrations)
                .Where(e => e.Date >= dateFrom && e.Date <= dateTo)
                .ToListAsync();

            var popularCategories = events
                .GroupBy(e => e.Category.ToString())
                .ToDictionary(g => g.Key, g => g.Count());

            // 1. Popular Events (by registration count)
            var popularEvents = events
                .OrderByDescending(e => e.RegisteredCount)
                .Take(10)
                .Select(e => new EventPerformanceDto
                {
                    Title = e.Title,
                    RegistrationCount = e.RegisteredCount
                })
                .ToList();

            // 2. Registration Rates (registrations / capacity)
            var registrationRates = events
                .Where(e => e.Capacity > 0)
                .Select(e => new EventPerformanceDto
                {
                    Title = e.Title,
                    RegistrationRate = Math.Round((double)e.RegisteredCount / e.Capacity * 100, 1)
                })
                .OrderByDescending(r => r.RegistrationRate)
                .Take(10)
                .ToList();

            // 3. Check-in Rates (checked-in / registrations)
            // Include events even with 0 registrations to show data on the chart
            var checkInRates = events
                .Select(e => {
                    var checkedInCount = e.EventRegistrations.Count(r => r.CheckedIn);
                    var totalRegistrations = e.RegisteredCount; // Use denormalized count
                    return new EventPerformanceDto
                    {
                        Title = e.Title,
                        CheckInRate = totalRegistrations > 0 
                            ? Math.Round((double)checkedInCount / totalRegistrations * 100, 1) 
                            : 0
                    };
                })
                .OrderByDescending(r => r.CheckInRate)
                .Take(10)
                .ToList();

            return new EventsAnalyticsDto
            {
                TotalEvents = events.Count,
                TotalRegistrations = events.Sum(e => e.EventRegistrations.Count),
                PopularCategories = popularCategories,
                PopularEvents = popularEvents,
                RegistrationRates = registrationRates,
                CheckInRates = checkInRates
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting events analytics");
            throw;
        }
    }
}

