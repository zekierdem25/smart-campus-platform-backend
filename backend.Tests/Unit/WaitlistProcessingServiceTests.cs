using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using System;
using System.Reflection;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class WaitlistProcessingServiceTests
{
    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitialize()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var notificationService = new Mock<INotificationService>();
        var logger = new Mock<ILogger<WaitlistProcessingService>>();

        // Act
        var service = new WaitlistProcessingService(context, notificationService.Object, logger.Object);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_ShouldSetContextField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var notificationService = new Mock<INotificationService>();
        var logger = new Mock<ILogger<WaitlistProcessingService>>();

        // Act
        var service = new WaitlistProcessingService(context, notificationService.Object, logger.Object);

        // Assert
        var contextField = typeof(WaitlistProcessingService)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var contextValue = contextField?.GetValue(service);

        Assert.NotNull(contextField);
        Assert.Same(context, contextValue);
    }

    [Fact]
    public void Constructor_ShouldSetNotificationServiceField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var notificationService = new Mock<INotificationService>();
        var logger = new Mock<ILogger<WaitlistProcessingService>>();

        // Act
        var service = new WaitlistProcessingService(context, notificationService.Object, logger.Object);

        // Assert
        var notificationServiceField = typeof(WaitlistProcessingService)
            .GetField("_notificationService", BindingFlags.NonPublic | BindingFlags.Instance);
        var notificationServiceValue = notificationServiceField?.GetValue(service);

        Assert.NotNull(notificationServiceField);
        Assert.Same(notificationService.Object, notificationServiceValue);
    }

    [Fact]
    public void Constructor_ShouldSetLoggerField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var notificationService = new Mock<INotificationService>();
        var logger = new Mock<ILogger<WaitlistProcessingService>>();

        // Act
        var service = new WaitlistProcessingService(context, notificationService.Object, logger.Object);

        // Assert
        var loggerField = typeof(WaitlistProcessingService)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerValue = loggerField?.GetValue(service);

        Assert.NotNull(loggerField);
        Assert.Same(logger.Object, loggerValue);
    }

    [Fact]
    public void Constructor_WithDifferentInstances_ShouldSetCorrectFields()
    {
        // Arrange
        var context1 = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var context2 = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var notificationService1 = new Mock<INotificationService>();
        var notificationService2 = new Mock<INotificationService>();
        var logger1 = new Mock<ILogger<WaitlistProcessingService>>();
        var logger2 = new Mock<ILogger<WaitlistProcessingService>>();

        // Act
        var service1 = new WaitlistProcessingService(context1, notificationService1.Object, logger1.Object);
        var service2 = new WaitlistProcessingService(context2, notificationService2.Object, logger2.Object);

        // Assert
        var contextField = typeof(WaitlistProcessingService)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var notificationServiceField = typeof(WaitlistProcessingService)
            .GetField("_notificationService", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerField = typeof(WaitlistProcessingService)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.Same(context1, contextField?.GetValue(service1));
        Assert.Same(context2, contextField?.GetValue(service2));
        Assert.Same(notificationService1.Object, notificationServiceField?.GetValue(service1));
        Assert.Same(notificationService2.Object, notificationServiceField?.GetValue(service2));
        Assert.Same(logger1.Object, loggerField?.GetValue(service1));
        Assert.Same(logger2.Object, loggerField?.GetValue(service2));
    }
}

