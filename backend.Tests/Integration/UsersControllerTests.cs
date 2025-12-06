using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SmartCampus.API.DTOs;

namespace SmartCampus.API.Tests.Integration;

public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UsersControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<string> GetAuthTokenAsync(string email = "zeki.erdem@smartcampus.com", string password = "Student123!")
    {
        var loginRequest = new LoginRequestDto { Email = email, Password = password };
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);
        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        return result?.AccessToken ?? string.Empty;
    }

    private async Task<string> GetAdminTokenAsync()
    {
        return await GetAuthTokenAsync("admin@smartcampus.com", "Admin123!");
    }

    #region GetCurrentUser Tests

    [Fact]
    public async Task GetCurrentUser_WithValidToken_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/users/me");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<UserResponseDto>>();
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("zeki.erdem@smartcampus.com", result.Data.Email);
    }

    [Fact]
    public async Task GetCurrentUser_WithoutToken_ShouldReturn401Unauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await _client.GetAsync("/api/v1/users/me");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCurrentUser_WithInvalidToken_ShouldReturn401Unauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

        // Act
        var response = await _client.GetAsync("/api/v1/users/me");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCurrentUser_StudentUser_ShouldIncludeStudentInfo()
    {
        // Arrange - using seed data
        var token = await GetAuthTokenAsync("zeki.erdem@smartcampus.com", "Student123!");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/users/me");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<UserResponseDto>>();
        Assert.NotNull(result?.Data?.StudentInfo);
        Assert.Equal("2021001", result.Data.StudentInfo.StudentNumber);
    }

    [Fact]
    public async Task GetCurrentUser_FacultyUser_ShouldIncludeFacultyInfo()
    {
        // Arrange - using seed data
        var token = await GetAuthTokenAsync("mehmet.sevri@smartcampus.com", "Faculty123!");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/users/me");

        // Assert
        var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<UserResponseDto>>();
        Assert.NotNull(result?.Data?.FacultyInfo);
        Assert.Equal("F001", result.Data.FacultyInfo.EmployeeNumber);
    }

    #endregion

    #region UpdateProfile Tests

    [Fact]
    public async Task UpdateProfile_WithValidData_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var request = new UpdateProfileRequestDto
        {
            FirstName = "Updated",
            LastName = "User",
            Phone = "5551234567"
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/v1/users/me", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<UserResponseDto>>();
        Assert.True(result?.Success);
    }

    [Fact]
    public async Task UpdateProfile_WithoutToken_ShouldReturn401Unauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = null;
        var request = new UpdateProfileRequestDto { FirstName = "Test" };

        // Act
        var response = await _client.PutAsJsonAsync("/api/v1/users/me", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    #endregion

    #region ChangePassword Tests

    [Fact]
    public async Task ChangePassword_WithValidData_ShouldReturn200OK()
    {
        // Arrange - using seed data (Ayse Yilmaz)
        var token = await GetAuthTokenAsync("ayse.yilmaz@smartcampus.com", "Faculty123!");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var request = new ChangePasswordRequestDto
        {
            CurrentPassword = "Faculty123!",
            NewPassword = "NewFaculty123!",
            ConfirmPassword = "NewFaculty123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users/me/change-password", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ChangePassword_WithWrongCurrentPassword_ShouldReturn400BadRequest()
    {
        // Arrange
        var token = await GetAuthTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var request = new ChangePasswordRequestDto
        {
            CurrentPassword = "WrongPassword!",
            NewPassword = "NewPassword123!",
            ConfirmPassword = "NewPassword123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users/me/change-password", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ChangePassword_WithoutToken_ShouldReturn401Unauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = null;
        var request = new ChangePasswordRequestDto
        {
            CurrentPassword = "Test123!",
            NewPassword = "NewTest123!",
            ConfirmPassword = "NewTest123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users/me/change-password", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    #endregion

    #region GetUsers (Admin) Tests

    [Fact]
    public async Task GetUsers_WithAdminToken_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/users");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserListResponseDto>();
        Assert.NotNull(result);
        Assert.True(result.Users.Count > 0);
    }

    [Fact]
    public async Task GetUsers_WithNonAdminToken_ShouldReturn403Forbidden()
    {
        // Arrange
        var token = await GetAuthTokenAsync(); // Student token
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/users");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetUsers_WithPagination_ShouldRespectLimits()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/users?page=1&limit=2");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserListResponseDto>();
        Assert.NotNull(result);
        Assert.True(result.Users.Count <= 2);
    }

    [Fact]
    public async Task GetUsers_WithRoleFilter_ShouldFilterByRole()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/users?role=Student");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserListResponseDto>();
        Assert.NotNull(result);
        Assert.All(result.Users, u => Assert.Equal("Student", u.Role));
    }

    [Fact]
    public async Task GetUsers_WithSearchTerm_ShouldFilterResults()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/users?search=student");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserListResponseDto>();
        Assert.NotNull(result);
    }

    #endregion

    #region GetDepartments Tests

    [Fact]
    public async Task GetDepartments_ShouldReturn200OK()
    {
        // Act - No authentication required
        var response = await _client.GetAsync("/api/v1/users/departments");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<List<DepartmentResponseDto>>>();
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Count > 0);
    }

    #endregion
}

