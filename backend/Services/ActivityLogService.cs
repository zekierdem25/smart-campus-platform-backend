using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public class ActivityLogService : IActivityLogService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ActivityLogService> _logger;

    public ActivityLogService(ApplicationDbContext context, ILogger<ActivityLogService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task RecordAsync(Guid userId, string action, string? description = null, string? ipAddress = null, string? userAgent = null)
    {
        var log = new ActivityLog
        {
            UserId = userId,
            Action = action,
            Description = description,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Activity log kaydedilirken hata oluştu. UserId: {UserId}, Action: {Action}", userId, action);
        }
    }

    public async Task<ApiResponseDto<List<ActivityLogResponseDto>>> GetRecentAsync(int limit = 10)
    {
        var safeLimit = Math.Clamp(limit, 1, 50);

        var logs = await _context.ActivityLogs
            .Include(a => a.User)
            .OrderByDescending(a => a.CreatedAt)
            .Take(safeLimit)
            .ToListAsync();

        var data = logs.Select(log => new ActivityLogResponseDto
        {
            Id = log.Id,
            UserId = log.UserId,
            UserName = log.User?.FullName ?? "Bilinmeyen",
            Role = log.User?.Role.ToString() ?? string.Empty,
            Action = log.Action,
            Description = log.Description,
            CreatedAt = log.CreatedAt
        }).ToList();

        return new ApiResponseDto<List<ActivityLogResponseDto>>
        {
            Success = true,
            Data = data
        };
    }
}


