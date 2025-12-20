using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public enum ScheduleDayOfWeek
{
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5
}

public class Schedule
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid SectionId { get; set; }

    [Required]
    public ScheduleDayOfWeek DayOfWeek { get; set; }

    [Required]
    public TimeSpan StartTime { get; set; }

    [Required]
    public TimeSpan EndTime { get; set; }

    [Required]
    public Guid ClassroomId { get; set; }

    [Required]
    [StringLength(20)]
    public string Semester { get; set; } = "Fall";

    [Range(2020, 2100)]
    public int Year { get; set; } = DateTime.Now.Year;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("SectionId")]
    public CourseSection Section { get; set; } = null!;

    [ForeignKey("ClassroomId")]
    public Classroom Classroom { get; set; } = null!;
}
