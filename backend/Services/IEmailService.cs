namespace SmartCampus.API.Services;

public interface IEmailService
{
    Task SendEmailVerificationAsync(string toEmail, string userName, string token);
    Task SendPasswordResetAsync(string toEmail, string userName, string token);
    Task SendWelcomeEmailAsync(string toEmail, string userName);
    Task Send2FACodeAsync(string toEmail, string userName, string code);
    Task SendAttendanceWarningAsync(string toEmail, string studentName, string courseCode);
    Task SendAttendanceFailureAsync(string toEmail, string studentName, string courseCode);
}

