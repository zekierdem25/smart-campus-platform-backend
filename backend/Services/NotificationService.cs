using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public class NotificationService : INotificationService
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        ApplicationDbContext context,
        IEmailService emailService,
        ILogger<NotificationService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task NotifyFacultyOnCourseDropAsync(Guid enrollmentId)
    {
        try
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                    .ThenInclude(s => s.User)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Course)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Instructor)
                        .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(e => e.Id == enrollmentId);

            if (enrollment == null)
            {
                _logger.LogWarning("Enrollment not found for notification: {EnrollmentId}", enrollmentId);
                return;
            }

            if (enrollment.Section.Instructor?.User?.Email == null)
            {
                _logger.LogWarning("Instructor email not found for enrollment: {EnrollmentId}", enrollmentId);
                return;
            }

            var studentName = $"{enrollment.Student.User.FirstName} {enrollment.Student.User.LastName}";
            var courseCode = enrollment.Section.Course.Code;
            var courseName = enrollment.Section.Course.Name;
            var instructorEmail = enrollment.Section.Instructor.User.Email;
            var instructorName = $"{enrollment.Section.Instructor.User.FirstName} {enrollment.Section.Instructor.User.LastName}";

            var subject = $"Öğrenci Ders Bıraktı - {courseCode}";
            var htmlBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
    <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
        <tr>
            <td style='padding: 40px 20px;'>
                <table role='presentation' style='width: 100%; max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);'>
                    <tr>
                        <td style='background-color: #1a73e8; padding: 30px; text-align: center; border-radius: 8px 8px 0 0;'>
                            <h1 style='margin: 0; color: #ffffff; font-size: 24px; font-weight: 600;'>Ders Bırakma Bildirimi</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style='padding: 30px;'>
                            <p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>
                                Sayın <strong>{instructorName}</strong>,
                            </p>
                            <p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>
                                <strong>{studentName}</strong> öğrencisi <strong>{courseCode} - {courseName}</strong> dersini bırakmıştır.
                            </p>
                            <div style='background-color: #f8f9fa; padding: 20px; border-radius: 4px; margin: 20px 0;'>
                                <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Öğrenci:</strong> {studentName}</p>
                                <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Ders Kodu:</strong> {courseCode}</p>
                                <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Ders Adı:</strong> {courseName}</p>
                            </div>
                            <p style='margin: 20px 0 0 0; color: #5f6368; font-size: 14px;'>
                                Bu bilgilendirme otomatik olarak gönderilmiştir.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed; border-radius: 0 0 8px 8px;'>
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

            // Send email asynchronously (fire and forget)
            _ = Task.Run(async () =>
            {
                try
                {
                    await _emailService.SendCustomEmailAsync(instructorEmail, subject, htmlBody);
                    _logger.LogInformation("Course drop notification sent to faculty: {Email}", instructorEmail);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send course drop notification to faculty: {Email}", instructorEmail);
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in NotifyFacultyOnCourseDropAsync for enrollment: {EnrollmentId}", enrollmentId);
        }
    }

    public async Task NotifyStudentOnGradeEntryAsync(Guid enrollmentId)
    {
        try
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                    .ThenInclude(s => s.User)
                .Include(e => e.Section)
                    .ThenInclude(s => s.Course)
                .FirstOrDefaultAsync(e => e.Id == enrollmentId);

            if (enrollment == null)
            {
                _logger.LogWarning("Enrollment not found for notification: {EnrollmentId}", enrollmentId);
                return;
            }

            if (enrollment.Student?.User?.Email == null)
            {
                _logger.LogWarning("Student email not found for enrollment: {EnrollmentId}", enrollmentId);
                return;
            }

            // Only notify if at least one grade has been entered
            if (!enrollment.MidtermGrade.HasValue && !enrollment.FinalGrade.HasValue && !enrollment.HomeworkGrade.HasValue)
            {
                _logger.LogInformation("No grades entered yet for enrollment: {EnrollmentId}", enrollmentId);
                return;
            }

            var studentName = $"{enrollment.Student.User.FirstName} {enrollment.Student.User.LastName}";
            var courseCode = enrollment.Section.Course.Code;
            var courseName = enrollment.Section.Course.Name;
            var studentEmail = enrollment.Student.User.Email;

            var gradeInfo = "";
            if (enrollment.MidtermGrade.HasValue)
                gradeInfo += $"<p style='margin: 5px 0; color: #5f6368; font-size: 14px;'><strong>Vize:</strong> {enrollment.MidtermGrade.Value:F1}</p>";
            if (enrollment.FinalGrade.HasValue)
                gradeInfo += $"<p style='margin: 5px 0; color: #5f6368; font-size: 14px;'><strong>Final:</strong> {enrollment.FinalGrade.Value:F1}</p>";
            if (enrollment.HomeworkGrade.HasValue)
                gradeInfo += $"<p style='margin: 5px 0; color: #5f6368; font-size: 14px;'><strong>Ödev:</strong> {enrollment.HomeworkGrade.Value:F1}</p>";
            if (!string.IsNullOrEmpty(enrollment.LetterGrade))
                gradeInfo += $"<p style='margin: 5px 0; color: #5f6368; font-size: 14px;'><strong>Harf Notu:</strong> {enrollment.LetterGrade}</p>";
            if (enrollment.GradePoint.HasValue)
                gradeInfo += $"<p style='margin: 5px 0; color: #5f6368; font-size: 14px;'><strong>Not Ortalaması:</strong> {enrollment.GradePoint.Value:F2}</p>";

            var subject = $"Notunuz Açıklandı - {courseCode}";
            var htmlBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
    <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
        <tr>
            <td style='padding: 40px 20px;'>
                <table role='presentation' style='width: 100%; max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);'>
                    <tr>
                        <td style='background-color: #34a853; padding: 30px; text-align: center; border-radius: 8px 8px 0 0;'>
                            <h1 style='margin: 0; color: #ffffff; font-size: 24px; font-weight: 600;'>Notunuz Açıklandı</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style='padding: 30px;'>
                            <p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>
                                Sayın <strong>{studentName}</strong>,
                            </p>
                            <p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>
                                <strong>{courseCode} - {courseName}</strong> dersiniz için notlarınız sisteme girilmiştir.
                            </p>
                            <div style='background-color: #f8f9fa; padding: 20px; border-radius: 4px; margin: 20px 0;'>
                                <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Ders Kodu:</strong> {courseCode}</p>
                                <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Ders Adı:</strong> {courseName}</p>
                                {gradeInfo}
                            </div>
                            <p style='margin: 20px 0 0 0; color: #5f6368; font-size: 14px;'>
                                Detaylı bilgi için sisteme giriş yapabilirsiniz.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed; border-radius: 0 0 8px 8px;'>
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

            // Send email asynchronously (fire and forget)
            _ = Task.Run(async () =>
            {
                try
                {
                    await _emailService.SendCustomEmailAsync(studentEmail, subject, htmlBody);
                    _logger.LogInformation("Grade entry notification sent to student: {Email}", studentEmail);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send grade entry notification to student: {Email}", studentEmail);
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in NotifyStudentOnGradeEntryAsync for enrollment: {EnrollmentId}", enrollmentId);
        }
    }

    public async Task NotifyStudentsOnAttendanceSessionStartAsync(Guid sessionId)
    {
        try
        {
            var session = await _context.AttendanceSessions
                .Include(s => s.Section)
                    .ThenInclude(sec => sec.Course)
                .Include(s => s.Instructor)
                    .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null)
            {
                _logger.LogWarning("Session not found for notification: {SessionId}", sessionId);
                return;
            }

            // Get all enrolled students for this section
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                    .ThenInclude(s => s.User)
                .Where(e => e.SectionId == session.SectionId && e.Status == EnrollmentStatus.Active)
                .ToListAsync();

            if (!enrollments.Any())
            {
                _logger.LogInformation("No enrolled students found for session: {SessionId}", sessionId);
                return;
            }

            var courseCode = session.Section.Course.Code;
            var courseName = session.Section.Course.Name;
            var instructorName = $"{session.Instructor.User.FirstName} {session.Instructor.User.LastName}";
            var sessionDate = session.Date.ToString("dd.MM.yyyy");
            var sessionTime = $"{session.StartTime:hh\\:mm} - {session.EndTime:hh\\:mm}";

            var subject = $"Yoklama Başlatıldı - {courseCode}";
            var htmlBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
</head>
<body style='margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, ""Helvetica Neue"", Arial, sans-serif; background-color: #f5f5f5;'>
    <table role='presentation' style='width: 100%; border-collapse: collapse; background-color: #f5f5f5;'>
        <tr>
            <td style='padding: 40px 20px;'>
                <table role='presentation' style='width: 100%; max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);'>
                    <tr>
                        <td style='background-color: #ea4335; padding: 30px; text-align: center; border-radius: 8px 8px 0 0;'>
                            <h1 style='margin: 0; color: #ffffff; font-size: 24px; font-weight: 600;'>Yoklama Başlatıldı</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style='padding: 30px;'>
                            <p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>
                                <strong>{courseCode} - {courseName}</strong> dersi için yoklama başlatılmıştır.
                            </p>
                            <div style='background-color: #f8f9fa; padding: 20px; border-radius: 4px; margin: 20px 0;'>
                                <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Ders Kodu:</strong> {courseCode}</p>
                                <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Ders Adı:</strong> {courseName}</p>
                                <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Öğretim Üyesi:</strong> {instructorName}</p>
                                <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {sessionDate}</p>
                                <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Saat:</strong> {sessionTime}</p>
                            </div>
                            <p style='margin: 20px 0 0 0; color: #ea4335; font-size: 14px; font-weight: 600;'>
                                ⚠️ Lütfen yoklamaya katılmak için sisteme giriş yapın!
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style='background-color: #f8f9fa; padding: 24px 30px; text-align: center; border-top: 1px solid #e8eaed; border-radius: 0 0 8px 8px;'>
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

            // Send email to all enrolled students asynchronously
            var emailTasks = enrollments
                .Where(e => e.Student?.User?.Email != null)
                .Select(enrollment => Task.Run(async () =>
                {
                    try
                    {
                        var studentEmail = enrollment.Student!.User!.Email!;
                        var studentName = $"{enrollment.Student.User.FirstName} {enrollment.Student.User.LastName}";
                        
                        // Personalize the email with student name
                        var personalizedBody = htmlBody.Replace(
                            "<p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>",
                            $"<p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>Sayın <strong>{studentName}</strong>,<br><br>");

                        await _emailService.SendCustomEmailAsync(studentEmail, subject, personalizedBody);
                        _logger.LogInformation("Attendance session notification sent to student: {Email}", studentEmail);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to send attendance session notification to student: {Email}", enrollment.Student?.User?.Email);
                    }
                }));

            await Task.WhenAll(emailTasks);
            _logger.LogInformation("Attendance session notifications sent to {Count} students for session: {SessionId}", emailTasks.Count(), sessionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in NotifyStudentsOnAttendanceSessionStartAsync for session: {SessionId}", sessionId);
        }
    }
}
