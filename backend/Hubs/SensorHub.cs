using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using SmartCampus.API.Services;
using SmartCampus.API.DTOs;

namespace SmartCampus.API.Hubs;

/// <summary>
/// SignalR Hub for real-time IoT sensor data streaming
/// </summary>
public class SensorHub : Hub
{
    private readonly ILogger<SensorHub> _logger;
    private readonly ISensorService _sensorService;

    public SensorHub(
        ILogger<SensorHub> logger,
        ISensorService sensorService)
    {
        _logger = logger;
        _sensorService = sensorService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId.Value}");
            _logger.LogInformation("User {UserId} connected to SensorHub. ConnectionId: {ConnectionId}", 
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
            _logger.LogInformation("User {UserId} disconnected from SensorHub. ConnectionId: {ConnectionId}", 
                userId.Value, Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(exception);
    }

    /// <summary>
    /// Join a sensor stream group to receive real-time updates for a specific sensor
    /// </summary>
    public async Task JoinSensorStream(Guid sensorId)
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            var groupName = $"sensor_{sensorId}";
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation("User {UserId} joined sensor stream group {GroupName}", userId.Value, groupName);
        }
    }

    /// <summary>
    /// Leave a sensor stream group
    /// </summary>
    public async Task LeaveSensorStream(Guid sensorId)
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            var groupName = $"sensor_{sensorId}";
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            _logger.LogInformation("User {UserId} left sensor stream group {GroupName}", userId.Value, groupName);
        }
    }

    /// <summary>
    /// Join all sensors stream (for dashboard view)
    /// </summary>
    public async Task JoinAllSensorsStream()
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "all_sensors");
            _logger.LogInformation("User {UserId} joined all sensors stream", userId.Value);
        }
    }

    /// <summary>
    /// Leave all sensors stream
    /// </summary>
    public async Task LeaveAllSensorsStream()
    {
        var userId = GetUserId();
        if (userId.HasValue)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "all_sensors");
            _logger.LogInformation("User {UserId} left all sensors stream", userId.Value);
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

