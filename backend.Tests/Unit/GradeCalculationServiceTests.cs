using SmartCampus.API.Models;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class GradeCalculationServiceTests
{
    private readonly GradeCalculationService _service;

    public GradeCalculationServiceTests()
    {
        _service = new GradeCalculationService();
    }

    // ========== CalculateLetterGrade Tests ==========

    [Theory]
    [InlineData(95, 95, 95, "AA", 4.0)]
    [InlineData(85, 85, 85, "BA", 3.5)]
    [InlineData(80, 80, 80, "BB", 3.0)]
    [InlineData(75, 75, 75, "CB", 2.5)]
    [InlineData(70, 70, 70, "CC", 2.0)]
    [InlineData(65, 65, 65, "DC", 1.5)]
    [InlineData(60, 60, 60, "DD", 1.0)]
    [InlineData(50, 50, 50, "FD", 0.5)]
    [InlineData(49, 49, 49, "FF", 0.0)]
    public void CalculateLetterGrade_WithAllGrades_ShouldReturnCorrectGrade(
        decimal midterm, decimal final, decimal homework, string expectedLetter, double expectedPoints)
    {
        var (letter, points) = _service.CalculateLetterGrade(midterm, final, homework);
        Assert.Equal(expectedLetter, letter);
        Assert.Equal((decimal)expectedPoints, points);
    }

    [Theory]
    [InlineData(100, 100, "AA", 4.0)] // (100*0.4 + 100*0.6) = 100
    [InlineData(50, 50, "FD", 0.5)]   // (50*0.4 + 50*0.6) = 50
    public void CalculateLetterGrade_WithoutHomework_ShouldUseDifferentWeights(
        decimal midterm, decimal final, string expectedLetter, double expectedPoints)
    {
        var (letter, points) = _service.CalculateLetterGrade(midterm, final, null);
        Assert.Equal(expectedLetter, letter);
        Assert.Equal((decimal)expectedPoints, points);
    }

    [Fact]
    public void CalculateLetterGrade_WithMissingMidtermOrFinal_ShouldReturnEmpty()
    {
        var (letter1, points1) = _service.CalculateLetterGrade(null, 100, 100);
        Assert.Equal("", letter1);
        Assert.Equal(0, points1);

        var (letter2, points2) = _service.CalculateLetterGrade(100, null, 100);
        Assert.Equal("", letter2);
        Assert.Equal(0, points2);
    }

    // ========== CalculateGPA Tests ==========

    [Fact]
    public void CalculateGPA_ShouldCalculateCorrectly()
    {
        // Arrange
        var enrollments = new List<Enrollment>
        {
            new Enrollment 
            { 
                GradePoint = 4.0m, // AA
                Section = new CourseSection { Course = new Course { Credits = 3 } } 
            },
            new Enrollment 
            { 
                GradePoint = 3.0m, // BB
                Section = new CourseSection { Course = new Course { Credits = 4 } } 
            },
            new Enrollment 
            { 
                GradePoint = 2.0m, // CC
                Section = new CourseSection { Course = new Course { Credits = 3 } } 
            }
        };

        // Calculation:
        // (4.0 * 3) + (3.0 * 4) + (2.0 * 3) = 12 + 12 + 6 = 30
        // Total Credits: 3 + 4 + 3 = 10
        // GPA: 30 / 10 = 3.00

        // Act
        var gpa = _service.CalculateGPA(enrollments);

        // Assert
        Assert.Equal(3.00m, gpa);
    }

    [Fact]
    public void CalculateGPA_WithEmptyList_ShouldReturnZero()
    {
        var gpa = _service.CalculateGPA(new List<Enrollment>());
        Assert.Equal(0, gpa);
    }

    [Fact]
    public void CalculateGPA_ShouldIgnoreIncompleteGrades()
    {
        var enrollments = new List<Enrollment>
        {
            new Enrollment 
            { 
                GradePoint = 4.0m, 
                Section = new CourseSection { Course = new Course { Credits = 3 } } 
            },
            new Enrollment 
            { 
                GradePoint = null, // Incomplete
                Section = new CourseSection { Course = new Course { Credits = 3 } } 
            }
        };

        // Act
        var gpa = _service.CalculateGPA(enrollments);

        // Assert
        // Should only count the first one: (4.0 * 3) / 3 = 4.00
        Assert.Equal(4.00m, gpa);
    }

    // ========== CalculateCGPA Tests ==========

    [Fact]
    public void CalculateCGPA_ShouldOnlyIncludeCompletedEnrollments()
    {
        // Arrange
        var enrollments = new List<Enrollment>
        {
            new Enrollment 
            { 
                Status = EnrollmentStatus.Completed,
                GradePoint = 4.0m, 
                Section = new CourseSection { Course = new Course { Credits = 3 } } 
            },
            new Enrollment 
            { 
                Status = EnrollmentStatus.Active, // Not completed
                GradePoint = 2.0m, 
                Section = new CourseSection { Course = new Course { Credits = 3 } } 
            },
            new Enrollment 
            { 
                Status = EnrollmentStatus.Dropped, // Dropped
                GradePoint = 0.0m, 
                Section = new CourseSection { Course = new Course { Credits = 3 } } 
            }
        };

        // Act
        var cgpa = _service.CalculateCGPA(enrollments);

        // Assert
        // Should only count the first one: (4.0 * 3) / 3 = 4.00
        Assert.Equal(4.00m, cgpa);
    }
}
