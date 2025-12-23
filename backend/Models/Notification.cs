using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum NotificationCategory
{
    Academic,
    Attendance,
    Meal,
    Event,
    Payment,
    System
}

public enum NotificationType
{
    Info,
    Warning,
    Error,
    Success
}

public class Notification
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required]
    public NotificationCategory Category { get; set; }

    [Required]
    public NotificationType Type { get; set; } = NotificationType.Info;

    public bool IsRead { get; set; } = false;

    public DateTime? ReadAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Optional: Related entity reference
    public Guid? RelatedEntityId { get; set; }

    [StringLength(50)]
    public string? RelatedEntityType { get; set; } // e.g., "Enrollment", "AttendanceSession", "Event"

    // Navigation properties
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}

