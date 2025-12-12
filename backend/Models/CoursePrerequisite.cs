using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCampus.API.Models;

public class CoursePrerequisite
{
    public Guid CourseId { get; set; }
    
    public Guid PrerequisiteCourseId { get; set; }

    // Navigation properties
    [ForeignKey("CourseId")]
    public Course Course { get; set; } = null!;

    [ForeignKey("PrerequisiteCourseId")]
    public Course PrerequisiteCourse { get; set; } = null!;
}
