using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class EnrollmentsControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IEnrollmentService> _mockEnrollmentService;
    private readonly Mock<IScheduleConflictService> _mockScheduleConflictService;
    private readonly Mock<INotificationService> _mockNotificationService;
    private readonly Mock<ILogger<EnrollmentsController>> _mockLogger;

    public EnrollmentsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mockEnrollmentService = new Mock<IEnrollmentService>();
        _mockScheduleConflictService = new Mock<IScheduleConflictService>();
        _mockNotificationService = new Mock<INotificationService>();
        _mockLogger = new Mock<ILogger<EnrollmentsController>>();
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new EnrollmentsController(
            _context,
            _mockEnrollmentService.Object,
            _mockScheduleConflictService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EnrollmentsController(
            null!,
            _mockEnrollmentService.Object,
            _mockScheduleConflictService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullEnrollmentService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EnrollmentsController(
            _context,
            null!,
            _mockScheduleConflictService.Object,
            _mockNotificationService.Object,
            _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullScheduleConflictService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EnrollmentsController(
            _context,
            _mockEnrollmentService.Object,
            null!,
            _mockNotificationService.Object,
            _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullNotificationService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EnrollmentsController(
            _context,
            _mockEnrollmentService.Object,
            _mockScheduleConflictService.Object,
            null!,
            _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EnrollmentsController(
            _context,
            _mockEnrollmentService.Object,
            _mockScheduleConflictService.Object,
            _mockNotificationService.Object,
            null!));
    }
}
