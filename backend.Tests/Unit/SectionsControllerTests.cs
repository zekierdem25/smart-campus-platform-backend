using Microsoft.EntityFrameworkCore;
using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class SectionsControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IScheduleConflictService> _mockScheduleConflictService;

    public SectionsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mockScheduleConflictService = new Mock<IScheduleConflictService>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new SectionsController(
            _context,
            _mockScheduleConflictService.Object);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new SectionsController(
            null!,
            _mockScheduleConflictService.Object));
    }

    [Fact]
    public void Constructor_WithNullScheduleConflictService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new SectionsController(
            _context,
            null!));
    }
}
