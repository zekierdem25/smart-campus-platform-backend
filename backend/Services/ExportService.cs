using ClosedXML.Excel;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using System.Text;
using System.Text.Json;

namespace SmartCampus.API.Services;

public class ExportService : IExportService
{
    private readonly ILogger<ExportService> _logger;

    public ExportService(ILogger<ExportService> logger)
    {
        _logger = logger;
    }

    public async Task<byte[]> ExportAnalyticsToExcelAsync(object analyticsData, string reportType)
    {
        return await Task.Run(() =>
        {
            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Report");

                // Header
                worksheet.Cell(1, 1).Value = $"Analytics Report - {reportType}";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 14;
                worksheet.Cell(2, 1).Value = $"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                int row = 4;

                // Convert data to JSON and parse for Excel
                var json = JsonSerializer.Serialize(analyticsData, new JsonSerializerOptions { WriteIndented = true });
                var jsonDoc = JsonDocument.Parse(json);

                // Simple export - flatten JSON structure
                ExportJsonToExcel(worksheet, jsonDoc.RootElement, ref row, 1);

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();

                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting analytics to Excel");
                throw;
            }
        });
    }

    private void ExportJsonToExcel(IXLWorksheet worksheet, JsonElement element, ref int row, int col)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var property in element.EnumerateObject())
                {
                    worksheet.Cell(row, col).Value = property.Name;
                    worksheet.Cell(row, col).Style.Font.Bold = true;
                    
                    if (property.Value.ValueKind == JsonValueKind.Array || property.Value.ValueKind == JsonValueKind.Object)
                    {
                        row++;
                        ExportJsonToExcel(worksheet, property.Value, ref row, col + 1);
                    }
                    else
                    {
                        worksheet.Cell(row, col + 1).Value = property.Value.ToString();
                        row++;
                    }
                }
                break;

            case JsonValueKind.Array:
                foreach (var item in element.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Object)
                    {
                        ExportJsonToExcel(worksheet, item, ref row, col);
                    }
                    else
                    {
                        worksheet.Cell(row, col).Value = item.ToString();
                        row++;
                    }
                }
                break;

            default:
                worksheet.Cell(row, col).Value = element.ToString();
                row++;
                break;
        }
    }

    public async Task<byte[]> ExportAnalyticsToPdfAsync(object analyticsData, string reportType, string title)
    {
        return await Task.Run(() =>
        {
            try
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                page.Width = XUnit.FromMillimeter(210); // A4 width
                page.Height = XUnit.FromMillimeter(297); // A4 height
                var gfx = XGraphics.FromPdfPage(page);

                // Fonts
                var fontTitle = new XFont("Arial", 18, XFontStyle.Bold);
                var fontHeader = new XFont("Arial", 12, XFontStyle.Bold);
                var fontNormal = new XFont("Arial", 10, XFontStyle.Regular);
                var fontSmall = new XFont("Arial", 9, XFontStyle.Regular);

                // Colors
                var colorBlue = XBrushes.DarkBlue;
                var colorBlack = XBrushes.Black;
                var colorGray = XBrushes.Gray;

                double yPos = 50;
                double leftMargin = 50;
                double rightMargin = page.Width.Point - 50;
                double pageWidth = page.Width.Point;

                // Title
                gfx.DrawString(title, fontTitle, colorBlue,
                    new XRect(leftMargin, yPos, pageWidth - 100, 25),
                    XStringFormats.TopCenter);
                yPos += 30;

                // Subtitle
                gfx.DrawString($"Report Type: {reportType}", fontSmall, colorGray,
                    new XRect(leftMargin, yPos, pageWidth - 100, 15),
                    XStringFormats.TopCenter);
                yPos += 20;

                gfx.DrawString($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}", fontSmall, colorGray,
                    new XRect(leftMargin, yPos, pageWidth - 100, 15),
                    XStringFormats.TopCenter);
                yPos += 30;

                // Divider
                gfx.DrawLine(new XPen(colorBlue, 2), leftMargin, yPos, rightMargin, yPos);
                yPos += 20;

                // Content - Simple text representation
                var json = JsonSerializer.Serialize(analyticsData, new JsonSerializerOptions { WriteIndented = true });
                var lines = json.Split('\n');
                
                foreach (var line in lines)
                {
                    if (yPos > page.Height.Point - 50)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yPos = 50;
                    }

                    if (line.Length > 0)
                    {
                        gfx.DrawString(line, fontNormal, colorBlack,
                            new XRect(leftMargin, yPos, pageWidth - 100, 15),
                            XStringFormats.TopLeft);
                        yPos += 15;
                    }
                }

                using var stream = new MemoryStream();
                document.Save(stream);
                return stream.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting analytics to PDF");
                throw;
            }
        });
    }

    public async Task<byte[]> ExportAnalyticsToCsvAsync(object analyticsData, string reportType)
    {
        return await Task.Run(() =>
        {
            try
            {
                var csv = new StringBuilder();
                csv.AppendLine($"Analytics Report - {reportType}");
                csv.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                csv.AppendLine();

                // Simple CSV export - convert JSON to CSV
                var json = JsonSerializer.Serialize(analyticsData);
                var jsonDoc = JsonDocument.Parse(json);

                ExportJsonToCsv(csv, jsonDoc.RootElement, "");

                return Encoding.UTF8.GetBytes(csv.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exporting analytics to CSV");
                throw;
            }
        });
    }

    private void ExportJsonToCsv(StringBuilder csv, JsonElement element, string prefix)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var property in element.EnumerateObject())
                {
                    var key = string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}.{property.Name}";
                    
                    if (property.Value.ValueKind == JsonValueKind.Array || property.Value.ValueKind == JsonValueKind.Object)
                    {
                        ExportJsonToCsv(csv, property.Value, key);
                    }
                    else
                    {
                        csv.AppendLine($"{key},{property.Value}");
                    }
                }
                break;

            case JsonValueKind.Array:
                int index = 0;
                foreach (var item in element.EnumerateArray())
                {
                    var key = $"{prefix}[{index}]";
                    if (item.ValueKind == JsonValueKind.Object || item.ValueKind == JsonValueKind.Array)
                    {
                        ExportJsonToCsv(csv, item, key);
                    }
                    else
                    {
                        csv.AppendLine($"{key},{item}");
                    }
                    index++;
                }
                break;

            default:
                csv.AppendLine($"{prefix},{element}");
                break;
        }
    }
}

