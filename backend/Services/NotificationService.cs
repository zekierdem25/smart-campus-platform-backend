using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Hubs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public class NotificationService : INotificationService
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        ApplicationDbContext context,
        IEmailService emailService,
        IHubContext<NotificationHub> hubContext,
        ILogger<NotificationService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
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

            // Create in-app notification
            var notificationTitle = $"Notunuz Açıklandı - {courseCode}";
            var notificationMessage = $"{courseCode} - {courseName} dersiniz için notlarınız sisteme girilmiştir.";
            var notificationType = enrollment.GradePoint >= 2.0m ? NotificationType.Success : NotificationType.Info;

            await CreateNotificationAsync(
                enrollment.Student.UserId,
                notificationTitle,
                notificationMessage,
                NotificationCategory.Academic,
                notificationType,
                enrollmentId,
                "Enrollment"
            );

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

    // ========== Part 3: Meal Notifications ==========

    public async Task SendMealReservationConfirmationAsync(Guid reservationId)
    {
        try
        {
            var reservation = await _context.MealReservations
                .Include(r => r.Menu)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation?.User?.Email == null)
            {
                _logger.LogWarning("User email not found for meal reservation: {ReservationId}", reservationId);
                return;
            }

            var mealTypeText = reservation.Menu.MealType.ToString() switch
            {
                "Breakfast" => "Kahvaltı",
                "Lunch" => "Öğle Yemeği",
                "Dinner" => "Akşam Yemeği",
                _ => reservation.Menu.MealType.ToString()
            };

            var subject = $"Yemek Rezervasyonunuz Onaylandı - {reservation.Menu.Date:dd.MM.yyyy}";
            var htmlBody = BuildEmailTemplate(
                "Yemek Rezervasyonu",
                "#34a853",
                $"Sayın <strong>{reservation.User.FirstName} {reservation.User.LastName}</strong>,",
                "Yemek rezervasyonunuz başarıyla oluşturuldu.",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {reservation.Menu.Date:dd.MM.yyyy}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Öğün:</strong> {mealTypeText}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>QR Kod:</strong> {reservation.QrCode}</p>");

            await SendEmailFireAndForgetAsync(reservation.User.Email, subject, htmlBody, "Meal reservation confirmation");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendMealReservationConfirmationAsync: {ReservationId}", reservationId);
        }
    }

    public async Task SendMealReservationCancellationAsync(Guid reservationId)
    {
        try
        {
            var reservation = await _context.MealReservations
                .Include(r => r.Menu)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation?.User?.Email == null) return;

            var subject = $"Yemek Rezervasyonunuz İptal Edildi - {reservation.Menu.Date:dd.MM.yyyy}";
            var htmlBody = BuildEmailTemplate(
                "Rezervasyon İptali",
                "#ea4335",
                $"Sayın <strong>{reservation.User.FirstName} {reservation.User.LastName}</strong>,",
                $"{reservation.Menu.Date:dd.MM.yyyy} tarihli yemek rezervasyonunuz iptal edilmiştir.",
                "");

            await SendEmailFireAndForgetAsync(reservation.User.Email, subject, htmlBody, "Meal reservation cancellation");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendMealReservationCancellationAsync: {ReservationId}", reservationId);
        }
    }

    // ========== Part 3: Event Notifications ==========

    public async Task SendEventRegistrationConfirmationAsync(Guid registrationId)
    {
        try
        {
            var registration = await _context.EventRegistrations
                .Include(r => r.Event)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == registrationId);

            if (registration?.User?.Email == null) return;

            var subject = $"Etkinlik Kaydınız Onaylandı - {registration.Event.Title}";
            var htmlBody = BuildEmailTemplate(
                "Etkinlik Kaydı",
                "#1a73e8",
                $"Sayın <strong>{registration.User.FirstName} {registration.User.LastName}</strong>,",
                $"<strong>{registration.Event.Title}</strong> etkinliğine kaydınız başarıyla tamamlandı.",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Etkinlik:</strong> {registration.Event.Title}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {registration.Event.Date:dd.MM.yyyy}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Saat:</strong> {registration.Event.StartTime:hh\\:mm} - {registration.Event.EndTime:hh\\:mm}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Konum:</strong> {registration.Event.Location}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>QR Kod:</strong> {registration.QrCode}</p>");

            await SendEmailFireAndForgetAsync(registration.User.Email, subject, htmlBody, "Event registration confirmation");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendEventRegistrationConfirmationAsync: {RegistrationId}", registrationId);
        }
    }

    public async Task SendEventRegistrationCancellationAsync(Guid registrationId)
    {
        try
        {
            var registration = await _context.EventRegistrations
                .Include(r => r.Event)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == registrationId);

            if (registration?.User?.Email == null) return;

            var subject = $"Etkinlik Kaydınız İptal Edildi - {registration.Event.Title}";
            var htmlBody = BuildEmailTemplate(
                "Kayıt İptali",
                "#ea4335",
                $"Sayın <strong>{registration.User.FirstName} {registration.User.LastName}</strong>,",
                $"<strong>{registration.Event.Title}</strong> etkinliğine kaydınız iptal edilmiştir.",
                "");

            await SendEmailFireAndForgetAsync(registration.User.Email, subject, htmlBody, "Event registration cancellation");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendEventRegistrationCancellationAsync: {RegistrationId}", registrationId);
        }
    }

    public async Task SendEventWaitlistPromotionAsync(Guid registrationId)
    {
        try
        {
            var registration = await _context.EventRegistrations
                .Include(r => r.Event)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == registrationId);

            if (registration?.User?.Email == null) return;

            var subject = $"Bekleme Listesinden Kaydınız Yapıldı - {registration.Event.Title}";
            var htmlBody = BuildEmailTemplate(
                "Waitlist Promosyonu",
                "#34a853",
                $"Sayın <strong>{registration.User.FirstName} {registration.User.LastName}</strong>,",
                "Bekleme listesinde yeriniz açıldı ve etkinliğe otomatik olarak kaydedildiniz!",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Etkinlik:</strong> {registration.Event.Title}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {registration.Event.Date:dd.MM.yyyy}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>QR Kod:</strong> {registration.QrCode}</p>");

            await SendEmailFireAndForgetAsync(registration.User.Email, subject, htmlBody, "Waitlist promotion");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendEventWaitlistPromotionAsync: {RegistrationId}", registrationId);
        }
    }

    // ========== Part 3: Classroom Reservation Notifications ==========

    public async Task SendClassroomReservationPendingAsync(Guid reservationId)
    {
        try
        {
            var reservation = await _context.ClassroomReservations
                .Include(r => r.Classroom)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation?.User?.Email == null) return;

            var subject = "Derslik Rezervasyonunuz Alındı";
            var htmlBody = BuildEmailTemplate(
                "Rezervasyon Bekleniyor",
                "#fbbc04",
                $"Sayın <strong>{reservation.User.FirstName} {reservation.User.LastName}</strong>,",
                "Derslik rezervasyon talebiniz alınmıştır. Onay bekleniyor.",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Derslik:</strong> {reservation.Classroom.Building} - {reservation.Classroom.RoomNumber}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {reservation.Date:dd.MM.yyyy}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Saat:</strong> {reservation.StartTime:hh\\:mm} - {reservation.EndTime:hh\\:mm}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Amaç:</strong> {reservation.Purpose}</p>");

            await SendEmailFireAndForgetAsync(reservation.User.Email, subject, htmlBody, "Classroom reservation pending");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendClassroomReservationPendingAsync: {ReservationId}", reservationId);
        }
    }

    public async Task SendClassroomReservationApprovalAsync(Guid reservationId)
    {
        try
        {
            var reservation = await _context.ClassroomReservations
                .Include(r => r.Classroom)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation?.User?.Email == null) return;

            var subject = "Derslik Rezervasyonunuz Onaylandı";
            var htmlBody = BuildEmailTemplate(
                "Rezervasyon Onaylandı",
                "#34a853",
                $"Sayın <strong>{reservation.User.FirstName} {reservation.User.LastName}</strong>,",
                "Derslik rezervasyonunuz onaylanmıştır.",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Derslik:</strong> {reservation.Classroom.Building} - {reservation.Classroom.RoomNumber}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {reservation.Date:dd.MM.yyyy}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Saat:</strong> {reservation.StartTime:hh\\:mm} - {reservation.EndTime:hh\\:mm}</p>");

            await SendEmailFireAndForgetAsync(reservation.User.Email, subject, htmlBody, "Classroom reservation approval");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendClassroomReservationApprovalAsync: {ReservationId}", reservationId);
        }
    }

    public async Task SendClassroomReservationRejectionAsync(Guid reservationId, string? reason = null)
    {
        try
        {
            var reservation = await _context.ClassroomReservations
                .Include(r => r.Classroom)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation?.User?.Email == null) return;

            var reasonText = !string.IsNullOrEmpty(reason) 
                ? $"<p style='margin: 10px 0 0 0; color: #ea4335; font-size: 14px;'><strong>Red Nedeni:</strong> {reason}</p>" 
                : "";

            var subject = "Derslik Rezervasyonunuz Reddedildi";
            var htmlBody = BuildEmailTemplate(
                "Rezervasyon Reddedildi",
                "#ea4335",
                $"Sayın <strong>{reservation.User.FirstName} {reservation.User.LastName}</strong>,",
                "Derslik rezervasyonunuz reddedilmiştir." + reasonText,
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Derslik:</strong> {reservation.Classroom.Building} - {reservation.Classroom.RoomNumber}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {reservation.Date:dd.MM.yyyy}</p>");

            await SendEmailFireAndForgetAsync(reservation.User.Email, subject, htmlBody, "Classroom reservation rejection");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendClassroomReservationRejectionAsync: {ReservationId}", reservationId);
        }
    }

    public async Task NotifyAdminClassroomReservationPendingAsync(Guid reservationId)
    {
        try
        {
            var reservation = await _context.ClassroomReservations
                .Include(r => r.Classroom)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null) return;

            // Get admin emails
            var adminEmails = await _context.Users
                .Where(u => u.Role == UserRole.Admin)
                .Select(u => u.Email)
                .ToListAsync();

            if (!adminEmails.Any()) return;

            var subject = "[Admin] Yeni Derslik Rezervasyon Talebi";
            var htmlBody = BuildEmailTemplate(
                "Yeni Rezervasyon Talebi",
                "#1a73e8",
                "Sayın Admin,",
                "Yeni bir derslik rezervasyon talebi var ve onay bekliyor.",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Talep Eden:</strong> {reservation.User.FirstName} {reservation.User.LastName}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Email:</strong> {reservation.User.Email}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Derslik:</strong> {reservation.Classroom.Building} - {reservation.Classroom.RoomNumber}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {reservation.Date:dd.MM.yyyy}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Saat:</strong> {reservation.StartTime:hh\\:mm} - {reservation.EndTime:hh\\:mm}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Amaç:</strong> {reservation.Purpose}</p>");

            foreach (var email in adminEmails)
            {
                await SendEmailFireAndForgetAsync(email, subject, htmlBody, "Admin classroom reservation notification");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in NotifyAdminClassroomReservationPendingAsync: {ReservationId}", reservationId);
        }
    }

    // ========== Part 3: Equipment Notifications ==========

    public async Task SendEquipmentBorrowingRequestAsync(Guid borrowingId)
    {
        try
        {
            var borrowing = await _context.EquipmentBorrowings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == borrowingId);

            if (borrowing?.User?.Email == null) return;

            var subject = $"Ekipman Ödünç Alma Talebiniz Alındı - {borrowing.Equipment.Name}";
            var htmlBody = BuildEmailTemplate(
                "Ekipman Talebi",
                "#fbbc04",
                $"Sayın <strong>{borrowing.User.FirstName} {borrowing.User.LastName}</strong>,",
                "Ekipman ödünç alma talebiniz alınmıştır. Admin onayı bekleniyor.",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Ekipman:</strong> {borrowing.Equipment.Name}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Seri No:</strong> {borrowing.Equipment.SerialNumber}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Beklenen İade:</strong> {borrowing.ExpectedReturnDate:dd.MM.yyyy}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Amaç:</strong> {borrowing.Purpose}</p>");

            await SendEmailFireAndForgetAsync(borrowing.User.Email, subject, htmlBody, "Equipment borrowing request");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendEquipmentBorrowingRequestAsync: {BorrowingId}", borrowingId);
        }
    }

    public async Task SendEquipmentBorrowingApprovalAsync(Guid borrowingId)
    {
        try
        {
            var borrowing = await _context.EquipmentBorrowings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == borrowingId);

            if (borrowing?.User?.Email == null) return;

            var subject = $"Ekipman Ödünç Alma Talebiniz Onaylandı - {borrowing.Equipment.Name}";
            var htmlBody = BuildEmailTemplate(
                "Talep Onaylandı",
                "#34a853",
                $"Sayın <strong>{borrowing.User.FirstName} {borrowing.User.LastName}</strong>,",
                "Ekipman ödünç alma talebiniz onaylanmıştır. Ekipmanı teslim alabilirsiniz.",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Ekipman:</strong> {borrowing.Equipment.Name}</p>
                   <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Konum:</strong> {borrowing.Equipment.Location}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>İade Tarihi:</strong> {borrowing.ExpectedReturnDate:dd.MM.yyyy}</p>");

            await SendEmailFireAndForgetAsync(borrowing.User.Email, subject, htmlBody, "Equipment borrowing approval");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendEquipmentBorrowingApprovalAsync: {BorrowingId}", borrowingId);
        }
    }

    public async Task SendEquipmentBorrowingRejectionAsync(Guid borrowingId, string? reason = null)
    {
        try
        {
            var borrowing = await _context.EquipmentBorrowings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == borrowingId);

            if (borrowing?.User?.Email == null) return;

            var reasonText = !string.IsNullOrEmpty(reason) 
                ? $"<p style='margin: 10px 0 0 0; color: #ea4335; font-size: 14px;'><strong>Red Nedeni:</strong> {reason}</p>" 
                : "";

            var subject = $"Ekipman Ödünç Alma Talebiniz Reddedildi - {borrowing.Equipment.Name}";
            var htmlBody = BuildEmailTemplate(
                "Talep Reddedildi",
                "#ea4335",
                $"Sayın <strong>{borrowing.User.FirstName} {borrowing.User.LastName}</strong>,",
                "Ekipman ödünç alma talebiniz reddedilmiştir." + reasonText,
                $@"<p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Ekipman:</strong> {borrowing.Equipment.Name}</p>");

            await SendEmailFireAndForgetAsync(borrowing.User.Email, subject, htmlBody, "Equipment borrowing rejection");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendEquipmentBorrowingRejectionAsync: {BorrowingId}", borrowingId);
        }
    }

    public async Task SendEquipmentReturnConfirmationAsync(Guid borrowingId)
    {
        try
        {
            var borrowing = await _context.EquipmentBorrowings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == borrowingId);

            if (borrowing?.User?.Email == null) return;

            var subject = $"Ekipman İadesi Tamamlandı - {borrowing.Equipment.Name}";
            var htmlBody = BuildEmailTemplate(
                "İade Tamamlandı",
                "#34a853",
                $"Sayın <strong>{borrowing.User.FirstName} {borrowing.User.LastName}</strong>,",
                "Ekipman iadeniz başarıyla tamamlanmıştır. Teşekkür ederiz.",
                $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Ekipman:</strong> {borrowing.Equipment.Name}</p>
                   <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>İade Tarihi:</strong> {borrowing.ActualReturnDate:dd.MM.yyyy}</p>");

            await SendEmailFireAndForgetAsync(borrowing.User.Email, subject, htmlBody, "Equipment return confirmation");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendEquipmentReturnConfirmationAsync: {BorrowingId}", borrowingId);
        }
    }

    // ========== Helper Methods ==========

    private string BuildEmailTemplate(string title, string color, string greeting, string message, string details)
    {
        return $@"
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
                        <td style='background-color: {color}; padding: 30px; text-align: center; border-radius: 8px 8px 0 0;'>
                            <h1 style='margin: 0; color: #ffffff; font-size: 24px; font-weight: 600;'>{title}</h1>
                        </td>
                    </tr>
                    <tr>
                        <td style='padding: 30px;'>
                            <p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>
                                {greeting}
                            </p>
                            <p style='margin: 0 0 20px 0; color: #202124; font-size: 16px; line-height: 1.5;'>
                                {message}
                            </p>
                            {(string.IsNullOrEmpty(details) ? "" : $"<div style='background-color: #f8f9fa; padding: 20px; border-radius: 4px; margin: 20px 0;'>{details}</div>")}
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
    }

    private Task SendEmailFireAndForgetAsync(string email, string subject, string body, string logContext)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                await _emailService.SendCustomEmailAsync(email, subject, body);
                _logger.LogInformation("{Context} notification sent to: {Email}", logContext, email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send {Context} notification to: {Email}", logContext, email);
            }
        });
        return Task.CompletedTask;
    }

    // ========== Part 4: In-App Notifications ==========

    public async Task CreateNotificationAsync(
        Guid userId,
        string title,
        string message,
        NotificationCategory category,
        NotificationType type = NotificationType.Info,
        Guid? relatedEntityId = null,
        string? relatedEntityType = null)
    {
        try
        {
            // Check user preferences
            var preferences = await GetUserPreferencesAsync(userId);
            var categoryPref = preferences.GetValueOrDefault(category, (email: true, push: true, sms: false));

            // Create in-app notification (always created, regardless of preferences)
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Category = category,
                Type = type,
                RelatedEntityId = relatedEntityId,
                RelatedEntityType = relatedEntityType
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send real-time notification via SignalR
            try
            {
                await _hubContext.Clients.Group($"user_{userId}")
                    .SendAsync("ReceiveNotification", new
                    {
                        notification.Id,
                        notification.Title,
                        notification.Message,
                        notification.Category,
                        notification.Type,
                        notification.IsRead,
                        notification.CreatedAt,
                        notification.RelatedEntityId,
                        notification.RelatedEntityType
                    });
            }
            catch (Exception hubEx)
            {
                // Log but don't fail if SignalR fails
                _logger.LogWarning(hubEx, "Failed to send real-time notification via SignalR for user {UserId}", userId);
            }

            _logger.LogInformation("In-app notification created for user {UserId}: {Title}", userId, title);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating notification for user {UserId}", userId);
            throw;
        }
    }

    public async Task<(List<Notification> notifications, int totalCount)> GetUserNotificationsAsync(
        Guid userId,
        int page = 1,
        int pageSize = 20,
        NotificationCategory? category = null,
        bool? isRead = null)
    {
        try
        {
            var query = _context.Notifications
                .Where(n => n.UserId == userId)
                .AsQueryable();

            if (category.HasValue)
                query = query.Where(n => n.Category == category.Value);

            if (isRead.HasValue)
                query = query.Where(n => n.IsRead == isRead.Value);

            var totalCount = await query.CountAsync();

            var notifications = await query
                .OrderByDescending(n => n.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (notifications, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting notifications for user {UserId}", userId);
            throw;
        }
    }

    public async Task MarkAsReadAsync(Guid notificationId, Guid userId)
    {
        try
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);

            if (notification == null)
                return;

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking notification {NotificationId} as read", notificationId);
            throw;
        }
    }

    public async Task<int> GetUnreadCountAsync(Guid userId)
    {
        try
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting unread count for user {UserId}", userId);
            throw;
        }
    }

    public async Task<Dictionary<NotificationCategory, (bool email, bool push, bool sms)>> GetUserPreferencesAsync(Guid userId)
    {
        try
        {
            var preferences = await _context.NotificationPreferences
                .Where(p => p.UserId == userId)
                .ToListAsync();

            var result = new Dictionary<NotificationCategory, (bool email, bool push, bool sms)>();

            // Get all categories
            foreach (var category in Enum.GetValues<NotificationCategory>())
            {
                var pref = preferences.FirstOrDefault(p => p.Category == category);
                result[category] = pref != null
                    ? (pref.EmailEnabled, pref.PushEnabled, pref.SmsEnabled)
                    : (true, true, false); // Defaults: email=true, push=true, sms=false
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting preferences for user {UserId}", userId);
            throw;
        }
    }

    public async Task UpdatePreferencesAsync(Guid userId, NotificationCategory category, bool emailEnabled, bool pushEnabled, bool smsEnabled)
    {
        try
        {
            var preference = await _context.NotificationPreferences
                .FirstOrDefaultAsync(p => p.UserId == userId && p.Category == category);

            if (preference == null)
            {
                preference = new NotificationPreferences
                {
                    UserId = userId,
                    Category = category,
                    EmailEnabled = emailEnabled,
                    PushEnabled = pushEnabled,
                    SmsEnabled = smsEnabled
                };
                _context.NotificationPreferences.Add(preference);
            }
            else
            {
                preference.EmailEnabled = emailEnabled;
                preference.PushEnabled = pushEnabled;
                preference.SmsEnabled = smsEnabled;
                preference.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating preferences for user {UserId}", userId);
            throw;
        }
    }
}
