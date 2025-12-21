using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class PaymentServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly Mock<ILogger<PaymentService>> _mockLogger;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly PaymentService _service;

    public PaymentServiceTests()
    {
        // Setup InMemory Database
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);

        // Setup Mocks
        _mockConfiguration = new Mock<IConfiguration>();
        _mockLogger = new Mock<ILogger<PaymentService>>();
        _mockEmailService = new Mock<IEmailService>();

        // Setup basic config
        var paymentSection = new Mock<IConfigurationSection>();
        paymentSection.Setup(x => x["Provider"]).Returns("Simulation");
        paymentSection.Setup(x => x["TestMode"]).Returns("true");
        _mockConfiguration.Setup(x => x.GetSection("Payment")).Returns(paymentSection.Object);

        _service = new PaymentService(_context, _mockConfiguration.Object, _mockLogger.Object, _mockEmailService.Object);
    }

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitialize()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var configuration = new Mock<IConfiguration>();
        var logger = new Mock<ILogger<PaymentService>>();
        var emailService = new Mock<IEmailService>();

        // Act
        var service = new PaymentService(context, configuration.Object, logger.Object, emailService.Object);

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
        var configuration = new Mock<IConfiguration>();
        var logger = new Mock<ILogger<PaymentService>>();
        var emailService = new Mock<IEmailService>();

        // Act
        var service = new PaymentService(context, configuration.Object, logger.Object, emailService.Object);

        // Assert
        var contextField = typeof(PaymentService)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var contextValue = contextField?.GetValue(service);

        Assert.NotNull(contextField);
        Assert.Same(context, contextValue);
    }

    [Fact]
    public void Constructor_ShouldSetConfigurationField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var configuration = new Mock<IConfiguration>();
        var logger = new Mock<ILogger<PaymentService>>();
        var emailService = new Mock<IEmailService>();

        // Act
        var service = new PaymentService(context, configuration.Object, logger.Object, emailService.Object);

        // Assert
        var configurationField = typeof(PaymentService)
            .GetField("_configuration", BindingFlags.NonPublic | BindingFlags.Instance);
        var configurationValue = configurationField?.GetValue(service);

        Assert.NotNull(configurationField);
        Assert.Same(configuration.Object, configurationValue);
    }

    [Fact]
    public void Constructor_ShouldSetLoggerField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var configuration = new Mock<IConfiguration>();
        var logger = new Mock<ILogger<PaymentService>>();
        var emailService = new Mock<IEmailService>();

        // Act
        var service = new PaymentService(context, configuration.Object, logger.Object, emailService.Object);

        // Assert
        var loggerField = typeof(PaymentService)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerValue = loggerField?.GetValue(service);

        Assert.NotNull(loggerField);
        Assert.Same(logger.Object, loggerValue);
    }

    [Fact]
    public void Constructor_ShouldSetEmailServiceField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var configuration = new Mock<IConfiguration>();
        var logger = new Mock<ILogger<PaymentService>>();
        var emailService = new Mock<IEmailService>();

        // Act
        var service = new PaymentService(context, configuration.Object, logger.Object, emailService.Object);

        // Assert
        var emailServiceField = typeof(PaymentService)
            .GetField("_emailService", BindingFlags.NonPublic | BindingFlags.Instance);
        var emailServiceValue = emailServiceField?.GetValue(service);

        Assert.NotNull(emailServiceField);
        Assert.Same(emailService.Object, emailServiceValue);
    }

    [Fact]
    public async Task VerifyWebhookSignatureAsync_WithValidSignature_ShouldReturnTrue()
    {
        // Arrange
        var payload = "test payload";
        var webhookSecret = "test_webhook_secret_key_12345";
        
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Payment:Provider", "Simulation" },
            { "Payment:WebhookSecret", webhookSecret },
            { "Payment:TestMode", "true" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var service = new PaymentService(_context, configuration, _mockLogger.Object, _mockEmailService.Object);

        // Compute expected signature
        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);
        var expectedSignature = method!.Invoke(null, new object[] { payload, webhookSecret }) as string;

        // Act
        var result = await service.VerifyWebhookSignatureAsync(payload, expectedSignature!);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task VerifyWebhookSignatureAsync_WithInvalidSignature_ShouldReturnFalse()
    {
        // Arrange
        var payload = "test payload";
        var invalidSignature = "invalid_signature";
        
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Payment:Provider", "Simulation" },
            { "Payment:WebhookSecret", "test_webhook_secret_key_12345" },
            { "Payment:TestMode", "true" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var service = new PaymentService(_context, configuration, _mockLogger.Object, _mockEmailService.Object);

        // Act
        var result = await service.VerifyWebhookSignatureAsync(payload, invalidSignature);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task VerifyWebhookSignatureAsync_WithEmptySignatureInTestMode_ShouldReturnTrue()
    {
        // Arrange
        var payload = "test payload";
        
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Payment:Provider", "Simulation" },
            { "Payment:TestMode", "true" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var service = new PaymentService(_context, configuration, _mockLogger.Object, _mockEmailService.Object);

        // Act
        var result = await service.VerifyWebhookSignatureAsync(payload, "");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task VerifyWebhookSignatureAsync_WithNullSignatureInTestMode_ShouldReturnTrue()
    {
        // Arrange
        var payload = "test payload";
        
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Payment:Provider", "Simulation" },
            { "Payment:TestMode", "true" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var service = new PaymentService(_context, configuration, _mockLogger.Object, _mockEmailService.Object);

        // Act
        var result = await service.VerifyWebhookSignatureAsync(payload, null!);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task VerifyWebhookSignatureAsync_WithStripeProvider_ShouldReturnTrue()
    {
        // Arrange
        var payload = "test payload";
        var signature = "test signature";
        
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Payment:Provider", "Stripe" },
            { "Payment:TestMode", "false" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var service = new PaymentService(_context, configuration, _mockLogger.Object, _mockEmailService.Object);

        // Act
        var result = await service.VerifyWebhookSignatureAsync(payload, signature);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task VerifyWebhookSignatureAsync_WithPayTRProvider_ShouldReturnTrue()
    {
        // Arrange
        var payload = "test payload";
        var signature = "test signature";
        
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Payment:Provider", "PayTR" },
            { "Payment:TestMode", "false" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var service = new PaymentService(_context, configuration, _mockLogger.Object, _mockEmailService.Object);

        // Act
        var result = await service.VerifyWebhookSignatureAsync(payload, signature);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task VerifyWebhookSignatureAsync_WithException_ShouldReturnFalse()
    {
        // Arrange
        var payload = "test payload";
        var signature = "test signature";
        
        // Setup configuration to throw exception
        _mockConfiguration.Setup(x => x.GetSection("Payment")).Throws(new Exception("Configuration error"));

        var service = new PaymentService(_context, _mockConfiguration.Object, _mockLogger.Object, _mockEmailService.Object);

        // Act
        var result = await service.VerifyWebhookSignatureAsync(payload, signature);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GenerateSimulationPaymentUrl_WithValidParameters_ShouldReturnCorrectUrl()
    {
        // Arrange
        var sessionId = "test_session_123";
        var amount = 100.50m;
        var userId = Guid.NewGuid();
        var frontendUrl = "https://example.com";
        
        _mockConfiguration.Setup(x => x["Frontend:Url"]).Returns(frontendUrl);

        var service = new PaymentService(_context, _mockConfiguration.Object, _mockLogger.Object, _mockEmailService.Object);

        var method = typeof(PaymentService)
            .GetMethod("GenerateSimulationPaymentUrl", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(service, new object[] { sessionId, amount, userId }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains(frontendUrl, result);
        Assert.Contains(sessionId, result);
        Assert.Contains(amount.ToString(), result);
        Assert.Contains(userId.ToString(), result);
        Assert.Contains("mode=simulation", result);
    }

    [Fact]
    public void GenerateSimulationPaymentUrl_WithNullFrontendUrl_ShouldUseDefaultUrl()
    {
        // Arrange
        var sessionId = "test_session_123";
        var amount = 100.50m;
        var userId = Guid.NewGuid();
        var defaultUrl = "http://localhost:3000";
        
        _mockConfiguration.Setup(x => x["Frontend:Url"]).Returns((string?)null);

        var service = new PaymentService(_context, _mockConfiguration.Object, _mockLogger.Object, _mockEmailService.Object);

        var method = typeof(PaymentService)
            .GetMethod("GenerateSimulationPaymentUrl", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(service, new object[] { sessionId, amount, userId }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.Contains(defaultUrl, result);
    }

    [Fact]
    public void GenerateSimulationPaymentUrl_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(PaymentService)
            .GetMethod("GenerateSimulationPaymentUrl", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    [Fact]
    public void ComputeHmacSha256_WithValidData_ShouldReturnBase64String()
    {
        // Arrange
        var data = "test data";
        var key = "test key";

        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Act
        var result = method!.Invoke(null, new object[] { data, key }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        // Base64 string should not contain spaces or special characters (except +, /, =)
        Assert.DoesNotContain(" ", result);
    }

    [Fact]
    public void ComputeHmacSha256_WithSameDataAndKey_ShouldReturnSameSignature()
    {
        // Arrange
        var data = "test data";
        var key = "test key";

        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Act
        var result1 = method!.Invoke(null, new object[] { data, key }) as string;
        var result2 = method!.Invoke(null, new object[] { data, key }) as string;

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void ComputeHmacSha256_WithDifferentData_ShouldReturnDifferentSignature()
    {
        // Arrange
        var data1 = "test data 1";
        var data2 = "test data 2";
        var key = "test key";

        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Act
        var result1 = method!.Invoke(null, new object[] { data1, key }) as string;
        var result2 = method!.Invoke(null, new object[] { data2, key }) as string;

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotEqual(result1, result2);
    }

    [Fact]
    public void ComputeHmacSha256_WithDifferentKey_ShouldReturnDifferentSignature()
    {
        // Arrange
        var data = "test data";
        var key1 = "test key 1";
        var key2 = "test key 2";

        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Act
        var result1 = method!.Invoke(null, new object[] { data, key1 }) as string;
        var result2 = method!.Invoke(null, new object[] { data, key2 }) as string;

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotEqual(result1, result2);
    }

    [Fact]
    public void ComputeHmacSha256_WithEmptyData_ShouldReturnSignature()
    {
        // Arrange
        var data = "";
        var key = "test key";

        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Act
        var result = method!.Invoke(null, new object[] { data, key }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void ComputeHmacSha256_WithEmptyKey_ShouldReturnSignature()
    {
        // Arrange
        var data = "test data";
        var key = "";

        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Act
        var result = method!.Invoke(null, new object[] { data, key }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void ComputeHmacSha256_IsStaticMethod_ShouldBeStatic()
    {
        // Arrange
        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsStatic);
    }

    [Fact]
    public void ComputeHmacSha256_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    [Fact]
    public void ComputeHmacSha256_WithLongData_ShouldReturnSignature()
    {
        // Arrange
        var data = new string('A', 10000);
        var key = "test key";

        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Act
        var result = method!.Invoke(null, new object[] { data, key }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void ComputeHmacSha256_WithSpecialCharacters_ShouldReturnSignature()
    {
        // Arrange
        var data = "test data with special chars: !@#$%^&*()_+-=[]{}|;':\",./<>?";
        var key = "test key with special: !@#$%^&*()";

        var method = typeof(PaymentService)
            .GetMethod("ComputeHmacSha256", BindingFlags.NonPublic | BindingFlags.Static);

        // Act
        var result = method!.Invoke(null, new object[] { data, key }) as string;

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
