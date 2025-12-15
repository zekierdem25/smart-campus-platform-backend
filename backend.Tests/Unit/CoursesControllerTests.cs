using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class CoursesControllerTests
{
    private readonly ApplicationDbContext _context;

    public CoursesControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
    }

    [Fact]
    public void Constructor_WithValidContext_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new CoursesController(_context);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange
        ApplicationDbContext? context = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CoursesController(context!));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid-json")]
    public void ParseSchedule_WithInvalidOrEmptyJson_ShouldReturnEmptyList(string? json)
    {
        // Arrange
        var controller = new CoursesController(_context);
        var methodInfo = typeof(CoursesController).GetMethod("ParseSchedule", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = methodInfo?.Invoke(controller, new object?[] { json }) as List<ScheduleSlotDto>;

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ParseSchedule_WithValidJson_ShouldReturnList()
    {
        // Arrange
        var controller = new CoursesController(_context);
        var json = "[{\"Day\":\"Monday\",\"StartTime\":\"09:00:00\",\"EndTime\":\"10:00:00\"}]";
        var methodInfo = typeof(CoursesController).GetMethod("ParseSchedule", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = methodInfo?.Invoke(controller, new object[] { json }) as List<ScheduleSlotDto>;

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Monday", result![0].Day);
    }
}
