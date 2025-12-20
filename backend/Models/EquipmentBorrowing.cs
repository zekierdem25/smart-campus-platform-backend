using System.ComponentModel.DataAnnotations;

namespace SmartCampus.API.Models;

/// <summary>
/// Status of an equipment borrowing request
/// </summary>
public enum BorrowingStatus
{
    /// <summary>
    /// Borrowing request is pending admin approval
    /// </summary>
    Pending,
    
    /// <summary>
    /// Borrowing request has been approved
    /// </summary>
    Approved,
    
    /// <summary>
    /// Equipment has been picked up and is currently borrowed
    /// </summary>
    Borrowed,
    
    /// <summary>
    /// Equipment has been returned
    /// </summary>
    Returned,
    
    /// <summary>
    /// Equipment is overdue (not returned by expected date)
    /// </summary>
    Overdue,
    
    /// <summary>
    /// Borrowing request was cancelled
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// Borrowing request was rejected by admin
    /// </summary>
    Rejected
}

/// <summary>
/// Represents an equipment borrowing record
/// </summary>
public class EquipmentBorrowing
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Equipment being borrowed
    /// </summary>
    public Guid EquipmentId { get; set; }

    /// <summary>
    /// User who is borrowing the equipment
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Date when the borrowing was initiated
    /// </summary>
    public DateTime BorrowDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Expected return date
    /// </summary>
    public DateTime ExpectedReturnDate { get; set; }

    /// <summary>
    /// Actual return date (null if not returned yet)
    /// </summary>
    public DateTime? ActualReturnDate { get; set; }

    /// <summary>
    /// Current status of the borrowing
    /// </summary>
    public BorrowingStatus Status { get; set; } = BorrowingStatus.Pending;

    /// <summary>
    /// Purpose or reason for borrowing
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Purpose { get; set; } = string.Empty;

    /// <summary>
    /// Additional notes
    /// </summary>
    [MaxLength(1000)]
    public string? Notes { get; set; }

    /// <summary>
    /// Admin who approved/rejected the request
    /// </summary>
    public Guid? ApprovedBy { get; set; }

    /// <summary>
    /// Date when the request was approved/rejected
    /// </summary>
    public DateTime? ApprovedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual Equipment Equipment { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual User? Approver { get; set; }
}
