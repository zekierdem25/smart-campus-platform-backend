using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum TransactionType
{
    Credit,
    Debit
}

public class Transaction
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid WalletId { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    [Range(0, 999999)]
    public decimal Amount { get; set; }

    [Required]
    [Range(0, 999999)]
    public decimal BalanceAfter { get; set; }

    [StringLength(50)]
    public string? ReferenceType { get; set; } // "MealReservation", "Topup", "Refund"

    public Guid? ReferenceId { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("WalletId")]
    public Wallet Wallet { get; set; } = null!;
}
