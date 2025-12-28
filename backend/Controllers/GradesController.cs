using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System.Security.Claims;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/grades")]
[Authorize]
public class GradesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IGradeCalculationService _gradeCalculationService;
    private readonly INotificationService _notificationService;
    private readonly ILogger<GradesController> _logger;

    public GradesController(
        ApplicationDbContext context,
        IGradeCalculationService gradeCalculationService,
        INotificationService notificationService,
        ILogger<GradesController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _gradeCalculationService = gradeCalculationService ?? throw new ArgumentNullException(nameof(gradeCalculationService));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get my grades (Student only)
    /// </summary>
    [HttpGet("my-grades")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<List<GradeDto>>> GetMyGrades()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        var enrollments = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Course)
            .Where(e => e.StudentId == student.Id)
            .OrderByDescending(e => e.Section.Year)
            .ThenByDescending(e => e.Section.Semester)
            .ToListAsync();

        var grades = enrollments.Select(e => new GradeDto
        {
            EnrollmentId = e.Id,
            CourseCode = e.Section != null && e.Section.Course != null ? e.Section.Course.Code : "UNKNOWN",
            CourseName = e.Section != null && e.Section.Course != null ? e.Section.Course.Name : "Unknown Course",
            Credits = e.Section != null && e.Section.Course != null ? e.Section.Course.Credits : 0,
            Semester = e.Section != null ? e.Section.Semester : "",
            Year = e.Section != null ? e.Section.Year : 0,
            MidtermGrade = e.MidtermGrade,
            FinalGrade = e.FinalGrade,
            HomeworkGrade = e.HomeworkGrade,
            LetterGrade = e.LetterGrade,
            GradePoint = e.GradePoint
        }).ToList();

        return Ok(grades);
    }

    /// <summary>
    /// Get transcript as JSON (Student only)
    /// </summary>
    [HttpGet("transcript")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<TranscriptDto>> GetTranscript()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students
            .Include(s => s.User)
            .Include(s => s.Department)
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        var enrollments = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Course)
            .Where(e => e.StudentId == student.Id && 
                       (e.Status == EnrollmentStatus.Completed || e.Status == EnrollmentStatus.Active))
            .OrderBy(e => e.Section.Year)
            .ThenBy(e => e.Section.Semester)
            .ToListAsync();

        // Group by semester
        var semesterGroups = enrollments
            .GroupBy(e => new { e.Section.Semester, e.Section.Year })
            .Select(g => new SemesterGradesDto
            {
                Semester = g.Key.Semester,
                Year = g.Key.Year,
                GPA = _gradeCalculationService.CalculateGPA(g.ToList()),
                Credits = g.Sum(e => e.Section.Course.Credits),
                Courses = g.Select(e => new GradeDto
                {
                    EnrollmentId = e.Id,
                    CourseCode = e.Section != null && e.Section.Course != null ? e.Section.Course.Code : "UNKNOWN",
                    CourseName = e.Section != null && e.Section.Course != null ? e.Section.Course.Name : "Unknown Course",
                    Credits = e.Section != null && e.Section.Course != null ? e.Section.Course.Credits : 0,
                    Semester = e.Section != null ? e.Section.Semester : "",
                    Year = e.Section != null ? e.Section.Year : 0,
                    MidtermGrade = e.MidtermGrade,
                    FinalGrade = e.FinalGrade,
                    HomeworkGrade = e.HomeworkGrade,
                    LetterGrade = e.LetterGrade,
                    GradePoint = e.GradePoint
                }).ToList()
            }).ToList();

        var transcript = new TranscriptDto
        {
            StudentId = student.Id,
            StudentName = $"{student.User.FirstName} {student.User.LastName}",
            StudentNumber = student.StudentNumber,
            DepartmentName = student.Department?.Name ?? "",
            EnrollmentYear = student.EnrollmentYear,
            CGPA = _gradeCalculationService.CalculateCGPA(enrollments),
            TotalCredits = enrollments.Sum(e => e.Section.Course.Credits),
            TotalECTS = enrollments.Sum(e => e.Section.Course.ECTS),
            Semesters = semesterGroups
        };

        return Ok(transcript);
    }

    /// <summary>
    /// Get transcript as PDF (Student only)
    /// </summary>
    [HttpGet("transcript/pdf")]
    [Authorize(Roles = "Student")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTranscriptPdf()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students
            .Include(s => s.User)
            .Include(s => s.Department)
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        
        if (student == null)
            return NotFound(new { message = "Öğrenci kaydı bulunamadı" });

        var enrollments = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Course)
            .Where(e => e.StudentId == student.Id && 
                       (e.Status == EnrollmentStatus.Completed || e.Status == EnrollmentStatus.Active))
            .OrderBy(e => e.Section.Year)
            .ThenBy(e => e.Section.Semester)
            .ToListAsync();

        // Group by semester
        var semesterGroups = enrollments
            .GroupBy(e => new { e.Section.Semester, e.Section.Year })
            .Select(g => new SemesterGradesDto
            {
                Semester = g.Key.Semester,
                Year = g.Key.Year,
                GPA = _gradeCalculationService.CalculateGPA(g.ToList()),
                Credits = g.Sum(e => e.Section.Course.Credits),
                Courses = g.Select(e => new GradeDto
                {
                    EnrollmentId = e.Id,
                    CourseCode = e.Section != null && e.Section.Course != null ? e.Section.Course.Code : "UNKNOWN",
                    CourseName = e.Section != null && e.Section.Course != null ? e.Section.Course.Name : "Unknown Course",
                    Credits = e.Section != null && e.Section.Course != null ? e.Section.Course.Credits : 0,
                    Semester = e.Section != null ? e.Section.Semester : "",
                    Year = e.Section != null ? e.Section.Year : 0,
                    MidtermGrade = e.MidtermGrade,
                    FinalGrade = e.FinalGrade,
                    HomeworkGrade = e.HomeworkGrade,
                    LetterGrade = e.LetterGrade,
                    GradePoint = e.GradePoint
                }).ToList()
            }).ToList();

        var transcript = new TranscriptDto
        {
            StudentId = student.Id,
            StudentName = $"{student.User.FirstName} {student.User.LastName}",
            StudentNumber = student.StudentNumber,
            DepartmentName = student.Department?.Name ?? "",
            EnrollmentYear = student.EnrollmentYear,
            CGPA = _gradeCalculationService.CalculateCGPA(enrollments),
            TotalCredits = enrollments.Sum(e => e.Section.Course.Credits),
            TotalECTS = enrollments.Sum(e => e.Section.Course.ECTS),
            Semesters = semesterGroups
        };

        // Generate PDF
        var pdfBytes = GenerateTranscriptPdf(transcript, student);

        // Create filename
        var fileName = $"Transkript_{student.StudentNumber}_{DateTime.Now:yyyyMMdd}.pdf";

        return File(pdfBytes, "application/pdf", fileName);
    }

    private byte[] GenerateTranscriptPdf(TranscriptDto transcript, Models.Student student)
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
        var fontTableHeader = new XFont("Arial", 9, XFontStyle.Bold);
        var fontTableContent = new XFont("Arial", 8, XFontStyle.Regular);

        // Colors - Koyu Lacivert tonları (#1e3a8a)
        var colorDarkNavy = new XSolidBrush(XColor.FromArgb(30, 58, 138)); // #1e3a8a
        var colorBlue = colorDarkNavy; // Eski colorBlue yerine koyu lacivert kullan
        var colorBlack = XBrushes.Black;
        var colorGray = XBrushes.Gray;
        var colorLightGray = XBrushes.LightGray;
        var colorWhite = XBrushes.White;

        double yPos = 30; // Padding: 30px
        double leftMargin = 30; // Padding: 30px
        double rightMargin = page.Width.Point - 30;
        double pageWidth = page.Width.Point;
        double pageHeight = page.Height.Point;

        // Logo (sol üst köşe) - RTEÜ logosu
        var logoSize = 60;
        var logoRect = new XRect(leftMargin, yPos, logoSize, logoSize);
        
        // Logo dosyasını yükle (wwwroot/Assets/logos/images.jpg)
        try
        {
            var logoPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Assets", "logos", "images.jpg");
            if (System.IO.File.Exists(logoPath))
            {
                var logoImage = XImage.FromFile(logoPath);
                gfx.DrawImage(logoImage, logoRect);
            }
            else
            {
                // Logo dosyası yoksa, basit bir placeholder (RTEÜ yazısı)
                gfx.DrawRectangle(new XPen(XColor.FromArgb(30, 58, 138), 1), 
                    new XSolidBrush(XColor.FromArgb(240, 240, 240)), logoRect);
                var logoFont = new XFont("Arial", 8, XFontStyle.Bold);
                gfx.DrawString("RTEÜ", logoFont, colorDarkNavy, logoRect, XStringFormats.Center);
            }
        }
        catch
        {
            // Logo yüklenemezse placeholder göster
            gfx.DrawRectangle(new XPen(XColor.FromArgb(30, 58, 138), 1), 
                new XSolidBrush(XColor.FromArgb(240, 240, 240)), logoRect);
            var logoFont = new XFont("Arial", 8, XFontStyle.Bold);
            gfx.DrawString("RTEÜ", logoFont, colorDarkNavy, logoRect, XStringFormats.Center);
        }

        // Kurumsal Header (ortalanmış)
        var headerY = yPos;
        var universityText = "RECEP TAYYİP ERDOĞAN ÜNİVERSİTESİ";
        var universityFont = new XFont("Arial", 16, XFontStyle.Bold);
        gfx.DrawString(universityText, universityFont, colorBlue,
            new XRect(leftMargin, headerY, pageWidth - 60, 20),
            XStringFormats.TopCenter);
        headerY += 22;

        var departmentText = "Öğrenci İşleri Daire Başkanlığı";
        var departmentFont = new XFont("Arial", 11, XFontStyle.Regular);
        gfx.DrawString(departmentText, departmentFont, colorGray,
            new XRect(leftMargin, headerY, pageWidth - 60, 15),
            XStringFormats.TopCenter);
        headerY += 20;

        // RESMİ TRANSKRİPT (küçültülmüş)
        var titleText = "RESMİ TRANSKRİPT";
        var titleFont = new XFont("Arial", 14, XFontStyle.Bold);
        gfx.DrawString(titleText, titleFont, colorBlue,
            new XRect(leftMargin, headerY, pageWidth - 60, 18),
            XStringFormats.TopCenter);
        headerY += 20;

        // Subtitle
        gfx.DrawString("Akıllı Kampüs Ekosistem Yönetim Platformu", fontSmall, colorGray,
            new XRect(leftMargin, headerY, pageWidth - 60, 12),
            XStringFormats.TopCenter);
        headerY += 20;

        // Divider line (koyu lacivert)
        gfx.DrawLine(new XPen(XColor.FromArgb(30, 58, 138), 0.5), leftMargin, headerY, rightMargin, headerY);
        yPos = headerY + 25;

        // Student Information Section
        var infoYStart = yPos;
        gfx.DrawString("ÖĞRENCİ BİLGİLERİ", fontHeader, colorBlue, leftMargin, yPos);
        yPos += 25;

        // 2 sütunlu grid yapısı - çizgiler olmadan
        var labelWidth = 140;
        var valueX = leftMargin + labelWidth + 20;
        var rowSpacing = 20;

        // Ad Soyad
        var labelFont = new XFont("Arial", 10, XFontStyle.Bold);
        gfx.DrawString("Adı Soyadı:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString(transcript.StudentName, fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Öğrenci Numarası
        gfx.DrawString("Öğrenci Numarası:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString(transcript.StudentNumber, fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Bölüm
        gfx.DrawString("Bölüm:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString(transcript.DepartmentName, fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Kayıt Yılı
        gfx.DrawString("Kayıt Yılı:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString(transcript.EnrollmentYear.ToString(), fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Academic Summary (sağ taraf)
        var summaryX = rightMargin - 200;
        var summaryY = infoYStart + 25;
        gfx.DrawString("AGNO (CGPA):", fontNormal, colorBlack, summaryX, summaryY);
        gfx.DrawString(transcript.CGPA.ToString("F2"), fontHeader, colorBlue, summaryX + 80, summaryY);

        gfx.DrawString("Toplam Kredi:", fontNormal, colorBlack, summaryX, summaryY + 20);
        gfx.DrawString(transcript.TotalCredits.ToString(), fontNormal, colorBlack, summaryX + 80, summaryY + 20);

        gfx.DrawString("Toplam AKTS:", fontNormal, colorBlack, summaryX, summaryY + 40);
        gfx.DrawString(transcript.TotalECTS.ToString(), fontNormal, colorBlack, summaryX + 80, summaryY + 40);

        yPos = Math.Max(yPos, summaryY + 60);
        yPos += 15;

        // Semester Sections
        foreach (var semester in transcript.Semesters)
        {
            // Check if we need a new page
            if (yPos > pageHeight - 150)
            {
                page = document.AddPage();
                page.Width = XUnit.FromMillimeter(210); // A4 width
                page.Height = XUnit.FromMillimeter(297); // A4 height
                gfx = XGraphics.FromPdfPage(page);
                yPos = 50;
            }

            // Semester Header
            // Dönem adını Türkçe formatına çevir
            string semesterText;
            string semesterName = semester.Semester;
            if (semesterName == "Fall")
            {
                semesterText = $"{semester.Year}-{semester.Year + 1} Güz Yarıyılı";
            }
            else if (semesterName == "Spring")
            {
                semesterText = $"{semester.Year - 1}-{semester.Year} Bahar Yarıyılı";
            }
            else if (semesterName == "Summer")
            {
                semesterText = $"{semester.Year - 1}-{semester.Year} Yaz Yarıyılı";
            }
            else
            {
                semesterText = $"{semester.Semester} {semester.Year}";
            }
            
            // Dönem başlığı şeridi - padding ve hizalama düzeltmeleri
            var semesterHeaderHeight = 35; // padding artırıldı (28'den 35'e)
            var semesterHeaderRect = new XRect(leftMargin, yPos, rightMargin - leftMargin, semesterHeaderHeight);
            var semesterHeaderBg = new XSolidBrush(XColor.FromArgb(243, 244, 246)); // Açık gri arka plan
            gfx.DrawRectangle(new XPen(colorDarkNavy, 0.5), semesterHeaderBg, semesterHeaderRect);
            
            // Dikey hizalama için padding hesaplama
            var textY = yPos + (semesterHeaderHeight / 2) - 5; // Dikey ortalama (yükseklik arttığı için güncellendi)
            
            // Sol taraf: Dönem adı
            gfx.DrawString(semesterText, fontHeader, colorDarkNavy, leftMargin + 8, textY);
            
            // Sağ taraf: GPA ve Kredi bilgisi
            var summaryText = $"Dönem GPA: {semester.GPA:F2} | Kredi: {semester.Credits}";
            var summaryTextWidth = gfx.MeasureString(summaryText, fontSmall).Width;
            gfx.DrawString(summaryText, fontSmall, colorBlack, rightMargin - summaryTextWidth - 8, textY);
            
            yPos += semesterHeaderHeight + 10; // marginBottom: 10 (tablo başlığına yapışmasın)

            // Table Header
            var tableYStart = yPos;
            var colWidths = new[] { 60.0, 180.0, 40.0, 50.0, 50.0 };
            var colX = leftMargin;
            var headerHeight = 20;
            var rowHeight = 15;

            // Draw header background (daha koyu gri) - kenarlık koyu lacivert
            var lightGrayColor = XColor.FromArgb(229, 231, 235); // #e5e7eb (daha koyu gri)
            var darkNavyPen = new XPen(XColor.FromArgb(30, 58, 138), 0.5); // #1e3a8a
            gfx.DrawRectangle(darkNavyPen, 
                new XSolidBrush(lightGrayColor),
                new XRect(leftMargin, yPos, rightMargin - leftMargin, headerHeight));

            // Header text
            gfx.DrawString("Kod", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[0], headerHeight), XStringFormats.Center);
            colX += colWidths[0];
            gfx.DrawString("Ders Adı", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[1], headerHeight), XStringFormats.Center);
            colX += colWidths[1];
            gfx.DrawString("Kredi", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[2], headerHeight), XStringFormats.Center);
            colX += colWidths[2];
            gfx.DrawString("Harf", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[3], headerHeight), XStringFormats.Center);
            colX += colWidths[3];
            gfx.DrawString("Puan", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[4], headerHeight), XStringFormats.Center);

            yPos += headerHeight;

            // Table rows
            foreach (var course in semester.Courses)
            {
                // Check if we need a new page for this row
                if (yPos + rowHeight > pageHeight - 100)
                {
                    page = document.AddPage();
                    page.Width = XUnit.FromMillimeter(210); // A4 width
                    page.Height = XUnit.FromMillimeter(297); // A4 height
                    gfx = XGraphics.FromPdfPage(page);
                    yPos = 50;
                    tableYStart = yPos;

                    // Redraw header on new page (daha koyu gri) - kenarlık koyu lacivert
                    var lightGrayColorNewPage = XColor.FromArgb(229, 231, 235); // #e5e7eb (daha koyu gri)
                    var darkNavyPenNewPage = new XPen(XColor.FromArgb(30, 58, 138), 0.5); // #1e3a8a
                    gfx.DrawRectangle(darkNavyPenNewPage, 
                        new XSolidBrush(lightGrayColorNewPage),
                        new XRect(leftMargin, yPos, rightMargin - leftMargin, headerHeight));
                    colX = leftMargin;
                    gfx.DrawString("Kod", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[0], headerHeight), XStringFormats.Center);
                    colX += colWidths[0];
                    gfx.DrawString("Ders Adı", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[1], headerHeight), XStringFormats.Center);
                    colX += colWidths[1];
                    gfx.DrawString("Kredi", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[2], headerHeight), XStringFormats.Center);
                    colX += colWidths[2];
                    gfx.DrawString("Harf", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[3], headerHeight), XStringFormats.Center);
                    colX += colWidths[3];
                    gfx.DrawString("Puan", fontTableHeader, colorBlack, new XRect(colX, yPos + 3, colWidths[4], headerHeight), XStringFormats.Center);
                    yPos += headerHeight;
                }

                // Draw row border (koyu lacivert, ince)
                var rowBorderPen = new XPen(XColor.FromArgb(30, 58, 138), 0.5); // #1e3a8a
                gfx.DrawLine(rowBorderPen, leftMargin, yPos, rightMargin, yPos);

                // Draw row content
                colX = leftMargin;
                gfx.DrawString(course.CourseCode, fontTableContent, colorBlack, new XRect(colX, yPos + 2, colWidths[0], rowHeight), XStringFormats.Center);
                colX += colWidths[0];
                
                // Truncate long course names
                var courseName = course.CourseName;
                if (gfx.MeasureString(courseName, fontTableContent).Width > colWidths[1])
                {
                    while (gfx.MeasureString(courseName + "...", fontTableContent).Width > colWidths[1] && courseName.Length > 0)
                    {
                        courseName = courseName.Substring(0, courseName.Length - 1);
                    }
                    courseName += "...";
                }
                gfx.DrawString(courseName, fontTableContent, colorBlack, new XRect(colX, yPos + 2, colWidths[1], rowHeight), XStringFormats.CenterLeft);
                colX += colWidths[1];
                
                gfx.DrawString(course.Credits.ToString(), fontTableContent, colorBlack, new XRect(colX, yPos + 2, colWidths[2], rowHeight), XStringFormats.Center);
                colX += colWidths[2];
                
                gfx.DrawString(course.LetterGrade ?? "-", fontTableContent, colorBlack, new XRect(colX, yPos + 2, colWidths[3], rowHeight), XStringFormats.Center);
                colX += colWidths[3];
                
                gfx.DrawString(course.GradePoint?.ToString("F2") ?? "-", fontTableContent, colorBlack, new XRect(colX, yPos + 2, colWidths[4], rowHeight), XStringFormats.Center);

                yPos += rowHeight;
            }

            // Draw table border (koyu lacivert, ince)
            var tableBorderPen = new XPen(XColor.FromArgb(30, 58, 138), 0.5);
            gfx.DrawRectangle(tableBorderPen, XBrushes.Transparent,
                new XRect(leftMargin, tableYStart, rightMargin - leftMargin, yPos - tableYStart));

            yPos += 15; // Space between semesters
        }

        // Footer - boydan boya ince çizgi (koyu lacivert)
        var footerLineY = pageHeight - 80;
        gfx.DrawLine(new XPen(XColor.FromArgb(30, 58, 138), 0.5), leftMargin, footerLineY, rightMargin, footerLineY);
        
        var footerY = footerLineY + 10;
        var currentDate = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"));
        
        // Sol alt: Belge Tarihi
        gfx.DrawString($"Belge Tarihi: {currentDate}", fontSmall, colorGray, leftMargin, footerY);
        
        // Sağ alt: Doğrulama Kodu
        var verificationCode = $"TR-{DateTime.Now:yyyy}-{transcript.StudentNumber.Substring(Math.Max(0, transcript.StudentNumber.Length - 3))}";
        var codeWidth = gfx.MeasureString($"Doğrulama Kodu: {verificationCode}", fontSmall).Width;
        gfx.DrawString($"Doğrulama Kodu: {verificationCode}", fontSmall, colorGray, rightMargin - codeWidth, footerY);
        
        // En alt: Elektronik İmza Notu (ortalanmış)
        var signatureNoteY = footerY + 15;
        var signatureNote = "Bu belge 5070 sayılı Elektronik İmza Kanunu uyarınca güvenli elektronik imza ile imzalanmıştır.";
        var noteFont = new XFont("Arial", 8, XFontStyle.Italic);
        gfx.DrawString(signatureNote, noteFont, colorGray,
            new XRect(leftMargin, signatureNoteY, rightMargin - leftMargin, 12),
            XStringFormats.TopCenter);

        // Convert to byte array
        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }

    // DrawInfoRow metodu artık kullanılmıyor - çizgiler kaldırıldı
    // Öğrenci bilgileri artık 2 sütunlu grid yapısında, çizgisiz olarak gösteriliyor

    /// <summary>
    /// Input grades for a student (Faculty only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult> InputGrades([FromBody] GradeInputRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var enrollment = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Instructor)
            .FirstOrDefaultAsync(e => e.Id == request.EnrollmentId);

        if (enrollment == null)
            return NotFound(new { message = "Enrollment not found" });

        // Verify faculty owns this section
        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId));

        if (faculty == null)
            return BadRequest(new { message = "Faculty profile not found" });

        if (enrollment.Section.InstructorId != faculty.Id && User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
            return Forbid();

        // Update grades
        if (request.MidtermGrade.HasValue)
            enrollment.MidtermGrade = request.MidtermGrade;
        if (request.FinalGrade.HasValue)
            enrollment.FinalGrade = request.FinalGrade;
        if (request.HomeworkGrade.HasValue)
            enrollment.HomeworkGrade = request.HomeworkGrade;

        // Calculate letter grade and grade point
        if (enrollment.MidtermGrade.HasValue && enrollment.FinalGrade.HasValue)
        {
            var (letterGrade, gradePoint) = _gradeCalculationService.CalculateLetterGrade(
                enrollment.MidtermGrade,
                enrollment.FinalGrade,
                enrollment.HomeworkGrade);

            enrollment.LetterGrade = letterGrade;
            enrollment.GradePoint = gradePoint;

            // If passing grade, mark as completed
            if (gradePoint >= 1.0m)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }
            else if (gradePoint > 0)
            {
                enrollment.Status = EnrollmentStatus.Failed;
            }
        }

        enrollment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Update student's CGPA
        var student = await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Section)
                    .ThenInclude(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == enrollment.StudentId);

        if (student != null)
        {
            student.CGPA = _gradeCalculationService.CalculateCGPA(student.Enrollments.ToList());
            await _context.SaveChangesAsync();
        }

        // Notify student asynchronously (fire and forget)
        _ = Task.Run(async () =>
        {
            try
            {
                await _notificationService.NotifyStudentOnGradeEntryAsync(request.EnrollmentId);
            }
            catch (Exception ex)
            {
                // Log error but don't fail the request
                _logger.LogError(ex, "Failed to send grade entry notification");
            }
        });

        return Ok(new 
        { 
            message = "Grades updated successfully",
            letterGrade = enrollment.LetterGrade,
            gradePoint = enrollment.GradePoint
        });
    }

    /// <summary>
    /// Bulk input grades for a section (Faculty only)
    /// </summary>
    [HttpPost("bulk")]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult> BulkInputGrades([FromBody] BulkGradeInputRequest request)
    {
        var results = new List<object>();

        foreach (var gradeInput in request.Grades)
        {
            try
            {
                var result = await InputGrades(gradeInput);
                results.Add(new { enrollmentId = gradeInput.EnrollmentId, success = true });
            }
            catch (Exception ex)
            {
                results.Add(new { enrollmentId = gradeInput.EnrollmentId, success = false, error = ex.Message });
            }
        }

        return Ok(new { message = "Bulk grade input completed", results });
    }
}
