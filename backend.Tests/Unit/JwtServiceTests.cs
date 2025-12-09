using SmartCampus.API.Models;
using SmartCampus.API.Services;
using SmartCampus.API.Tests.Helpers;

namespace SmartCampus.API.Tests.Unit;

public class JwtServiceTests
{
    private readonly JwtService _jwtService;

    public JwtServiceTests()
    {
        var configuration = MockServices.CreateMockConfiguration();
        _jwtService = new JwtService(configuration);
    }

    [Fact]
    public void GenerateAccessToken_ShouldReturnValidToken()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = "test@test.edu",
            Role = UserRole.Student
        };

        // Act
        var token = _jwtService.GenerateAccessToken(user);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public void GenerateAccessToken_ShouldContainUserClaims()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "Test",
            LastName = "User",
            Email = "test@test.edu",
            Role = UserRole.Admin
        };

        // Act
        var token = _jwtService.GenerateAccessToken(user);
        var validatedUserId = _jwtService.ValidateAccessToken(token);

        // Assert
        Assert.NotNull(validatedUserId);
        Assert.Equal(userId, validatedUserId);
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnValidToken()
    {
        // Act
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Assert
        Assert.NotNull(refreshToken);
        Assert.NotEmpty(refreshToken);
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnUniqueTokens()
    {
        // Act
        var token1 = _jwtService.GenerateRefreshToken();
        var token2 = _jwtService.GenerateRefreshToken();

        // Assert
        Assert.NotEqual(token1, token2);
    }

    [Fact]
    public void ValidateAccessToken_WithValidToken_ShouldReturnUserId()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = "Test",
            LastName = "User",
            Email = "test@test.edu",
            Role = UserRole.Student
        };
        var token = _jwtService.GenerateAccessToken(user);

        // Act
        var result = _jwtService.ValidateAccessToken(token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result);
    }

    [Fact]
    public void ValidateAccessToken_WithInvalidToken_ShouldReturnNull()
    {
        // Arrange
        var invalidToken = "invalid-token";

        // Act
        var result = _jwtService.ValidateAccessToken(invalidToken);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ValidateRefreshToken_WithValidFormat_ShouldReturnTrue()
    {
        // Arrange
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Act
        var result = _jwtService.ValidateRefreshToken(refreshToken);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateRefreshToken_WithInvalidFormat_ShouldReturnFalse()
    {
        // Arrange
        var invalidToken = "invalid-token";

        // Act
        var result = _jwtService.ValidateRefreshToken(invalidToken);

        // Assert
        Assert.False(result);
    }
}

