using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.Tests.Helpers;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class SurveysControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<ILogger<SurveysController>> _mockLogger;

    public SurveysControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _mockLogger = MockServices.CreateMockLogger<SurveysController>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new SurveysController(_context, _mockLogger.Object);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_ShouldSetDependenciesCorrectly()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );
        var logger = MockServices.CreateMockLogger<SurveysController>().Object;

        // Act
        var controller = new SurveysController(context, logger);

        // Assert
        Assert.NotNull(controller);
        // Controller'ın field'ları private olduğu için reflection kullanarak kontrol edebiliriz
        var contextField = typeof(SurveysController)
            .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var loggerField = typeof(SurveysController)
            .GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.NotNull(loggerField);

        Assert.Equal(context, contextField.GetValue(controller));
        Assert.Equal(logger, loggerField.GetValue(controller));
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
        var logger = MockServices.CreateMockLogger<SurveysController>().Object;

        // Act
        var controller1 = new SurveysController(context1, logger);
        var controller2 = new SurveysController(context2, logger);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(SurveysController)
            .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.Equal(context1, contextField.GetValue(controller1));
        Assert.Equal(context2, contextField.GetValue(controller2));
        Assert.NotEqual(contextField.GetValue(controller1), contextField.GetValue(controller2));
    }

    [Fact]
    public void Constructor_WithDifferentLoggerInstances_ShouldSetCorrectly()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );
        var logger1 = MockServices.CreateMockLogger<SurveysController>().Object;
        var logger2 = MockServices.CreateMockLogger<SurveysController>().Object;

        // Act
        var controller1 = new SurveysController(context, logger1);
        var controller2 = new SurveysController(context, logger2);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var loggerField = typeof(SurveysController)
            .GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(loggerField);
        Assert.Equal(logger1, loggerField.GetValue(controller1));
        Assert.Equal(logger2, loggerField.GetValue(controller2));
    }

    [Fact]
    public void Constructor_WithSameContextAndLogger_ShouldSetSameInstances()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );
        var logger = MockServices.CreateMockLogger<SurveysController>().Object;

        // Act
        var controller1 = new SurveysController(context, logger);
        var controller2 = new SurveysController(context, logger);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(SurveysController)
            .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var loggerField = typeof(SurveysController)
            .GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.NotNull(loggerField);
        Assert.Equal(context, contextField.GetValue(controller1));
        Assert.Equal(context, contextField.GetValue(controller2));
        Assert.Equal(logger, loggerField.GetValue(controller1));
        Assert.Equal(logger, loggerField.GetValue(controller2));
    }
}

