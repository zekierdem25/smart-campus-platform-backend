using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum AcademicTitle
{
    ResearchAssistant,
    Lecturer,
    AssistantProfessor,
    AssociateProfessor,
    Professor
}

public class Faculty
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(20)]
    public string EmployeeNumber { get; set; } = string.Empty;

    [Required]
    public Guid DepartmentId { get; set; }

    [Required]
    public AcademicTitle Title { get; set; } = AcademicTitle.Lecturer;

    [StringLength(100)]
    public string? OfficeLocation { get; set; }

    [StringLength(500)]
    public string? OfficeHours { get; set; }

    [StringLength(1000)]
    public string? Specialization { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [ForeignKey("DepartmentId")]
    public Department Department { get; set; } = null!;

    public ICollection<CourseSection> TeachingSections { get; set; } = new List<CourseSection>();
    
    public ICollection<AttendanceSession> AttendanceSessions { get; set; } = new List<AttendanceSession>();
}

