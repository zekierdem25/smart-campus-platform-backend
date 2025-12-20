using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class Cafeteria
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Location { get; set; }

    [Range(1, 10000)]
    public int Capacity { get; set; } = 100;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<MealMenu> MealMenus { get; set; } = new List<MealMenu>();
    public ICollection<MealReservation> MealReservations { get; set; } = new List<MealReservation>();
}
