using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf.IO;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/documents")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DocumentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Öğrenci belgesi PDF olarak indir (Student only)
    /// </summary>
    [HttpGet("student-certificate")]
    [Authorize(Roles = "Student")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudentCertificate()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
        {
            return Unauthorized(new { message = "Kullanıcı kimliği bulunamadı" });
        }

        // Öğrenci bilgilerini getir
        var student = await _context.Students
            .Include(s => s.User)
            .Include(s => s.Department)
            .FirstOrDefaultAsync(s => s.UserId == userId.Value);

        if (student == null)
        {
            return NotFound(new { message = "Öğrenci kaydı bulunamadı" });
        }

        // PDF oluştur
        var pdfBytes = GenerateCertificatePdf(student);

        // Dosya adı oluştur
        var fileName = $"OgrenciBelgesi_{student.StudentNumber}_{DateTime.Now:yyyyMMdd}.pdf";

        return File(pdfBytes, "application/pdf", fileName);
    }

    private byte[] GenerateCertificatePdf(Models.Student student)
    {
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
        var fontHeader = new XFont("Arial", 12, XFontStyle.Bold);
        var fontNormal = new XFont("Arial", 11, XFontStyle.Regular);
        var fontSmall = new XFont("Arial", 9, XFontStyle.Regular);

        // Renkler
        var colorBlue = XBrushes.DarkBlue;
        var colorBlack = XBrushes.Black;
        var colorGray = XBrushes.Gray;

        double yPos = 50;
        double leftMargin = 50;
        double rightMargin = 550;
        double lineHeight = 25;

        // Başlık
        var titleText = "ÖĞRENCİ BELGESİ";
        var titleSize = gfx.MeasureString(titleText, fontTitle);
        gfx.DrawString(titleText, fontTitle, colorBlue, 
            new XRect(leftMargin, yPos, page.Width - 100, 30), 
            XStringFormats.TopCenter);
        yPos += 40;

        // Alt başlık
        gfx.DrawString("Akıllı Kampüs Ekosistem Yönetim Platformu", fontSmall, colorGray,
            new XRect(leftMargin, yPos, page.Width - 100, 20),
            XStringFormats.TopCenter);
        yPos += 30;

        // Çizgi
        gfx.DrawLine(new XPen(colorBlue, 2), leftMargin, yPos, rightMargin, yPos);
        yPos += 20;

        // Öğrenci bilgileri
        var currentDate = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"));
        var enrollmentDate = student.CreatedAt.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"));
        var activeStatus = student.User.IsActive ? "Aktif" : "Pasif";
        
        // Sınıf hesaplama
        var currentYear = DateTime.Now.Year;
        var yearsSinceEnrollment = currentYear - student.EnrollmentYear;
        var classLevel = Math.Min(4, Math.Max(1, yearsSinceEnrollment + 1));

        // Bilgi satırları
        DrawInfoRow(gfx, "Ad Soyad:", $"{student.User.FirstName} {student.User.LastName}", 
            leftMargin, yPos, rightMargin, fontHeader, fontNormal, colorBlack);
        yPos += lineHeight;

        DrawInfoRow(gfx, "Öğrenci Numarası:", student.StudentNumber, 
            leftMargin, yPos, rightMargin, fontHeader, fontNormal, colorBlack);
        yPos += lineHeight;

        DrawInfoRow(gfx, "Bölüm:", student.Department?.Name ?? "Belirtilmemiş", 
            leftMargin, yPos, rightMargin, fontHeader, fontNormal, colorBlack);
        yPos += lineHeight;

        DrawInfoRow(gfx, "Sınıf:", $"{classLevel}. Sınıf", 
            leftMargin, yPos, rightMargin, fontHeader, fontNormal, colorBlack);
        yPos += lineHeight;

        DrawInfoRow(gfx, "Kayıt Tarihi:", enrollmentDate, 
            leftMargin, yPos, rightMargin, fontHeader, fontNormal, colorBlack);
        yPos += lineHeight;

        DrawInfoRow(gfx, "Aktiflik Durumu:", activeStatus, 
            leftMargin, yPos, rightMargin, fontHeader, fontNormal, colorBlack);
        yPos += lineHeight;

        DrawInfoRow(gfx, "Kayıt Yılı:", student.EnrollmentYear.ToString(), 
            leftMargin, yPos, rightMargin, fontHeader, fontNormal, colorBlack);
        yPos += lineHeight;

        DrawInfoRow(gfx, "Dönem:", $"{student.CurrentSemester}. Dönem", 
            leftMargin, yPos, rightMargin, fontHeader, fontNormal, colorBlack);
        yPos += 30;

        // Barkod bölümü
        var barcodeText = $"STU-{student.StudentNumber}-{DateTime.Now:yyyyMMddHHmmss}";
        var barcodeRect = new XRect(leftMargin, yPos, rightMargin - leftMargin, 60);
        gfx.DrawRectangle(new XPen(colorGray, 1), XBrushes.LightGray, barcodeRect);
        
        gfx.DrawString("BELGE KODU", fontHeader, colorBlue,
            new XRect(leftMargin, yPos + 5, rightMargin - leftMargin, 15),
            XStringFormats.TopCenter);
        
        var barcodeFont = new XFont("Courier New", 14, XFontStyle.Bold);
        gfx.DrawString(barcodeText, barcodeFont, colorBlack,
            new XRect(leftMargin, yPos + 25, rightMargin - leftMargin, 20),
            XStringFormats.TopCenter);
        
        gfx.DrawString("Bu kod ile belgenin doğruluğu kontrol edilebilir", fontSmall, colorGray,
            new XRect(leftMargin, yPos + 45, rightMargin - leftMargin, 15),
            XStringFormats.TopCenter);
        
        yPos += 80;

        // Alt bilgi
        gfx.DrawString($"Belge Tarihi: {currentDate}", fontSmall, colorGray,
            new XRect(leftMargin, yPos, rightMargin - leftMargin, 15),
            XStringFormats.TopRight);
        yPos += 30;

        // İmza bölümü
        var signatureY = page.Height - 80;
        var signatureWidth = 150;
        var signatureSpacing = (rightMargin - leftMargin - (signatureWidth * 2)) / 3;

        // Sol imza
        gfx.DrawLine(new XPen(colorBlack, 1), 
            leftMargin + signatureSpacing, signatureY,
            leftMargin + signatureSpacing + signatureWidth, signatureY);
        gfx.DrawString("Öğrenci İmzası", fontSmall, colorBlack,
            new XRect(leftMargin + signatureSpacing, signatureY + 5, signatureWidth, 15),
            XStringFormats.TopCenter);

        // Sağ imza
        gfx.DrawLine(new XPen(colorBlack, 1),
            rightMargin - signatureSpacing - signatureWidth, signatureY,
            rightMargin - signatureSpacing, signatureY);
        gfx.DrawString("Sistem Onayı", fontSmall, colorBlack,
            new XRect(rightMargin - signatureSpacing - signatureWidth, signatureY + 5, signatureWidth, 15),
            XStringFormats.TopCenter);

        // PDF'i byte array'e dönüştür
        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }

    private void DrawInfoRow(XGraphics gfx, string label, string value, 
        double left, double y, double right, XFont labelFont, XFont valueFont, XBrush brush)
    {
        var labelWidth = 150;
        gfx.DrawString(label, labelFont, brush, left, y);
        gfx.DrawString(value, valueFont, brush, left + labelWidth, y);
        gfx.DrawLine(new XPen(XBrushes.LightGray, 0.5), left, y + 15, right, y + 15);
    }

    private Guid? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }
        return null;
    }
}
