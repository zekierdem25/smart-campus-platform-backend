using System.ComponentModel.DataAnnotations;

namespace SmartCampus.API.Models;

/// <summary>
/// Represents a pending payment waiting for confirmation
/// </summary>
public class PendingPayment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Payment session ID from the payment provider
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string SessionId { get; set; } = string.Empty;

    /// <summary>
    /// User who initiated the payment
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Payment amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Payment currency (default: TRY)
    /// </summary>
    [MaxLength(3)]
    public string Currency { get; set; } = "TRY";

    /// <summary>
    /// Payment status: pending, completed, failed, expired
    /// </summary>
    [MaxLength(20)]
    public string Status { get; set; } = "pending";

    /// <summary>
    /// Payment provider: Stripe, PayTR, Simulation
    /// </summary>
    [MaxLength(50)]
    public string Provider { get; set; } = "Simulation";

    /// <summary>
    /// When the payment was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the payment expires
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// When the payment was completed
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
}
