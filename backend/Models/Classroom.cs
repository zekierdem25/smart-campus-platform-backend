using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class Classroom
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    public string Building { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string RoomNumber { get; set; } = string.Empty;

    public int Capacity { get; set; } = 30;

    [Column(TypeName = "decimal(10,7)")]
    public decimal Latitude { get; set; }

    [Column(TypeName = "decimal(10,7)")]
    public decimal Longitude { get; set; }

    [StringLength(1000)]
    public string? FeaturesJson { get; set; } // ["projector", "whiteboard", "ac"]

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<CourseSection> Sections { get; set; } = new List<CourseSection>();
}
