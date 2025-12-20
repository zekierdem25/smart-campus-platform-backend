using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using Hangfire;

namespace SmartCampus.API.Services;

/// <summary>
/// Interface for waitlist processing background jobs
/// </summary>
public interface IWaitlistProcessingService
{
    /// <summary>
    /// Process expired waitlist entries
    /// </summary>
    Task ProcessExpiredWaitlistEntriesAsync();

    /// <summary>
    /// Notify users about to expire on the waitlist
    /// </summary>
    Task NotifyWaitlistExpiringAsync();

    /// <summary>
    /// Process equipment overdue notifications
    /// </summary>
    Task ProcessOverdueEquipmentAsync();
}

/// <summary>
/// Background job service for processing waitlist and equipment overdue
/// </summary>
public class WaitlistProcessingService : IWaitlistProcessingService
{
    private readonly ApplicationDbContext _context;
    private readonly INotificationService _notificationService;
    private readonly ILogger<WaitlistProcessingService> _logger;

    // Waitlist entry expires after 24 hours of being notified
    private const int WaitlistExpirationHours = 24;

    public WaitlistProcessingService(
        ApplicationDbContext context,
        INotificationService notificationService,
        ILogger<WaitlistProcessingService> logger)
    {
        _context = context;
        _notificationService = notificationService;
        _logger = logger;
    }

    /// <summary>
    /// Process expired waitlist entries - runs daily
    /// Marks entries as expired if they were notified but didn't respond
    /// </summary>
    public async Task ProcessExpiredWaitlistEntriesAsync()
    {
        _logger.LogInformation("Processing expired waitlist entries at {Time}", DateTime.UtcNow);

        try
        {
            var expirationTime = DateTime.UtcNow.AddHours(-WaitlistExpirationHours);

            var expiredEntries = await _context.EventWaitlists
                .Where(w => w.Status == WaitlistStatus.Notified && 
                           w.NotifiedAt.HasValue && 
                           w.NotifiedAt < expirationTime)
                .ToListAsync();

            _logger.LogInformation("Found {Count} expired waitlist entries", expiredEntries.Count);

            foreach (var entry in expiredEntries)
            {
                entry.Status = WaitlistStatus.Expired;
                entry.UpdatedAt = DateTime.UtcNow;
            }

            if (expiredEntries.Any())
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Marked {Count} waitlist entries as expired", expiredEntries.Count);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing expired waitlist entries");
        }
    }

    /// <summary>
    /// Notify users whose waitlist entries are about to expire (4 hours warning)
    /// </summary>
    public async Task NotifyWaitlistExpiringAsync()
    {
        _logger.LogInformation("Checking for expiring waitlist entries at {Time}", DateTime.UtcNow);

        try
        {
            var warningThreshold = DateTime.UtcNow.AddHours(-(WaitlistExpirationHours - 4)); // 4 hours before expiration

            var expiringEntries = await _context.EventWaitlists
                .Include(w => w.User)
                .Include(w => w.Event)
                .Where(w => w.Status == WaitlistStatus.Notified && 
                           w.NotifiedAt.HasValue && 
                           w.NotifiedAt >= warningThreshold &&
                           w.NotifiedAt < warningThreshold.AddHours(1)) // Within 1-hour window
                .ToListAsync();

            _logger.LogInformation("Found {Count} expiring waitlist entries to warn", expiringEntries.Count);

            foreach (var entry in expiringEntries)
            {
                // Log warning notification (email implementation would go here)
                _logger.LogInformation(
                    "Waitlist expiring soon: User {UserId} for Event {EventId}",
                    entry.UserId, entry.EventId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error notifying expiring waitlist entries");
        }
    }

    /// <summary>
    /// Process overdue equipment - mark as overdue and notify
    /// </summary>
    public async Task ProcessOverdueEquipmentAsync()
    {
        _logger.LogInformation("Processing overdue equipment at {Time}", DateTime.UtcNow);

        try
        {
            var now = DateTime.UtcNow;

            // Find borrowed equipment that is now overdue
            var overdueBorrowings = await _context.EquipmentBorrowings
                .Include(b => b.Equipment)
                .Include(b => b.User)
                .Where(b => b.Status == BorrowingStatus.Borrowed && 
                           b.ExpectedReturnDate < now)
                .ToListAsync();

            _logger.LogInformation("Found {Count} overdue equipment borrowings", overdueBorrowings.Count);

            foreach (var borrowing in overdueBorrowings)
            {
                borrowing.Status = BorrowingStatus.Overdue;
                borrowing.UpdatedAt = DateTime.UtcNow;

                _logger.LogWarning(
                    "Equipment marked as overdue: {EquipmentName} borrowed by {UserEmail}",
                    borrowing.Equipment.Name, borrowing.User.Email);
            }

            if (overdueBorrowings.Any())
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated {Count} equipment borrowings to overdue status", overdueBorrowings.Count);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing overdue equipment");
        }
    }
}

/// <summary>
/// Static class for registering recurring background jobs
/// </summary>
public static class BackgroundJobsRegistration
{
    /// <summary>
    /// Register all recurring background jobs
    /// </summary>
    public static void RegisterRecurringJobs()
    {
        // Event reminders - every hour
        RecurringJob.AddOrUpdate<IEventReminderService>(
            "event-reminders",
            service => service.ProcessEventRemindersAsync(),
            Cron.Hourly);

        // Expired waitlist processing - every 2 hours
        RecurringJob.AddOrUpdate<IWaitlistProcessingService>(
            "waitlist-expiration",
            service => service.ProcessExpiredWaitlistEntriesAsync(),
            "0 */2 * * *"); // Every 2 hours

        // Waitlist expiring warnings - every hour
        RecurringJob.AddOrUpdate<IWaitlistProcessingService>(
            "waitlist-expiring-warning",
            service => service.NotifyWaitlistExpiringAsync(),
            Cron.Hourly);

        // Overdue equipment processing - twice daily (8am and 8pm)
        RecurringJob.AddOrUpdate<IWaitlistProcessingService>(
            "overdue-equipment",
            service => service.ProcessOverdueEquipmentAsync(),
            "0 8,20 * * *"); // At 08:00 and 20:00
    }
}
