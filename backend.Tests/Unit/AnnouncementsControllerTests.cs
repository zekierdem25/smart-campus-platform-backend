using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class AnnouncementsControllerTests
{
    private readonly ApplicationDbContext _context;

    public AnnouncementsControllerTests()
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
        var controller = new AnnouncementsController(_context);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange
        ApplicationDbContext? context = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new AnnouncementsController(context!));
    }
}
