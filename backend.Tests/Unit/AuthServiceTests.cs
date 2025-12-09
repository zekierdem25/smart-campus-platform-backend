using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using SmartCampus.API.Tests.Helpers;

namespace SmartCampus.API.Tests.Unit;

public class AuthServiceTests
{
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Mock<ILogger<AuthService>> _mockLogger;
    private readonly IJwtService _jwtService;
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

    public AuthServiceTests()
    {
        _mockEmailService = MockServices.CreateMockEmailService();
        _mockLogger = MockServices.CreateMockLogger<AuthService>();
        _configuration = MockServices.CreateMockConfiguration();
        _jwtService = new JwtService(_configuration);
    }

    private AuthService CreateAuthService(SmartCampus.API.Data.ApplicationDbContext context)
    {
        return new AuthService(
            context,
            _jwtService,
            _mockEmailService.Object,
            _configuration,
            _mockLogger.Object
        );
    }

    #region Register Tests

    [Fact]
    public async Task RegisterAsync_WithValidData_ShouldSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new RegisterRequestDto
        {
            FirstName = "New",
            LastName = "User",
            Email = "newuser@test.edu",
            Password = "NewUser123!",
            ConfirmPassword = "NewUser123!",
            UserType = "Student",
            StudentNumber = "2024001",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        Assert.True(result.Success);
        Assert.Contains("başarılı", result.Message.ToLower());
    }

    [Fact]
    public async Task RegisterAsync_WithExistingEmail_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new RegisterRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "student@test.edu", // Already exists
            Password = "Test123!",
            ConfirmPassword = "Test123!",
            UserType = "Student",
            StudentNumber = "2024002",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("zaten sistemde kayıtlı", result.Message.ToLower());
    }

    [Fact]
    public async Task RegisterAsync_WithInvalidDepartment_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new RegisterRequestDto
        {
            FirstName = "New",
            LastName = "User",
            Email = "newuser2@test.edu",
            Password = "NewUser123!",
            ConfirmPassword = "NewUser123!",
            UserType = "Student",
            StudentNumber = "2024003",
            DepartmentId = Guid.Parse("99999999-9999-9999-9999-999999999999") // Invalid
        };

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("geçersiz bölüm", result.Message.ToLower());
    }

    [Fact]
    public async Task RegisterAsync_WithExistingStudentNumber_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new RegisterRequestDto
        {
            FirstName = "New",
            LastName = "User",
            Email = "newuser3@test.edu",
            Password = "NewUser123!",
            ConfirmPassword = "NewUser123!",
            UserType = "Student",
            StudentNumber = "2021001", // Already exists
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("öğrenci numarası", result.Message.ToLower());
    }

    [Fact]
    public async Task RegisterAsync_AsFaculty_ShouldCreateFacultyRecord()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new RegisterRequestDto
        {
            FirstName = "New",
            LastName = "Faculty",
            Email = "newfaculty@test.edu",
            Password = "Faculty123!",
            ConfirmPassword = "Faculty123!",
            UserType = "Faculty",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        Assert.True(result.Success);
        _mockEmailService.Verify(
            x => x.SendEmailVerificationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);
    }

    #endregion

    #region Login Tests

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ShouldSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new LoginRequestDto
        {
            Email = "student@test.edu",
            Password = "Student123!"
        };

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.AccessToken);
        Assert.NotNull(result.RefreshToken);
        Assert.NotNull(result.User);
        Assert.Equal("student@test.edu", result.User.Email);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidEmail_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new LoginRequestDto
        {
            Email = "nonexistent@test.edu",
            Password = "Test123!"
        };

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("hatalı", result.Message.ToLower());
    }

    [Fact]
    public async Task LoginAsync_WithInvalidPassword_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new LoginRequestDto
        {
            Email = "student@test.edu",
            Password = "WrongPassword123!"
        };

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("hatalı", result.Message.ToLower());
    }

    [Fact]
    public async Task LoginAsync_WithUnverifiedEmail_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new LoginRequestDto
        {
            Email = "unverified@test.edu",
            Password = "Test123!"
        };

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("doğrulama", result.Message.ToLower());
    }

    [Fact]
    public async Task LoginAsync_WithInactiveUser_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var request = new LoginRequestDto
        {
            Email = "inactive@test.edu",
            Password = "Test123!"
        };

        // Act
        var result = await authService.LoginAsync(request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("deaktif", result.Message.ToLower());
    }

    #endregion

    #region Token Tests

    [Fact]
    public async Task RefreshTokenAsync_WithValidToken_ShouldSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        
        // First login to get a refresh token
        var loginResult = await authService.LoginAsync(new LoginRequestDto
        {
            Email = "student@test.edu",
            Password = "Student123!"
        });

        // Act
        var result = await authService.RefreshTokenAsync(loginResult.RefreshToken!);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.AccessToken);
        Assert.NotNull(result.RefreshToken);
    }

    [Fact]
    public async Task RefreshTokenAsync_WithInvalidToken_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);

        // Act
        var result = await authService.RefreshTokenAsync("invalid-refresh-token");

        // Assert
        Assert.False(result.Success);
        Assert.Contains("geçersiz", result.Message.ToLower());
    }

    [Fact]
    public async Task LogoutAsync_ShouldInvalidateToken()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");
        
        // First login
        var loginResult = await authService.LoginAsync(new LoginRequestDto
        {
            Email = "student@test.edu",
            Password = "Student123!"
        });

        // Act
        await authService.LogoutAsync(userId, loginResult.RefreshToken!);

        // Try to use the token
        var refreshResult = await authService.RefreshTokenAsync(loginResult.RefreshToken!);

        // Assert
        Assert.False(refreshResult.Success);
    }

    #endregion

    #region Password Reset Tests

    [Fact]
    public async Task ForgotPasswordAsync_WithExistingEmail_ShouldSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);

        // Act
        var result = await authService.ForgotPasswordAsync("student@test.edu");

        // Assert
        Assert.True(result.Success);
        _mockEmailService.Verify(
            x => x.SendPasswordResetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
            Times.Once);
    }

    [Fact]
    public async Task ForgotPasswordAsync_WithNonExistentEmail_ShouldStillSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);

        // Act
        var result = await authService.ForgotPasswordAsync("nonexistent@test.edu");

        // Assert - Should succeed for security reasons
        Assert.True(result.Success);
    }

    [Fact]
    public async Task VerifyEmailAsync_WithValidToken_ShouldSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);
        
        // Create a verification token with RegistrationData (new system)
        var registrationData = new RegisterRequestDto
        {
            FirstName = "Verify",
            LastName = "Test",
            Email = "verify.test@test.edu",
            Password = "Verify123!",
            ConfirmPassword = "Verify123!",
            UserType = "Student",
            StudentNumber = "2024998",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };
        var registrationDataJson = System.Text.Json.JsonSerializer.Serialize(registrationData);
        
        var token = new EmailVerificationToken
        {
            UserId = null, // No user yet, email not verified
            Token = "test-verification-token",
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            RegistrationData = registrationDataJson
        };
        context.EmailVerificationTokens.Add(token);
        await context.SaveChangesAsync();

        // Act
        var result = await authService.VerifyEmailAsync("test-verification-token");

        // Assert
        Assert.True(result.Success);
        Assert.Contains("doğrulandı", result.Message.ToLower());
    }

    [Fact]
    public async Task VerifyEmailAsync_WithInvalidToken_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var authService = CreateAuthService(context);

        // Act
        var result = await authService.VerifyEmailAsync("invalid-token");

        // Assert
        Assert.False(result.Success);
        Assert.Contains("geçersiz", result.Message.ToLower());
    }

    #endregion
}

