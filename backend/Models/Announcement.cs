using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class Announcement
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(5000)]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public Guid AuthorId { get; set; }

    public Guid? CourseId { get; set; } // Nullable - if null, it's a general announcement

    public bool IsImportant { get; set; } = false;

    // Navigation properties
    [ForeignKey("AuthorId")]
    public User Author { get; set; } = null!;

    [ForeignKey("CourseId")]
    public Course? Course { get; set; }
}
