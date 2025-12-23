using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace SmartCampus.API.Hubs;

/// <summary>
/// SignalR Hub for real-time notifications
/// </summary>
public class NotificationHub : Hub
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            // Add user to their personal group
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId.Value}");
            _logger.LogInformation("User {UserId} connected to NotificationHub. ConnectionId: {ConnectionId}", 
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
            _logger.LogInformation("User {UserId} disconnected from NotificationHub. ConnectionId: {ConnectionId}", 
                userId.Value, Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(exception);
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

