using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

[Collection("PdfGeneration")]
public class GradesControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IGradeCalculationService> _mockGradeCalculationService;
    private readonly Mock<INotificationService> _mockNotificationService;
    private readonly Mock<ILogger<GradesController>> _mockLogger;

    public GradesControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mockGradeCalculationService = new Mock<IGradeCalculationService>();
        _mockNotificationService = new Mock<INotificationService>();
        _mockLogger = new Mock<ILogger<GradesController>>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new GradesController(
            _context,
            _mockGradeCalculationService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GradesController(
            null!,
            _mockGradeCalculationService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullGradeCalculationService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GradesController(
            _context,
            null!,
            _mockNotificationService.Object,
            _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullNotificationService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GradesController(
            _context,
            _mockGradeCalculationService.Object,
            null!,
            _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GradesController(
            _context,
            _mockGradeCalculationService.Object,
            _mockNotificationService.Object,
            null!));
    }

    [Fact]
    public void GenerateTranscriptPdf_WithValidData_ShouldReturnByteArray()
    {
        // Arrange
        var controller = new GradesController(
            _context,
            _mockGradeCalculationService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object);

        var transcript = new TranscriptDto
        {
            StudentId = Guid.NewGuid(),
            StudentName = "Ali Veli",
            StudentNumber = "123456789",
            DepartmentName = "Bilgisayar Mühendisliği",
            EnrollmentYear = 2023,
            CGPA = 3.5m,
            TotalCredits = 120,
            TotalECTS = 240,
            Semesters = new List<SemesterGradesDto>
            {
                new SemesterGradesDto
                {
                    Semester = "Güz",
                    Year = 2023,
                    GPA = 3.5m,
                    Credits = 30,
                    Courses = new List<GradeDto>
                    {
                        new GradeDto { CourseCode = "CS101", CourseName = "Intro to CS", Credits = 5, MidtermGrade = 80, FinalGrade = 90, GradePoint = 4.0m, LetterGrade = "AA" }
                    }
                }
            }
        };

        var student = new Student
        {
            StudentNumber = "123456789",
            EnrollmentYear = 2023,
            User = new User { FirstName = "Ali", LastName = "Veli" },
            Department = new Department { Name = "Bilgisayar Mühendisliği" }
        };

        var methodInfo = typeof(GradesController).GetMethod("GenerateTranscriptPdf", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = methodInfo?.Invoke(controller, new object[] { transcript, student }) as byte[];

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result!.Length > 100);
    }
}
