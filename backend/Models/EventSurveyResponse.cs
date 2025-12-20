using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

/// <summary>
/// User response to an event survey
/// </summary>
public class EventSurveyResponse
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Survey this response belongs to
    /// </summary>
    public Guid SurveyId { get; set; }

    /// <summary>
    /// User who submitted the response
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// JSON containing the user's responses
    /// Example:
    /// {
    ///   "rating": 5,
    ///   "feedback": "Great event!",
    ///   "wouldRecommend": true
    /// }
    /// </summary>
    [Required]
    [Column(TypeName = "TEXT")]
    public string ResponsesJson { get; set; } = "{}";

    /// <summary>
    /// When the response was submitted
    /// </summary>
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// IP address of submission (for analytics)
    /// </summary>
    [MaxLength(50)]
    public string? IpAddress { get; set; }

    // Navigation properties
    [ForeignKey("SurveyId")]
    public virtual EventSurvey Survey { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}
