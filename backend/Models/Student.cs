using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class Student
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(20)]
    public string StudentNumber { get; set; } = string.Empty;

    [Required]
    public Guid DepartmentId { get; set; }

    [Range(0, 4.0)]
    [Column(TypeName = "decimal(3,2)")]
    public decimal GPA { get; set; } = 0;

    [Range(0, 4.0)]
    [Column(TypeName = "decimal(3,2)")]
    public decimal CGPA { get; set; } = 0;

    public int CurrentSemester { get; set; } = 1;

    public int EnrollmentYear { get; set; } = DateTime.Now.Year;

    public bool IsScholarship { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; } = null!;
}

