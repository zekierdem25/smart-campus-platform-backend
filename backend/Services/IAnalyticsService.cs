using SmartCampus.API.DTOs;

namespace SmartCampus.API.Services;

/// <summary>
/// Service interface for analytics and reporting
/// </summary>
public interface IAnalyticsService
{
    // Dashboard Analytics
    Task<DashboardMetricsDto> GetDashboardMetricsAsync();
    
    // Academic Performance Analytics
    Task<AcademicPerformanceMetricsDto> GetAcademicPerformanceAsync(string? semester = null, int? year = null);
    
    // Attendance Analytics
    Task<AttendanceAnalyticsDto> GetAttendanceAnalyticsAsync(string? semester = null, int? year = null, Guid? courseId = null);
    
    // Meal Usage Analytics
    Task<MealUsageAnalyticsDto> GetMealUsageAnalyticsAsync(DateTime? dateFrom = null, DateTime? dateTo = null);
    
    // Events Analytics
    Task<EventsAnalyticsDto> GetEventsAnalyticsAsync(DateTime? dateFrom = null, DateTime? dateTo = null);
}
