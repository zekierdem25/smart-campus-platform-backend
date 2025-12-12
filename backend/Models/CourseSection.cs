using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class CourseSection
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CourseId { get; set; }

    [Range(1, 99)]
    public int SectionNumber { get; set; } = 1;

    [Required]
    [StringLength(20)]
    public string Semester { get; set; } = "Fall"; // "Fall", "Spring", "Summer"

    [Range(2020, 2100)]
    public int Year { get; set; } = DateTime.Now.Year;

    [Required]
    public Guid InstructorId { get; set; }

    public Guid? ClassroomId { get; set; }

    [Range(1, 500)]
    public int Capacity { get; set; } = 40;

    [Range(0, 500)]
    public int EnrolledCount { get; set; } = 0;

    [StringLength(2000)]
    public string? ScheduleJson { get; set; } // [{"day":"Monday","startTime":"09:00","endTime":"10:50"}]

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey("CourseId")]
    public Course Course { get; set; } = null!;

    [ForeignKey("InstructorId")]
    public Faculty Instructor { get; set; } = null!;

    [ForeignKey("ClassroomId")]
    public Classroom? Classroom { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    
    public ICollection<AttendanceSession> AttendanceSessions { get; set; } = new List<AttendanceSession>();
}
