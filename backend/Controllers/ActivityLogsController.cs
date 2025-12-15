using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCampus.API.DTOs;
using SmartCampus.API.Services;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/activity-logs")]
public class ActivityLogsController : ControllerBase
{
    private readonly IActivityLogService _activityLogService;

    public ActivityLogsController(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService ?? throw new ArgumentNullException(nameof(activityLogService));
    }

    /// <summary>
    /// Son kullanıcı aktivitelerini getirir (sadece admin).
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("recent")]
    [ProducesResponseType(typeof(ApiResponseDto<List<ActivityLogResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecent([FromQuery] int limit = 10)
    {
        var result = await _activityLogService.GetRecentAsync(limit);
        return Ok(result);
    }
}


