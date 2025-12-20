using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum MealType
{
    Lunch,
    Dinner
}

public class MealMenu
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CafeteriaId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public MealType MealType { get; set; }

    [Required]
    [Column(TypeName = "TEXT")]
    public string ItemsJson { get; set; } = "[]"; // JSON array of menu items

    [Column(TypeName = "TEXT")]
    public string? NutritionJson { get; set; } // JSON: {calories, protein, carbs, fat}

    public int CalorieCount { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; } = 0.00m;

    public bool IsPublished { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("CafeteriaId")]
    public Cafeteria Cafeteria { get; set; } = null!;

    public ICollection<MealReservation> MealReservations { get; set; } = new List<MealReservation>();
}
