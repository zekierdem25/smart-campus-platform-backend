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

public class MealReservationIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public MealReservationIntegrationTests(CustomWebApplicationFactory factory)
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
    public async Task CreateReservation_WithValidMenu_ShouldReturnCreated()
    {
        // Arrange
        var token = await AuthenticateAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Seed a future menu
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            // Ensure cafeteria exists
            if (!db.Cafeterias.Any())
            {
                db.Cafeterias.Add(new Cafeteria { Id = Guid.NewGuid(), Name = "Main Hall", Location = "Building A", IsActive = true });
                await db.SaveChangesAsync();
            }
            var cafeteria = db.Cafeterias.First();

            var menuId = Guid.NewGuid();
            var menu = new MealMenu
            {
                Id = menuId,
                CafeteriaId = cafeteria.Id,
                Date = DateTime.UtcNow.AddDays(15).Date, // Future date
                MealType = MealType.Lunch,
                ItemsJson = "[\"Soup\", \"Main Dish\"]",
                IsPublished = true,
                CreatedAt = DateTime.UtcNow
            };
            db.MealMenus.Add(menu);
            
            // Ensure user has wallet balance
            var userId = db.Users.First(u => u.Email == "zeki.erdem@smartcampus.com").Id;
            var wallet = db.Wallets.FirstOrDefault(w => w.UserId == userId);
            if (wallet == null)
            {
                db.Wallets.Add(new Wallet { UserId = userId, Balance = 1000m });
            }
            else
            {
                wallet.Balance = 1000m;
            }

            await db.SaveChangesAsync();
            
            // Act
            var request = new CreateReservationDto(menuId);
            var response = await _client.PostAsJsonAsync("/api/v1/meals/reservations", request);

            // Assert
            if (response.StatusCode != HttpStatusCode.Created)
            {
                var error = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Status: {response.StatusCode}, Error: {error}");
            }
            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }

    [Fact]
    public async Task CreateReservation_Duplicate_ShouldReturnBadRequest()
    {
        // Arrange
        var token = await AuthenticateAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        Guid menuId = Guid.NewGuid();

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
             // Ensure cafeteria exists
            if (!db.Cafeterias.Any())
            {
                db.Cafeterias.Add(new Cafeteria { Id = Guid.NewGuid(), Name = "Main Hall", Location = "Building A", IsActive = true });
                await db.SaveChangesAsync();
            }
            var cafeteria = db.Cafeterias.First();

            var menu = new MealMenu
            {
                Id = menuId,
                CafeteriaId = cafeteria.Id,
                Date = DateTime.UtcNow.AddDays(16).Date,
                MealType = MealType.Dinner,
                ItemsJson = "[\"Soup\"]",
                IsPublished = true
            };
            db.MealMenus.Add(menu);
            await db.SaveChangesAsync();
        }

        // First reservation
        var request = new CreateReservationDto(menuId);
        var res1 = await _client.PostAsJsonAsync("/api/v1/meals/reservations", request);
        res1.EnsureSuccessStatusCode();

        // Act - Second reservation
        var res2 = await _client.PostAsJsonAsync("/api/v1/meals/reservations", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, res2.StatusCode);
    }
}
