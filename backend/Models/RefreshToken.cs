using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class RefreshToken
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(500)]
    public string Token { get; set; } = string.Empty;

    [Required]
    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; }

    [StringLength(100)]
    public string? ReplacedByToken { get; set; }

    [StringLength(100)]
    public string? RevokedReason { get; set; }

    [StringLength(50)]
    public string? CreatedByIp { get; set; }

    [StringLength(50)]
    public string? RevokedByIp { get; set; }

    // Computed properties
    [NotMapped]
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    [NotMapped]
    public bool IsRevoked => RevokedAt != null;

    [NotMapped]
    public bool IsActive => !IsRevoked && !IsExpired;

    // Navigation properties
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}

