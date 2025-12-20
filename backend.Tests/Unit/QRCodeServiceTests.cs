using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class QRCodeServiceTests
{
    private readonly Mock<ILogger<QRCodeService>> _mockLogger;
    private readonly QRCodeService _service;

    public QRCodeServiceTests()
    {
        _mockLogger = new Mock<ILogger<QRCodeService>>();
        _service = new QRCodeService(_mockLogger.Object);
    }

    [Fact]
    public void GenerateQRCode_ShouldReturnValidBase64String()
    {
        // Arrange
        var prefix = "TEST";
        var data = new Dictionary<string, object> { { "key", "value" } };

        // Act
        var result = _service.GenerateQRCode(prefix, data);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        
        // Verify it is valid Base64
        Span<byte> buffer = new byte[result.Length];
        Assert.True(Convert.TryFromBase64String(result, buffer, out _));
    }

    [Fact]
    public void GenerateSimpleQRCode_ShouldReturnLegacyFormat()
    {
        // Arrange
        var prefix = "TEST";

        // Act
        var result = _service.GenerateSimpleQRCode(prefix);

        // Assert
        Assert.NotNull(result);
        Assert.StartsWith("TEST-", result);
        Assert.Contains("-", result);
        
        // Format check: PREFIX-GUID
        var parts = result.Split('-');
        Assert.Equal(2, parts.Length);
        Assert.Equal("TEST", parts[0]);
        Assert.True(Guid.TryParse(parts[1], out _));
    }

    [Fact]
    public void ValidateQRCode_ValidNewFormat_ShouldReturnTrue()
    {
        // Arrange
        var prefix = "ATTD";
        var qrCode = _service.GenerateQRCode(prefix);

        // Act
        var result = _service.ValidateQRCode(qrCode, "ATTD");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateQRCode_ValidLegacyFormat_ShouldReturnTrue()
    {
        // Arrange
        var prefix = "MEAL";
        var qrCode = $"{prefix}-{Guid.NewGuid():N}";

        // Act
        var result = _service.ValidateQRCode(qrCode, "MEAL");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateQRCode_InvalidPrefix_ShouldReturnFalse()
    {
        // Arrange
        var qrCode = _service.GenerateQRCode("WRONG");

        // Act
        var result = _service.ValidateQRCode(qrCode, "RIGHT");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateQRCode_InvalidFormat_ShouldReturnFalse()
    {
        // Arrange
        var qrCode = "not-a-valid-qr-code";

        // Act
        var result = _service.ValidateQRCode(qrCode, "TEST");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ParseQRCode_ValidNewFormat_ShouldReturnData()
    {
        // Arrange
        var prefix = "EVENT";
        var inputData = new Dictionary<string, object> 
        { 
            { "eventId", "123" },
            { "role", "student" }
        };
        var qrCode = _service.GenerateQRCode(prefix, inputData);

        // Act
        var result = _service.ParseQRCode(qrCode);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(prefix, result.Prefix);
        Assert.False(result.IsLegacy);
        Assert.NotNull(result.Timestamp);
        
        // Check data JSON integration
        // Note: System.Text.Json deserializes numbers as JsonElement, strings as JsonElement or object.
        // The service uses JsonSerializer.Deserialize<Dictionary<string, object>> which creates JsonElement values.
        Assert.Contains("eventId", result.Data);
        Assert.Contains("role", result.Data);
    }

    [Fact]
    public void ParseQRCode_ValidLegacyFormat_ShouldReturnData()
    {
        // Arrange
        var prefix = "MEAL";
        var guid = Guid.NewGuid().ToString("N");
        var qrCode = $"{prefix}-{guid}";

        // Act
        var result = _service.ParseQRCode(qrCode);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(prefix, result.Prefix);
        Assert.True(result.IsLegacy);
        Assert.Equal(DateTime.MinValue, result.Timestamp);
        Assert.Contains("guid", result.Data);
        Assert.Equal(guid, result.Data["guid"].ToString());
    }

    [Fact]
    public void IsLegacyFormat_ValidLegacyString_ShouldReturnTrue()
    {
        // Arrange
        var qrCode = "MEAL-a1b2c3d4e5f607890123456789abcdef";

        // Act
        var result = _service.IsLegacyFormat(qrCode);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsLegacyFormat_NewFormatString_ShouldReturnFalse()
    {
        // Arrange
        var qrCode = _service.GenerateQRCode("TEST");

        // Act
        var result = _service.IsLegacyFormat(qrCode);

        // Assert
        Assert.False(result);
    }
}
