using SmartCampus.API.DTOs;

namespace SmartCampus.API.Services;

public interface IActivityLogService
{
    Task RecordAsync(Guid userId, string action, string? description = null, string? ipAddress = null, string? userAgent = null);
    Task<ApiResponseDto<List<ActivityLogResponseDto>>> GetRecentAsync(int limit = 10);
}


