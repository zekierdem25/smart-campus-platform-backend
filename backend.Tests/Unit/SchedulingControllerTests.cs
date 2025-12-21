using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using SmartCampus.API.Tests.Helpers;
using System.Reflection;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class SchedulingControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<ISchedulingService> _mockSchedulingService;
    private readonly Mock<ILogger<SchedulingController>> _mockLogger;

    public SchedulingControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _mockSchedulingService = new Mock<ISchedulingService>();
        _mockLogger = MockServices.CreateMockLogger<SchedulingController>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new SchedulingController(_context, _mockSchedulingService.Object, _mockLogger.Object);

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
        var schedulingService = new Mock<ISchedulingService>().Object;
        var logger = MockServices.CreateMockLogger<SchedulingController>().Object;

        // Act
        var controller = new SchedulingController(context, schedulingService, logger);

        // Assert
        Assert.NotNull(controller);
        // Controller'ın field'ları private olduğu için reflection kullanarak kontrol edebiliriz
        var contextField = typeof(SchedulingController)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var schedulingServiceField = typeof(SchedulingController)
            .GetField("_schedulingService", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerField = typeof(SchedulingController)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.NotNull(schedulingServiceField);
        Assert.NotNull(loggerField);

        Assert.Equal(context, contextField.GetValue(controller));
        Assert.Equal(schedulingService, schedulingServiceField.GetValue(controller));
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
        var schedulingService = new Mock<ISchedulingService>().Object;
        var logger = MockServices.CreateMockLogger<SchedulingController>().Object;

        // Act
        var controller1 = new SchedulingController(context1, schedulingService, logger);
        var controller2 = new SchedulingController(context2, schedulingService, logger);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(SchedulingController)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.Equal(context1, contextField.GetValue(controller1));
        Assert.Equal(context2, contextField.GetValue(controller2));
        Assert.NotEqual(contextField.GetValue(controller1), contextField.GetValue(controller2));
    }

    [Fact]
    public void Constructor_WithDifferentSchedulingServiceInstances_ShouldSetCorrectly()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );
        var schedulingService1 = new Mock<ISchedulingService>().Object;
        var schedulingService2 = new Mock<ISchedulingService>().Object;
        var logger = MockServices.CreateMockLogger<SchedulingController>().Object;

        // Act
        var controller1 = new SchedulingController(context, schedulingService1, logger);
        var controller2 = new SchedulingController(context, schedulingService2, logger);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var schedulingServiceField = typeof(SchedulingController)
            .GetField("_schedulingService", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(schedulingServiceField);
        Assert.Equal(schedulingService1, schedulingServiceField.GetValue(controller1));
        Assert.Equal(schedulingService2, schedulingServiceField.GetValue(controller2));
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
        var schedulingService = new Mock<ISchedulingService>().Object;
        var logger1 = MockServices.CreateMockLogger<SchedulingController>().Object;
        var logger2 = MockServices.CreateMockLogger<SchedulingController>().Object;

        // Act
        var controller1 = new SchedulingController(context, schedulingService, logger1);
        var controller2 = new SchedulingController(context, schedulingService, logger2);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var loggerField = typeof(SchedulingController)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(loggerField);
        Assert.Equal(logger1, loggerField.GetValue(controller1));
        Assert.Equal(logger2, loggerField.GetValue(controller2));
    }

    [Fact]
    public void Constructor_WithSameDependencies_ShouldSetSameInstances()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );
        var schedulingService = new Mock<ISchedulingService>().Object;
        var logger = MockServices.CreateMockLogger<SchedulingController>().Object;

        // Act
        var controller1 = new SchedulingController(context, schedulingService, logger);
        var controller2 = new SchedulingController(context, schedulingService, logger);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(SchedulingController)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var schedulingServiceField = typeof(SchedulingController)
            .GetField("_schedulingService", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerField = typeof(SchedulingController)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.NotNull(schedulingServiceField);
        Assert.NotNull(loggerField);
        Assert.Equal(context, contextField.GetValue(controller1));
        Assert.Equal(context, contextField.GetValue(controller2));
        Assert.Equal(schedulingService, schedulingServiceField.GetValue(controller1));
        Assert.Equal(schedulingService, schedulingServiceField.GetValue(controller2));
        Assert.Equal(logger, loggerField.GetValue(controller1));
        Assert.Equal(logger, loggerField.GetValue(controller2));
    }

    [Theory]
    [InlineData(ScheduleDayOfWeek.Monday, "MO")]
    [InlineData(ScheduleDayOfWeek.Tuesday, "TU")]
    [InlineData(ScheduleDayOfWeek.Wednesday, "WE")]
    [InlineData(ScheduleDayOfWeek.Thursday, "TH")]
    [InlineData(ScheduleDayOfWeek.Friday, "FR")]
    public void GetDayAbbreviation_WithValidDayOfWeek_ShouldReturnCorrectAbbreviation(ScheduleDayOfWeek day, string expectedAbbreviation)
    {
        // Arrange
        var controller = new SchedulingController(_context, _mockSchedulingService.Object, _mockLogger.Object);
        var method = typeof(SchedulingController)
            .GetMethod("GetDayAbbreviation", BindingFlags.NonPublic | BindingFlags.Static);

        // Assert
        Assert.NotNull(method);

        // Act
        var result = method!.Invoke(null, new object[] { day }) as string;

        // Assert
        Assert.Equal(expectedAbbreviation, result);
    }

    [Fact]
    public void GetDayAbbreviation_WithInvalidDayOfWeek_ShouldReturnDefaultAbbreviation()
    {
        // Arrange
        var controller = new SchedulingController(_context, _mockSchedulingService.Object, _mockLogger.Object);
        var method = typeof(SchedulingController)
            .GetMethod("GetDayAbbreviation", BindingFlags.NonPublic | BindingFlags.Static);

        // Assert
        Assert.NotNull(method);

        // Act - Test with an invalid enum value (cast an int that's not in the enum)
        var invalidDay = (ScheduleDayOfWeek)999;
        var result = method!.Invoke(null, new object[] { invalidDay }) as string;

        // Assert - Should return default "MO" for invalid values
        Assert.Equal("MO", result);
    }

    [Fact]
    public void GetDayAbbreviation_IsStaticMethod_ShouldBeCallableWithoutInstance()
    {
        // Arrange
        var method = typeof(SchedulingController)
            .GetMethod("GetDayAbbreviation", BindingFlags.NonPublic | BindingFlags.Static);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsStatic);
    }

    [Fact]
    public void GetDayAbbreviation_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingController)
            .GetMethod("GetDayAbbreviation", BindingFlags.NonPublic | BindingFlags.Static);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }
}

