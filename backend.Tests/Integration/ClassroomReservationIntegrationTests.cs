using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using SmartCampus.API.Controllers; // For DTOs
using SmartCampus.API.Data;
using SmartCampus.API.DTOs; // For Auth DTOs
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Integration;

public class ClassroomReservationIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public ClassroomReservationIntegrationTests(CustomWebApplicationFactory factory)
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

    [Fact]
    public async Task CreateReservation_ValidData_ShouldReturnCreated()
    {
        // Arrange
        var token = await AuthenticateAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        Guid classroomId = Guid.NewGuid();

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var classroom = new Classroom
            {
                Id = classroomId,
                Building = "Engineering",
                RoomNumber = "E-205",
                Capacity = 40,
                IsActive = true
            };
            db.Classrooms.Add(classroom);
            await db.SaveChangesAsync();
        }

        var request = new CreateClassroomReservationDto(
            classroomId,
            DateTime.UtcNow.AddDays(7),
            TimeSpan.FromHours(14),
            TimeSpan.FromHours(16),
            "Study Group"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/reservations", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateReservation_Conflict_ShouldReturnBadRequest()
    {
        // Arrange
        var token = await AuthenticateAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        Guid classroomId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(8).Date;

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
             var userId = db.Users.First(u => u.Email == "zeki.erdem@smartcampus.com").Id;
            
            var classroom = new Classroom
            {
                Id = classroomId,
                Building = "Science",
                RoomNumber = "S-101",
                Capacity = 30,
                IsActive = true
            };
            db.Classrooms.Add(classroom);

            // Existing approved reservation
            db.ClassroomReservations.Add(new ClassroomReservation
            {
                Id = Guid.NewGuid(),
                ClassroomId = classroomId,
                UserId = userId,
                Date = date,
                StartTime = TimeSpan.FromHours(10),
                EndTime = TimeSpan.FromHours(12),
                Status = ReservationStatus.Approved
            });

            await db.SaveChangesAsync();
        }

        var request = new CreateClassroomReservationDto(
            classroomId,
            date,
            TimeSpan.FromHours(11), // Overlap! 11:00 is inside 10-12
            TimeSpan.FromHours(13),
            "Conflict Test"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/reservations", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
