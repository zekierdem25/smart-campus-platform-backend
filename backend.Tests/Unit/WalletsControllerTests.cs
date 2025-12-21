using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using SmartCampus.API.Tests.Helpers;
using System.Reflection;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class WalletsControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IPaymentService> _mockPaymentService;
    private readonly Mock<ILogger<WalletsController>> _mockLogger;

    public WalletsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _mockPaymentService = new Mock<IPaymentService>();
        _mockLogger = MockServices.CreateMockLogger<WalletsController>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new WalletsController(_context, _mockPaymentService.Object, _mockLogger.Object);

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
        var paymentService = new Mock<IPaymentService>().Object;
        var logger = MockServices.CreateMockLogger<WalletsController>().Object;

        // Act
        var controller = new WalletsController(context, paymentService, logger);

        // Assert
        Assert.NotNull(controller);
        // Controller'ın field'ları private olduğu için reflection kullanarak kontrol edebiliriz
        var contextField = typeof(WalletsController)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var paymentServiceField = typeof(WalletsController)
            .GetField("_paymentService", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerField = typeof(WalletsController)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.NotNull(paymentServiceField);
        Assert.NotNull(loggerField);

        Assert.Equal(context, contextField.GetValue(controller));
        Assert.Equal(paymentService, paymentServiceField.GetValue(controller));
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
        var paymentService = new Mock<IPaymentService>().Object;
        var logger = MockServices.CreateMockLogger<WalletsController>().Object;

        // Act
        var controller1 = new WalletsController(context1, paymentService, logger);
        var controller2 = new WalletsController(context2, paymentService, logger);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(WalletsController)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.Equal(context1, contextField.GetValue(controller1));
        Assert.Equal(context2, contextField.GetValue(controller2));
        Assert.NotEqual(contextField.GetValue(controller1), contextField.GetValue(controller2));
    }

    [Fact]
    public void Constructor_WithDifferentPaymentServiceInstances_ShouldSetCorrectly()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options
        );
        var paymentService1 = new Mock<IPaymentService>().Object;
        var paymentService2 = new Mock<IPaymentService>().Object;
        var logger = MockServices.CreateMockLogger<WalletsController>().Object;

        // Act
        var controller1 = new WalletsController(context, paymentService1, logger);
        var controller2 = new WalletsController(context, paymentService2, logger);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var paymentServiceField = typeof(WalletsController)
            .GetField("_paymentService", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(paymentServiceField);
        Assert.Equal(paymentService1, paymentServiceField.GetValue(controller1));
        Assert.Equal(paymentService2, paymentServiceField.GetValue(controller2));
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
        var paymentService = new Mock<IPaymentService>().Object;
        var logger1 = MockServices.CreateMockLogger<WalletsController>().Object;
        var logger2 = MockServices.CreateMockLogger<WalletsController>().Object;

        // Act
        var controller1 = new WalletsController(context, paymentService, logger1);
        var controller2 = new WalletsController(context, paymentService, logger2);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var loggerField = typeof(WalletsController)
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
        var paymentService = new Mock<IPaymentService>().Object;
        var logger = MockServices.CreateMockLogger<WalletsController>().Object;

        // Act
        var controller1 = new WalletsController(context, paymentService, logger);
        var controller2 = new WalletsController(context, paymentService, logger);

        // Assert
        Assert.NotNull(controller1);
        Assert.NotNull(controller2);

        var contextField = typeof(WalletsController)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var paymentServiceField = typeof(WalletsController)
            .GetField("_paymentService", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerField = typeof(WalletsController)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(contextField);
        Assert.NotNull(paymentServiceField);
        Assert.NotNull(loggerField);
        Assert.Equal(context, contextField.GetValue(controller1));
        Assert.Equal(context, contextField.GetValue(controller2));
        Assert.Equal(paymentService, paymentServiceField.GetValue(controller1));
        Assert.Equal(paymentService, paymentServiceField.GetValue(controller2));
        Assert.Equal(logger, loggerField.GetValue(controller1));
        Assert.Equal(logger, loggerField.GetValue(controller2));
    }
}

