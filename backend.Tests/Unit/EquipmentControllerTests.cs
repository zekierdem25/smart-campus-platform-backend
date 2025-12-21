using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using SmartCampus.API.Tests.Helpers;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class EquipmentControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<ILogger<EquipmentController>> _mockLogger;
    private readonly Mock<INotificationService> _mockNotificationService;

    public EquipmentControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _mockLogger = MockServices.CreateMockLogger<EquipmentController>();
        _mockNotificationService = new Mock<INotificationService>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new EquipmentController(
            _context,
            _mockLogger.Object,
            _mockNotificationService.Object
        );

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
        var logger = MockServices.CreateMockLogger<EquipmentController>().Object;
        var notificationService = new Mock<INotificationService>().Object;

        // Act
        var controller = new EquipmentController(
            context,
            logger,
            notificationService
        );

        // Assert
        Assert.NotNull(controller);
        // Controller'ın field'ları private olduğu için reflection kullanarak kontrol edebiliriz
        var contextField = typeof(EquipmentController)
            .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var loggerField = typeof(EquipmentController)
            .GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var notificationServiceField = typeof(EquipmentController)
            .GetField("_notificationService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.NotNull(loggerField);
        Assert.NotNull(notificationServiceField);

        Assert.Equal(context, contextField.GetValue(controller));
        Assert.Equal(logger, loggerField.GetValue(controller));
        Assert.Equal(notificationService, notificationServiceField.GetValue(controller));
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
        var logger = MockServices.CreateMockLogger<EquipmentController>().Object;
        var notificationService = new Mock<INotificationService>().Object;

        // Act
        var controller1 = new EquipmentController(context1, logger, notificationService);
        var controller2 = new EquipmentController(context2, logger, notificationService);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(EquipmentController)
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
        var logger1 = MockServices.CreateMockLogger<EquipmentController>().Object;
        var logger2 = MockServices.CreateMockLogger<EquipmentController>().Object;
        var notificationService = new Mock<INotificationService>().Object;

        // Act
        var controller1 = new EquipmentController(context, logger1, notificationService);
        var controller2 = new EquipmentController(context, logger2, notificationService);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var loggerField = typeof(EquipmentController)
            .GetField("_logger", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(loggerField);
        Assert.Equal(logger1, loggerField.GetValue(controller1));
        Assert.Equal(logger2, loggerField.GetValue(controller2));
    }

    [Fact]
    public void Constructor_WithDifferentNotificationServiceInstances_ShouldSetCorrectly()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );
        var logger = MockServices.CreateMockLogger<EquipmentController>().Object;
        var notificationService1 = new Mock<INotificationService>().Object;
        var notificationService2 = new Mock<INotificationService>().Object;

        // Act
        var controller1 = new EquipmentController(context, logger, notificationService1);
        var controller2 = new EquipmentController(context, logger, notificationService2);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var notificationServiceField = typeof(EquipmentController)
            .GetField("_notificationService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        Assert.NotNull(notificationServiceField);
        Assert.Equal(notificationService1, notificationServiceField.GetValue(controller1));
        Assert.Equal(notificationService2, notificationServiceField.GetValue(controller2));
    }
}

