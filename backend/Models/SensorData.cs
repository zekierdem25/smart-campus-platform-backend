using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

/// <summary>
/// Represents time-series data from IoT sensors
/// </summary>
public class SensorData
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Reference to the sensor that generated this data
    /// </summary>
    [Required]
    public Guid SensorId { get; set; }

    /// <summary>
    /// Timestamp when the data was recorded
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The sensor reading value
    /// </summary>
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Value { get; set; }

    /// <summary>
    /// Unit of measurement (e.g., "°C", "%", "kW")
    /// </summary>
    [MaxLength(20)]
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Whether this reading triggered an anomaly/alert
    /// </summary>
    public bool IsAnomaly { get; set; } = false;

    /// <summary>
    /// Anomaly reason if IsAnomaly is true
    /// </summary>
    [MaxLength(500)]
    public string? AnomalyReason { get; set; }

    /// <summary>
    /// Metadata as JSON (optional additional data)
    /// </summary>
    [Column(TypeName = "text")]
    public string? MetadataJson { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("SensorId")]
    public virtual Sensor Sensor { get; set; } = null!;
}

