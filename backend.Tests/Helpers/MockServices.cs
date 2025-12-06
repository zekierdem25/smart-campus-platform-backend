using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Models;
using SmartCampus.API.Services;

namespace SmartCampus.API.Tests.Helpers;

public static class MockServices
{
    public static IConfiguration CreateMockConfiguration()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "JWT:SecretKey", "super-secret-test-key-min-32-characters-long-for-testing" },
            { "JWT:Issuer", "TestIssuer" },
            { "JWT:Audience", "TestAudience" },
            { "JWT:AccessTokenExpirationMinutes", "15" },
            { "JWT:RefreshTokenExpirationDays", "7" },
            { "Frontend:Url", "http://localhost:3000" },
            { "FileUpload:MaxFileSize", "5242880" },
            { "FileUpload:AllowedExtensions", "jpg,jpeg,png" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    public static Mock<IEmailService> CreateMockEmailService()
    {
        var mockEmailService = new Mock<IEmailService>();
        
        mockEmailService
            .Setup(x => x.SendEmailVerificationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        mockEmailService
            .Setup(x => x.SendPasswordResetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        mockEmailService
            .Setup(x => x.SendWelcomeEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        return mockEmailService;
    }

    public static Mock<IJwtService> CreateMockJwtService()
    {
        var mockJwtService = new Mock<IJwtService>();

        mockJwtService
            .Setup(x => x.GenerateAccessToken(It.IsAny<User>()))
            .Returns("mock-access-token");

        mockJwtService
            .Setup(x => x.GenerateRefreshToken())
            .Returns("mock-refresh-token");

        mockJwtService
            .Setup(x => x.ValidateAccessToken(It.IsAny<string>()))
            .Returns(Guid.NewGuid());

        return mockJwtService;
    }

    public static Mock<ILogger<T>> CreateMockLogger<T>()
    {
        return new Mock<ILogger<T>>();
    }
}

