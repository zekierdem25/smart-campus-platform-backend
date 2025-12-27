using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

/// <summary>
/// Type of IoT sensor
/// </summary>
public enum SensorType
{
    Temperature,    // Sıcaklık sensörü
    Humidity,       // Nem sensörü
    Occupancy,      // Doluluk sensörü
    Energy,         // Enerji tüketim sensörü
    AirQuality,     // Hava kalitesi sensörü
    Light,          // Işık sensörü
    Motion,         // Hareket sensörü
    Other
}

/// <summary>
/// Status of the sensor
/// </summary>
public enum SensorStatus
{
    Active,         // Aktif ve çalışıyor
    Warning,        // Uyarı durumu (eşik değer aşıldı)
    Critical,       // Kritik durum
    Offline,        // Çevrimdışı
    Maintenance    // Bakımda
}

/// <summary>
/// Represents an IoT sensor in the smart campus
/// </summary>
public class Sensor
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Unique sensor identifier (e.g., "TEMP-001", "OCCUPANCY-A-01")
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string SensorId { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable name of the sensor
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Type of sensor
    /// </summary>
    public SensorType Type { get; set; }

    /// <summary>
    /// Physical location (building/room)
    /// </summary>
    [MaxLength(200)]
    public string? Location { get; set; }

    /// <summary>
    /// Current status of the sensor
    /// </summary>
    public SensorStatus Status { get; set; } = SensorStatus.Active;

    /// <summary>
    /// Unit of measurement (e.g., "°C", "%", "kW", "people")
    /// </summary>
    [MaxLength(20)]
    public string Unit { get; set; } = string.Empty;

    /// <summary>
    /// Minimum threshold value for warnings
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? MinThreshold { get; set; }

    /// <summary>
    /// Maximum threshold value for warnings
    /// </summary>
    [Column(TypeName = "decimal(10,2)")]
    public decimal? MaxThreshold { get; set; }

    /// <summary>
    /// Whether the sensor is active in the system
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Description or additional details
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<SensorData> SensorData { get; set; } = new List<SensorData>();
}

