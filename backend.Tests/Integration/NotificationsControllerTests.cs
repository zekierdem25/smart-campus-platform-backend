using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using SmartCampus.API.Models;
using SmartCampus.API.Controllers;
using SmartCampus.API.DTOs;
using SmartCampus.API.Tests.Integration;

namespace SmartCampus.API.Tests.Integration;

public class NotificationsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public NotificationsControllerTests(CustomWebApplicationFactory factory)
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

    private async Task<string> GetStudentTokenAsync()
    {
        return await GetAuthTokenAsync("zeki.erdem@smartcampus.com", "Student123!");
    }

    #region GET Notifications

    [Fact]
    public async Task GetNotifications_WithAuth_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/notifications");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<NotificationsResponse>();
        Assert.NotNull(result);
        Assert.NotNull(result.Notifications);
        Assert.NotNull(result.Pagination);
    }

    [Fact]
    public async Task GetNotifications_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/notifications?page=1&pageSize=5");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<NotificationsResponse>();
        Assert.NotNull(result);
        Assert.True(result.Notifications.Count <= 5);
        Assert.Equal(1, result.Pagination.Page);
        Assert.Equal(5, result.Pagination.PageSize);
    }

    [Fact]
    public async Task GetNotifications_WithCategoryFilter_ShouldReturnFiltered()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/notifications?category=Academic");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<NotificationsResponse>();
        Assert.NotNull(result);
        // All returned notifications should be Academic category
        Assert.All(result.Notifications, n => Assert.Equal("Academic", n.Category));
    }

    [Fact]
    public async Task GetNotifications_WithReadFilter_ShouldReturnFiltered()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/notifications?isRead=false");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<NotificationsResponse>();
        Assert.NotNull(result);
        // All returned notifications should be unread
        Assert.All(result.Notifications, n => Assert.False(n.IsRead));
    }

    [Fact]
    public async Task GetNotifications_WithoutAuth_ShouldReturn401Unauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await _client.GetAsync("/api/v1/notifications");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    #endregion

    #region GET Unread Count

    [Fact]
    public async Task GetUnreadCount_WithAuth_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/notifications/unread-count");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UnreadCountResponse>();
        Assert.NotNull(result);
        Assert.True(result.UnreadCount >= 0);
    }

    [Fact]
    public async Task GetUnreadCount_WithoutAuth_ShouldReturn401Unauthorized()
    {
        // Arrange
        _client.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await _client.GetAsync("/api/v1/notifications/unread-count");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    #endregion

    #region PUT Mark As Read

    [Fact]
    public async Task MarkAsRead_WithValidId_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // First, get a notification to mark as read
        var getResponse = await _client.GetAsync("/api/v1/notifications?isRead=false&pageSize=1");
        var notifications = await getResponse.Content.ReadFromJsonAsync<NotificationsResponse>();
        
        if (notifications?.Notifications.Count > 0)
        {
            var notificationId = notifications.Notifications[0].Id;

            // Act
            var response = await _client.PutAsync($"/api/v1/notifications/{notificationId}/read", null);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        else
        {
            // Skip test if no unread notifications
            Assert.True(true, "No unread notifications to test");
        }
    }

    [Fact]
    public async Task MarkAsRead_WithInvalidId_ShouldReturn404NotFound()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var invalidId = Guid.NewGuid();

        // Act
        var response = await _client.PutAsync($"/api/v1/notifications/{invalidId}/read", null);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region PUT Mark All As Read

    [Fact]
    public async Task MarkAllAsRead_WithAuth_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.PutAsync("/api/v1/notifications/mark-all-read", null);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    #endregion

    #region DELETE Notification

    [Fact]
    public async Task DeleteNotification_WithValidId_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // First, get a notification to delete
        var getResponse = await _client.GetAsync("/api/v1/notifications?pageSize=1");
        var notifications = await getResponse.Content.ReadFromJsonAsync<NotificationsResponse>();
        
        if (notifications?.Notifications.Count > 0)
        {
            var notificationId = notifications.Notifications[0].Id;

            // Act
            var response = await _client.DeleteAsync($"/api/v1/notifications/{notificationId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        else
        {
            // Skip test if no notifications
            Assert.True(true, "No notifications to test deletion");
        }
    }

    [Fact]
    public async Task DeleteNotification_WithInvalidId_ShouldReturn404NotFound()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var invalidId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"/api/v1/notifications/{invalidId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region GET Preferences

    [Fact]
    public async Task GetPreferences_WithAuth_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/notifications/preferences");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<PreferencesResponse>();
        Assert.NotNull(result);
        Assert.NotNull(result.Preferences);
        Assert.NotEmpty(result.Preferences);
    }

    #endregion

    #region PUT Update Preferences

    [Fact]
    public async Task UpdatePreferences_WithValidData_ShouldReturn200OK()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var updateDto = new UpdatePreferencesDto
        {
            Preferences = new List<PreferenceDto>
            {
                new PreferenceDto
                {
                    Category = "Academic",
                    EmailEnabled = true,
                    PushEnabled = true,
                    SmsEnabled = false
                }
            }
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/v1/notifications/preferences", updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdatePreferences_WithEmptyData_ShouldReturn400BadRequest()
    {
        // Arrange
        var token = await GetStudentTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var updateDto = new UpdatePreferencesDto
        {
            Preferences = new List<PreferenceDto>()
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/v1/notifications/preferences", updateDto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion
}

// Response DTOs for deserialization
public class NotificationsResponse
{
    public List<NotificationDto> Notifications { get; set; } = new();
    public PaginationDto Pagination { get; set; } = new();
}

public class NotificationDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? RelatedEntityId { get; set; }
    public string? RelatedEntityType { get; set; }
}

public class PaginationDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}

public class UnreadCountResponse
{
    public int UnreadCount { get; set; }
}

public class PreferencesResponse
{
    public List<PreferenceItemDto> Preferences { get; set; } = new();
}

public class PreferenceItemDto
{
    public string Category { get; set; } = string.Empty;
    public bool EmailEnabled { get; set; }
    public bool PushEnabled { get; set; }
    public bool SmsEnabled { get; set; }
}

