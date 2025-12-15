using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class AttendanceControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IAttendanceService> _mockAttendanceService;
    private readonly Mock<ISpoofingDetectionService> _mockSpoofingService;
    private readonly Mock<INotificationService> _mockNotificationService;
    private readonly Mock<ILogger<AttendanceController>> _mockLogger;
    private readonly Mock<IWebHostEnvironment> _mockEnvironment;

    public AttendanceControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);

        _mockAttendanceService = new Mock<IAttendanceService>();
        _mockSpoofingService = new Mock<ISpoofingDetectionService>();
        _mockNotificationService = new Mock<INotificationService>();
        _mockLogger = new Mock<ILogger<AttendanceController>>();
        _mockEnvironment = new Mock<IWebHostEnvironment>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new AttendanceController(
            _context,
            _mockAttendanceService.Object,
            _mockSpoofingService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object,
            _mockEnvironment.Object);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AttendanceController(
            null!,
            _mockAttendanceService.Object,
            _mockSpoofingService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object,
            _mockEnvironment.Object));
    }

    [Fact]
    public void Constructor_WithNullAttendanceService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AttendanceController(
            _context,
            null!,
            _mockSpoofingService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object,
            _mockEnvironment.Object));
    }

    [Fact]
    public void Constructor_WithNullSpoofingService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AttendanceController(
            _context,
            _mockAttendanceService.Object,
            null!,
            _mockNotificationService.Object,
            _mockLogger.Object,
            _mockEnvironment.Object));
    }

    [Fact]
    public void Constructor_WithNullNotificationService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AttendanceController(
            _context,
            _mockAttendanceService.Object,
            _mockSpoofingService.Object,
            null!,
            _mockLogger.Object,
            _mockEnvironment.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AttendanceController(
            _context,
            _mockAttendanceService.Object,
            _mockSpoofingService.Object,
            _mockNotificationService.Object,
            null!,
            _mockEnvironment.Object));
    }

    [Fact]
    public void Constructor_WithNullEnvironment_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AttendanceController(
            _context,
            _mockAttendanceService.Object,
            _mockSpoofingService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object,
            null!));
    }
}
