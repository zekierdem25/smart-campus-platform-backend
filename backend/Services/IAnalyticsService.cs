namespace SmartCampus.API.Services;

/// <summary>
/// Service interface for analytics and reporting
/// </summary>
public interface IAnalyticsService
{
    // Dashboard Analytics
    Task<object> GetDashboardMetricsAsync();
    
    // Academic Performance Analytics
    Task<object> GetAcademicPerformanceAsync(string? semester = null, int? year = null);
    
    // Attendance Analytics
    Task<object> GetAttendanceAnalyticsAsync(string? semester = null, int? year = null, Guid? courseId = null);
    
    // Meal Usage Analytics
    Task<object> GetMealUsageAnalyticsAsync(DateTime? dateFrom = null, DateTime? dateTo = null);
    
    // Events Analytics
    Task<object> GetEventsAnalyticsAsync(DateTime? dateFrom = null, DateTime? dateTo = null);
}

