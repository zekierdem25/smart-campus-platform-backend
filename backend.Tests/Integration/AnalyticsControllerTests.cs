using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SmartCampus.API.DTOs;
using SmartCampus.API.Tests.Integration;

namespace SmartCampus.API.Tests.Integration;

public class AnalyticsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public AnalyticsControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    private async Task<string> GetAuthTokenAsync(string email, string password)
    {
        var loginRequest = new LoginRequestDto { Email = email, Password = password };
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);
        
        if (!response.IsSuccessStatusCode)
        {
             var error = await response.Content.ReadAsStringAsync();
             throw new Exception($"Login failed for {email}. Status: {response.StatusCode}, Error: {error}");
        }

        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        return result?.AccessToken ?? string.Empty;
    }

    private async Task<string> GetAdminTokenAsync()
    {
        return await GetAuthTokenAsync("admin@smartcampus.com", "Admin123!");
    }

    private async Task<string> GetFacultyTokenAsync()
    {
        return await GetAuthTokenAsync("mehmet.sevri@smartcampus.com", "Faculty123!");
    }

    private async Task<string> GetStudentTokenAsync()
    {
        return await GetAuthTokenAsync("zeki.erdem@smartcampus.com", "Student123!");
    }

    #region GET Dashboard Metrics

    [Fact]
    public async Task GetDashboard_AsAdmin_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/dashboard");

        // Assert
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<DashboardMetricsDto>();
        Assert.NotNull(result);
        // Counters should be non-negative (can be 0 if DB empty)
        Assert.True(result.TotalStudents >= 0);
        Assert.True(result.TotalCourses >= 0);
    }

    [Fact]
    public async Task GetDashboard_AsStudent_ShouldReturn403Forbidden()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/dashboard");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetDashboard_WithoutToken_ShouldReturn401Unauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/dashboard");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    #endregion

    #region GET Academic Performance

    [Fact]
    public async Task GetAcademicPerformance_AsAdmin_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/academic-performance?semester=Fall&year=2024");

        // Assert
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AcademicPerformanceMetricsDto>();
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAcademicPerformance_AsFaculty_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetFacultyTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/academic-performance");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAcademicPerformance_AsStudent_ShouldReturn403Forbidden()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/academic-performance");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    #endregion

    #region GET Attendance Analytics

    [Fact]
    public async Task GetAttendanceAnalytics_AsAdmin_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/attendance");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AttendanceAnalyticsDto>();
        Assert.NotNull(result);
    }

    #endregion

    #region GET Meal Usage Analytics

    [Fact]
    public async Task GetMealUsageAnalytics_AsAdmin_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/meal-usage");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<MealUsageAnalyticsDto>();
        Assert.NotNull(result);
    }

    #endregion

    #region GET Events Analytics

    [Fact]
    public async Task GetEventsAnalytics_AsAdmin_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/events");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<EventsAnalyticsDto>();
        Assert.NotNull(result);
    }

    #endregion

    #region Export Analytics

    [Fact]
    public async Task ExportAnalytics_AsAdmin_Excel_ShouldReturnFile()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/export/academic?format=excel");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", response.Content.Headers.ContentType?.MediaType);
        Assert.NotNull(response.Content.Headers.ContentDisposition?.FileName);
    }

    [Fact]
    public async Task ExportAnalytics_AsAdmin_Pdf_ShouldReturnFile()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/export/attendance?format=pdf");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/pdf", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task ExportAnalytics_AsAdmin_Csv_ShouldReturnFile()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/export/meal?format=csv");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("text/csv", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task ExportAnalytics_WithInvalidReportType_ShouldReturn400BadRequest()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/export/invalid-report?format=excel");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task ExportAnalytics_WithInvalidFormat_ShouldReturn400BadRequest()
    {
        // Arrange
        var token = await GetAdminTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/analytics/export/academic?format=invalid-format");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion
}

// DTO Classes needed for deserialization if not available in project yet
// If these exist in API project, these placeholders won't be needed or can be removed.
// Assuming they exist in SmartCampus.API.DTOs based on usage above.
