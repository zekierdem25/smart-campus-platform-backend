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
            <html>
            <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #2563eb;'>Merhaba {userName},</h2>
                    <p>Smart Campus'a hoş geldiniz! Hesabınızı aktifleştirmek için aşağıdaki butona tıklayın:</p>
                    <div style='text-align: center; margin: 30px 0;'>
                        <a href='{verificationLink}' 
                           style='background-color: #2563eb; color: white; padding: 12px 30px; 
                                  text-decoration: none; border-radius: 5px; display: inline-block;'>
                            Email Adresimi Doğrula
                        </a>
                    </div>
                    <p>Veya bu linki tarayıcınıza kopyalayın:</p>
                    <p style='word-break: break-all; color: #666;'>{verificationLink}</p>
                    <p style='color: #666; font-size: 14px;'>Bu link 24 saat geçerlidir.</p>
                    <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
                    <p style='color: #999; font-size: 12px;'>
                        Bu emaili siz talep etmediyseniz, lütfen dikkate almayın.
                    </p>
                </div>
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
            <html>
            <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #2563eb;'>Merhaba {userName},</h2>
                    <p>Şifre sıfırlama talebiniz alındı. Şifrenizi sıfırlamak için aşağıdaki butona tıklayın:</p>
                    <div style='text-align: center; margin: 30px 0;'>
                        <a href='{resetLink}' 
                           style='background-color: #dc2626; color: white; padding: 12px 30px; 
                                  text-decoration: none; border-radius: 5px; display: inline-block;'>
                            Şifremi Sıfırla
                        </a>
                    </div>
                    <p>Veya bu linki tarayıcınıza kopyalayın:</p>
                    <p style='word-break: break-all; color: #666;'>{resetLink}</p>
                    <p style='color: #666; font-size: 14px;'>Bu link 24 saat geçerlidir.</p>
                    <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
                    <p style='color: #999; font-size: 12px;'>
                        Bu emaili siz talep etmediyseniz, şifreniz güvende demektir. Lütfen dikkate almayın.
                    </p>
                </div>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendWelcomeEmailAsync(string toEmail, string userName)
    {
        var subject = "Smart Campus'a Hoş Geldiniz!";
        var body = $@"
            <html>
            <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                    <h2 style='color: #2563eb;'>Hoş Geldiniz {userName}!</h2>
                    <p>Smart Campus ailesine katıldığınız için teşekkür ederiz.</p>
                    <p>Artık aşağıdaki özelliklere erişebilirsiniz:</p>
                    <ul>
                        <li>Ders kayıt ve yönetimi</li>
                        <li>GPS tabanlı yoklama sistemi</li>
                        <li>Yemekhane rezervasyonu</li>
                        <li>Etkinlik takibi</li>
                        <li>ve daha fazlası...</li>
                    </ul>
                    <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;'>
                    <p style='color: #999; font-size: 12px;'>
                        Smart Campus Ekibi
                    </p>
                </div>
            </body>
            </html>";

        await SendEmailAsync(toEmail, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
        try
        {
            var smtpServer = _configuration["Email:SmtpServer"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
            var smtpUsername = _configuration["Email:SmtpUsername"];
            var smtpPassword = _configuration["Email:SmtpPassword"];
            var fromEmail = _configuration["Email:FromEmail"] ?? "noreply@smartcampus.com";
            var fromName = _configuration["Email:FromName"] ?? "Smart Campus";

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
            await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
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

