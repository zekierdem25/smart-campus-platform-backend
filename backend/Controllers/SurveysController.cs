using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using System.Security.Claims;
using System.Text.Json;

namespace SmartCampus.API.Controllers;

/// <summary>
/// Controller for managing event surveys and responses
/// </summary>
[ApiController]
[Route("api/v1/events/{eventId}/surveys")]
[Authorize]
public class SurveysController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SurveysController> _logger;

    public SurveysController(ApplicationDbContext context, ILogger<SurveysController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // ========== SURVEY CRUD ==========

    /// <summary>
    /// Get all surveys for an event
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<object>> GetSurveys(Guid eventId)
    {
        var evt = await _context.Events.FindAsync(eventId);
        if (evt == null)
            return NotFound(new { message = "Etkinlik bulunamadı" });

        var surveys = await _context.EventSurveys
            .Where(s => s.EventId == eventId)
            .Select(s => new
            {
                s.Id,
                s.Title,
                s.Description,
                s.IsActive,
                s.StartsAt,
                s.EndsAt,
                s.CreatedAt,
                ResponseCount = s.Responses.Count
            })
            .ToListAsync();

        return Ok(surveys);
    }

    /// <summary>
    /// Get a specific survey with schema
    /// </summary>
    [HttpGet("{surveyId}")]
    public async Task<ActionResult<object>> GetSurvey(Guid eventId, Guid surveyId)
    {
        var survey = await _context.EventSurveys
            .Where(s => s.Id == surveyId && s.EventId == eventId)
            .Select(s => new
            {
                s.Id,
                s.EventId,
                s.Title,
                s.Description,
                Schema = s.SchemaJson,
                s.IsActive,
                s.StartsAt,
                s.EndsAt,
                s.CreatedAt,
                ResponseCount = s.Responses.Count
            })
            .FirstOrDefaultAsync();

        if (survey == null)
            return NotFound(new { message = "Anket bulunamadı" });

        return Ok(new
        {
            survey.Id,
            survey.EventId,
            survey.Title,
            survey.Description,
            Schema = JsonSerializer.Deserialize<object>(survey.Schema),
            survey.IsActive,
            survey.StartsAt,
            survey.EndsAt,
            survey.CreatedAt,
            survey.ResponseCount
        });
    }

    /// <summary>
    /// Create a new survey for an event (Admin/Faculty only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> CreateSurvey(Guid eventId, [FromBody] CreateSurveyDto dto)
    {
        var evt = await _context.Events.FindAsync(eventId);
        if (evt == null)
            return NotFound(new { message = "Etkinlik bulunamadı" });

        var survey = new EventSurvey
        {
            EventId = eventId,
            Title = dto.Title,
            Description = dto.Description,
            SchemaJson = JsonSerializer.Serialize(dto.Schema),
            IsActive = true,
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt
        };

        _context.EventSurveys.Add(survey);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Survey created: {SurveyId} for event {EventId}", survey.Id, eventId);

        return CreatedAtAction(nameof(GetSurvey), new { eventId, surveyId = survey.Id }, new
        {
            survey.Id,
            survey.Title,
            message = "Anket oluşturuldu"
        });
    }

    /// <summary>
    /// Update a survey (Admin/Faculty only)
    /// </summary>
    [HttpPut("{surveyId}")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> UpdateSurvey(Guid eventId, Guid surveyId, [FromBody] UpdateSurveyDto dto)
    {
        var survey = await _context.EventSurveys
            .FirstOrDefaultAsync(s => s.Id == surveyId && s.EventId == eventId);

        if (survey == null)
            return NotFound(new { message = "Anket bulunamadı" });

        if (!string.IsNullOrEmpty(dto.Title))
            survey.Title = dto.Title;

        if (dto.Description != null)
            survey.Description = dto.Description;

        if (dto.Schema != null)
            survey.SchemaJson = JsonSerializer.Serialize(dto.Schema);

        if (dto.IsActive.HasValue)
            survey.IsActive = dto.IsActive.Value;

        if (dto.StartsAt.HasValue)
            survey.StartsAt = dto.StartsAt.Value;

        if (dto.EndsAt.HasValue)
            survey.EndsAt = dto.EndsAt.Value;

        survey.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Anket güncellendi", survey.Id });
    }

    /// <summary>
    /// Delete a survey (Admin/Faculty only)
    /// </summary>
    [HttpDelete("{surveyId}")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<IActionResult> DeleteSurvey(Guid eventId, Guid surveyId)
    {
        var survey = await _context.EventSurveys
            .FirstOrDefaultAsync(s => s.Id == surveyId && s.EventId == eventId);

        if (survey == null)
            return NotFound(new { message = "Anket bulunamadı" });

        _context.EventSurveys.Remove(survey);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Anket silindi" });
    }

    // ========== SURVEY RESPONSES ==========

    /// <summary>
    /// Submit a survey response
    /// </summary>
    [HttpPost("{surveyId}/responses")]
    public async Task<ActionResult<object>> SubmitResponse(Guid eventId, Guid surveyId, [FromBody] SurveyResponseDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var survey = await _context.EventSurveys
            .FirstOrDefaultAsync(s => s.Id == surveyId && s.EventId == eventId);

        if (survey == null)
            return NotFound(new { message = "Anket bulunamadı" });

        if (!survey.IsActive)
            return BadRequest(new { message = "Bu anket aktif değil" });

        // Check if survey is within time window
        var now = DateTime.UtcNow;
        if (survey.StartsAt.HasValue && now < survey.StartsAt.Value)
            return BadRequest(new { message = "Bu anket henüz başlamadı" });
        if (survey.EndsAt.HasValue && now > survey.EndsAt.Value)
            return BadRequest(new { message = "Bu anket sona erdi" });

        // Check if user was registered for the event
        var isRegistered = await _context.EventRegistrations
            .AnyAsync(r => r.EventId == eventId && r.UserId == userId);

        if (!isRegistered)
            return BadRequest(new { message = "Bu etkinliğe kayıtlı değilsiniz" });

        // Check for duplicate response
        var existingResponse = await _context.EventSurveyResponses
            .AnyAsync(r => r.SurveyId == surveyId && r.UserId == userId);

        if (existingResponse)
            return BadRequest(new { message = "Bu anketi zaten doldurdunuz" });

        var response = new EventSurveyResponse
        {
            SurveyId = surveyId,
            UserId = userId,
            ResponsesJson = JsonSerializer.Serialize(dto.Responses),
            SubmittedAt = DateTime.UtcNow,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
        };

        _context.EventSurveyResponses.Add(response);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Survey response submitted: {ResponseId} by user {UserId}", response.Id, userId);

        return Ok(new { message = "Anket cevabınız kaydedildi", response.Id });
    }

    /// <summary>
    /// Get my response to a survey
    /// </summary>
    [HttpGet("{surveyId}/my-response")]
    public async Task<ActionResult<object>> GetMyResponse(Guid eventId, Guid surveyId)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var response = await _context.EventSurveyResponses
            .Where(r => r.SurveyId == surveyId && r.UserId == userId)
            .Select(r => new
            {
                r.Id,
                Responses = r.ResponsesJson,
                r.SubmittedAt
            })
            .FirstOrDefaultAsync();

        if (response == null)
            return NotFound(new { message = "Henüz cevap vermediniz" });

        return Ok(new
        {
            response.Id,
            Responses = JsonSerializer.Deserialize<object>(response.Responses),
            response.SubmittedAt
        });
    }

    /// <summary>
    /// Get all responses to a survey (Admin/Faculty only)
    /// </summary>
    [HttpGet("{surveyId}/responses")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> GetResponses(
        Guid eventId, 
        Guid surveyId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var survey = await _context.EventSurveys
            .FirstOrDefaultAsync(s => s.Id == surveyId && s.EventId == eventId);

        if (survey == null)
            return NotFound(new { message = "Anket bulunamadı" });

        var totalCount = await _context.EventSurveyResponses
            .CountAsync(r => r.SurveyId == surveyId);

        var responses = await _context.EventSurveyResponses
            .Include(r => r.User)
            .Where(r => r.SurveyId == surveyId)
            .OrderByDescending(r => r.SubmittedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new
            {
                r.Id,
                User = new
                {
                    r.User.Id,
                    Name = $"{r.User.FirstName} {r.User.LastName}",
                    r.User.Email
                },
                Responses = r.ResponsesJson,
                r.SubmittedAt
            })
            .ToListAsync();

        return Ok(new
        {
            surveyId,
            surveyTitle = survey.Title,
            totalResponses = totalCount,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize),
            responses = responses.Select(r => new
            {
                r.Id,
                r.User,
                Responses = JsonSerializer.Deserialize<object>(r.Responses),
                r.SubmittedAt
            })
        });
    }

    /// <summary>
    /// Get survey analytics/summary (Admin/Faculty only)
    /// </summary>
    [HttpGet("{surveyId}/analytics")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> GetSurveyAnalytics(Guid eventId, Guid surveyId)
    {
        var survey = await _context.EventSurveys
            .FirstOrDefaultAsync(s => s.Id == surveyId && s.EventId == eventId);

        if (survey == null)
            return NotFound(new { message = "Anket bulunamadı" });

        var responses = await _context.EventSurveyResponses
            .Where(r => r.SurveyId == surveyId)
            .Select(r => r.ResponsesJson)
            .ToListAsync();

        var totalResponses = responses.Count;

        // Parse responses and calculate basic stats
        var parsedResponses = responses
            .Select(r => JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(r))
            .Where(r => r != null)
            .ToList();

        // Calculate numeric field averages
        var numericStats = new Dictionary<string, object>();
        if (parsedResponses.Any())
        {
            var firstResponse = parsedResponses.First();
            foreach (var key in firstResponse!.Keys)
            {
                var values = parsedResponses
                    .Where(r => r!.ContainsKey(key) && r[key].ValueKind == JsonValueKind.Number)
                    .Select(r => r![key].GetDouble())
                    .ToList();

                if (values.Any())
                {
                    numericStats[key] = new
                    {
                        Average = Math.Round(values.Average(), 2),
                        Min = values.Min(),
                        Max = values.Max(),
                        Count = values.Count
                    };
                }
            }
        }

        return Ok(new
        {
            surveyId,
            surveyTitle = survey.Title,
            totalResponses,
            numericStats,
            responseTimeline = await _context.EventSurveyResponses
                .Where(r => r.SurveyId == surveyId)
                .GroupBy(r => r.SubmittedAt.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .OrderBy(x => x.Date)
                .ToListAsync()
        });
    }
}

// DTOs
public record CreateSurveyDto(
    string Title,
    string? Description,
    object Schema,
    DateTime? StartsAt,
    DateTime? EndsAt
);

public record UpdateSurveyDto(
    string? Title,
    string? Description,
    object? Schema,
    bool? IsActive,
    DateTime? StartsAt,
    DateTime? EndsAt
);

public record SurveyResponseDto(object Responses);
