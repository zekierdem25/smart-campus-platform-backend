using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Services;
using System.Reflection;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class ActivityLogsControllerTests
{
    private readonly Mock<IActivityLogService> _mockActivityLogService;

    public ActivityLogsControllerTests()
    {
        _mockActivityLogService = new Mock<IActivityLogService>();
    }

    [Fact]
    public void Constructor_WithValidService_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new ActivityLogsController(_mockActivityLogService.Object);

        // Assert
        Assert.NotNull(controller);
        
        // Reflection ile private field kontrolü (opsiyonel ama daha iyi doğrulama sağlar)
        var field = typeof(ActivityLogsController).GetField("_activityLogService", BindingFlags.NonPublic | BindingFlags.Instance);
        var value = field?.GetValue(controller);
        Assert.Same(_mockActivityLogService.Object, value);
    }

    [Fact]
    public void Constructor_WithNullService_ShouldThrowArgumentNullException()
    {
        // Arrange
        IActivityLogService? service = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ActivityLogsController(service!));
    }
}
