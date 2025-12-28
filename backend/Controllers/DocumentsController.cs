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
        _context = context ?? throw new ArgumentNullException(nameof(context));
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
        page.Width = XUnit.FromMillimeter(210); // A4 width
        page.Height = XUnit.FromMillimeter(297); // A4 height
        var gfx = XGraphics.FromPdfPage(page);

        // Fonts
        var fontTitle = new XFont("Arial", 18, XFontStyle.Bold);
        var fontHeader = new XFont("Arial", 12, XFontStyle.Bold);
        var fontNormal = new XFont("Arial", 10, XFontStyle.Regular);
        var fontSmall = new XFont("Arial", 9, XFontStyle.Regular);

        // Colors - Koyu Lacivert tonları (#1e3a8a)
        var colorDarkNavy = new XSolidBrush(XColor.FromArgb(30, 58, 138)); // #1e3a8a
        var colorBlue = colorDarkNavy;
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

        // ÖĞRENCİ BELGESİ (küçültülmüş)
        var titleText = "ÖĞRENCİ BELGESİ";
        var titleFont = new XFont("Arial", 14, XFontStyle.Bold);
        gfx.DrawString(titleText, titleFont, colorBlue,
            new XRect(leftMargin, headerY, pageWidth - 60, 18),
            XStringFormats.TopCenter);
        headerY += 30; // Yazılım adı kaldırıldı, boşluk artırıldı

        // Divider line (koyu lacivert)
        gfx.DrawLine(new XPen(XColor.FromArgb(30, 58, 138), 0.5), leftMargin, headerY, rightMargin, headerY);
        yPos = headerY + 25;

        // Öğrenci Bilgileri Section
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
        gfx.DrawString($"{student.User.FirstName} {student.User.LastName}", fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Öğrenci Numarası
        gfx.DrawString("Öğrenci Numarası:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString(student.StudentNumber, fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Bölüm
        gfx.DrawString("Bölüm:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString(student.Department?.Name ?? "Belirtilmemiş", fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Sınıf hesaplama
        var currentYear = DateTime.Now.Year;
        var yearsSinceEnrollment = currentYear - student.EnrollmentYear;
        var classLevel = Math.Min(4, Math.Max(1, yearsSinceEnrollment + 1));
        
        gfx.DrawString("Sınıf:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString($"{classLevel}. Sınıf", fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Kayıt Yılı
        gfx.DrawString("Kayıt Yılı:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString(student.EnrollmentYear.ToString(), fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Aktiflik Durumu
        var activeStatus = student.User.IsActive ? "Aktif" : "Pasif";
        gfx.DrawString("Aktiflik Durumu:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString(activeStatus, fontNormal, colorBlack, valueX, yPos);
        yPos += rowSpacing;

        // Dönem
        gfx.DrawString("Dönem:", labelFont, colorBlack, leftMargin, yPos);
        gfx.DrawString($"{student.CurrentSemester}. Dönem", fontNormal, colorBlack, valueX, yPos);
        yPos += 30;

        // Doğrulama Kutusu (Ortalanmış ve Dar - Kibarlaştırılmış)
        var barcodeText = $"STU-{student.StudentNumber}-{DateTime.Now:yyyyMMddHHmmss}";
        var boxWidth = 400; // Sabit genişlik (sayfanın yaklaşık %60'ı)
        var boxX = (pageWidth - boxWidth) / 2; // Ortalama
        var boxHeight = 50;
        var boxY = yPos;
        var barcodeRect = new XRect(boxX, boxY, boxWidth, boxHeight);
        
        // Basit dikdörtgen (yuvarlatılmış köşeler için PdfSharpCore'da doğrudan destek yok, ince kenarlık ile daha zarif görünüm)
        gfx.DrawRectangle(new XPen(XColor.FromArgb(209, 213, 219), 0.8), // Daha ince kenarlık
            new XSolidBrush(XColor.FromArgb(249, 250, 251)), barcodeRect);
        
        gfx.DrawString("BELGE KODU", fontHeader, colorDarkNavy,
            new XRect(boxX, boxY + 5, boxWidth, 15),
            XStringFormats.TopCenter);
        
        var barcodeFont = new XFont("Courier New", 12, XFontStyle.Bold);
        gfx.DrawString(barcodeText, barcodeFont, colorBlack,
            new XRect(boxX, boxY + 22, boxWidth, 18),
            XStringFormats.TopCenter);
        
        gfx.DrawString("Bu kod ile belgenin doğruluğu kontrol edilebilir", fontSmall, colorGray,
            new XRect(boxX, boxY + 38, boxWidth, 12),
            XStringFormats.TopCenter);
        
        yPos += 70;

        // Yasal Uyarı
        var legalNote = "Bu belge 5070 sayılı Elektronik İmza Kanunu uyarınca güvenli elektronik imza ile oluşturulmuştur.";
        var noteFont = new XFont("Arial", 8, XFontStyle.Italic);
        gfx.DrawString(legalNote, noteFont, colorGray,
            new XRect(leftMargin, yPos, rightMargin - leftMargin, 12),
            XStringFormats.TopCenter);
        
        yPos += 20;

        // Footer - boydan boya ince çizgi (koyu lacivert)
        var footerLineY = pageHeight - 60;
        gfx.DrawLine(new XPen(XColor.FromArgb(30, 58, 138), 0.5), leftMargin, footerLineY, rightMargin, footerLineY);
        
        var footerY = footerLineY + 10;
        var currentDate = DateTime.Now.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR"));
        
        // Sol alt: Belge Tarihi
        gfx.DrawString($"Belge Tarihi: {currentDate}", fontSmall, colorGray, leftMargin, footerY);
        
        // Sağ alt: Doğrulama Kodu
        var verificationCode = $"TR-{DateTime.Now:yyyy}-{student.StudentNumber.Substring(Math.Max(0, student.StudentNumber.Length - 3))}";
        var codeWidth = gfx.MeasureString($"Doğrulama Kodu: {verificationCode}", fontSmall).Width;
        gfx.DrawString($"Doğrulama Kodu: {verificationCode}", fontSmall, colorGray, rightMargin - codeWidth, footerY);

        // PDF'i byte array'e dönüştür
        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }

    // DrawInfoRow metodu artık kullanılmıyor - çizgiler kaldırıldı
    // Öğrenci bilgileri artık 2 sütunlu grid yapısında, çizgisiz olarak gösteriliyor

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
