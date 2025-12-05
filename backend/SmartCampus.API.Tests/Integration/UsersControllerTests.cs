using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;
using SmartCampus.API.Data;
using SmartCampus.API.Tests.Helpers;

namespace SmartCampus.API.Tests.Integration;

/// <summary>
/// UsersController için integration testler
/// Sena: Bu dosyaya test metodlarını yazacaksın
/// 
/// NOT: .NET 9.0 top-level statements kullandığı için WebApplicationFactory kullanımı için
/// Program.cs'deki implicit Program class'ını kullanıyoruz.
/// </summary>
public class UsersControllerTests : IClassFixture<WebApplicationFactory<SmartCampus.API.Program>>
{
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _context;

    public UsersControllerTests(WebApplicationFactory<SmartCampus.API.Program> factory)
    {
        // In-memory database kullan
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
                );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb_" + Guid.NewGuid().ToString());
                });
            });
        }).CreateClient();

        var scope = factory.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    // TODO: Sena - Test metodlarını yaz
    
    // Minimum 5 test yazılmalı:
    // 1. GET /api/v1/users/me - 200 OK (authenticated)
    // 2. GET /api/v1/users/me - 401 Unauthorized (no token)
    // 3. PUT /api/v1/users/me - 200 OK
    // 4. POST /api/v1/users/me/profile-picture - 200 OK
    // 5. GET /api/v1/users - 200 OK (admin only)
    
    // Örnek test yapısı:
    // [Fact]
    // public async Task GetProfile_AuthenticatedUser_Returns200()
    // {
    //     // Arrange - Önce login ol, token al
    //     // Act
    //     // Assert
    // }
}

