using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum ReservationStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled
}

public class ClassroomReservation
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid ClassroomId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    [StringLength(500)]
    public string? Purpose { get; set; }

    [Required]
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    public Guid? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("ClassroomId")]
    public Classroom Classroom { get; set; } = null!;

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [ForeignKey("ApprovedBy")]
    public User? Approver { get; set; }
}
