namespace SmartCampus.API.Services;

/// <summary>
/// Service interface for exporting data to various formats
/// </summary>
public interface IExportService
{
    /// <summary>
    /// Export analytics data to Excel
    /// </summary>
    Task<byte[]> ExportAnalyticsToExcelAsync(object analyticsData, string reportType);

    /// <summary>
    /// Export analytics data to PDF
    /// </summary>
    Task<byte[]> ExportAnalyticsToPdfAsync(object analyticsData, string reportType, string title);

    /// <summary>
    /// Export analytics data to CSV
    /// </summary>
    Task<byte[]> ExportAnalyticsToCsvAsync(object analyticsData, string reportType);
}

