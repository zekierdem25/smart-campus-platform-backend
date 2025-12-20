using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum EventCategory
{
    Conference,
    Workshop,
    Social,
    Sports,
    Academic
}

public enum EventStatus
{
    Draft,
    Published,
    Ongoing,
    Completed,
    Cancelled
}

public class Event
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    [Required]
    public EventCategory Category { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    [StringLength(500)]
    public string? Location { get; set; }

    [Range(1, 10000)]
    public int Capacity { get; set; } = 100;

    public int RegisteredCount { get; set; } = 0;

    [Required]
    public DateTime RegistrationDeadline { get; set; }

    public bool IsPaid { get; set; } = false;

    [Range(0, 10000)]
    public decimal? Price { get; set; }

    [Required]
    public EventStatus Status { get; set; } = EventStatus.Draft;

    [Required]
    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("CreatedBy")]
    public User Creator { get; set; } = null!;

    public ICollection<EventRegistration> EventRegistrations { get; set; } = new List<EventRegistration>();
}
