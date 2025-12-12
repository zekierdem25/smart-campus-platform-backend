using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum AttendanceSessionStatus
{
    Active,
    Closed
}

public class AttendanceSession
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SectionId { get; set; }

    [Required]
    public Guid InstructorId { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow.Date;

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    [Column(TypeName = "decimal(10,7)")]
    public decimal Latitude { get; set; }

    [Column(TypeName = "decimal(10,7)")]
    public decimal Longitude { get; set; }

    [Range(5, 100)]
    public int GeofenceRadius { get; set; } = 15; // meters

    [StringLength(500)]
    public string? QrCode { get; set; } // JWT token for QR verification

    public DateTime? QrCodeExpiresAt { get; set; }

    public AttendanceSessionStatus Status { get; set; } = AttendanceSessionStatus.Active;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("SectionId")]
    public CourseSection Section { get; set; } = null!;

    [ForeignKey("InstructorId")]
    public Faculty Instructor { get; set; } = null!;

    public ICollection<AttendanceRecord> Records { get; set; } = new List<AttendanceRecord>();
    
    public ICollection<ExcuseRequest> ExcuseRequests { get; set; } = new List<ExcuseRequest>();
}
