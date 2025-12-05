using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    Guid? ValidateAccessToken(string token);
    bool ValidateRefreshToken(string token);
}

