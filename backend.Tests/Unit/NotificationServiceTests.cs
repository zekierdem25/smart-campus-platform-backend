using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class NotificationServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Mock<ILogger<NotificationService>> _mockLogger;

    public NotificationServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mockEmailService = new Mock<IEmailService>();
        _mockLogger = new Mock<ILogger<NotificationService>>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitialize()
    {
        var service = new NotificationService(_context, _mockEmailService.Object, _mockLogger.Object);
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new NotificationService(null!, _mockEmailService.Object, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullEmailService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new NotificationService(_context, null!, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new NotificationService(_context, _mockEmailService.Object, null!));
    }

    // ========== NotifyFacultyOnCourseDropAsync Tests ==========

    [Fact]
    public async Task NotifyFacultyOnCourseDropAsync_WithValidEnrollment_ShouldSendEmail()
    {
        // Arrange
        var service = new NotificationService(_context, _mockEmailService.Object, _mockLogger.Object);
        
        var instructorUser = new User { Id = Guid.NewGuid(), FirstName = "Prof", LastName = "X", Email = "prof@smartcampus.edu" };
        var instructor = new Faculty { Id = Guid.NewGuid(), User = instructorUser };
        
        var studentUser = new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };
        var student = new Student { Id = Guid.NewGuid(), User = studentUser };

        var course = new Course { Id = Guid.NewGuid(), Code = "CS101", Name = "Intro to CS" };
        var section = new CourseSection { Id = Guid.NewGuid(), Course = course, Instructor = instructor };

        var enrollment = new Enrollment { Id = Guid.NewGuid(), Student = student, Section = section };
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        // Act
        await service.NotifyFacultyOnCourseDropAsync(enrollment.Id);

        // Wait a bit for pending background task (Notify... uses Task.Run fire-and-forget for email)
        await Task.Delay(100);

        // Assert
        _mockEmailService.Verify(x => x.SendCustomEmailAsync(
            It.Is<string>(e => e == instructorUser.Email),
            It.Is<string>(s => s.Contains("Öğrenci Ders Bıraktı")),
            It.IsAny<string>()), Times.Once);
    }

    // ========== NotifyStudentOnGradeEntryAsync Tests ==========

    [Fact]
    public async Task NotifyStudentOnGradeEntryAsync_WithGrades_ShouldSendEmail()
    {
        // Arrange
        var service = new NotificationService(_context, _mockEmailService.Object, _mockLogger.Object);
        
        var studentUser = new User { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Student", Email = "alice@smartcampus.edu" };
        var student = new Student { Id = Guid.NewGuid(), User = studentUser };

        var course = new Course { Id = Guid.NewGuid(), Code = "MATH101", Name = "Calculus" };
        var section = new CourseSection { Id = Guid.NewGuid(), Course = course };

        var enrollment = new Enrollment 
        { 
            Id = Guid.NewGuid(), 
            Student = student, 
            Section = section,
            MidtermGrade = 85
        };
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        // Act
        await service.NotifyStudentOnGradeEntryAsync(enrollment.Id);

        // Wait a bit for pending background task
        await Task.Delay(100);

        // Assert
        _mockEmailService.Verify(x => x.SendCustomEmailAsync(
            It.Is<string>(e => e == studentUser.Email),
            It.Is<string>(s => s.Contains("Notunuz Açıklandı")),
            It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task NotifyStudentOnGradeEntryAsync_NoGrades_ShouldNotSendEmail()
    {
         // Arrange
        var service = new NotificationService(_context, _mockEmailService.Object, _mockLogger.Object);
        
        var studentUser = new User { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Builder", Email = "bob@smartcampus.edu" };
        var student = new Student { Id = Guid.NewGuid(), User = studentUser };
        var course = new Course { Id = Guid.NewGuid(), Code = "PHY101", Name = "Physics" };
        var section = new CourseSection { Id = Guid.NewGuid(), Course = course };

        var enrollment = new Enrollment 
        { 
            Id = Guid.NewGuid(), 
            Student = student, 
            Section = section,
            MidtermGrade = null,
            FinalGrade = null,
            HomeworkGrade = null
        };
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        // Act
        await service.NotifyStudentOnGradeEntryAsync(enrollment.Id);

         // Wait a bit
        await Task.Delay(50);

        // Assert
        _mockEmailService.Verify(x => x.SendCustomEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()), Times.Never);
    }
}
