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
        _context = context;
        _gradeCalculationService = gradeCalculationService;
        _notificationService = notificationService;
        _logger = logger;
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
            CourseCode = e.Section.Course.Code,
            CourseName = e.Section.Course.Name,
            Credits = e.Section.Course.Credits,
            Semester = e.Section.Semester,
            Year = e.Section.Year,
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
                    CourseCode = e.Section.Course.Code,
                    CourseName = e.Section.Course.Name,
                    Credits = e.Section.Course.Credits,
                    Semester = e.Section.Semester,
                    Year = e.Section.Year,
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
                    CourseCode = e.Section.Course.Code,
                    CourseName = e.Section.Course.Name,
                    Credits = e.Section.Course.Credits,
                    Semester = e.Section.Semester,
                    Year = e.Section.Year,
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

        // Colors
        var colorBlue = XBrushes.DarkBlue;
        var colorBlack = XBrushes.Black;
        var colorGray = XBrushes.Gray;
        var colorLightGray = XBrushes.LightGray;
        var colorWhite = XBrushes.White;

        double yPos = 50;
        double leftMargin = 50;
        double rightMargin = page.Width.Point - 50;
        double pageWidth = page.Width.Point;
        double pageHeight = page.Height.Point;

        // Header with title
        var titleText = "RESMİ TRANSKRİPT";
        var titleSize = gfx.MeasureString(titleText, fontTitle);
        gfx.DrawString(titleText, fontTitle, colorBlue,
            new XRect(leftMargin, yPos, pageWidth - 100, 25),
            XStringFormats.TopCenter);
        yPos += 30;

        // Subtitle
        gfx.DrawString("Akıllı Kampüs Ekosistem Yönetim Platformu", fontSmall, colorGray,
            new XRect(leftMargin, yPos, pageWidth - 100, 15),
            XStringFormats.TopCenter);
        yPos += 25;

        // Divider line
        gfx.DrawLine(new XPen(colorBlue, 2), leftMargin, yPos, rightMargin, yPos);
        yPos += 20;

        // Student Information Section
        var infoYStart = yPos;
        gfx.DrawString("ÖĞRENCİ BİLGİLERİ", fontHeader, colorBlue, leftMargin, yPos);
        yPos += 20;

        DrawInfoRow(gfx, "Ad Soyad:", transcript.StudentName, leftMargin, yPos, rightMargin, fontNormal, fontNormal, colorBlack);
        yPos += 18;

        DrawInfoRow(gfx, "Öğrenci Numarası:", transcript.StudentNumber, leftMargin, yPos, rightMargin, fontNormal, fontNormal, colorBlack);
        yPos += 18;

        DrawInfoRow(gfx, "Bölüm:", transcript.DepartmentName, leftMargin, yPos, rightMargin, fontNormal, fontNormal, colorBlack);
        yPos += 18;

        DrawInfoRow(gfx, "Kayıt Yılı:", transcript.EnrollmentYear.ToString(), leftMargin, yPos, rightMargin, fontNormal, fontNormal, colorBlack);
        yPos += 18;

        // Academic Summary
        var summaryX = rightMargin - 200;
        gfx.DrawString("AGNO (CGPA):", fontNormal, colorBlack, summaryX, infoYStart + 20);
        gfx.DrawString(transcript.CGPA.ToString("F2"), fontHeader, colorBlue, summaryX + 80, infoYStart + 20);

        gfx.DrawString("Toplam Kredi:", fontNormal, colorBlack, summaryX, infoYStart + 38);
        gfx.DrawString(transcript.TotalCredits.ToString(), fontNormal, colorBlack, summaryX + 80, infoYStart + 38);

        gfx.DrawString("Toplam AKTS:", fontNormal, colorBlack, summaryX, infoYStart + 56);
        gfx.DrawString(transcript.TotalECTS.ToString(), fontNormal, colorBlack, summaryX + 80, infoYStart + 56);

        yPos += 20;

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
            var semesterText = $"{semester.Semester} {semester.Year}";
            var semesterHeaderRect = new XRect(leftMargin, yPos, rightMargin - leftMargin, 20);
            gfx.DrawRectangle(new XPen(colorBlue, 1), new XSolidBrush(XColor.FromArgb(240, 240, 255)), semesterHeaderRect);
            
            gfx.DrawString(semesterText, fontHeader, colorBlue, leftMargin + 5, yPos + 5);
            gfx.DrawString($"Dönem GPA: {semester.GPA:F2} | Kredi: {semester.Credits}", fontSmall, colorBlack, rightMargin - 150, yPos + 5);
            yPos += 25;

            // Table Header
            var tableYStart = yPos;
            var colWidths = new[] { 60.0, 180.0, 40.0, 50.0, 50.0 };
            var colX = leftMargin;
            var headerHeight = 20;
            var rowHeight = 15;

            // Draw header background
            gfx.DrawRectangle(new XPen(colorBlue, 1), new XSolidBrush(XColor.FromArgb(220, 230, 255)),
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

                    // Redraw header on new page
                    gfx.DrawRectangle(new XPen(colorBlue, 1), new XSolidBrush(XColor.FromArgb(220, 230, 255)),
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

                // Draw row border
                gfx.DrawLine(new XPen(colorLightGray, 0.5), leftMargin, yPos, rightMargin, yPos);

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

            // Draw table border
            gfx.DrawRectangle(new XPen(colorBlue, 1), XBrushes.Transparent,
                new XRect(leftMargin, tableYStart, rightMargin - leftMargin, yPos - tableYStart));

            yPos += 15; // Space between semesters
        }

        // Footer
        var footerY = pageHeight - 60;
        var currentDate = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"));
        gfx.DrawString($"Belge Tarihi: {currentDate}", fontSmall, colorGray,
            new XRect(leftMargin, footerY, rightMargin - leftMargin, 15),
            XStringFormats.TopRight);

        // Signature area
        var signatureY = pageHeight - 40;
        var signatureWidth = 120;
        var signatureSpacing = (rightMargin - leftMargin - (signatureWidth * 2)) / 3;

        gfx.DrawLine(new XPen(colorBlack, 1),
            leftMargin + signatureSpacing, signatureY,
            leftMargin + signatureSpacing + signatureWidth, signatureY);
        gfx.DrawString("Öğrenci İmzası", fontSmall, colorBlack,
            new XRect(leftMargin + signatureSpacing, signatureY + 3, signatureWidth, 12),
            XStringFormats.TopCenter);

        gfx.DrawLine(new XPen(colorBlack, 1),
            rightMargin - signatureSpacing - signatureWidth, signatureY,
            rightMargin - signatureSpacing, signatureY);
        gfx.DrawString("Sistem Onayı", fontSmall, colorBlack,
            new XRect(rightMargin - signatureSpacing - signatureWidth, signatureY + 3, signatureWidth, 12),
            XStringFormats.TopCenter);

        // Convert to byte array
        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }

    private void DrawInfoRow(XGraphics gfx, string label, string value,
        double left, double y, double right, XFont labelFont, XFont valueFont, XBrush brush)
    {
        var labelWidth = 120;
        gfx.DrawString(label, labelFont, brush, left, y);
        gfx.DrawString(value, valueFont, brush, left + labelWidth, y);
        gfx.DrawLine(new XPen(XBrushes.LightGray, 0.5), left, y + 12, right, y + 12);
    }

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
