using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;

namespace SmartCampus.API.Extensions.BackgroundServices;

public class AttendanceWarningJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AttendanceWarningJob> _logger;
    private const int WarningThreshold = 15; // %15 devamsızlık = Uyarı (Katılım < %85)
    private const int FailureThreshold = 30; // %30 devamsızlık = Kaldı (Katılım < %70)

    public AttendanceWarningJob(IServiceProvider serviceProvider, ILogger<AttendanceWarningJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("AttendanceWarningJob başlatıldı. Her gece 02:00'de çalışacak.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var now = DateTime.Now;
                var nextRun = now.Date.AddDays(1).AddHours(2); // Yarın 02:00
                
                // Eğer şu an 02:00'den önceyse, bugün 02:00'de çalıştır
                if (now.Hour < 2)
                {
                    nextRun = now.Date.AddHours(2);
                }

                var delay = nextRun - now;
                _logger.LogInformation("Bir sonraki çalışma zamanı: {NextRun} ({Delay} sonra)", nextRun, delay);

                await Task.Delay(delay, stoppingToken);

                if (!stoppingToken.IsCancellationRequested)
                {
                    await ProcessAttendanceWarningsAsync();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("AttendanceWarningJob durduruldu.");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AttendanceWarningJob çalışırken hata oluştu.");
                // Hata olsa bile servis çalışmaya devam etsin
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }

    private async Task ProcessAttendanceWarningsAsync()
    {
        _logger.LogInformation("Devamsızlık uyarı kontrolü başlatılıyor...");

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        // Aktif enrollments'ları al (sadece Active status)
        var enrollments = await context.Enrollments
            .Include(e => e.Student)
                .ThenInclude(s => s.User)
            .Include(e => e.Section)
                .ThenInclude(s => s.Course)
            .Include(e => e.Section)
                .ThenInclude(s => s.AttendanceSessions)
                    .ThenInclude(a => a.Records)
            .Where(e => e.Status == EnrollmentStatus.Active)
            .ToListAsync();

        _logger.LogInformation("{Count} aktif enrollment bulundu.", enrollments.Count);

        // Onaylanmış mazeret taleplerini al
        var approvedExcuses = await context.ExcuseRequests
            .Where(er => er.Status == ExcuseRequestStatus.Approved)
            .ToListAsync();

        int warningEmailsSent = 0;
        int failureEmailsSent = 0;

        foreach (var enrollment in enrollments)
        {
            try
            {
                // Kapalı session'ları al
                var closedSessions = enrollment.Section.AttendanceSessions
                    .Where(s => s.Status == AttendanceSessionStatus.Closed)
                    .ToList();

                if (closedSessions.Count == 0)
                {
                    continue; // Henüz kapalı session yoksa atla
                }

                // Öğrencinin katıldığı session'ları say
                var attendedSessions = closedSessions
                    .Count(s => s.Records.Any(r => r.StudentId == enrollment.StudentId));

                // Öğrencinin mazeretli olduğu session'ları say
                var excusedSessions = closedSessions
                    .Count(s => approvedExcuses.Any(er => 
                        er.StudentId == enrollment.StudentId && 
                        er.SessionId == s.Id));

                // Toplam katılım (katılan + mazeretli)
                var totalAttended = attendedSessions + excusedSessions;
                var totalSessions = closedSessions.Count;

                // Katılım yüzdesi
                var attendancePercentage = totalSessions > 0
                    ? Math.Round((decimal)totalAttended / totalSessions * 100, 1)
                    : 100;

                // Devamsızlık yüzdesi
                var absencePercentage = 100 - attendancePercentage;

                var courseCode = enrollment.Section.Course.Code;
                var studentName = $"{enrollment.Student.User.FirstName} {enrollment.Student.User.LastName}";
                var studentEmail = enrollment.Student.User.Email;

                // %30 devamsızlık kontrolü (Katılım < %70)
                if (absencePercentage >= FailureThreshold && !enrollment.FailureEmailSent)
                {
                    _logger.LogWarning(
                        "Öğrenci {StudentName} ({StudentNumber}) {CourseCode} dersinden %{AbsencePercentage} devamsızlık ile kaldı.",
                        studentName,
                        enrollment.Student.StudentNumber,
                        courseCode,
                        absencePercentage);

                    await emailService.SendAttendanceFailureAsync(studentEmail, studentName, courseCode);
                    
                    enrollment.FailureEmailSent = true;
                    enrollment.UpdatedAt = DateTime.UtcNow;
                    failureEmailsSent++;
                }
                // %15 devamsızlık kontrolü (Katılım < %85)
                else if (absencePercentage >= WarningThreshold && !enrollment.WarningEmailSent)
                {
                    _logger.LogWarning(
                        "Öğrenci {StudentName} ({StudentNumber}) {CourseCode} dersinden %{AbsencePercentage} devamsızlık ile uyarı aldı.",
                        studentName,
                        enrollment.Student.StudentNumber,
                        courseCode,
                        absencePercentage);

                    await emailService.SendAttendanceWarningAsync(studentEmail, studentName, courseCode);
                    
                    enrollment.WarningEmailSent = true;
                    enrollment.UpdatedAt = DateTime.UtcNow;
                    warningEmailsSent++;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Enrollment {EnrollmentId} için devamsızlık kontrolü sırasında hata oluştu.",
                    enrollment.Id);
            }
        }

        // Değişiklikleri kaydet
        if (warningEmailsSent > 0 || failureEmailsSent > 0)
        {
            await context.SaveChangesAsync();
            _logger.LogInformation(
                "Devamsızlık kontrolü tamamlandı. {WarningCount} uyarı emaili, {FailureCount} kaldı emaili gönderildi.",
                warningEmailsSent,
                failureEmailsSent);
        }
        else
        {
            _logger.LogInformation("Devamsızlık kontrolü tamamlandı. Yeni uyarı veya kaldı durumu bulunamadı.");
        }
    }
}

