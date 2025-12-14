using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SmartCampus.API.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailVerificationAsync(string toEmail, string userName, string token)
    {
        var frontendUrl = _configuration["Frontend:Url"] ?? "http://localhost:3000";
        var verificationLink = $"{frontendUrl}/verify-email/{token}";

        var subject = "Smart Campus - Email Doğrulama";
        var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            </head>
            <body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
                <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
                    <tr>
                        <td align='center' style='padding: 40px 20px;'>
                            <table role='presentation' style='max-width: 600px; width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); overflow: hidden;'>
                                <!-- Header with Gradient -->
                                <tr>
                                    <td style='background: linear-gradient(135deg, #1a73e8 0%, #1668d6 50%, #0d47a1 100%); padding: 40px 30px; text-align: center;'>
                                        <h1 style='margin: 0; color: #ffffff; font-size: 28px; font-weight: 700; letter-spacing: -0.5px;'>
                                            Smart Campus
                                        </h1>
                                        <p style='margin: 8px 0 0 0; color: rgba(255, 255, 255, 0.9); font-size: 16px; font-weight: 300;'>
                                            Akıllı Kampüs Yönetim Platformu
                                        </p>
                                    </td>
                                </tr>
                                
                                <!-- Content -->
                                <tr>
                                    <td style='padding: 40px 30px;'>
                                        <h2 style='margin: 0 0 20px 0; color: #1a1a1a; font-size: 24px; font-weight: 600;'>
                                            Merhaba {userName} 👋
                                        </h2>
                                        <p style='margin: 0 0 24px 0; color: #5f6368; font-size: 16px; line-height: 1.6;'>
                                            Smart Campus'a hoş geldiniz! Hesabınızı aktifleştirmek ve platformun tüm özelliklerine erişmek için email adresinizi doğrulamanız gerekmektedir.
                                        </p>
                                        
                                        <!-- CTA Button -->
                                        <table role='presentation' style='width: 100%; border-collapse: collapse; margin: 32px 0;'>
                                            <tr>
                                                <td align='center'>
                                                    <a href='{verificationLink}' 
                                                       style='display: inline-block; background: linear-gradient(135deg, #1a73e8 0%, #1668d6 100%); color: #ffffff; text-decoration: none; padding: 16px 40px; border-radius: 8px; font-size: 16px; font-weight: 600; box-shadow: 0 4px 12px rgba(26, 115, 232, 0.3); transition: all 0.3s ease;'>
                                                        ✉️ Email Adresimi Doğrula
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <p style='margin: 24px 0 16px 0; color: #5f6368; font-size: 14px; text-align: center;'>
                                            Buton çalışmıyor mu? Aşağıdaki linki tarayıcınıza kopyalayın:
                                        </p>
                                        <div style='background-color: #f8f9fa; border: 1px solid #e8eaed; border-radius: 8px; padding: 12px; margin: 16px 0;'>
                                            <p style='margin: 0; word-break: break-all; color: #1a73e8; font-size: 13px; font-family: monospace;'>
                                                {verificationLink}
                                            </p>
                                        </div>
                                        
                                        <!-- Info Box -->
                                        <div style='background-color: #e8f0fe; border-left: 4px solid #1a73e8; padding: 16px; margin: 24px 0; border-radius: 4px;'>
                                            <p style='margin: 0; color: #1967d2; font-size: 14px; font-weight: 500;'>
                                                ⏰ Bu link 24 saat geçerlidir.
                                            </p>
                                        </div>
                                    </td>
                                </tr>
                                
                                <!-- Footer -->
                                <tr>
                                    <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed;'>
                                        <p style='margin: 0 0 12px 0; color: #5f6368; font-size: 14px;'>
                                            Bu emaili siz talep etmediyseniz, lütfen dikkate almayın.
                                        </p>
                                        <p style='margin: 0; color: #9aa0a6; font-size: 12px;'>
                                            © {DateTime.Now.Year} Smart Campus. Tüm hakları saklıdır.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendPasswordResetAsync(string toEmail, string userName, string token)
    {
        var frontendUrl = _configuration["Frontend:Url"] ?? "http://localhost:3000";
        var resetLink = $"{frontendUrl}/reset-password/{token}";

        var subject = "Smart Campus - Şifre Sıfırlama";
        var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            </head>
            <body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
                <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
                    <tr>
                        <td align='center' style='padding: 40px 20px;'>
                            <table role='presentation' style='max-width: 600px; width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); overflow: hidden;'>
                                <!-- Header with Gradient -->
                                <tr>
                                    <td style='background: linear-gradient(135deg, #dc2626 0%, #b91c1c 50%, #991b1b 100%); padding: 40px 30px; text-align: center;'>
                                        <h1 style='margin: 0; color: #ffffff; font-size: 28px; font-weight: 700; letter-spacing: -0.5px;'>
                                            🔒 Şifre Sıfırlama
                                        </h1>
                                        <p style='margin: 8px 0 0 0; color: rgba(255, 255, 255, 0.9); font-size: 16px; font-weight: 300;'>
                                            Smart Campus
                                        </p>
                                    </td>
                                </tr>
                                
                                <!-- Content -->
                                <tr>
                                    <td style='padding: 40px 30px;'>
                                        <h2 style='margin: 0 0 20px 0; color: #1a1a1a; font-size: 24px; font-weight: 600;'>
                                            Merhaba {userName} 👋
                                        </h2>
                                        <p style='margin: 0 0 24px 0; color: #5f6368; font-size: 16px; line-height: 1.6;'>
                                            Şifre sıfırlama talebiniz alındı. Hesabınızın güvenliği için şifrenizi sıfırlamak üzere aşağıdaki butona tıklayın.
                                        </p>
                                        
                                        <!-- CTA Button -->
                                        <table role='presentation' style='width: 100%; border-collapse: collapse; margin: 32px 0;'>
                                            <tr>
                                                <td align='center'>
                                                    <a href='{resetLink}' 
                                                       style='display: inline-block; background: linear-gradient(135deg, #dc2626 0%, #b91c1c 100%); color: #ffffff; text-decoration: none; padding: 16px 40px; border-radius: 8px; font-size: 16px; font-weight: 600; box-shadow: 0 4px 12px rgba(220, 38, 38, 0.3); transition: all 0.3s ease;'>
                                                        🔑 Şifremi Sıfırla
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <p style='margin: 24px 0 16px 0; color: #5f6368; font-size: 14px; text-align: center;'>
                                            Buton çalışmıyor mu? Aşağıdaki linki tarayıcınıza kopyalayın:
                                        </p>
                                        <div style='background-color: #f8f9fa; border: 1px solid #e8eaed; border-radius: 8px; padding: 12px; margin: 16px 0;'>
                                            <p style='margin: 0; word-break: break-all; color: #dc2626; font-size: 13px; font-family: monospace;'>
                                                {resetLink}
                                            </p>
                                        </div>
                                        
                                        <!-- Warning Box -->
                                        <div style='background-color: #fef2f2; border-left: 4px solid #dc2626; padding: 16px; margin: 24px 0; border-radius: 4px;'>
                                            <p style='margin: 0; color: #991b1b; font-size: 14px; font-weight: 500;'>
                                                ⏰ Bu link 24 saat geçerlidir.
                                            </p>
                                            <p style='margin: 8px 0 0 0; color: #991b1b; font-size: 13px;'>
                                                Bu emaili siz talep etmediyseniz, şifreniz güvende demektir. Lütfen dikkate almayın.
                                            </p>
                                        </div>
                                    </td>
                                </tr>
                                
                                <!-- Footer -->
                                <tr>
                                    <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed;'>
                                        <p style='margin: 0; color: #9aa0a6; font-size: 12px;'>
                                            © {DateTime.Now.Year} Smart Campus. Tüm hakları saklıdır.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendWelcomeEmailAsync(string toEmail, string userName)
    {
        var subject = "Smart Campus'a Hoş Geldiniz!";
        var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            </head>
            <body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
                <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
                    <tr>
                        <td align='center' style='padding: 40px 20px;'>
                            <table role='presentation' style='max-width: 600px; width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); overflow: hidden;'>
                                <!-- Header with Gradient -->
                                <tr>
                                    <td style='background: linear-gradient(135deg, #1a73e8 0%, #1668d6 50%, #0d47a1 100%); padding: 40px 30px; text-align: center;'>
                                        <h1 style='margin: 0; color: #ffffff; font-size: 28px; font-weight: 700; letter-spacing: -0.5px;'>
                                            🎉 Hoş Geldiniz!
                                        </h1>
                                        <p style='margin: 8px 0 0 0; color: rgba(255, 255, 255, 0.9); font-size: 16px; font-weight: 300;'>
                                            Smart Campus Ailesine Katıldınız
                                        </p>
                                    </td>
                                </tr>
                                
                                <!-- Content -->
                                <tr>
                                    <td style='padding: 40px 30px;'>
                                        <h2 style='margin: 0 0 20px 0; color: #1a1a1a; font-size: 24px; font-weight: 600;'>
                                            Merhaba {userName}! 👋
                                        </h2>
                                        <p style='margin: 0 0 24px 0; color: #5f6368; font-size: 16px; line-height: 1.6;'>
                                            Email adresiniz başarıyla doğrulandı. Smart Campus platformunun tüm özelliklerine artık erişebilirsiniz!
                                        </p>
                                        
                                        <!-- Features List -->
                                        <div style='background-color: #f8f9fa; border-radius: 8px; padding: 24px; margin: 24px 0;'>
                                            <h3 style='margin: 0 0 16px 0; color: #1a1a1a; font-size: 18px; font-weight: 600;'>
                                                ✨ Platform Özellikleri
                                            </h3>
                                            <ul style='margin: 0; padding-left: 20px; color: #5f6368; font-size: 15px; line-height: 1.8;'>
                                                <li style='margin-bottom: 8px;'>📚 Ders kayıt ve yönetimi</li>
                                                <li style='margin-bottom: 8px;'>📍 GPS tabanlı yoklama sistemi</li>
                                                <li style='margin-bottom: 8px;'>🍽️ Yemekhane rezervasyonu</li>
                                                <li style='margin-bottom: 8px;'>📅 Etkinlik takibi</li>
                                                <li style='margin-bottom: 0;'>🎓 Akademik bilgi yönetimi</li>
                                            </ul>
                                        </div>
                                        
                                        <!-- CTA Button -->
                                        <table role='presentation' style='width: 100%; border-collapse: collapse; margin: 32px 0;'>
                                            <tr>
                                                <td align='center'>
                                                    <a href='{_configuration["Frontend:Url"] ?? "http://localhost:3000"}/login' 
                                                       style='display: inline-block; background: linear-gradient(135deg, #1a73e8 0%, #1668d6 100%); color: #ffffff; text-decoration: none; padding: 16px 40px; border-radius: 8px; font-size: 16px; font-weight: 600; box-shadow: 0 4px 12px rgba(26, 115, 232, 0.3);'>
                                                        🚀 Platforma Giriş Yap
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                
                                <!-- Footer -->
                                <tr>
                                    <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed;'>
                                        <p style='margin: 0 0 12px 0; color: #5f6368; font-size: 14px;'>
                                            Sorularınız için bizimle iletişime geçebilirsiniz.
                                        </p>
                                        <p style='margin: 0; color: #9aa0a6; font-size: 12px;'>
                                            © {DateTime.Now.Year} Smart Campus. Tüm hakları saklıdır.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task Send2FACodeAsync(string toEmail, string userName, string code)
    {
        var subject = "Smart Campus - Giriş Doğrulama Kodu";
        var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            </head>
            <body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
                <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
                    <tr>
                        <td align='center' style='padding: 40px 20px;'>
                            <table role='presentation' style='max-width: 600px; width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); overflow: hidden;'>
                                <!-- Header with Gradient -->
                                <tr>
                                    <td style='background: linear-gradient(135deg, #059669 0%, #047857 50%, #065f46 100%); padding: 40px 30px; text-align: center;'>
                                        <h1 style='margin: 0; color: #ffffff; font-size: 28px; font-weight: 700; letter-spacing: -0.5px;'>
                                            🔐 Giriş Doğrulama
                                        </h1>
                                        <p style='margin: 8px 0 0 0; color: rgba(255, 255, 255, 0.9); font-size: 16px; font-weight: 300;'>
                                            Smart Campus
                                        </p>
                                    </td>
                                </tr>
                                
                                <!-- Content -->
                                <tr>
                                    <td style='padding: 40px 30px;'>
                                        <h2 style='margin: 0 0 20px 0; color: #1a1a1a; font-size: 24px; font-weight: 600;'>
                                            Merhaba {userName} 👋
                                        </h2>
                                        <p style='margin: 0 0 24px 0; color: #5f6368; font-size: 16px; line-height: 1.6;'>
                                            Hesabınıza giriş yapmak için aşağıdaki doğrulama kodunu kullanın:
                                        </p>
                                        
                                        <!-- Code Display -->
                                        <table role='presentation' style='width: 100%; border-collapse: collapse; margin: 32px 0;'>
                                            <tr>
                                                <td align='center'>
                                                    <div style='background: linear-gradient(135deg, #f0fdf4 0%, #dcfce7 100%); border: 2px solid #059669; border-radius: 12px; padding: 24px 48px; display: inline-block;'>
                                                        <span style='font-size: 36px; font-weight: 700; letter-spacing: 8px; color: #059669; font-family: monospace;'>
                                                            {code}
                                                        </span>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        <!-- Warning Box -->
                                        <div style='background-color: #fef3c7; border-left: 4px solid #d97706; padding: 16px; margin: 24px 0; border-radius: 4px;'>
                                            <p style='margin: 0; color: #92400e; font-size: 14px; font-weight: 500;'>
                                                ⏰ Bu kod 5 dakika içinde geçerliliğini yitirecektir.
                                            </p>
                                            <p style='margin: 8px 0 0 0; color: #92400e; font-size: 13px;'>
                                                Bu giriş talebini siz yapmadıysanız, lütfen bu emaili dikkate almayın ve şifrenizi değiştirmeyi düşünün.
                                            </p>
                                        </div>
                                    </td>
                                </tr>
                                
                                <!-- Footer -->
                                <tr>
                                    <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed;'>
                                        <p style='margin: 0; color: #9aa0a6; font-size: 12px;'>
                                            © {DateTime.Now.Year} Smart Campus. Tüm hakları saklıdır.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendAttendanceWarningAsync(string toEmail, string studentName, string courseCode)
    {
        var subject = "Smart Campus - Devamsızlık Uyarısı";
        var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            </head>
            <body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
                <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
                    <tr>
                        <td align='center' style='padding: 40px 20px;'>
                            <table role='presentation' style='max-width: 600px; width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); overflow: hidden;'>
                                <!-- Header with Warning Color -->
                                <tr>
                                    <td style='background: linear-gradient(135deg, #f59e0b 0%, #d97706 50%, #b45309 100%); padding: 40px 30px; text-align: center;'>
                                        <h1 style='margin: 0; color: #ffffff; font-size: 28px; font-weight: 700; letter-spacing: -0.5px;'>
                                            ⚠️ Devamsızlık Uyarısı
                                        </h1>
                                        <p style='margin: 8px 0 0 0; color: rgba(255, 255, 255, 0.9); font-size: 16px; font-weight: 300;'>
                                            Smart Campus
                                        </p>
                                    </td>
                                </tr>
                                
                                <!-- Content -->
                                <tr>
                                    <td style='padding: 40px 30px;'>
                                        <h2 style='margin: 0 0 20px 0; color: #1a1a1a; font-size: 24px; font-weight: 600;'>
                                            Sayın {studentName},
                                        </h2>
                                        <p style='margin: 0 0 24px 0; color: #5f6368; font-size: 16px; line-height: 1.6;'>
                                            <strong>{courseCode}</strong> dersinden devamsızlık oranınız sınıra yaklaşmaktadır. Lütfen dikkat ediniz.
                                        </p>
                                        
                                        <!-- Warning Box -->
                                        <div style='background-color: #fef3c7; border-left: 4px solid #f59e0b; padding: 16px; margin: 24px 0; border-radius: 4px;'>
                                            <p style='margin: 0; color: #92400e; font-size: 14px; font-weight: 500;'>
                                                📌 Önemli Bilgi
                                            </p>
                                            <p style='margin: 8px 0 0 0; color: #92400e; font-size: 13px;'>
                                                Devamsızlık oranınız %15 seviyesine ulaştığında uyarı alırsınız. %30 seviyesine ulaştığında dersten kalırsınız.
                                            </p>
                                        </div>
                                        
                                        <p style='margin: 24px 0 0 0; color: #5f6368; font-size: 14px; line-height: 1.6;'>
                                            Devamsızlık durumunuzu kontrol etmek için Smart Campus sistemine giriş yapabilirsiniz.
                                        </p>
                                    </td>
                                </tr>
                                
                                <!-- Footer -->
                                <tr>
                                    <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed;'>
                                        <p style='margin: 0; color: #9aa0a6; font-size: 12px;'>
                                            © {DateTime.Now.Year} Smart Campus. Tüm hakları saklıdır.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendAttendanceFailureAsync(string toEmail, string studentName, string courseCode)
    {
        var subject = "Smart Campus - Devamsızlık Sınırı Aşıldı";
        var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            </head>
            <body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
                <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
                    <tr>
                        <td align='center' style='padding: 40px 20px;'>
                            <table role='presentation' style='max-width: 600px; width: 100%; border-collapse: collapse; background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); overflow: hidden;'>
                                <!-- Header with Error Color -->
                                <tr>
                                    <td style='background: linear-gradient(135deg, #dc2626 0%, #b91c1c 50%, #991b1b 100%); padding: 40px 30px; text-align: center;'>
                                        <h1 style='margin: 0; color: #ffffff; font-size: 28px; font-weight: 700; letter-spacing: -0.5px;'>
                                            ❌ Devamsızlık Sınırı Aşıldı
                                        </h1>
                                        <p style='margin: 8px 0 0 0; color: rgba(255, 255, 255, 0.9); font-size: 16px; font-weight: 300;'>
                                            Smart Campus
                                        </p>
                                    </td>
                                </tr>
                                
                                <!-- Content -->
                                <tr>
                                    <td style='padding: 40px 30px;'>
                                        <h2 style='margin: 0 0 20px 0; color: #1a1a1a; font-size: 24px; font-weight: 600;'>
                                            Sayın {studentName},
                                        </h2>
                                        <p style='margin: 0 0 24px 0; color: #5f6368; font-size: 16px; line-height: 1.6;'>
                                            <strong>{courseCode}</strong> dersinden devamsızlık sınırını aştınız ve dersten kaldınız.
                                        </p>
                                        
                                        <!-- Error Box -->
                                        <div style='background-color: #fee2e2; border-left: 4px solid #dc2626; padding: 16px; margin: 24px 0; border-radius: 4px;'>
                                            <p style='margin: 0; color: #991b1b; font-size: 14px; font-weight: 500;'>
                                                ⚠️ Önemli Uyarı
                                            </p>
                                            <p style='margin: 8px 0 0 0; color: #991b1b; font-size: 13px;'>
                                                Devamsızlık oranınız %30 seviyesini aştığı için bu dersten kaldınız. Detaylı bilgi için lütfen akademik danışmanınızla iletişime geçiniz.
                                            </p>
                                        </div>
                                        
                                        <p style='margin: 24px 0 0 0; color: #5f6368; font-size: 14px; line-height: 1.6;'>
                                            Devamsızlık durumunuzu kontrol etmek için Smart Campus sistemine giriş yapabilirsiniz.
                                        </p>
                                    </td>
                                </tr>
                                
                                <!-- Footer -->
                                <tr>
                                    <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed;'>
                                        <p style='margin: 0; color: #9aa0a6; font-size: 12px;'>
                                            © {DateTime.Now.Year} Smart Campus. Tüm hakları saklıdır.
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendCustomEmailAsync(string toEmail, string subject, string htmlBody)
    {
        await SendEmailAsync(toEmail, subject, htmlBody);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        try
        {
            // Önce environment variable'dan oku, yoksa appsettings'ten al
            var smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? _configuration["Email:SmtpServer"];
            var smtpPortStr = Environment.GetEnvironmentVariable("SMTP_PORT") ?? _configuration["Email:SmtpPort"];
            var smtpPort = int.Parse(smtpPortStr ?? "587");
            var smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? _configuration["Email:SmtpUsername"];
            var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? _configuration["Email:SmtpPassword"];
            var fromEmail = Environment.GetEnvironmentVariable("SMTP_FROM_EMAIL") ?? _configuration["Email:FromEmail"] ?? "noreply@smartcampus.com";
            var fromName = Environment.GetEnvironmentVariable("SMTP_FROM_NAME") ?? _configuration["Email:FromName"] ?? "Smart Campus";

            // SMTP yapılandırması yoksa, sadece log'la (development için)
            if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
            {
                _logger.LogWarning("SMTP yapılandırması eksik. Email gönderilmedi: {ToEmail}, Subject: {Subject}", toEmail, subject);
                _logger.LogInformation("Email içeriği: {Body}", htmlBody);
                return;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromEmail));
            message.To.Add(new MailboxAddress(toEmail, toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            
            // Port 465 için SSL, diğer portlar için StartTLS kullan
            var secureSocketOptions = smtpPort == 465 
                ? SecureSocketOptions.SslOnConnect 
                : SecureSocketOptions.StartTls;
            
            await client.ConnectAsync(smtpServer, smtpPort, secureSocketOptions);
            await client.AuthenticateAsync(smtpUsername, smtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email başarıyla gönderildi: {ToEmail}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Email gönderimi başarısız: {ToEmail}", toEmail);
            // Email hatası kritik değil, işlemi durdurmuyoruz
        }
    }
}

