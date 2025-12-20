using System.ComponentModel.DataAnnotations;

namespace SmartCampus.API.Models;

/// <summary>
/// Type of equipment available for borrowing
/// </summary>
public enum EquipmentType
{
    Laptop,
    Projector,
    Camera,
    Microphone,
    Tablet,
    Tripod,
    SoundSystem,
    Other
}

/// <summary>
/// Current status of equipment
/// </summary>
public enum EquipmentStatus
{
    /// <summary>
    /// Equipment is available for borrowing
    /// </summary>
    Available,
    
    /// <summary>
    /// Equipment is currently borrowed
    /// </summary>
    Borrowed,
    
    /// <summary>
    /// Equipment is under maintenance
    /// </summary>
    Maintenance,
    
    /// <summary>
    /// Equipment has been retired from service
    /// </summary>
    Retired
}

/// <summary>
/// Represents equipment that can be borrowed from the campus
/// </summary>
public class Equipment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Name of the equipment
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Type/category of equipment
    /// </summary>
    public EquipmentType Type { get; set; }

    /// <summary>
    /// Unique serial number or asset tag
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Current status of the equipment
    /// </summary>
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

    /// <summary>
    /// Physical location (building/room)
    /// </summary>
    [MaxLength(200)]
    public string? Location { get; set; }

    /// <summary>
    /// Description or additional details
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Brand/manufacturer of the equipment
    /// </summary>
    [MaxLength(100)]
    public string? Brand { get; set; }

    /// <summary>
    /// Model number
    /// </summary>
    [MaxLength(100)]
    public string? Model { get; set; }

    /// <summary>
    /// Whether the equipment is active in the system
    /// </summary>
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<EquipmentBorrowing> Borrowings { get; set; } = new List<EquipmentBorrowing>();
}
