using Microsoft.EntityFrameworkCore;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.Services;
using SmartCampus.API.DTOs;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class AttendanceServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IJwtService> _mockJwtService;

    public AttendanceServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mockJwtService = new Mock<IJwtService>();
    }

    // ========== Constructor Tests ==========

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitialize()
    {
        var service = new AttendanceService(_context, _mockJwtService.Object);
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AttendanceService(null!, _mockJwtService.Object));
    }

    [Fact]
    public void Constructor_WithNullJwtService_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new AttendanceService(_context, null!));
    }

    // ========== CalculateDistance & IsWithinGeofence Tests ==========

    [Fact]
    public void CalculateDistance_SameCoordinates_ShouldReturnZero()
    {
        var service = new AttendanceService(_context, _mockJwtService.Object);
        var distance = service.CalculateDistance(41.0082m, 28.9784m, 41.0082m, 28.9784m);
        Assert.Equal(0, distance);
    }

    [Fact]
    public void IsWithinGeofence_WhenInside_ShouldReturnTrue()
    {
        var service = new AttendanceService(_context, _mockJwtService.Object);
        // Using very close coordinates (approx less than 1m)
        bool result = service.IsWithinGeofence(41.0082m, 28.9784m, 41.0082m, 28.9784m, 10, 5);
        Assert.True(result);
    }

    [Fact]
    public void IsWithinGeofence_WhenOutside_ShouldReturnFalse()
    {
        var service = new AttendanceService(_context, _mockJwtService.Object);
        // Approx 1 degree latitude difference is ~111km, definitely outside 10 meter radius
        bool result = service.IsWithinGeofence(41.0082m, 28.9784m, 42.0082m, 28.9784m, 10, 5);
        Assert.False(result);
    }

    // ========== GenerateQrCode & ValidateQrCode Tests ==========

    [Fact]
    public void ValidateQrCode_WithValidToken_ShouldReturnTrue()
    {
        // Arrange
        var service = new AttendanceService(_context, _mockJwtService.Object);
        var sessionId = Guid.NewGuid();
        
        // Act
        var qrCode = service.GenerateQrCode(sessionId);
        var isValid = service.ValidateQrCode(qrCode, sessionId);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void ValidateQrCode_WithInvalidSessionId_ShouldReturnFalse()
    {
        // Arrange
        var service = new AttendanceService(_context, _mockJwtService.Object);
        var sessionId = Guid.NewGuid();
        var otherSessionId = Guid.NewGuid();
        
        // Act
        var qrCode = service.GenerateQrCode(sessionId);
        var isValid = service.ValidateQrCode(qrCode, otherSessionId); // Validate against wrong session

        // Assert
        Assert.False(isValid);
    }

    [Fact]
    public void ValidateQrCode_WithInvalidBase64_ShouldReturnFalse()
    {
        // Arrange
        var service = new AttendanceService(_context, _mockJwtService.Object);
        var sessionId = Guid.NewGuid();
        
        // Act
        var isValid = service.ValidateQrCode("invalid-base64-string", sessionId);

        // Assert
        Assert.False(isValid);
    }
}
