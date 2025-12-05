using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Tests.Helpers;

namespace SmartCampus.API.Tests.Integration;

/// <summary>
/// AuthController için integration testler
/// Sena: Bu dosyaya test metodlarını yazacaksın
/// 
/// NOT: .NET 9.0 top-level statements kullandığı için WebApplicationFactory kullanımı için
/// Program.cs'deki implicit Program class'ını kullanıyoruz.
/// </summary>
public class AuthControllerTests : IClassFixture<WebApplicationFactory<SmartCampus.API.Program>>
{
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _context;

    public AuthControllerTests(WebApplicationFactory<SmartCampus.API.Program> factory)
    {
        // In-memory database kullan
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Gerçek database'i kaldır
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
                );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // In-memory database ekle
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb_" + Guid.NewGuid().ToString());
                });
            });
        }).CreateClient();

        // DbContext'i al
        var scope = factory.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    // TODO: Sena - Test metodlarını yaz
    
    // Minimum 10 test yazılmalı:
    // 1. POST /api/v1/auth/register - 201 Created
    // 2. POST /api/v1/auth/register - 400 Bad Request (invalid email)
    // 3. POST /api/v1/auth/login - 200 OK
    // 4. POST /api/v1/auth/login - 401 Unauthorized
    // 5. POST /api/v1/auth/refresh - 200 OK
    // 6. POST /api/v1/auth/refresh - 401 Unauthorized (invalid token)
    // 7. POST /api/v1/auth/forgot-password - 200 OK
    // 8. POST /api/v1/auth/reset-password - 200 OK
    // 9. POST /api/v1/auth/reset-password - 400 Bad Request (invalid token)
    // 10. POST /api/v1/auth/logout - 204 No Content
    
    // Örnek test yapısı:
    // [Fact]
    // public async Task Register_ValidRequest_Returns201()
    // {
    //     // Arrange
    //     var request = TestHelpers.CreateRegisterRequest();
    //     
    //     // Act
    //     var response = await _client.PostAsJsonAsync("/api/v1/auth/register", request);
    //     
    //     // Assert
    //     response.StatusCode.Should().Be(HttpStatusCode.Created);
    //     var content = await response.Content.ReadFromJsonAsync<AuthResponse>();
    //     content.Should().NotBeNull();
    //     content!.Success.Should().BeTrue();
    // }
}

