using Microsoft.EntityFrameworkCore;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using SmartCampus.API.Models;
using SmartCampus.API.DTOs;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class EnrollmentServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IPrerequisiteService> _mockPrerequisiteService;
    private readonly Mock<IScheduleConflictService> _mockScheduleConflictService;

    public EnrollmentServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mockPrerequisiteService = new Mock<IPrerequisiteService>();
        _mockScheduleConflictService = new Mock<IScheduleConflictService>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitialize()
    {
        var service = new EnrollmentService(
            _context,
            _mockPrerequisiteService.Object,
            _mockScheduleConflictService.Object);
            
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EnrollmentService(
            null!,
            _mockPrerequisiteService.Object,
            _mockScheduleConflictService.Object));
    }

    [Fact]
    public void Constructor_WithNullPrerequisiteService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EnrollmentService(
            _context,
            null!,
            _mockScheduleConflictService.Object));
    }

    [Fact]
    public void Constructor_WithNullScheduleConflictService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EnrollmentService(
            _context,
            _mockPrerequisiteService.Object,
            null!));
    }

    // ========== CheckEnrollmentEligibilityAsync Tests ==========

    [Fact]
    public async Task CheckEnrollmentEligibilityAsync_WhenSectionValid_ShouldReturnCanEnrollTrue()
    {
        // Arrange
        var service = new EnrollmentService(
            _context,
            _mockPrerequisiteService.Object,
            _mockScheduleConflictService.Object);

        var course = new Course { Id = Guid.NewGuid(), Name = "Test Course", Code = "TC101" };
        var section = new CourseSection 
        { 
            Id = Guid.NewGuid(), 
            Course = course,
            CourseId = course.Id,
            Capacity = 30, 
            EnrolledCount = 10,
            ScheduleJson = "[]",
            IsActive = true
        };
        _context.CourseSections.Add(section);
        await _context.SaveChangesAsync();

        _mockPrerequisiteService.Setup(s => s.GetMissingPrerequisitesAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<string>());

        _mockScheduleConflictService.Setup(s => s.ParseScheduleJson(It.IsAny<string>()))
            .Returns(new List<ScheduleSlotDto>());
            
        _mockScheduleConflictService.Setup(s => s.HasScheduleConflict(It.IsAny<List<ScheduleSlotDto>>(), It.IsAny<List<ScheduleSlotDto>>()))
            .Returns(false);

        // Act
        var result = await service.CheckEnrollmentEligibilityAsync(Guid.NewGuid(), section.Id);

        // Assert
        Assert.True(result.CanEnroll);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task CheckEnrollmentEligibilityAsync_WhenSectionDoesNotExist_ShouldReturnError()
    {
        // Arrange
        var service = new EnrollmentService(
            _context,
            _mockPrerequisiteService.Object,
            _mockScheduleConflictService.Object);

        // Act
        var result = await service.CheckEnrollmentEligibilityAsync(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        Assert.False(result.CanEnroll);
        Assert.Contains("Ders şubesi bulunamadı", result.Errors);
    }

    [Fact]
    public async Task CheckEnrollmentEligibilityAsync_WhenPrerequisitesMissing_ShouldReturnError()
    {
        // Arrange
        var service = new EnrollmentService(
            _context,
            _mockPrerequisiteService.Object,
            _mockScheduleConflictService.Object);

        var course = new Course { Id = Guid.NewGuid(), Name = "Advanced Course", Code = "AC101" };
        var section = new CourseSection 
        { 
            Id = Guid.NewGuid(), 
            Course = course, 
            CourseId = course.Id,
            ScheduleJson = "[]",
            IsActive = true
        };
        _context.CourseSections.Add(section);
        await _context.SaveChangesAsync();

        _mockPrerequisiteService.Setup(s => s.GetMissingPrerequisitesAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<string> { "Basic Course" });

        // Act
        var result = await service.CheckEnrollmentEligibilityAsync(Guid.NewGuid(), section.Id);

        // Assert
        Assert.False(result.CanEnroll);
        Assert.Contains("Eksik önkoşullar: Basic Course", result.Errors);
    }

    [Fact]
    public async Task CheckEnrollmentEligibilityAsync_WhenCapacityFull_ShouldReturnError()
    {
        // Arrange
        var service = new EnrollmentService(
            _context,
            _mockPrerequisiteService.Object,
            _mockScheduleConflictService.Object);

        var course = new Course { Id = Guid.NewGuid(), Name = "Full Course", Code = "FC101" };
        var section = new CourseSection 
        { 
            Id = Guid.NewGuid(), 
            Course = course, 
            CourseId = course.Id,
            Capacity = 30, 
            EnrolledCount = 30, // Full
            ScheduleJson = "[]",
            IsActive = true
        };
        _context.CourseSections.Add(section);
        await _context.SaveChangesAsync();

         _mockPrerequisiteService.Setup(s => s.GetMissingPrerequisitesAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<string>());

        // Act
        var result = await service.CheckEnrollmentEligibilityAsync(Guid.NewGuid(), section.Id);

        // Assert
        Assert.False(result.CanEnroll);
        Assert.Contains("Kontenjan dolu", result.Errors);
    }
}
