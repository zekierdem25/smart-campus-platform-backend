using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class EmailVerificationToken
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // UserId artık nullable - email doğrulanmadan kullanıcı oluşturulmayacak
    public Guid? UserId { get; set; }

    [Required]
    [StringLength(500)]
    public string Token { get; set; } = string.Empty;

    [Required]
    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UsedAt { get; set; }

    // Kayıt bilgileri (JSON formatında) - email doğrulanmadan önce kullanıcı oluşturulmayacak
    [Column(TypeName = "TEXT")]
    public string RegistrationData { get; set; } = string.Empty;

    // Computed properties
    [NotMapped]
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    [NotMapped]
    public bool IsUsed => UsedAt != null;

    [NotMapped]
    public bool IsValid => !IsExpired && !IsUsed;

    // Navigation properties
    [ForeignKey("UserId")]
    public User? User { get; set; }
}

