using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum MealReservationStatus
{
    Reserved,
    Used,
    Cancelled
}

public class MealReservation
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid MenuId { get; set; }

    [Required]
    public Guid CafeteriaId { get; set; }

    [Required]
    public MealType MealType { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Range(0, 10000)]
    public decimal Amount { get; set; } = 0;

    [Required]
    [StringLength(500)]
    public string QrCode { get; set; } = string.Empty;

    [Required]
    public MealReservationStatus Status { get; set; } = MealReservationStatus.Reserved;

    public DateTime? UsedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [ForeignKey("MenuId")]
    public MealMenu Menu { get; set; } = null!;

    [ForeignKey("CafeteriaId")]
    public Cafeteria Cafeteria { get; set; } = null!;
}
