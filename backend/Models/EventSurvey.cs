using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

/// <summary>
/// Survey for collecting feedback after events
/// </summary>
public class EventSurvey
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Event this survey belongs to
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Survey title
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Survey description
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// JSON schema defining the form fields
    /// Example:
    /// {
    ///   "fields": [
    ///     {"id": "rating", "type": "number", "label": "Rating", "required": true, "min": 1, "max": 5},
    ///     {"id": "feedback", "type": "textarea", "label": "Feedback", "required": false}
    ///   ]
    /// }
    /// </summary>
    [Required]
    [Column(TypeName = "TEXT")]
    public string SchemaJson { get; set; } = "{}";

    /// <summary>
    /// Whether the survey is active and accepting responses
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Start date for accepting responses (null = immediately)
    /// </summary>
    public DateTime? StartsAt { get; set; }

    /// <summary>
    /// End date for accepting responses (null = no end)
    /// </summary>
    public DateTime? EndsAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("EventId")]
    public virtual Event Event { get; set; } = null!;
    public virtual ICollection<EventSurveyResponse> Responses { get; set; } = new List<EventSurveyResponse>();
}
