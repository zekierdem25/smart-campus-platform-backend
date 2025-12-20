using System.ComponentModel.DataAnnotations;

namespace SmartCampus.API.Models;

/// <summary>
/// Status of a waitlist entry
/// </summary>
public enum WaitlistStatus
{
    /// <summary>
    /// User is waiting in the queue
    /// </summary>
    Pending,
    
    /// <summary>
    /// User has been notified that a spot is available
    /// </summary>
    Notified,
    
    /// <summary>
    /// User has been automatically registered from waitlist
    /// </summary>
    Registered,
    
    /// <summary>
    /// User cancelled their waitlist entry
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// Waitlist entry expired (user didn't respond to notification)
    /// </summary>
    Expired
}

/// <summary>
/// Represents a user on the waitlist for an event
/// Uses FIFO (First In First Out) ordering based on Position
/// </summary>
public class EventWaitlist
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Event that the user is waitlisted for
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// User who is on the waitlist
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Position in the waitlist queue (1 = first in line)
    /// Used for FIFO ordering
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    /// Current status of the waitlist entry
    /// </summary>
    public WaitlistStatus Status { get; set; } = WaitlistStatus.Pending;

    /// <summary>
    /// When the user was added to the waitlist
    /// </summary>
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the user was notified of an available spot (if applicable)
    /// </summary>
    public DateTime? NotifiedAt { get; set; }

    /// <summary>
    /// When the status was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual Event Event { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
