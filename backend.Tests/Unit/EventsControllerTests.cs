using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class EventsControllerTests
{
    private readonly ApplicationDbContext _context;

    public EventsControllerTests()
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
        var controller = new EventsController(_context);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_ShouldSetContextCorrectly()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );

        // Act
        var controller = new EventsController(context);

        // Assert
        Assert.NotNull(controller);
        // Controller'ın field'ı private olduğu için reflection kullanarak kontrol edebiliriz
        var contextField = typeof(EventsController)
            .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.Equal(context, contextField.GetValue(controller));
    }

    [Fact]
    public void Constructor_WithDifferentContextInstances_ShouldSetCorrectly()
    {
        // Arrange
        var context1 = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );
        var context2 = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );

        // Act
        var controller1 = new EventsController(context1);
        var controller2 = new EventsController(context2);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(EventsController)
            .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.Equal(context1, contextField.GetValue(controller1));
        Assert.Equal(context2, contextField.GetValue(controller2));
        Assert.NotEqual(contextField.GetValue(controller1), contextField.GetValue(controller2));
    }

    [Fact]
    public void Constructor_WithSameContextInstance_ShouldSetSameContext()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );

        // Act
        var controller1 = new EventsController(context);
        var controller2 = new EventsController(context);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(EventsController)
            .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.Equal(context, contextField.GetValue(controller1));
        Assert.Equal(context, contextField.GetValue(controller2));
        Assert.Equal(contextField.GetValue(controller1), contextField.GetValue(controller2));
    }
}

