using Hangfire;
using SmartCampus.API.Services;

namespace SmartCampus.API.Extensions.BackgroundServices;

/// <summary>
/// Registration for recurring background jobs using Hangfire
/// </summary>
public static class BackgroundJobsRegistration
{
    /// <summary>
    /// Register all recurring background jobs
    /// </summary>
    public static void RegisterRecurringJobs()
    {
        // Daily absence warnings - Her sabah 08:00
        RecurringJob.AddOrUpdate(
            "daily-absence-warnings",
            () => ProcessDailyAbsenceWarnings(),
            "0 8 * * *", // Cron: Every day at 08:00
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });

        // Event reminders - 1 day before - Her gün 09:00'de kontrol et
        RecurringJob.AddOrUpdate(
            "event-reminders-1day",
            () => ProcessEventReminders(1),
            "0 9 * * *", // Cron: Every day at 09:00
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });

        // Event reminders - 1 hour before - Her saat başı
        RecurringJob.AddOrUpdate(
            "event-reminders-1hour",
            () => ProcessEventReminders(0.04), // ~1 hour
            "0 * * * *", // Cron: Every hour
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });

        // Event reminders - every hour (from WaitlistProcessingService)
        RecurringJob.AddOrUpdate<IEventReminderService>(
            "event-reminders",
            service => service.ProcessEventRemindersAsync(),
            Cron.Hourly);

        // Meal reservation reminders - 1 hour before - Her saat başı
        RecurringJob.AddOrUpdate(
            "meal-reservation-reminders",
            () => ProcessMealReservationReminders(),
            "0 * * * *", // Cron: Every hour
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });

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

        // Analytics data aggregation - Daily at 01:00
        RecurringJob.AddOrUpdate(
            "analytics-aggregation",
            () => ProcessAnalyticsAggregation(),
            "0 1 * * *", // Cron: Every day at 01:00
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });

        // Log cleanup - Weekly on Sunday at 03:00
        RecurringJob.AddOrUpdate(
            "log-cleanup",
            () => ProcessLogCleanup(),
            "0 3 * * 0", // Cron: Every Sunday at 03:00
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });
    }

    /// <summary>
    /// Process daily absence warnings (called by Hangfire)
    /// Note: This is a placeholder - actual implementation should use services
    /// </summary>
    [AutomaticRetry(Attempts = 3)]
    public static void ProcessDailyAbsenceWarnings()
    {
        // This will be handled by AttendanceWarningJob BackgroundService
        // Hangfire job is registered but actual processing is done by the service
        // This can be used for additional processing if needed
    }

    /// <summary>
    /// Process event reminders
    /// </summary>
    [AutomaticRetry(Attempts = 3)]
    public static void ProcessEventReminders(double daysBefore)
    {
        // This will use IEventReminderService
        // Implementation should be in EventReminderService
    }

    /// <summary>
    /// Process meal reservation reminders
    /// </summary>
    [AutomaticRetry(Attempts = 3)]
    public static void ProcessMealReservationReminders()
    {
        // Implementation: Send reminders 1 hour before meal time
        // This can be added to NotificationService or create a new service
    }

    /// <summary>
    /// Process analytics data aggregation
    /// </summary>
    [AutomaticRetry(Attempts = 3)]
    public static void ProcessAnalyticsAggregation()
    {
        // Implementation: Aggregate daily analytics data for faster queries
        // This can pre-calculate dashboard metrics
    }

    /// <summary>
    /// Process log cleanup
    /// </summary>
    [AutomaticRetry(Attempts = 3)]
    public static void ProcessLogCleanup()
    {
        // Implementation: Clean up old activity logs (keep last 90 days)
        // This can be added to ActivityLogService
    }
}

