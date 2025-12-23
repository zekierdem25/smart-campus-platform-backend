using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace SmartCampus.API.Hubs;

/// <summary>
/// SignalR Hub for real-time attendance updates
/// </summary>
public class AttendanceHub : Hub
{
    private readonly ILogger<AttendanceHub> _logger;

    public AttendanceHub(ILogger<AttendanceHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId.Value}");
            _logger.LogInformation("User {UserId} connected to AttendanceHub. ConnectionId: {ConnectionId}", 
                userId.Value, Context.ConnectionId);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId.Value}");
            _logger.LogInformation("User {UserId} disconnected from AttendanceHub. ConnectionId: {ConnectionId}", 
                userId.Value, Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Join a session group (for faculty to receive real-time check-ins)
    /// </summary>
    public async Task JoinSessionGroup(Guid sessionId)
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            var groupName = $"session_{sessionId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation("User {UserId} joined session group {GroupName}", userId.Value, groupName);
        }
    }

    /// <summary>
    /// Leave a session group
    /// </summary>
    public async Task LeaveSessionGroup(Guid sessionId)
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            var groupName = $"session_{sessionId}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation("User {UserId} left session group {GroupName}", userId.Value, groupName);
        }
    }

    /// <summary>
    /// Get user ID from JWT claims
    /// </summary>
    private Guid? GetUserId()
    {
        var userIdClaim = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return null;
        return userId;
    }
}

