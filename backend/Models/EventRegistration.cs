using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class EventRegistration
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid EventId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(500)]
    public string QrCode { get; set; } = string.Empty;

    public bool CheckedIn { get; set; } = false;

    public DateTime? CheckedInAt { get; set; }

    [Column(TypeName = "TEXT")]
    public string? CustomFieldsJson { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("EventId")]
    public Event Event { get; set; } = null!;

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}
