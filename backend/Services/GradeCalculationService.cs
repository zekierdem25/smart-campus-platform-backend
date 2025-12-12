using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public interface IGradeCalculationService
{
    (string letterGrade, decimal gradePoint) CalculateLetterGrade(decimal? midterm, decimal? final, decimal? homework);
    decimal CalculateGPA(List<Enrollment> enrollments);
    decimal CalculateCGPA(List<Enrollment> allEnrollments);
}

public class GradeCalculationService : IGradeCalculationService
{
    /// <summary>
    /// Calculates letter grade and grade point based on course grades.
    /// Formula: Average = Midterm * 0.3 + Homework * 0.2 + Final * 0.5
    /// </summary>
    public (string letterGrade, decimal gradePoint) CalculateLetterGrade(decimal? midterm, decimal? final, decimal? homework)
    {
        if (!midterm.HasValue || !final.HasValue)
            return ("", 0);

        // Calculate weighted average
        // If homework is null, use midterm 40%, final 60%
        decimal average;
        if (homework.HasValue)
        {
            average = midterm.Value * 0.3m + homework.Value * 0.2m + final.Value * 0.5m;
        }
        else
        {
            average = midterm.Value * 0.4m + final.Value * 0.6m;
        }

        // Turkish grading system (4.0 scale)
        return average switch
        {
            >= 90 => ("AA", 4.0m),
            >= 85 => ("BA", 3.5m),
            >= 80 => ("BB", 3.0m),
            >= 75 => ("CB", 2.5m),
            >= 70 => ("CC", 2.0m),
            >= 65 => ("DC", 1.5m),
            >= 60 => ("DD", 1.0m),
            >= 50 => ("FD", 0.5m),
            _ => ("FF", 0.0m)
        };
    }

    /// <summary>
    /// Calculates GPA for a specific semester/period.
    /// GPA = Sum(GradePoint * Credits) / Sum(Credits)
    /// </summary>
    public decimal CalculateGPA(List<Enrollment> enrollments)
    {
        var validEnrollments = enrollments
            .Where(e => e.GradePoint.HasValue && e.Section?.Course != null)
            .ToList();

        if (!validEnrollments.Any())
            return 0;

        var totalCredits = validEnrollments.Sum(e => e.Section.Course.Credits);
        if (totalCredits == 0)
            return 0;

        var weightedSum = validEnrollments.Sum(e => e.GradePoint!.Value * e.Section.Course.Credits);
        
        return Math.Round(weightedSum / totalCredits, 2);
    }

    /// <summary>
    /// Calculates CGPA (Cumulative GPA) for all completed enrollments.
    /// </summary>
    public decimal CalculateCGPA(List<Enrollment> allEnrollments)
    {
        var completedEnrollments = allEnrollments
            .Where(e => e.Status == EnrollmentStatus.Completed && 
                       e.GradePoint.HasValue && 
                       e.Section?.Course != null)
            .ToList();

        return CalculateGPA(completedEnrollments);
    }
}
