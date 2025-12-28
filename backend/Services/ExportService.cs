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

                // --- 1. Header (Logo/Name) ---
                worksheet.Cell(1, 1).Value = "SMART CAMPUS ANALYTICS";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;
                worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.DarkBlue;
                worksheet.Range(1, 1, 1, 5).Merge(); // Merge across 5 columns

                worksheet.Cell(2, 1).Value = $"Report: {reportType} | Date: {DateTime.Now:yyyy-MM-dd HH:mm}";
                worksheet.Cell(2, 1).Style.Font.FontSize = 10;
                worksheet.Cell(2, 1).Style.Font.FontColor = XLColor.Gray;

                int row = 4;

                // Convert data to JSON and parse for Excel
                var json = JsonSerializer.Serialize(analyticsData, new JsonSerializerOptions { WriteIndented = true });
                var jsonDoc = JsonDocument.Parse(json);

                // --- Export Logic with Styling ---
                ExportJsonToExcel(worksheet, jsonDoc.RootElement, ref row, 1);

                // --- 4. Auto-Fit Columns ---
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
                    // --- 2. Main Section Headers Styling ---
                    var headerCell = worksheet.Cell(row, col);
                    headerCell.Value = property.Name;
                    headerCell.Style.Font.Bold = true;
                    headerCell.Style.Fill.BackgroundColor = XLColor.LightBlue; // Light Blue Background
                    headerCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    
                    // Special handling for GradeDistribution (Table Format)
                    if (property.Name.Equals("GradeDistribution", StringComparison.OrdinalIgnoreCase) && property.Value.ValueKind == JsonValueKind.Array)
                    {
                         row++;
                         var startRow = row;
                         // Create Table Headers
                         worksheet.Cell(row, col).Value = "Grade";
                         worksheet.Cell(row, col).Style.Font.Bold = true;
                         worksheet.Cell(row, col).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                         
                         worksheet.Cell(row, col + 1).Value = "Count";
                         worksheet.Cell(row, col + 1).Style.Font.Bold = true;
                         worksheet.Cell(row, col + 1).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                         
                         row++;

                         foreach (var item in property.Value.EnumerateArray())
                         {
                             // Assuming item is object like { "name": "AA", "count": 5 }
                             if (item.ValueKind == JsonValueKind.Object)
                             {
                                 string name = item.GetProperty("name").ToString();
                                 string count = item.GetProperty("count").ToString();
                                 
                                 worksheet.Cell(row, col).Value = name;
                                 worksheet.Cell(row, col + 1).Value = int.Parse(count);
                                 row++;
                             }
                         }
                         
                         // --- 3. Borders for Table ---
                         var tableRange = worksheet.Range(startRow, col, row - 1, col + 1);
                         tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                         tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    }
                    else if (property.Value.ValueKind == JsonValueKind.Array || property.Value.ValueKind == JsonValueKind.Object)
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

                // Define Fonts (System fonts usually work for PdfSharpCore on Windows, but we ensure encoding)
                var fontTitle = new XFont("Arial", 22, XFontStyle.Bold);
                var fontHeader = new XFont("Arial", 14, XFontStyle.Bold);
                var fontNormal = new XFont("Arial", 11, XFontStyle.Regular);
                var fontBold = new XFont("Arial", 11, XFontStyle.Bold);
                var fontSmall = new XFont("Arial", 9, XFontStyle.Regular);
                var fontFooter = new XFont("Arial", 8, XFontStyle.Italic);

                // Colors
                var colorNavy = XColors.Navy;
                var colorGray = XColors.Gray;
                var colorBlack = XColors.Black;
                var colorLightBlue = XColors.LightBlue;
                var colorRed = XColors.DarkRed;
                var colorOrange = XColors.DarkOrange;

                double yPos = 40;
                double leftMargin = 40;
                double rightMargin = page.Width.Point - 40;
                double pageWidth = page.Width.Point;
                double contentWidth = rightMargin - leftMargin;

                // --- 1. Header Area ---
                // Institution Name
                gfx.DrawString("SMART CAMPUS", fontHeader, XBrushes.DarkGray,
                    new XRect(0, yPos, pageWidth, 20), XStringFormats.TopCenter);
                yPos += 25;

                // Dynamic Report Title
                string reportTitleText = "AKADEMİK PERFORMANS RAPORU";
                if (reportType.ToLower() == "attendance") reportTitleText = "YOKLAMA ANALİTİĞİ RAPORU";
                else if (reportType.ToLower() == "events") reportTitleText = "ETKİNLİK ANALİTİĞİ RAPORU";
                else if (reportType.ToLower() == "meal") reportTitleText = "YEMEK KULLANIM ANALİTİĞİ RAPORU";
                
                gfx.DrawString(reportTitleText, fontTitle, new XSolidBrush(colorNavy),
                    new XRect(0, yPos, pageWidth, 30), XStringFormats.TopCenter);
                yPos += 35;

                // Creation Date
                gfx.DrawString($"Oluşturulma: {DateTime.Now:dd.MM.yyyy HH:mm}", fontSmall, XBrushes.Gray,
                    new XRect(0, yPos, pageWidth, 15), XStringFormats.TopCenter);
                yPos += 30;

                // Divider Line
                gfx.DrawLine(new XPen(colorNavy, 2), leftMargin, yPos, rightMargin, yPos);
                yPos += 30;

                // Parse Data
                var json = JsonSerializer.Serialize(analyticsData);
                var data = JsonDocument.Parse(json).RootElement;

                // --- 2. Summary Info Section ---
                if (reportType.ToLower() == "academic")
                {
                    string term = "N/A";
                    if (data.TryGetProperty("semester", out var s))
                    {
                        term = s.ToString();
                        if (data.TryGetProperty("year", out var y))
                        {
                            term += " " + y.ToString();
                        }
                    }
                    else if (data.TryGetProperty("Term", out var t))
                    {
                        term = t.ToString();
                    }
                    else if (data.TryGetProperty("term", out var t2))
                    {
                        term = t2.ToString();
                    }
                    
                    double gpa = 0.0;
                    if (data.TryGetProperty("averageGPA", out var g)) gpa = g.GetDouble();
                    else if (data.TryGetProperty("AverageGpa", out var ag)) gpa = ag.GetDouble();

                    // Draw Summary Box
                    var boxHeight = 60;
                    var colWidth = contentWidth / 2 - 10;
                    
                    // Term Box
                    var rectTerm = new XRect(leftMargin, yPos, colWidth, boxHeight);
                    gfx.DrawRectangle(new XPen(colorNavy), new XSolidBrush(XColors.AliceBlue), rectTerm);
                    gfx.DrawString("Dönem", fontBold, XBrushes.Navy, new XRect(leftMargin + 10, yPos + 10, colWidth, 20), XStringFormats.TopLeft);
                    gfx.DrawString(term, fontNormal, XBrushes.Black, new XRect(leftMargin + 10, yPos + 35, colWidth, 20), XStringFormats.TopLeft);

                    // GPA Box
                    var rectGpa = new XRect(leftMargin + colWidth + 20, yPos, colWidth, boxHeight);
                    gfx.DrawRectangle(new XPen(colorNavy), new XSolidBrush(XColors.AliceBlue), rectGpa);
                    gfx.DrawString("Genel Ortalama (GPA)", fontBold, XBrushes.Navy, new XRect(leftMargin + colWidth + 30, yPos + 10, colWidth, 20), XStringFormats.TopLeft);
                    gfx.DrawString(gpa.ToString("F2"), fontNormal, XBrushes.Black, new XRect(leftMargin + colWidth + 30, yPos + 35, colWidth, 20), XStringFormats.TopLeft);

                    yPos += boxHeight + 30;

                    // --- 3. Grade Distribution Table ---
                    gfx.DrawString("Not Dağılımı", fontHeader, XBrushes.Black, leftMargin, yPos);
                    yPos += 20;

                    // Table Header
                    var tableY = yPos;
                    var col1X = leftMargin;
                    var col2X = leftMargin + 150;
                    var rowHeight = 25;

                    gfx.DrawRectangle(new XSolidBrush(colorLightBlue), col1X, tableY, 150, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(colorLightBlue), col2X, tableY, 150, rowHeight);
                    gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 150, rowHeight);
                    gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 150, rowHeight);
                    
                    gfx.DrawString("Harf Notu", fontBold, XBrushes.Black, new XRect(col1X, tableY, 150, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Öğrenci Sayısı", fontBold, XBrushes.Black, new XRect(col2X, tableY, 150, rowHeight), XStringFormats.Center);
                    
                    tableY += rowHeight;

                    // Table Content
                    JsonElement grades = default;
                    if (data.TryGetProperty("gradeDistribution", out var gd)) grades = gd;
                    else if (data.TryGetProperty("GradeDistribution", out var gd2)) grades = gd2;

                    if (grades.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in grades.EnumerateArray())
                        {
                            string name = item.TryGetProperty("name", out var n) ? n.ToString() : "";
                            string count = item.TryGetProperty("count", out var c) ? c.ToString() : "0";

                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 150, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 150, rowHeight);

                            gfx.DrawString(name, fontNormal, XBrushes.Black, new XRect(col1X, tableY, 150, rowHeight), XStringFormats.Center);
                            gfx.DrawString(count, fontNormal, XBrushes.Black, new XRect(col2X, tableY, 150, rowHeight), XStringFormats.Center);

                            tableY += rowHeight;
                        }
                    }
                    else if (grades.ValueKind == JsonValueKind.Object)
                    {
                         foreach (var prop in grades.EnumerateObject())
                        {
                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 150, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 150, rowHeight);

                            gfx.DrawString(prop.Name, fontNormal, XBrushes.Black, new XRect(col1X, tableY, 150, rowHeight), XStringFormats.Center);
                            gfx.DrawString(prop.Value.ToString(), fontNormal, XBrushes.Black, new XRect(col2X, tableY, 150, rowHeight), XStringFormats.Center);

                            tableY += rowHeight;
                        }
                    }
                }
                else if (reportType.ToLower() == "attendance")
                {
                    // --- ATTENDANCE REPORT LOGIC ---
                    
                    // 1. Summary Box (Overall Attendance)
                    double overallRate = 0.0;
                    if (data.TryGetProperty("overallAttendanceRate", out var r)) overallRate = r.GetDouble();
                    else if (data.TryGetProperty("OverallAttendanceRate", out var r2)) overallRate = r2.GetDouble();

                    var boxHeight = 60;
                    var colWidth = contentWidth / 2 - 10;
                    var rectRate = new XRect(leftMargin, yPos, colWidth, boxHeight);
                    gfx.DrawRectangle(new XPen(colorNavy), new XSolidBrush(XColors.AliceBlue), rectRate);
                    gfx.DrawString("Genel Devam Oranı", fontBold, XBrushes.Navy, new XRect(leftMargin + 10, yPos + 10, colWidth, 20), XStringFormats.TopLeft);
                    gfx.DrawString($"%{overallRate:F1}", fontTitle, XBrushes.Black, new XRect(leftMargin + 10, yPos + 35, colWidth, 20), XStringFormats.TopLeft);
                    yPos += boxHeight + 30;

                    // 2. Low Attendance Courses Table
                    gfx.DrawString("Düşük Katılımlı Dersler", fontHeader, new XSolidBrush(colorOrange), leftMargin, yPos);
                    yPos += 20;

                    var tableY = yPos;
                    var col1X = leftMargin; 
                    var col2X = leftMargin + 100; 
                    var col3X = leftMargin + 350;
                    var rowHeight = 25;

                    // Headers
                    gfx.DrawRectangle(new XSolidBrush(XColors.Orange), col1X, tableY, 100, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(XColors.Orange), col2X, tableY, 250, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(XColors.Orange), col3X, tableY, 100, rowHeight);
                    gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 100, rowHeight);
                    gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 250, rowHeight);
                    gfx.DrawRectangle(new XPen(colorBlack), col3X, tableY, 100, rowHeight);

                    gfx.DrawString("Kod", fontBold, XBrushes.White, new XRect(col1X, tableY, 100, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Ders Adı", fontBold, XBrushes.White, new XRect(col2X, tableY, 250, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Oran", fontBold, XBrushes.White, new XRect(col3X, tableY, 100, rowHeight), XStringFormats.Center);
                    tableY += rowHeight;

                    JsonElement lowCourses = default;
                    JsonElement coursesList = default;
                    if (data.TryGetProperty("lowAttendanceCourses", out var lc)) lowCourses = lc;
                    else if (data.TryGetProperty("LowAttendanceCourses", out var lc2)) lowCourses = lc2;

                    if (lowCourses.ValueKind == JsonValueKind.Object && lowCourses.TryGetProperty("courses", out var cl)) coursesList = cl;
                    else if (lowCourses.ValueKind == JsonValueKind.Array) coursesList = lowCourses;
                    
                    if (coursesList.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var course in coursesList.EnumerateArray())
                        {
                            string code = course.TryGetProperty("courseCode", out var cc) ? cc.ToString() : (course.TryGetProperty("CourseId", out var ci) ? ci.ToString().Substring(0, Math.Min(8, ci.ToString().Length)) : "-");
                            string name = course.TryGetProperty("courseName", out var cn) ? cn.ToString() : (course.TryGetProperty("CourseName", out var cn2) ? cn2.ToString() : "-");
                            double rate = course.TryGetProperty("attendanceRate", out var ar) ? ar.GetDouble() : (course.TryGetProperty("AttendanceRate", out var ar2) ? ar2.GetDouble() : 0.0);

                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 100, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 250, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col3X, tableY, 100, rowHeight);

                            gfx.DrawString(code, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 90, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(name, fontNormal, XBrushes.Black, new XRect(col2X + 5, tableY + 5, 240, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString($"%{rate:F1}", fontBold, XBrushes.DarkOrange, new XRect(col3X, tableY + 5, 100, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                    yPos = tableY + 30;

                    // 3. Critical Students Table
                    gfx.DrawString("Kritik Devamsızlar (Risk Altında)", fontHeader, new XSolidBrush(colorRed), leftMargin, yPos);
                    yPos += 20;
                    tableY = yPos;

                    // Headers
                    gfx.DrawRectangle(new XSolidBrush(XColors.DarkRed), col1X, tableY, 100, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(XColors.DarkRed), col2X, tableY, 250, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(XColors.DarkRed), col3X, tableY, 100, rowHeight);
                    gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 100, rowHeight);
                    gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 250, rowHeight);
                    gfx.DrawRectangle(new XPen(colorBlack), col3X, tableY, 100, rowHeight);

                    gfx.DrawString("Numara", fontBold, XBrushes.White, new XRect(col1X, tableY, 100, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Öğrenci Adı", fontBold, XBrushes.White, new XRect(col2X, tableY, 250, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Oran", fontBold, XBrushes.White, new XRect(col3X, tableY, 100, rowHeight), XStringFormats.Center);
                    tableY += rowHeight;

                    JsonElement criticalStudents = default;
                    JsonElement studList = default;
                    if (data.TryGetProperty("criticalStudents", out var cs)) criticalStudents = cs;
                    else if (data.TryGetProperty("AtRiskStudents", out var ar)) criticalStudents = ar;

                    if (criticalStudents.ValueKind == JsonValueKind.Object && criticalStudents.TryGetProperty("students", out var sl)) studList = sl;
                    else if (criticalStudents.ValueKind == JsonValueKind.Array) studList = criticalStudents;

                    if (studList.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var student in studList.EnumerateArray())
                        {
                            string num = student.TryGetProperty("studentNumber", out var sn) ? sn.ToString() : (student.TryGetProperty("StudentNumber", out var sn2) ? sn2.ToString() : "-");
                            string name = student.TryGetProperty("name", out var snm) ? snm.ToString() : (student.TryGetProperty("Name", out var snm2) ? snm2.ToString() : "-");
                            double rate = student.TryGetProperty("attendanceRate", out var sar) ? sar.GetDouble() : (student.TryGetProperty("AttendanceRate", out var sar2) ? sar2.GetDouble() : 0.0);

                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 100, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 250, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col3X, tableY, 100, rowHeight);

                            gfx.DrawString(num, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 90, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(name, fontNormal, XBrushes.Black, new XRect(col2X + 5, tableY + 5, 240, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString($"%{rate:F1}", fontBold, XBrushes.DarkRed, new XRect(col3X, tableY + 5, 100, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                }
                else if (reportType.ToLower() == "events")
                {
                    // --- EVENTS REPORT LOGIC ---

                    // 1. Summary Box
                    int totalEvents = 0;
                    if (data.TryGetProperty("totalEvents", out var te)) totalEvents = te.GetInt32();
                    else if (data.TryGetProperty("TotalEvents", out var te2)) totalEvents = te2.GetInt32();

                    int totalRegistrations = 0;
                    if (data.TryGetProperty("totalRegistrations", out var tr)) totalRegistrations = tr.GetInt32();
                    else if (data.TryGetProperty("TotalRegistrations", out var tr2)) totalRegistrations = tr2.GetInt32();

                    var boxHeight = 60;
                    var colWidth = contentWidth / 2 - 10;
                    
                    var rectEvents = new XRect(leftMargin, yPos, colWidth, boxHeight);
                    gfx.DrawRectangle(new XPen(colorNavy), new XSolidBrush(XColors.AliceBlue), rectEvents);
                    gfx.DrawString("Toplam Etkinlik", fontBold, XBrushes.Navy, new XRect(leftMargin + 10, yPos + 10, colWidth, 20), XStringFormats.TopLeft);
                    gfx.DrawString(totalEvents.ToString(), fontTitle, XBrushes.Black, new XRect(leftMargin + 10, yPos + 35, colWidth, 20), XStringFormats.TopLeft);

                    var rectParticipants = new XRect(leftMargin + colWidth + 20, yPos, colWidth, boxHeight);
                    gfx.DrawRectangle(new XPen(colorNavy), new XSolidBrush(XColors.AliceBlue), rectParticipants);
                    gfx.DrawString("Toplam Katılımcı", fontBold, XBrushes.Navy, new XRect(leftMargin + colWidth + 30, yPos + 10, colWidth, 20), XStringFormats.TopLeft);
                    gfx.DrawString(totalRegistrations.ToString(), fontTitle, XBrushes.Black, new XRect(leftMargin + colWidth + 30, yPos + 35, colWidth, 20), XStringFormats.TopLeft);
                    
                    yPos += boxHeight + 30;

                    // 2. Popular Events Table
                    gfx.DrawString("En Popüler Etkinlikler", fontHeader, new XSolidBrush(colorNavy), leftMargin, yPos);
                    yPos += 20;

                    var tableY = yPos;
                    var col1X = leftMargin; 
                    var col2X = leftMargin + 350; 
                    var rowHeight = 25;

                    // Headers
                    gfx.DrawRectangle(new XSolidBrush(colorNavy), col1X, tableY, 350, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(colorNavy), col2X, tableY, 100, rowHeight);
                    
                    gfx.DrawString("Etkinlik Adı", fontBold, XBrushes.White, new XRect(col1X, tableY, 350, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Kayıt", fontBold, XBrushes.White, new XRect(col2X, tableY, 100, rowHeight), XStringFormats.Center);
                    tableY += rowHeight;

                    JsonElement popularEvents = default;
                    if (data.TryGetProperty("popularEvents", out var pe)) popularEvents = pe;
                    else if (data.TryGetProperty("PopularEvents", out var pe2)) popularEvents = pe2;

                    if (popularEvents.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var evt in popularEvents.EnumerateArray())
                        {
                            string title = evt.TryGetProperty("title", out var t) ? t.ToString() : (evt.TryGetProperty("Title", out var t2) ? t2.ToString() : "-");
                            string count = evt.TryGetProperty("registrationCount", out var c) ? c.ToString() : (evt.TryGetProperty("RegistrationCount", out var c2) ? c2.ToString() : "0");

                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 350, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 100, rowHeight);

                            gfx.DrawString(title, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 340, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(count, fontNormal, XBrushes.Black, new XRect(col2X, tableY + 5, 100, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                    yPos = tableY + 30;

                    // 3. Category Breakdown Table
                    gfx.DrawString("Kategori Dağılımı", fontHeader, new XSolidBrush(colorNavy), leftMargin, yPos);
                    yPos += 20;
                    tableY = yPos;

                    // Headers
                    gfx.DrawRectangle(new XSolidBrush(colorLightBlue), col1X, tableY, 200, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(colorLightBlue), col1X + 200, tableY, 100, rowHeight);
                    
                    gfx.DrawString("Kategori", fontBold, XBrushes.Black, new XRect(col1X, tableY, 200, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Sayı", fontBold, XBrushes.Black, new XRect(col1X + 200, tableY, 100, rowHeight), XStringFormats.Center);
                    tableY += rowHeight;

                    // Handle 'popularCategories' object or 'categoryBreakdown' array? 
                    // Screenshot showed "PopularCategories": { "Social": 1 ... }
                    // Frontend used 'categoryBreakdown' array. Backend likely sends object in screenshot, but let's handle object.
                    
                    JsonElement categories = default;
                    if (data.TryGetProperty("popularCategories", out var pc)) categories = pc;
                    else if (data.TryGetProperty("PopularCategories", out var pc2)) categories = pc2;
                    // Also check for categoryBreakdown if array is sent
                    else if (data.TryGetProperty("categoryBreakdown", out var cb)) categories = cb;
                    else if (data.TryGetProperty("CategoryBreakdown", out var cb2)) categories = cb2;

                    if (categories.ValueKind == JsonValueKind.Object)
                    {
                        foreach (var prop in categories.EnumerateObject())
                        {
                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 200, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col1X + 200, tableY, 100, rowHeight);

                            gfx.DrawString(prop.Name, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 190, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(prop.Value.ToString(), fontNormal, XBrushes.Black, new XRect(col1X + 200, tableY + 5, 100, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                    else if (categories.ValueKind == JsonValueKind.Array) // Handling Array format [ {Category: "Social", Count: 5} ]
                    {
                         foreach (var item in categories.EnumerateArray())
                        {
                             string cat = item.TryGetProperty("Category", out var c) ? c.ToString() : (item.TryGetProperty("category", out var c2) ? c2.ToString() : "-");
                             string cnt = item.TryGetProperty("Count", out var co) ? co.ToString() : (item.TryGetProperty("count", out var co2) ? co2.ToString() : "0");

                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 200, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col1X + 200, tableY, 100, rowHeight);

                            gfx.DrawString(cat, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 190, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(cnt, fontNormal, XBrushes.Black, new XRect(col1X + 200, tableY + 5, 100, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                }
                else if (reportType.ToLower() == "meal")
                {
                    // --- MEAL REPORT LOGIC ---

                    // 1. Summary Box
                    int totalMeals = 0;
                    if (data.TryGetProperty("totalMealsServed", out var tm)) totalMeals = tm.GetInt32();
                    else if (data.TryGetProperty("TotalMealsServed", out var tm2)) totalMeals = tm2.GetInt32();

                    var boxHeight = 60;
                    var colWidth = contentWidth / 2 - 10;
                    
                    var rectMeals = new XRect(leftMargin, yPos, colWidth, boxHeight);
                    gfx.DrawRectangle(new XPen(colorNavy), new XSolidBrush(XColors.AliceBlue), rectMeals);
                    gfx.DrawString("Toplam Sunulan Yemek", fontBold, XBrushes.Navy, new XRect(leftMargin + 10, yPos + 10, colWidth, 20), XStringFormats.TopLeft);
                    gfx.DrawString(totalMeals.ToString(), fontTitle, XBrushes.Black, new XRect(leftMargin + 10, yPos + 35, colWidth, 20), XStringFormats.TopLeft);
                    
                    yPos += boxHeight + 30;

                    // 2. Cafeteria Usage Table
                    gfx.DrawString("Kafeterya Kullanımı", fontHeader, new XSolidBrush(colorNavy), leftMargin, yPos);
                    yPos += 20;

                    var tableY = yPos;
                    var col1X = leftMargin; 
                    var col2X = leftMargin + 300; 
                    var rowHeight = 25;

                    // Headers
                    gfx.DrawRectangle(new XSolidBrush(colorLightBlue), col1X, tableY, 300, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(colorLightBlue), col2X, tableY, 150, rowHeight);
                    
                    gfx.DrawString("Kafeterya", fontBold, XBrushes.Black, new XRect(col1X, tableY, 300, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Kullanım Sayısı", fontBold, XBrushes.Black, new XRect(col2X, tableY, 150, rowHeight), XStringFormats.Center);
                    tableY += rowHeight;

                    JsonElement cafeterias = default;
                    if (data.TryGetProperty("usageByCafeteria", out var uc)) cafeterias = uc;
                    else if (data.TryGetProperty("UsageByCafeteria", out var uc2)) cafeterias = uc2;
                    else if (data.TryGetProperty("cafeteriaUtilization", out var cu)) cafeterias = cu; // Fallback to array if provided
                    else if (data.TryGetProperty("CafeteriaUtilization", out var cu2)) cafeterias = cu2;

                    if (cafeterias.ValueKind == JsonValueKind.Object)
                    {
                        foreach (var prop in cafeterias.EnumerateObject())
                        {
                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 300, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 150, rowHeight);

                            gfx.DrawString(prop.Name, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 290, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(prop.Value.ToString(), fontNormal, XBrushes.Black, new XRect(col2X, tableY + 5, 150, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                    else if (cafeterias.ValueKind == JsonValueKind.Array)
                    {
                         foreach (var item in cafeterias.EnumerateArray())
                        {
                             string name = item.TryGetProperty("Cafeteria", out var c) ? c.ToString() : (item.TryGetProperty("cafeteria", out var c2) ? c2.ToString() : "-");
                             string count = item.TryGetProperty("TotalReservations", out var co) ? co.ToString() : (item.TryGetProperty("totalReservations", out var co2) ? co2.ToString() : "0");

                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 300, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col2X, tableY, 150, rowHeight);

                            gfx.DrawString(name, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 290, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(count, fontNormal, XBrushes.Black, new XRect(col2X, tableY + 5, 150, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                    yPos = tableY + 30;

                    // 3. Peak Hours (Meal Types) Table
                    gfx.DrawString("Öğün Dağılımı (Yoğun Saatler)", fontHeader, new XSolidBrush(colorOrange), leftMargin, yPos);
                    yPos += 20;
                    tableY = yPos;

                    // Headers
                    gfx.DrawRectangle(new XSolidBrush(XColors.Bisque), col1X, tableY, 200, rowHeight);
                    gfx.DrawRectangle(new XSolidBrush(XColors.Bisque), col1X + 200, tableY, 150, rowHeight);
                    
                    gfx.DrawString("Öğün / Yemek Tipi", fontBold, XBrushes.Black, new XRect(col1X, tableY, 200, rowHeight), XStringFormats.Center);
                    gfx.DrawString("Rezervasyon", fontBold, XBrushes.Black, new XRect(col1X + 200, tableY, 150, rowHeight), XStringFormats.Center);
                    tableY += rowHeight;

                    JsonElement peakHours = default;
                    if (data.TryGetProperty("peakHours", out var ph)) peakHours = ph;
                    else if (data.TryGetProperty("PeakHours", out var ph2)) peakHours = ph2;

                    if (peakHours.ValueKind == JsonValueKind.Object)
                    {
                        foreach (var prop in peakHours.EnumerateObject())
                        {
                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 200, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col1X + 200, tableY, 150, rowHeight);

                            gfx.DrawString(prop.Name, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 190, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(prop.Value.ToString(), fontNormal, XBrushes.Black, new XRect(col1X + 200, tableY + 5, 150, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                    else if (peakHours.ValueKind == JsonValueKind.Array) 
                    {
                         foreach (var item in peakHours.EnumerateArray())
                        {
                             string type = item.TryGetProperty("MealType", out var mt) ? mt.ToString() : (item.TryGetProperty("mealType", out var mt2) ? mt2.ToString() : "-");
                             string count = item.TryGetProperty("Count", out var c) ? c.ToString() : (item.TryGetProperty("count", out var c2) ? c2.ToString() : "0");

                            gfx.DrawRectangle(new XPen(colorBlack), col1X, tableY, 200, rowHeight);
                            gfx.DrawRectangle(new XPen(colorBlack), col1X + 200, tableY, 150, rowHeight);

                            gfx.DrawString(type, fontNormal, XBrushes.Black, new XRect(col1X + 5, tableY + 5, 190, rowHeight), XStringFormats.TopLeft);
                            gfx.DrawString(count, fontNormal, XBrushes.Black, new XRect(col1X + 200, tableY + 5, 150, rowHeight), XStringFormats.TopCenter);
                            tableY += rowHeight;
                        }
                    }
                }
                else
                {
                    // Fallback for other report types
                     var plainJson = JsonSerializer.Serialize(analyticsData, new JsonSerializerOptions { WriteIndented = true });
                     var lines = plainJson.Split('\n');
                     foreach (var line in lines)
                     {
                         if (yPos > page.Height.Point - 50) { page = document.AddPage(); gfx = XGraphics.FromPdfPage(page); yPos = 50; }
                         gfx.DrawString(line, fontNormal, XBrushes.Black, new XRect(leftMargin, yPos, contentWidth, 15), XStringFormats.TopLeft);
                         yPos += 15;
                     }
                }

                // --- 4. Footer ---
                var footerY = page.Height.Point - 40;
                gfx.DrawLine(new XPen(colorGray, 0.5), leftMargin, footerY, rightMargin, footerY);
                gfx.DrawString("Bu döküman dijital olarak oluşturulmuştur. / This document is generated digitally.", fontFooter, XBrushes.Gray,
                    new XRect(0, footerY + 5, pageWidth, 20), XStringFormats.TopCenter);

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
