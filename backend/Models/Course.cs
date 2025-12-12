using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class Course
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(20)]
    public string Code { get; set; } = string.Empty; // "BM301"

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty; // "Yazılım Mühendisliği"

    [StringLength(2000)]
    public string? Description { get; set; }

    [Range(1, 10)]
    public int Credits { get; set; } = 3;

    [Range(1, 15)]
    public int ECTS { get; set; } = 5;

    [StringLength(500)]
    public string? SyllabusUrl { get; set; }

    [Required]
    public Guid DepartmentId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; } = null!;

    public ICollection<CourseSection> Sections { get; set; } = new List<CourseSection>();
    
    public ICollection<CoursePrerequisite> Prerequisites { get; set; } = new List<CoursePrerequisite>();
    
    public ICollection<CoursePrerequisite> RequiredFor { get; set; } = new List<CoursePrerequisite>();
}
