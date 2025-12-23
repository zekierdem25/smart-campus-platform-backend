using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCampus.API.Services;

namespace SmartCampus.API.Controllers;

/// <summary>
/// Controller for analytics and reporting endpoints
/// </summary>
[ApiController]
[Route("api/v1/analytics")]
[Authorize]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly IExportService _exportService;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(
        IAnalyticsService analyticsService,
        IExportService exportService,
        ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _exportService = exportService;
        _logger = logger;
    }

    /// <summary>
    /// Get admin dashboard metrics
    /// </summary>
    [HttpGet("dashboard")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetDashboard()
    {
        try
        {
            var metrics = await _analyticsService.GetDashboardMetricsAsync();
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting dashboard metrics");
            return StatusCode(500, new { message = "Dashboard metrikleri alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get academic performance analytics
    /// </summary>
    [HttpGet("academic-performance")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> GetAcademicPerformance(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null)
    {
        try
        {
            var analytics = await _analyticsService.GetAcademicPerformanceAsync(semester, year);
            return Ok(analytics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting academic performance analytics");
            return StatusCode(500, new { message = "Akademik performans analitiği alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get attendance analytics
    /// </summary>
    [HttpGet("attendance")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> GetAttendanceAnalytics(
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null,
        [FromQuery] Guid? courseId = null)
    {
        try
        {
            var analytics = await _analyticsService.GetAttendanceAnalyticsAsync(semester, year, courseId);
            return Ok(analytics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting attendance analytics");
            return StatusCode(500, new { message = "Yoklama analitiği alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get meal usage analytics
    /// </summary>
    [HttpGet("meal-usage")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetMealUsageAnalytics(
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null)
    {
        try
        {
            var analytics = await _analyticsService.GetMealUsageAnalyticsAsync(dateFrom, dateTo);
            return Ok(analytics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting meal usage analytics");
            return StatusCode(500, new { message = "Yemek kullanım analitiği alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get events analytics
    /// </summary>
    [HttpGet("events")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<object>> GetEventsAnalytics(
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null)
    {
        try
        {
            var analytics = await _analyticsService.GetEventsAnalyticsAsync(dateFrom, dateTo);
            return Ok(analytics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting events analytics");
            return StatusCode(500, new { message = "Etkinlik analitiği alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Export analytics data to Excel, PDF, or CSV
    /// </summary>
    [HttpGet("export/{reportType}")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<IActionResult> ExportAnalytics(
        string reportType,
        [FromQuery] string format = "excel", // excel, pdf, csv
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null,
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null,
        [FromQuery] Guid? courseId = null)
    {
        try
        {
            object analyticsData;
            string title;

            // Get analytics data based on report type
            switch (reportType.ToLower())
            {
                case "academic":
                    analyticsData = await _analyticsService.GetAcademicPerformanceAsync(semester, year);
                    title = "Academic Performance Report";
                    break;

                case "attendance":
                    analyticsData = await _analyticsService.GetAttendanceAnalyticsAsync(semester, year, courseId);
                    title = "Attendance Analytics Report";
                    break;

                case "meal":
                    analyticsData = await _analyticsService.GetMealUsageAnalyticsAsync(dateFrom, dateTo);
                    title = "Meal Usage Report";
                    break;

                case "events":
                    analyticsData = await _analyticsService.GetEventsAnalyticsAsync(dateFrom, dateTo);
                    title = "Events Analytics Report";
                    break;

                case "dashboard":
                    analyticsData = await _analyticsService.GetDashboardMetricsAsync();
                    title = "Dashboard Metrics Report";
                    break;

                default:
                    return BadRequest(new { message = "Geçersiz rapor tipi. Geçerli tipler: academic, attendance, meal, events, dashboard" });
            }

            byte[] fileBytes;
            string contentType;
            string fileName;

            // Export based on format
            switch (format.ToLower())
            {
                case "excel":
                    fileBytes = await _exportService.ExportAnalyticsToExcelAsync(analyticsData, reportType);
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileName = $"{reportType}_report_{DateTime.Now:yyyyMMdd}.xlsx";
                    break;

                case "pdf":
                    fileBytes = await _exportService.ExportAnalyticsToPdfAsync(analyticsData, reportType, title);
                    contentType = "application/pdf";
                    fileName = $"{reportType}_report_{DateTime.Now:yyyyMMdd}.pdf";
                    break;

                case "csv":
                    fileBytes = await _exportService.ExportAnalyticsToCsvAsync(analyticsData, reportType);
                    contentType = "text/csv";
                    fileName = $"{reportType}_report_{DateTime.Now:yyyyMMdd}.csv";
                    break;

                default:
                    return BadRequest(new { message = "Geçersiz format. Geçerli formatlar: excel, pdf, csv" });
            }

            return File(fileBytes, contentType, fileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting analytics data");
            return StatusCode(500, new { message = "Rapor dışa aktarılırken bir hata oluştu." });
        }
    }
}

