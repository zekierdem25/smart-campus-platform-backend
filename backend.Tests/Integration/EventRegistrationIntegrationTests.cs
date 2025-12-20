using System.Net;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartCampus.API.Controllers; // For DTOs
using SmartCampus.API.Data;
using SmartCampus.API.DTOs; // For Auth DTOs
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Integration;

public class EventRegistrationIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public EventRegistrationIntegrationTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    private async Task<string> AuthenticateAsync()
    {
        var loginRequest = new LoginRequestDto
        {
            Email = "zeki.erdem@smartcampus.com",
            Password = "Student123!"
        };

        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", loginRequest);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        return result!.AccessToken!;
    }

    [Fact(Skip = "InMemory provider does not support SQL atomic updates")]
    public async Task RegisterForEvent_WithValidEvent_ShouldReturnOk()
    {
        // Arrange
        var token = await AuthenticateAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        Guid eventId = Guid.NewGuid();

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userId = db.Users.First(u => u.Email == "zeki.erdem@smartcampus.com").Id; // Should exist

            var evt = new Event
            {
                Id = eventId,
                Title = "Integration Test Event",
                Category = EventCategory.Academic,
                Date = DateTime.UtcNow.AddDays(10),
                StartTime = TimeSpan.FromHours(10),
                EndTime = TimeSpan.FromHours(12),
                Capacity = 50,
                RegistrationDeadline = DateTime.UtcNow.AddDays(9),
                Status = EventStatus.Published,
                CreatedBy = userId // Self created
            };
            db.Events.Add(evt);
            await db.SaveChangesAsync();
        }

        // Act
        var response = await _client.PostAsJsonAsync($"/api/v1/events/{eventId}/register", new EventRegistrationDto(null));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var evt = await db.Events.FindAsync(eventId);
            Assert.Equal(1, evt.RegisteredCount);
        }
    }

    [Fact(Skip = "InMemory provider does not support SQL atomic updates")]
    public async Task RegisterForEvent_FullCapacity_ShouldAddToWaitlist()
    {
        // Arrange
        var token = await AuthenticateAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        Guid eventId = Guid.NewGuid();

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
             var userId = db.Users.First(u => u.Email == "zeki.erdem@smartcampus.com").Id;
            
            var evt = new Event
            {
                Id = eventId,
                Title = "Full Event",
                Category = EventCategory.Social,
                Date = DateTime.UtcNow.AddDays(5),
                Capacity = 1, // Max 1 person
                RegistrationDeadline = DateTime.UtcNow.AddDays(4),
                Status = EventStatus.Published,
                RegisteredCount = 1, // Already full
                CreatedBy = userId
            };
            db.Events.Add(evt);
            
            // Needs to check if RegisteredCount logic in DB allows atomic update test?
            // The controller uses `WHERE RegisteredCount < Capacity`
            // So if I set RegisteredCount=1 and Capacity=1, it should fail update and go to waitlist
            
            await db.SaveChangesAsync();
        }

        // Act
        var response = await _client.PostAsJsonAsync($"/api/v1/events/{eventId}/register", new EventRegistrationDto(null));

        // Assert
        // Expect 200 OK but with message about waitlist
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        using (var jsonDoc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync()))
        {
            var isWaitlisted = jsonDoc.RootElement.GetProperty("isWaitlisted").GetBoolean();
            Assert.True(isWaitlisted);
        }
    }
}
