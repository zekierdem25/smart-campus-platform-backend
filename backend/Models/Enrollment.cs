using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum EnrollmentStatus
{
    Active,
    Dropped,
    Completed,
    Failed
}

public class Enrollment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid StudentId { get; set; }

    [Required]
    public Guid SectionId { get; set; }

    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;

    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal? MidtermGrade { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal? FinalGrade { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal? HomeworkGrade { get; set; }

    [StringLength(5)]
    public string? LetterGrade { get; set; } // "AA", "BA", "BB", "CB", "CC", "DC", "DD", "FD", "FF"

    [Column(TypeName = "decimal(3,2)")]
    [Range(0, 4.0)]
    public decimal? GradePoint { get; set; } // 4.0, 3.5, 3.0...

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("StudentId")]
    public Student Student { get; set; } = null!;

    [ForeignKey("SectionId")]
    public CourseSection Section { get; set; } = null!;
}
