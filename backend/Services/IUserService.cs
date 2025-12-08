using SmartCampus.API.DTOs;

namespace SmartCampus.API.Services;

public interface IUserService
{
    Task<ApiResponseDto<UserResponseDto>> GetCurrentUserAsync(Guid userId);
    Task<ApiResponseDto<UserResponseDto>> UpdateProfileAsync(Guid userId, UpdateProfileRequestDto request);
    Task<ApiResponseDto<string>> UpdateProfilePictureAsync(Guid userId, IFormFile file);
    Task<ApiResponseDto<bool>> ChangePasswordAsync(Guid userId, ChangePasswordRequestDto request);
    Task<UserListResponseDto> GetUsersAsync(UserListRequestDto request);
    Task<ApiResponseDto<List<DepartmentResponseDto>>> GetDepartmentsAsync();
    Task<ApiResponseDto<bool>> DeleteUserAsync(Guid userId, Guid deletedByUserId);
}

