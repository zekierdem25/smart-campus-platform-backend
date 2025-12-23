using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class NotificationPreferences
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public NotificationCategory Category { get; set; }

    public bool EmailEnabled { get; set; } = true;

    public bool PushEnabled { get; set; } = true;

    public bool SmsEnabled { get; set; } = false; // SMS is usually disabled by default

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}

