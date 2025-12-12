using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

/// <summary>
/// 2FA (İki Faktörlü Kimlik Doğrulama) kodu
/// Login sırasında email'e gönderilen 6 haneli doğrulama kodu
/// </summary>
public class TwoFactorCode
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Kodu alan kullanıcının ID'si
    /// </summary>
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary>
    /// 6 haneli doğrulama kodu
    /// </summary>
    [Required]
    [StringLength(6)]
    public string Code { get; set; } = null!;

    /// <summary>
    /// Geçici token (login sonrası frontend'e gönderilir)
    /// </summary>
    [Required]
    public string TempToken { get; set; } = null!;

    /// <summary>
    /// Kodun geçerlilik süresi (5 dakika)
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Kod kullanıldı mı?
    /// </summary>
    public bool IsUsed { get; set; } = false;

    /// <summary>
    /// Yanlış deneme sayısı (max 5)
    /// </summary>
    public int AttemptCount { get; set; } = 0;

    /// <summary>
    /// Kilitleme bitiş zamanı (5 yanlış deneme sonrası)
    /// </summary>
    public DateTime? LockoutEndAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Kod hala geçerli mi?
    /// </summary>
    [NotMapped]
    public bool IsValid => !IsUsed && ExpiresAt > DateTime.UtcNow && LockoutEndAt == null;

    /// <summary>
    /// Kilitli mi?
    /// </summary>
    [NotMapped]
    public bool IsLocked => LockoutEndAt.HasValue && LockoutEndAt > DateTime.UtcNow;
}
