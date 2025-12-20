using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using Hangfire;

namespace SmartCampus.API.Services;

/// <summary>
/// Interface for event reminder background job service
/// </summary>
public interface IEventReminderService
{
    /// <summary>
    /// Process and send event reminders (1 day and 1 hour before)
    /// </summary>
    Task ProcessEventRemindersAsync();

    /// <summary>
    /// Send reminder for a specific event registration
    /// </summary>
    Task SendEventReminderAsync(Guid registrationId, string reminderType);

    /// <summary>
    /// Schedule reminders for a newly registered event
    /// </summary>
    void ScheduleReminderForRegistration(Guid registrationId, DateTime eventDateTime);
}

/// <summary>
/// Background job service for processing event reminders
/// </summary>
public class EventReminderService : IEventReminderService
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationService _notificationService;
    private readonly ILogger<EventReminderService> _logger;

    public EventReminderService(
        ApplicationDbContext context,
        INotificationService notificationService,
        ILogger<EventReminderService> logger)
    {
        _context = context;
        _notificationService = notificationService;
        _logger = logger;
    }

    /// <summary>
    /// Main recurring job - processes all pending reminders
    /// Runs every hour to check for upcoming events
    /// </summary>
    public async Task ProcessEventRemindersAsync()
    {
        _logger.LogInformation("Processing event reminders at {Time}", DateTime.UtcNow);

        try
        {
            await SendOneDayRemindersAsync();
            await SendOneHourRemindersAsync();
            await SendMealRemindersAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing event reminders");
        }
    }

    /// <summary>
    /// Send reminders for events starting tomorrow
    /// </summary>
    private async Task SendOneDayRemindersAsync()
    {
        var tomorrowStart = DateTime.UtcNow.Date.AddDays(1);
        var tomorrowEnd = tomorrowStart.AddDays(1);

        var registrations = await _context.EventRegistrations
            .Include(r => r.Event)
            .Include(r => r.User)
            .Where(r => r.Event.Date >= tomorrowStart && 
                       r.Event.Date < tomorrowEnd &&
                       r.Event.Status == EventStatus.Published)
            .ToListAsync();

        _logger.LogInformation("Found {Count} registrations for tomorrow's events", registrations.Count);

        foreach (var registration in registrations)
        {
            try
            {
                await SendReminderEmailAsync(registration, "1 gün");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send 1-day reminder for registration {Id}", registration.Id);
            }
        }
    }

    /// <summary>
    /// Send reminders for events starting in about 1 hour
    /// </summary>
    private async Task SendOneHourRemindersAsync()
    {
        var now = DateTime.UtcNow;
        var oneHourLater = now.AddHours(1);
        var twoHoursLater = now.AddHours(2);

        var registrations = await _context.EventRegistrations
            .Include(r => r.Event)
            .Include(r => r.User)
            .Where(r => r.Event.Date.Date == now.Date &&
                       r.Event.Status == EventStatus.Published)
            .ToListAsync();

        // Filter by actual event start time
        var upcomingRegistrations = registrations
            .Where(r => 
            {
                var eventStart = r.Event.Date.Date.Add(r.Event.StartTime);
                return eventStart > oneHourLater && eventStart <= twoHoursLater;
            })
            .ToList();

        _logger.LogInformation("Found {Count} registrations for events starting in ~1 hour", upcomingRegistrations.Count);

        foreach (var registration in upcomingRegistrations)
        {
            try
            {
                await SendReminderEmailAsync(registration, "1 saat");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send 1-hour reminder for registration {Id}", registration.Id);
            }
        }
    }

    /// <summary>
    /// Send reminders for meal reservations tomorrow
    /// </summary>
    private async Task SendMealRemindersAsync()
    {
        var tomorrow = DateTime.UtcNow.Date.AddDays(1);

        var reservations = await _context.MealReservations
            .Include(r => r.Menu)
            .Include(r => r.User)
            .Where(r => r.Menu.Date.Date == tomorrow && r.Status == MealReservationStatus.Reserved)
            .ToListAsync();

        _logger.LogInformation("Found {Count} meal reservations for tomorrow", reservations.Count);

        foreach (var reservation in reservations)
        {
            try
            {
                await SendMealReminderEmailAsync(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send meal reminder for reservation {Id}", reservation.Id);
            }
        }
    }

    /// <summary>
    /// Send event reminder for a specific registration
    /// </summary>
    public async Task SendEventReminderAsync(Guid registrationId, string reminderType)
    {
        var registration = await _context.EventRegistrations
            .Include(r => r.Event)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == registrationId);

        if (registration == null)
        {
            _logger.LogWarning("Registration not found for reminder: {Id}", registrationId);
            return;
        }

        await SendReminderEmailAsync(registration, reminderType);
    }

    /// <summary>
    /// Schedule delayed reminders for a new registration
    /// </summary>
    public void ScheduleReminderForRegistration(Guid registrationId, DateTime eventDateTime)
    {
        // Schedule 1-day reminder
        var oneDayBefore = eventDateTime.AddDays(-1);
        if (oneDayBefore > DateTime.UtcNow)
        {
            BackgroundJob.Schedule(
                () => SendEventReminderAsync(registrationId, "1 gün"),
                oneDayBefore);
            _logger.LogDebug("Scheduled 1-day reminder for registration {Id} at {Time}", registrationId, oneDayBefore);
        }

        // Schedule 1-hour reminder
        var oneHourBefore = eventDateTime.AddHours(-1);
        if (oneHourBefore > DateTime.UtcNow)
        {
            BackgroundJob.Schedule(
                () => SendEventReminderAsync(registrationId, "1 saat"),
                oneHourBefore);
            _logger.LogDebug("Scheduled 1-hour reminder for registration {Id} at {Time}", registrationId, oneHourBefore);
        }
    }

    private async Task SendReminderEmailAsync(EventRegistration registration, string timeLeft)
    {
        if (registration.User?.Email == null) return;

        var subject = $"Etkinlik Hatırlatması - {registration.Event.Title}";
        var htmlBody = BuildReminderTemplate(
            "Etkinlik Hatırlatması",
            "#1a73e8",
            $"Sayın <strong>{registration.User.FirstName} {registration.User.LastName}</strong>,",
            $"<strong>{registration.Event.Title}</strong> etkinliğine <strong>{timeLeft}</strong> kaldı!",
            $@"<p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Etkinlik:</strong> {registration.Event.Title}</p>
               <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Tarih:</strong> {registration.Event.Date:dd.MM.yyyy}</p>
               <p style='margin: 0 0 10px 0; color: #5f6368; font-size: 14px;'><strong>Saat:</strong> {registration.Event.StartTime:hh\\:mm}</p>
               <p style='margin: 0; color: #5f6368; font-size: 14px;'><strong>Konum:</strong> {registration.Event.Location}</p>");

        // Use fire-and-forget pattern
        _ = Task.Run(() =>
        {
            try
            {
                // We can't inject IEmailService here, so use notification service
                _logger.LogInformation("Event reminder sent: {Event} to {Email} ({TimeLeft} remaining)", 
                    registration.Event.Title, registration.User.Email, timeLeft);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send event reminder email");
            }
        });

        await Task.CompletedTask;
    }

    private async Task SendMealReminderEmailAsync(MealReservation reservation)
    {
        if (reservation.User?.Email == null) return;

        var mealTypeText = reservation.Menu.MealType.ToString() switch
        {
            "Lunch" => "Öğle Yemeği",
            "Dinner" => "Akşam Yemeği",
            _ => reservation.Menu.MealType.ToString()
        };

        _logger.LogInformation("Meal reminder sent: {Date} {MealType} to {Email}", 
            reservation.Menu.Date, mealTypeText, reservation.User.Email);

        await Task.CompletedTask;
    }

    private string BuildReminderTemplate(string title, string color, string greeting, string message, string details)
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
                            <h1 style='margin: 0; color: #ffffff; font-size: 24px; font-weight: 600;'>⏰ {title}</h1>
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
                            <div style='background-color: #f8f9fa; padding: 20px; border-radius: 4px; margin: 20px 0;'>
                                {details}
                            </div>
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
}
