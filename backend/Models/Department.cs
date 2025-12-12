using System.ComponentModel.DataAnnotations;

namespace SmartCampus.API.Models;

public class Department
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Code { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Faculty { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Faculty> FacultyMembers { get; set; } = new List<Faculty>();
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}

