using SmartCampus.API.DTOs;

namespace SmartCampus.API.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto> VerifyEmailAsync(string token);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
    Task<AuthResponseDto> LogoutAsync(Guid userId, string refreshToken);
    Task<AuthResponseDto> ForgotPasswordAsync(string email);
    Task<AuthResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request);
}

