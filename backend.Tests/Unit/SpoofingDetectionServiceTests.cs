using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class SpoofingDetectionServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IAttendanceService> _mockAttendanceService;
    private readonly Mock<IConfiguration> _mockConfiguration;

    public SpoofingDetectionServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        
        _mockAttendanceService = new Mock<IAttendanceService>();
        _mockConfiguration = new Mock<IConfiguration>();
        
        // Setup configuration section for GetSection(key).Get<List<string>>()
        var mockSection = new Mock<IConfigurationSection>();
        mockSection.Setup(s => s.Value).Returns((string?)null); 
        _mockConfiguration.Setup(c => c.GetSection("CampusSettings:AllowedIpRanges")).Returns(mockSection.Object);
    }

    private SpoofingDetectionService CreateService(List<string>? allowedIps = null, bool bypassIpCheck = false)
    {
        // Setup IP ranges
        var mockIpSection = new Mock<IConfigurationSection>();
        
        if (allowedIps != null)
        {
            // This mocking is tricky for .Get<List<string>>() extension method
            // Instead, since the service uses ParseCampusNetworks which reads from config,
            // we might need to rely on the fact that Get<T>() binds from configuration.
            // However, Moq with extensions is hard.
            // Let's assume the service code reads: _configuration.GetSection(...).Get<List<string>>()
            // mocking extension methods is not directly possible with Moq.
            // WORKAROUND: Use InMemoryConfigurationSource or build IConfigurationRoot.
        }
        
        // Better approach for IConfiguration: build it for real
        var inMemorySettings = new Dictionary<string, string?>
        {
            {"CampusSettings:BypassIpCheck", bypassIpCheck.ToString()}
        };

        if (allowedIps != null)
        {
            for (int i = 0; i < allowedIps.Count; i++)
            {
                inMemorySettings[$"CampusSettings:AllowedIpRanges:{i}"] = allowedIps[i];
            }
        }
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        return new SpoofingDetectionService(_context, _mockAttendanceService.Object, configuration);
    }

    [Fact]
    public void Constructor_WithNullDependencies_ShouldThrowArgumentNullException()
    {
        var config = new ConfigurationBuilder().Build();
        Assert.Throws<ArgumentNullException>(() => new SpoofingDetectionService(null!, _mockAttendanceService.Object, config));
        Assert.Throws<ArgumentNullException>(() => new SpoofingDetectionService(_context, null!, config));
        Assert.Throws<ArgumentNullException>(() => new SpoofingDetectionService(_context, _mockAttendanceService.Object, null!));
    }

    [Theory]
    [InlineData("127.0.0.1", true)]
    [InlineData("::1", true)]
    [InlineData("192.168.1.5", true)]
    [InlineData("10.0.0.5", true)]
    public void IsCampusIp_LocalAddresses_ShouldReturnTrue(string ip, bool expected)
    {
        var service = CreateService(allowedIps: new List<string> { "193.140.0.0/16" });
        Assert.Equal(expected, service.IsCampusIp(ip));
    }

    [Fact]
    public void IsCampusIp_WhenNoConfig_ShouldReturnTrue()
    {
        var service = CreateService(allowedIps: null); // No restriction
        Assert.True(service.IsCampusIp("8.8.8.8"));
    }

    [Fact]
    public void IsCampusIp_WithConfig_ShouldValidateCorrectly()
    {
        var service = CreateService(allowedIps: new List<string> { "193.140.0.0/16" }); // YTU IP Range

        Assert.True(service.IsCampusIp("193.140.10.5")); // Inside
        Assert.False(service.IsCampusIp("8.8.8.8")); // Outside
    }

    [Fact]
    public async Task CheckForSpoofingAsync_MockLocation_ShouldReturnSuspicious()
    {
        var service = CreateService();
        var result = await service.CheckForSpoofingAsync(
            Guid.NewGuid(), 
            41.0m, 29.0m, 
            "127.0.0.1", 
            DateTime.UtcNow, 
            isMockLocation: true);

        Assert.True(result.IsSuspicious);
        Assert.Equal("MOCK_LOCATION_DETECTED", result.Reason);
    }

    [Fact]
    public async Task CheckForSpoofingAsync_IpNotCampus_ShouldReturnSuspicious()
    {
        // BypassIpCheck = false, Allowed = YTU
        var service = CreateService(allowedIps: new List<string> { "193.140.0.0/16" }, bypassIpCheck: false);
        
        var result = await service.CheckForSpoofingAsync(
            Guid.NewGuid(), 
            41.0m, 29.0m, 
            "8.8.8.8", // External IP
            DateTime.UtcNow, 
            isMockLocation: false);

        Assert.True(result.IsSuspicious);
        Assert.Equal("IP_NOT_CAMPUS", result.Reason);
    }

    [Fact]
    public async Task CheckForSpoofingAsync_ImpossibleVelocity_ShouldReturnSuspicious()
    {
        var service = CreateService(bypassIpCheck: true);
        var studentId = Guid.NewGuid();
        var now = DateTime.UtcNow;

        _mockAttendanceService.Setup(x => x.GetLastAttendanceRecordAsync(studentId))
            .ReturnsAsync(new AttendanceRecord 
            { 
                CheckInTime = now.AddMinutes(-2), // 2 minutes ago
                Latitude = 41.0m,
                Longitude = 29.0m
            });

        // 100km away in 2 minutes -> Impossible
        _mockAttendanceService.Setup(x => x.CalculateDistance(41.0m, 29.0m, 42.0m, 30.0m))
            .Returns(100000); // 100km

        var result = await service.CheckForSpoofingAsync(
            studentId, 
            42.0m, 30.0m, // Far away
            "127.0.0.1", 
            now, 
            isMockLocation: false);

        Assert.True(result.IsSuspicious);
        Assert.Equal("VELOCITY_IMPOSSIBLE", result.Reason);
    }

    [Fact]
    public async Task CheckForSpoofingAsync_StaticSensorDataOnMobile_ShouldReturnSuspicious()
    {
        var service = CreateService(bypassIpCheck: true);
        
        var result = await service.CheckForSpoofingAsync(
            Guid.NewGuid(), 
            41.0m, 29.0m, 
            "127.0.0.1", 
            DateTime.UtcNow, 
            isMockLocation: false,
            sensorData: new SensorDataDto { X = 0, Y = 0, Z = 0, Unavailable = false }, // Perfectly static
            userAgent: "Mozilla/5.0 (iPhone; CPU iPhone OS 15_0 like Mac OS X)" // Mobile
        );

        Assert.True(result.IsSuspicious);
        Assert.Equal("STATIC_SENSOR_DATA", result.Reason);
    }

    [Fact]
    public async Task CheckForSpoofingAsync_StaticSensorDataOnDesktop_ShouldNotReturnSuspicious()
    {
        var service = CreateService(bypassIpCheck: true);
        
        var result = await service.CheckForSpoofingAsync(
            Guid.NewGuid(), 
            41.0m, 29.0m, 
            "127.0.0.1", 
            DateTime.UtcNow, 
            isMockLocation: false,
            sensorData: new SensorDataDto { X = 0, Y = 0, Z = 0, Unavailable = false }, 
            userAgent: "Mozilla/5.0 (Windows NT 10.0; Win64; x64)" // Desktop
        );

        Assert.False(result.IsSuspicious);
    }
}
