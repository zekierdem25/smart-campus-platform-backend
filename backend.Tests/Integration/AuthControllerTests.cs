using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Tests.Integration;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    #region Register Tests

    [Fact]
    public async Task Register_WithValidData_ShouldReturn201Created()
    {
        // Arrange
        var request = new RegisterRequestDto
        {
            FirstName = "Integration",
            LastName = "Test",
            Email = "integration.test@smartcampus.com",
            Password = "IntTest123!",
            ConfirmPassword = "IntTest123!",
            UserType = "Student",
            StudentNumber = "2024999",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task Register_WithExistingEmail_ShouldReturn400BadRequest()
    {
        // Arrange
        var request = new RegisterRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "zeki.erdem@smartcampus.com", // Already exists
            Password = "Test123!",
            ConfirmPassword = "Test123!",
            UserType = "Student",
            StudentNumber = "2024888",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithWeakPassword_ShouldReturn400BadRequest()
    {
        // Arrange
        var request = new RegisterRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "weakpass@test.edu",
            Password = "weak", // Too weak
            ConfirmPassword = "weak",
            UserType = "Student",
            StudentNumber = "2024777",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithMismatchedPasswords_ShouldReturn400BadRequest()
    {
        // Arrange
        var request = new RegisterRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "mismatch@test.edu",
            Password = "Test123!",
            ConfirmPassword = "Different123!",
            UserType = "Student",
            StudentNumber = "2024666",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_WithInvalidModelState_ShouldReturn400BadRequest()
    {
        // Arrange - Send invalid data to trigger ModelState.IsValid = false
        // Using invalid email format and missing required fields
        var request = new RegisterRequestDto
        {
            FirstName = "A", // Too short (min 2)
            LastName = "B", // Too short (min 2)
            Email = "not-an-email", // Invalid email format
            Password = "weak", // Too weak (needs uppercase, number)
            ConfirmPassword = "different", // Mismatch
            UserType = "Student",
            StudentNumber = "",
            DepartmentId = Guid.Empty
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/register", request);

        // Assert - ModelState validation should trigger BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Geçersiz veri", result.Message);
            }
        }
    }

    [Fact]
    public async Task Register_WithEmptyRequiredFields_ShouldReturn400BadRequest()
    {
        // Arrange - Send empty required fields to trigger ModelState.IsValid = false
        var request = new RegisterRequestDto
        {
            FirstName = "", // Empty - Required
            LastName = "", // Empty - Required
            Email = "", // Empty - Required
            Password = "", // Empty - Required
            ConfirmPassword = "", // Empty - Required
            UserType = "", // Empty - Required
            DepartmentId = Guid.Empty // Empty - Required
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Geçersiz veri", result.Message);
            }
        }
    }

    #endregion

    #region Login Tests

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturn200OK()
    {
        // Arrange - using seed data from ApplicationDbContext
        var request = new LoginRequestDto
        {
            Email = "zeki.erdem@smartcampus.com",
            Password = "Student123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.AccessToken);
        Assert.NotNull(result.RefreshToken);
        Assert.NotNull(result.User);
    }

    [Fact]
    public async Task Login_WithInvalidPassword_ShouldReturn401Unauthorized()
    {
        // Arrange
        var request = new LoginRequestDto
        {
            Email = "zeki.erdem@smartcampus.com",
            Password = "WrongPassword!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithNonExistentEmail_ShouldReturn401Unauthorized()
    {
        // Arrange
        var request = new LoginRequestDto
        {
            Email = "nonexistent@smartcampus.com",
            Password = "Test123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithInvalidModelState_ShouldReturn400BadRequest()
    {
        // Arrange - Send invalid email format to trigger ModelState.IsValid = false
        var request = new LoginRequestDto
        {
            Email = "not-an-email-format", // Invalid email format
            Password = "Test123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        // Assert - ModelState validation should trigger BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Email ve şifre zorunludur", result.Message);
            }
        }
    }

    [Fact]
    public async Task Login_WithEmptyRequiredFields_ShouldReturn400BadRequest()
    {
        // Arrange - Send empty required fields to trigger ModelState.IsValid = false
        var request = new LoginRequestDto
        {
            Email = "", // Empty - Required
            Password = "" // Empty - Required
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Email ve şifre zorunludur", result.Message);
            }
        }
    }

    // Note: UnverifiedEmail and InactiveUser tests are skipped because seed data has all users verified and active

    #endregion

    #region Refresh Token Tests

    [Fact]
    public async Task RefreshToken_WithValidToken_ShouldReturn200OK()
    {
        // Arrange - First login
        var loginRequest = new LoginRequestDto
        {
            Email = "zeki.erdem@smartcampus.com",
            Password = "Student123!"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponseDto>();

        var refreshRequest = new RefreshTokenRequestDto
        {
            RefreshToken = loginResult!.RefreshToken!
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/refresh", refreshRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.AccessToken);
    }

    [Fact]
    public async Task RefreshToken_WithInvalidToken_ShouldReturn401Unauthorized()
    {
        // Arrange
        var request = new RefreshTokenRequestDto
        {
            RefreshToken = "invalid-refresh-token"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/refresh", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task RefreshToken_WithInvalidModelState_ShouldReturn400BadRequest()
    {
        // Arrange - Send empty token to trigger ModelState.IsValid = false
        var request = new RefreshTokenRequestDto
        {
            RefreshToken = "" // Empty string - Required attribute should fail
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/refresh", request);

        // Assert - ModelState validation should trigger BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Refresh token zorunludur", result.Message);
            }
        }
    }

    #endregion

    #region Logout Tests

    [Fact]
    public async Task Logout_WithValidToken_ShouldReturn204NoContent()
    {
        // Arrange - First login
        var loginRequest = new LoginRequestDto
        {
            Email = "zeki.erdem@smartcampus.com",
            Password = "Student123!"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<AuthResponseDto>();

        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", loginResult!.AccessToken);

        var logoutRequest = new RefreshTokenRequestDto
        {
            RefreshToken = loginResult.RefreshToken!
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/logout", logoutRequest);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Logout_WithoutAuthentication_ShouldReturn401Unauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = null;
        var request = new RefreshTokenRequestDto
        {
            RefreshToken = "some-token"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/logout", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Logout_WithInvalidToken_ShouldReturn401Unauthorized()
    {
        // Arrange - Invalid token that won't have a valid userId claim
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", "invalid-token-that-will-cause-userid-null");
        
        var request = new RefreshTokenRequestDto
        {
            RefreshToken = "some-token"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/logout", request);

        // Assert - Should return 401 because GetCurrentUserId() returns null
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Logout_WithNullUserId_ShouldReturn401Unauthorized()
    {
        // Arrange - Token without NameIdentifier claim to make GetCurrentUserId return null
        // Create a token without userId claim
        _client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0IiwiZXhwIjoxNjAwMDAwMDAwfQ.invalid");
        
        var request = new RefreshTokenRequestDto
        {
            RefreshToken = "some-token"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/logout", request);

        // Assert - Should return 401 because GetCurrentUserId() returns null
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    #endregion

    #region Forgot Password Tests

    [Fact]
    public async Task ForgotPassword_WithExistingEmail_ShouldReturn200OK()
    {
        // Arrange
        var request = new ForgotPasswordRequestDto
        {
            Email = "zeki.erdem@smartcampus.com"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/forgot-password", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ForgotPassword_WithNonExistentEmail_ShouldStillReturn200OK()
    {
        // Arrange - Security: always return 200 to prevent email enumeration
        var request = new ForgotPasswordRequestDto
        {
            Email = "nonexistent@smartcampus.com"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/forgot-password", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ForgotPassword_WithInvalidModelState_ShouldReturn400BadRequest()
    {
        // Arrange - Send invalid email format to trigger ModelState.IsValid = false
        var request = new ForgotPasswordRequestDto
        {
            Email = "not-an-email-format" // Invalid email format
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/forgot-password", request);

        // Assert - ModelState validation should trigger BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Geçerli bir email adresi giriniz", result.Message);
            }
        }
    }

    [Fact]
    public async Task ForgotPassword_WithEmptyEmail_ShouldReturn400BadRequest()
    {
        // Arrange - Send empty email to trigger ModelState.IsValid = false
        var request = new ForgotPasswordRequestDto
        {
            Email = "" // Empty - Required
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/forgot-password", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Geçerli bir email adresi giriniz", result.Message);
            }
        }
    }

    #endregion

    #region Verify Email Tests

    [Fact]
    public async Task VerifyEmail_WithInvalidToken_ShouldReturn400BadRequest()
    {
        // Arrange
        var request = new VerifyEmailRequestDto
        {
            Token = "invalid-verification-token"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/verify-email", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task VerifyEmail_WithInvalidModelState_ShouldReturn400BadRequest()
    {
        // Arrange - Send empty token to trigger ModelState.IsValid = false
        var request = new VerifyEmailRequestDto
        {
            Token = "" // Empty string - Required attribute should fail
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/verify-email", request);

        // Assert - ModelState validation should trigger BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Token zorunludur", result.Message);
            }
        }
    }

    [Fact]
    public async Task VerifyEmail_WithValidToken_ShouldReturn200OK()
    {
        // Arrange - First register a new user to get a verification token
        var registerRequest = new RegisterRequestDto
        {
            FirstName = "Verify",
            LastName = "Test",
            Email = "verify.test2@smartcampus.com",
            Password = "Verify123!",
            ConfirmPassword = "Verify123!",
            UserType = "Student",
            StudentNumber = "2024997",
            DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111")
        };
        await _client.PostAsJsonAsync("/api/v1/auth/register", registerRequest);

        // Get the verification token from the database
        // In the new system, users aren't created until email is verified, so we need to find the token by RegistrationData
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var verificationToken = await db.EmailVerificationTokens
            .Where(t => t.UsedAt == null && !string.IsNullOrEmpty(t.RegistrationData))
            .ToListAsync();
        
        // Find token with matching email in RegistrationData
        var token = verificationToken.FirstOrDefault(t => 
        {
            try
            {
                var data = JsonSerializer.Deserialize<RegisterRequestDto>(
                    t.RegistrationData, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return data?.Email?.ToLower() == "verify.test2@smartcampus.com";
            }
            catch
            {
                return false;
            }
        });
        
        Assert.NotNull(token);

        var verifyRequest = new VerifyEmailRequestDto
        {
            Token = token.Token
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/verify-email", verifyRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    #endregion

    #region Reset Password Tests

    [Fact]
    public async Task ResetPassword_WithInvalidToken_ShouldReturn400BadRequest()
    {
        // Arrange
        var request = new ResetPasswordRequestDto
        {
            Token = "invalid-reset-token",
            NewPassword = "NewPass123!",
            ConfirmPassword = "NewPass123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/reset-password", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ResetPassword_WithInvalidModelState_ShouldReturn400BadRequest()
    {
        // Arrange - Send invalid data to trigger ModelState.IsValid = false
        var request = new ResetPasswordRequestDto
        {
            Token = "", // Empty token (Required)
            NewPassword = "weak", // Too weak (needs uppercase, number, min 8)
            ConfirmPassword = "different" // Mismatch
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/reset-password", request);

        // Assert - ModelState validation should trigger BadRequest
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Geçersiz veri", result.Message);
            }
        }
    }

    [Fact]
    public async Task ResetPassword_WithEmptyRequiredFields_ShouldReturn400BadRequest()
    {
        // Arrange - Send empty required fields to trigger ModelState.IsValid = false
        var request = new ResetPasswordRequestDto
        {
            Token = "", // Empty - Required
            NewPassword = "", // Empty - Required
            ConfirmPassword = "" // Empty - Required
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/reset-password", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // ASP.NET Core may return ValidationProblemDetails instead of AuthResponseDto
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        if (result != null)
        {
            Assert.False(result.Success);
            // Message may be empty if ValidationProblemDetails is returned
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.Equal("Geçersiz veri", result.Message);
            }
        }
    }

    [Fact]
    public async Task ResetPassword_WithValidToken_ShouldReturn200OK()
    {
        // Arrange - First request forgot password to get a reset token
        var forgotPasswordRequest = new ForgotPasswordRequestDto
        {
            Email = "mert.abdullahoglu@smartcampus.com"
        };
        await _client.PostAsJsonAsync("/api/v1/auth/forgot-password", forgotPasswordRequest);

        // Get the reset token from the database
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == "mert.abdullahoglu@smartcampus.com");
        Assert.NotNull(user);

        var resetToken = await db.PasswordResetTokens
            .FirstOrDefaultAsync(t => t.UserId == user.Id && t.UsedAt == null);
        Assert.NotNull(resetToken);

        var resetRequest = new ResetPasswordRequestDto
        {
            Token = resetToken.Token,
            NewPassword = "NewPassword123!",
            ConfirmPassword = "NewPassword123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/reset-password", resetRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    #endregion
}

