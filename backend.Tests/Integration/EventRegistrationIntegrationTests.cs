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

}
