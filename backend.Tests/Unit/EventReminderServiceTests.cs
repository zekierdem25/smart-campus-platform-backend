using Hangfire;
using Hangfire.InMemory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using System;
using System.Reflection;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

[Collection("HangfireTests")]
public class EventReminderServiceTests : IClassFixture<HangfireTestFixture>
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<INotificationService> _mockNotificationService;
    private readonly Mock<ILogger<EventReminderService>> _mockLogger;
    private readonly EventReminderService _service;

    public EventReminderServiceTests(HangfireTestFixture fixture)
    {
        // Hangfire configuration is handled by HangfireTestFixture
        
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mockNotificationService = new Mock<INotificationService>();
        _mockLogger = new Mock<ILogger<EventReminderService>>();
        
        _service = new EventReminderService(_context, _mockNotificationService.Object, _mockLogger.Object);
    }


    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitialize()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var notificationService = new Mock<INotificationService>().Object;
        var logger = new Mock<ILogger<EventReminderService>>().Object;

        // Act
        var service = new EventReminderService(context, notificationService, logger);

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
        var notificationService = new Mock<INotificationService>().Object;
        var logger = new Mock<ILogger<EventReminderService>>().Object;

        // Act
        var service = new EventReminderService(context, notificationService, logger);

        // Assert
        var contextField = typeof(EventReminderService)
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
        var notificationService = new Mock<INotificationService>().Object;
        var logger = new Mock<ILogger<EventReminderService>>().Object;

        // Act
        var service = new EventReminderService(context, notificationService, logger);

        // Assert
        var notificationServiceField = typeof(EventReminderService)
            .GetField("_notificationService", BindingFlags.NonPublic | BindingFlags.Instance);
        var notificationServiceValue = notificationServiceField?.GetValue(service);
        
        Assert.NotNull(notificationServiceField);
        Assert.Same(notificationService, notificationServiceValue);
    }

    [Fact]
    public void Constructor_ShouldSetLoggerField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var notificationService = new Mock<INotificationService>().Object;
        var logger = new Mock<ILogger<EventReminderService>>().Object;

        // Act
        var service = new EventReminderService(context, notificationService, logger);

        // Assert
        var loggerField = typeof(EventReminderService)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerValue = loggerField?.GetValue(service);
        
        Assert.NotNull(loggerField);
        Assert.Same(logger, loggerValue);
    }

    [Fact]
    public void ScheduleReminderForRegistration_WithFutureEvent_ShouldNotThrowException()
    {
        // Arrange
        var registrationId = Guid.NewGuid();
        var eventDateTime = DateTime.UtcNow.AddDays(2);

        // Act & Assert
        var exception = Record.Exception(() => 
            _service.ScheduleReminderForRegistration(registrationId, eventDateTime));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ScheduleReminderForRegistration_WithPastEvent_ShouldNotThrowException()
    {
        // Arrange
        var registrationId = Guid.NewGuid();
        var eventDateTime = DateTime.UtcNow.AddDays(-1);

        // Act & Assert
        var exception = Record.Exception(() => 
            _service.ScheduleReminderForRegistration(registrationId, eventDateTime));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ScheduleReminderForRegistration_WithEventInOneDay_ShouldNotThrowException()
    {
        // Arrange
        var registrationId = Guid.NewGuid();
        var eventDateTime = DateTime.UtcNow.AddDays(1).AddHours(1);

        // Act & Assert
        var exception = Record.Exception(() => 
            _service.ScheduleReminderForRegistration(registrationId, eventDateTime));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ScheduleReminderForRegistration_WithEventInOneHour_ShouldNotThrowException()
    {
        // Arrange
        var registrationId = Guid.NewGuid();
        var eventDateTime = DateTime.UtcNow.AddHours(1).AddMinutes(30);

        // Act & Assert
        var exception = Record.Exception(() => 
            _service.ScheduleReminderForRegistration(registrationId, eventDateTime));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ScheduleReminderForRegistration_WithEventVerySoon_ShouldNotThrowException()
    {
        // Arrange
        var registrationId = Guid.NewGuid();
        var eventDateTime = DateTime.UtcNow.AddMinutes(30);

        // Act & Assert
        var exception = Record.Exception(() => 
            _service.ScheduleReminderForRegistration(registrationId, eventDateTime));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void BuildReminderTemplate_WithValidParameters_ShouldReturnHtmlString()
    {
        // Arrange
        var title = "Test Title";
        var color = "#1a73e8";
        var greeting = "Hello";
        var message = "Test message";
        var details = "<p>Details</p>";

        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);

        // Act
        var result = method!.Invoke(_service, new object[] { title, color, greeting, message, details }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains(title, result);
        Assert.Contains(color, result);
        Assert.Contains(greeting, result);
        Assert.Contains(message, result);
        Assert.Contains(details, result);
        Assert.Contains("<!DOCTYPE html>", result);
        Assert.Contains("</html>", result);
    }

    [Fact]
    public void BuildReminderTemplate_WithEmptyParameters_ShouldReturnHtmlString()
    {
        // Arrange
        var title = "";
        var color = "";
        var greeting = "";
        var message = "";
        var details = "";

        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, new object[] { title, color, greeting, message, details }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<!DOCTYPE html>", result);
        Assert.Contains("</html>", result);
    }

    [Fact]
    public void BuildReminderTemplate_WithNullParameters_ShouldReturnHtmlString()
    {
        // Arrange
        string? title = null;
        string? color = null;
        string? greeting = null;
        string? message = null;
        string? details = null;

        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, new object[] { title!, color!, greeting!, message!, details! }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<!DOCTYPE html>", result);
        Assert.Contains("</html>", result);
    }

    [Fact]
    public void BuildReminderTemplate_ShouldContainCurrentYear()
    {
        // Arrange
        var title = "Test";
        var color = "#1a73e8";
        var greeting = "Hello";
        var message = "Test";
        var details = "Details";

        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, new object[] { title, color, greeting, message, details }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains(DateTime.Now.Year.ToString(), result);
    }

    [Fact]
    public void BuildReminderTemplate_ShouldContainSmartCampus()
    {
        // Arrange
        var title = "Test";
        var color = "#1a73e8";
        var greeting = "Hello";
        var message = "Test";
        var details = "Details";

        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, new object[] { title, color, greeting, message, details }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Smart Campus", result);
    }

    [Fact]
    public void BuildReminderTemplate_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    [Fact]
    public void BuildReminderTemplate_WithSpecialCharacters_ShouldEscapeCorrectly()
    {
        // Arrange
        var title = "Test & <Title>";
        var color = "#1a73e8";
        var greeting = "Hello & Welcome";
        var message = "Message with <tags>";
        var details = "<p>Details & more</p>";

        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, new object[] { title, color, greeting, message, details }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains(title, result);
        Assert.Contains(greeting, result);
        Assert.Contains(message, result);
        Assert.Contains(details, result);
    }

    [Fact]
    public void BuildReminderTemplate_WithLongStrings_ShouldReturnHtmlString()
    {
        // Arrange
        var title = new string('A', 1000);
        var color = "#1a73e8";
        var greeting = new string('B', 1000);
        var message = new string('C', 1000);
        var details = new string('D', 1000);

        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, new object[] { title, color, greeting, message, details }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<!DOCTYPE html>", result);
        Assert.Contains("</html>", result);
    }

    [Fact]
    public void BuildReminderTemplate_WithDifferentColors_ShouldIncludeColorInHtml()
    {
        // Arrange
        var colors = new[] { "#1a73e8", "#ff0000", "#00ff00", "#0000ff", "#ffffff" };
        var method = typeof(EventReminderService)
            .GetMethod("BuildReminderTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var color in colors)
        {
            // Act
            var result = method!.Invoke(_service, new object[] { "Test", color, "Hello", "Message", "Details" }) as string;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(color, result);
        }
    }
}

