using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/academic-calendar")]
[Authorize]
public class AcademicCalendarController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AcademicCalendarController(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Get all academic events (optionally filtered by date range and type)
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<AcademicEventDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<AcademicEventDto>>> GetAcademicEvents(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] string? type = null)
    {
        var query = _context.AcademicEvents.AsQueryable();

        // Filter by date range
        if (startDate.HasValue)
        {
            query = query.Where(e => e.EndDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(e => e.StartDate <= endDate.Value);
        }

        // Filter by type
        if (!string.IsNullOrEmpty(type) && Enum.TryParse<AcademicEventType>(type, true, out var eventType))
        {
            query = query.Where(e => e.Type == eventType);
        }

        var events = await query
            .OrderBy(e => e.StartDate)
            .ToListAsync();

        var result = events.Select(e => new AcademicEventDto
        {
            Id = e.Id,
            Title = e.Title,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Type = e.Type.ToString(),
            Description = e.Description,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Get a specific academic event by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AcademicEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AcademicEventDto>> GetAcademicEvent(Guid id)
    {
        var academicEvent = await _context.AcademicEvents
            .FirstOrDefaultAsync(e => e.Id == id);

        if (academicEvent == null)
        {
            return NotFound(new { message = "Akademik etkinlik bulunamadı" });
        }

        var result = new AcademicEventDto
        {
            Id = academicEvent.Id,
            Title = academicEvent.Title,
            StartDate = academicEvent.StartDate,
            EndDate = academicEvent.EndDate,
            Type = academicEvent.Type.ToString(),
            Description = academicEvent.Description,
            CreatedAt = academicEvent.CreatedAt,
            UpdatedAt = academicEvent.UpdatedAt
        };

        return Ok(result);
    }

    /// <summary>
    /// Create a new academic event (Admin and Faculty only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Faculty,Admin")]
    [ProducesResponseType(typeof(AcademicEventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<AcademicEventDto>> CreateAcademicEvent([FromBody] CreateAcademicEventDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Geçersiz veri", errors = ModelState });
        }

        // Validate dates
        if (request.EndDate < request.StartDate)
        {
            return BadRequest(new { message = "Bitiş tarihi başlangıç tarihinden önce olamaz" });
        }

        // Validate type
        if (!Enum.TryParse<AcademicEventType>(request.Type, true, out var eventType))
        {
            return BadRequest(new { message = "Geçersiz etkinlik tipi. Geçerli tipler: Exam, Holiday, General" });
        }

        var academicEvent = new AcademicEvent
        {
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Type = eventType,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.AcademicEvents.Add(academicEvent);
        await _context.SaveChangesAsync();

        var result = new AcademicEventDto
        {
            Id = academicEvent.Id,
            Title = academicEvent.Title,
            StartDate = academicEvent.StartDate,
            EndDate = academicEvent.EndDate,
            Type = academicEvent.Type.ToString(),
            Description = academicEvent.Description,
            CreatedAt = academicEvent.CreatedAt,
            UpdatedAt = academicEvent.UpdatedAt
        };

        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update an academic event (Admin and Faculty only)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Faculty,Admin")]
    [ProducesResponseType(typeof(AcademicEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<AcademicEventDto>> UpdateAcademicEvent(Guid id, [FromBody] UpdateAcademicEventDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Geçersiz veri", errors = ModelState });
        }

        var academicEvent = await _context.AcademicEvents
            .FirstOrDefaultAsync(e => e.Id == id);

        if (academicEvent == null)
        {
            return NotFound(new { message = "Akademik etkinlik bulunamadı" });
        }

        // Update fields if provided
        if (!string.IsNullOrEmpty(request.Title))
        {
            academicEvent.Title = request.Title;
        }

        if (request.StartDate.HasValue)
        {
            academicEvent.StartDate = request.StartDate.Value;
        }

        if (request.EndDate.HasValue)
        {
            academicEvent.EndDate = request.EndDate.Value;
        }

        // Validate dates
        if (academicEvent.EndDate < academicEvent.StartDate)
        {
            return BadRequest(new { message = "Bitiş tarihi başlangıç tarihinden önce olamaz" });
        }

        if (!string.IsNullOrEmpty(request.Type))
        {
            if (!Enum.TryParse<AcademicEventType>(request.Type, true, out var eventType))
            {
                return BadRequest(new { message = "Geçersiz etkinlik tipi. Geçerli tipler: Exam, Holiday, General" });
            }
            academicEvent.Type = eventType;
        }

        if (request.Description != null)
        {
            academicEvent.Description = request.Description;
        }

        academicEvent.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var result = new AcademicEventDto
        {
            Id = academicEvent.Id,
            Title = academicEvent.Title,
            StartDate = academicEvent.StartDate,
            EndDate = academicEvent.EndDate,
            Type = academicEvent.Type.ToString(),
            Description = academicEvent.Description,
            CreatedAt = academicEvent.CreatedAt,
            UpdatedAt = academicEvent.UpdatedAt
        };

        return Ok(result);
    }

    /// <summary>
    /// Delete an academic event (Admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteAcademicEvent(Guid id)
    {
        var academicEvent = await _context.AcademicEvents
            .FirstOrDefaultAsync(e => e.Id == id);

        if (academicEvent == null)
        {
            return NotFound(new { message = "Akademik etkinlik bulunamadı" });
        }

        _context.AcademicEvents.Remove(academicEvent);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
