using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System.Security.Claims;

namespace SmartCampus.API.Controllers;

/// <summary>
/// Controller for in-app notifications
/// </summary>
[ApiController]
[Route("api/v1/notifications")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(
        ApplicationDbContext context,
        ILogger<NotificationsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get user's notifications with pagination, filtering, and sorting
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<object>> GetNotifications(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] NotificationCategory? category = null,
        [FromQuery] bool? isRead = null,
        [FromQuery] string sortBy = "createdAt",
        [FromQuery] string sortOrder = "desc")
    {
        try
        {
            var userIdClaims = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !Guid.TryParse(userIdClaims, out var userId))
                return Unauthorized();

            var query = _context.Notifications
                .Where(n => n.UserId == userId)
                .AsQueryable();

            // Filter by category
            if (category.HasValue)
                query = query.Where(n => n.Category == category.Value);

            // Filter by read status
            if (isRead.HasValue)
                query = query.Where(n => n.IsRead == isRead.Value);

            // Sorting
            query = sortBy.ToLower() switch
            {
                "createdat" or "created_at" => sortOrder.ToLower() == "asc"
                    ? query.OrderBy(n => n.CreatedAt)
                    : query.OrderByDescending(n => n.CreatedAt),
                "category" => sortOrder.ToLower() == "asc"
                    ? query.OrderBy(n => n.Category)
                    : query.OrderByDescending(n => n.Category),
                _ => query.OrderByDescending(n => n.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var notifications = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new
                {
                    n.Id,
                    n.Title,
                    n.Message,
                    n.Category,
                    n.Type,
                    n.IsRead,
                    n.ReadAt,
                    n.CreatedAt,
                    n.RelatedEntityId,
                    n.RelatedEntityType
                })
                .ToListAsync();

            return Ok(new
            {
                notifications,
                pagination = new
                {
                    page,
                    pageSize,
                    totalCount,
                    totalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting notifications");
            return StatusCode(500, new { message = "Bildirimler alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get unread notification count
    /// </summary>
    [HttpGet("unread-count")]
    public async Task<ActionResult<object>> GetUnreadCount()
    {
        try
        {
            var userIdClaims = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !Guid.TryParse(userIdClaims, out var userId))
                return Unauthorized();

            var count = await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);

            return Ok(new { unreadCount = count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting unread count");
            return StatusCode(500, new { message = "Okunmamış bildirim sayısı alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Mark notification as read
    /// </summary>
    [HttpPut("{id}/read")]
    public async Task<ActionResult<object>> MarkAsRead(Guid id)
    {
        try
        {
            var userIdClaims = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !Guid.TryParse(userIdClaims, out var userId))
                return Unauthorized();

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (notification == null)
                return NotFound(new { message = "Bildirim bulunamadı." });

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Bildirim okundu olarak işaretlendi." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking notification as read");
            return StatusCode(500, new { message = "Bildirim işaretlenirken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Mark all notifications as read
    /// </summary>
    [HttpPut("mark-all-read")]
    public async Task<ActionResult<object>> MarkAllAsRead()
    {
        try
        {
            var userIdClaims = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !Guid.TryParse(userIdClaims, out var userId))
                return Unauthorized();

            var unreadNotifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            var now = DateTime.UtcNow;
            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = true;
                notification.ReadAt = now;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = $"{unreadNotifications.Count} bildirim okundu olarak işaretlendi." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking all notifications as read");
            return StatusCode(500, new { message = "Bildirimler işaretlenirken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Delete a notification
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<object>> DeleteNotification(Guid id)
    {
        try
        {
            var userIdClaims = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !Guid.TryParse(userIdClaims, out var userId))
                return Unauthorized();

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (notification == null)
                return NotFound(new { message = "Bildirim bulunamadı." });

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Bildirim silindi." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting notification");
            return StatusCode(500, new { message = "Bildirim silinirken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Get user's notification preferences
    /// </summary>
    [HttpGet("preferences")]
    public async Task<ActionResult<object>> GetPreferences()
    {
        try
        {
            var userIdClaims = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !Guid.TryParse(userIdClaims, out var userId))
                return Unauthorized();

            var preferences = await _context.NotificationPreferences
                .Where(p => p.UserId == userId)
                .Select(p => new
                {
                    Category = p.Category.ToString(),
                    p.EmailEnabled,
                    p.PushEnabled,
                    p.SmsEnabled
                })
                .ToListAsync();

            // If no preferences exist, return defaults
            if (!preferences.Any())
            {
                var defaultCategories = Enum.GetValues<NotificationCategory>();
                preferences = defaultCategories.Select(c => new
                {
                    Category = c.ToString(),
                    EmailEnabled = true,
                    PushEnabled = true,
                    SmsEnabled = false
                }).ToList();
            }

            return Ok(new { preferences });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting notification preferences");
            return StatusCode(500, new { message = "Bildirim tercihleri alınırken bir hata oluştu." });
        }
    }

    /// <summary>
    /// Update user's notification preferences
    /// </summary>
    [HttpPut("preferences")]
    public async Task<ActionResult<object>> UpdatePreferences([FromBody] UpdatePreferencesDto dto)
    {
        try
        {
            var userIdClaims = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaims) || !Guid.TryParse(userIdClaims, out var userId))
                return Unauthorized();

            if (dto.Preferences == null || !dto.Preferences.Any())
                return BadRequest(new { message = "Tercihler boş olamaz." });

            foreach (var pref in dto.Preferences)
            {
                if (!Enum.TryParse<NotificationCategory>(pref.Category, out var category))
                    continue;

                var existing = await _context.NotificationPreferences
                    .FirstOrDefaultAsync(p => p.UserId == userId && p.Category == category);

                if (existing == null)
                {
                    existing = new NotificationPreferences
                    {
                        UserId = userId,
                        Category = category,
                        EmailEnabled = pref.EmailEnabled,
                        PushEnabled = pref.PushEnabled,
                        SmsEnabled = pref.SmsEnabled
                    };
                    _context.NotificationPreferences.Add(existing);
                }
                else
                {
                    existing.EmailEnabled = pref.EmailEnabled;
                    existing.PushEnabled = pref.PushEnabled;
                    existing.SmsEnabled = pref.SmsEnabled;
                    existing.UpdatedAt = DateTime.UtcNow;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Bildirim tercihleri güncellendi." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating notification preferences");
            return StatusCode(500, new { message = "Bildirim tercihleri güncellenirken bir hata oluştu." });
        }
    }
}

// DTOs
public class UpdatePreferencesDto
{
    public List<PreferenceDto> Preferences { get; set; } = new();
}

public class PreferenceDto
{
    public string Category { get; set; } = string.Empty;
    public bool EmailEnabled { get; set; }
    public bool PushEnabled { get; set; }
    public bool SmsEnabled { get; set; }
}

