using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum ExcuseRequestStatus
{
    Pending,
    Approved,
    Rejected
}

public class ExcuseRequest
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public Guid SessionId { get; set; }

    [Required]
    [StringLength(1000)]
    public string Reason { get; set; } = string.Empty;

    [StringLength(500)]
    public string? DocumentUrl { get; set; } // Uploaded document

    public ExcuseRequestStatus Status { get; set; } = ExcuseRequestStatus.Pending;

    public Guid? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; } // Reviewer notes

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("StudentId")]
    public Student Student { get; set; } = null!;

    [ForeignKey("SessionId")]
    public AttendanceSession Session { get; set; } = null!;

    [ForeignKey("ReviewedBy")]
    public Faculty? Reviewer { get; set; }
}
