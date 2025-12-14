using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class AttendanceRecord
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SessionId { get; set; }

    [Required]
    public Guid StudentId { get; set; }

    public DateTime CheckInTime { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(10,7)")]
    public decimal Latitude { get; set; }

    [Column(TypeName = "decimal(10,7)")]
    public decimal Longitude { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal DistanceFromCenter { get; set; } // meters

    [Column(TypeName = "decimal(10,2)")]
    public decimal Accuracy { get; set; } // GPS accuracy in meters

    public bool IsFlagged { get; set; } = false;

    [StringLength(100)]
    public string? FlagReason { get; set; } // "GPS_SPOOFING", "IP_MISMATCH", "VELOCITY_IMPOSSIBLE"

    [StringLength(45)]
    public string? IpAddress { get; set; }

    [StringLength(500)]
    public string? UserAgent { get; set; }

    public bool IsQrVerified { get; set; } = false; // If verified via QR code

    // Sensor data (Accelerometer) for spoofing detection
    [Column(TypeName = "decimal(10,2)")]
    public decimal? SensorAccelerationX { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? SensorAccelerationY { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? SensorAccelerationZ { get; set; }

    public bool SensorDataUnavailable { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("SessionId")]
    public AttendanceSession Session { get; set; } = null!;

    [ForeignKey("StudentId")]
    public Student Student { get; set; } = null!;
}
