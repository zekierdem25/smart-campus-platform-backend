using System.Text.Json.Serialization;

namespace SmartCampus.API.DTOs;

public class DashboardMetricsDto
{
    public int TotalStudents { get; set; }
    public int TotalCourses { get; set; }
    public int TotalFaculty { get; set; }
    public int ActiveEnrollments { get; set; }
}

public class AcademicPerformanceMetricsDto
{
    public string? Term { get; set; }
    public double AverageGpa { get; set; }
    public Dictionary<string, double> GpaByDepartment { get; set; } = new();
    public Dictionary<string, int> GradeDistribution { get; set; } = new();
}

public class AttendanceAnalyticsDto
{
    public double OverallAttendanceRate { get; set; }
    public List<CourseAttendanceDto> LowAttendanceCourses { get; set; } = new();
    public List<StudentAttendanceRiskDto> AtRiskStudents { get; set; } = new();
}

public class CourseAttendanceDto
{
    public Guid CourseId { get; set; }
    public string? CourseName { get; set; }
    public double AttendanceRate { get; set; }
}

public class StudentAttendanceRiskDto
{
    public Guid StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? CourseName { get; set; }
    public double AttendanceRate { get; set; }
}

public class MealUsageAnalyticsDto
{
    public int TotalMealsServed { get; set; }
    public Dictionary<string, int> UsageByCafeteria { get; set; } = new();
    public Dictionary<string, int> PeakHours { get; set; } = new();
}

public class EventsAnalyticsDto
{
    public int TotalEvents { get; set; }
    public int TotalRegistrations { get; set; }
    public Dictionary<string, int> PopularCategories { get; set; } = new();
    
    // Graphic data
    public List<EventPerformanceDto> PopularEvents { get; set; } = new();
    public List<EventPerformanceDto> RegistrationRates { get; set; } = new();
    public List<EventPerformanceDto> CheckInRates { get; set; } = new();
}

public class EventPerformanceDto
{
    public string? Title { get; set; }
    public int RegistrationCount { get; set; }
    public double RegistrationRate { get; set; }
    public double CheckInRate { get; set; }
}
