using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using System.Security.Claims;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/events")]
[Authorize]
public class EventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ========== EVENT ENDPOINTS ==========

    /// <summary>
    /// Get all events with optional filtering
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<object>> GetEvents(
        [FromQuery] EventCategory? category = null,
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null,
        [FromQuery] EventStatus? status = null,
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _context.Events
            .Include(e => e.Creator)
            .Where(e => e.Status != EventStatus.Draft) // Only show published events
            .AsQueryable();

        if (category.HasValue)
            query = query.Where(e => e.Category == category.Value);

        if (dateFrom.HasValue)
            query = query.Where(e => e.Date >= dateFrom.Value);

        if (dateTo.HasValue)
            query = query.Where(e => e.Date <= dateTo.Value);

        if (status.HasValue)
            query = query.Where(e => e.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.Title.Contains(search) || (e.Description != null && e.Description.Contains(search)));

        var totalCount = await query.CountAsync();

        var events = await query
            .OrderBy(e => e.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new
            {
                e.Id,
                e.Title,
                e.Description,
                e.Category,
                e.Date,
                e.StartTime,
                e.EndTime,
                e.Location,
                e.Capacity,
                e.RegisteredCount,
                RemainingSpots = e.Capacity - e.RegisteredCount,
                e.RegistrationDeadline,
                e.IsPaid,
                e.Price,
                e.Status,
                CreatorName = e.Creator != null ? $"{e.Creator.FirstName} {e.Creator.LastName}" : "Unknown Creator",
                e.CreatedAt
            })
            .ToListAsync();

        return Ok(new
        {
            events,
            totalCount,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        });
    }

    /// <summary>
    /// Get event by ID
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<object>> GetEvent(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var evt = await _context.Events
            .Include(e => e.Creator)
            .Where(e => e.Id == id)
            .Select(e => new
            {
                e.Id,
                e.Title,
                e.Description,
                e.Category,
                e.Date,
                e.StartTime,
                e.EndTime,
                e.Location,
                e.Capacity,
                e.RegisteredCount,
                RemainingSpots = e.Capacity - e.RegisteredCount,
                e.RegistrationDeadline,
                e.IsPaid,
                e.Price,
                e.Status,
                CreatorName = e.Creator != null ? $"{e.Creator.FirstName} {e.Creator.LastName}" : "Unknown Creator",
                e.CreatedAt,
                e.UpdatedAt
            })
            .FirstOrDefaultAsync();

        if (evt == null)
            return NotFound(new { message = "Etkinlik bulunamadı" });

        // Check if user is registered
        bool isRegistered = false;
        if (!string.IsNullOrEmpty(userId))
        {
            var userGuid = Guid.Parse(userId);
            isRegistered = await _context.EventRegistrations
                .AnyAsync(r => r.EventId == id && r.UserId == userGuid);
        }

        return Ok(new { Event = evt, IsRegistered = isRegistered });
    }

    /// <summary>
    /// Create a new event (Admin/EventManager only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<Event>> CreateEvent([FromBody] CreateEventDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        if (dto.Date < DateTime.UtcNow)
            return BadRequest(new { message = "Etkinlik tarihi geçmiş olamaz" });

        if (dto.RegistrationDeadline > dto.Date)
            return BadRequest(new { message = "Kayıt son tarihi etkinlik tarihinden sonra olamaz" });

        var evt = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Location = dto.Location,
            Capacity = dto.Capacity,
            RegistrationDeadline = dto.RegistrationDeadline,
            IsPaid = dto.IsPaid,
            Price = dto.IsPaid ? dto.Price : null,
            Status = dto.Status,
            CreatedBy = userId
        };

        _context.Events.Add(evt);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEvent), new { id = evt.Id }, evt);
    }

    /// <summary>
    /// Update an event
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var evt = await _context.Events.FindAsync(id);
        if (evt == null)
            return NotFound(new { message = "Etkinlik bulunamadı" });

        // Check if user is creator or admin
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (evt.CreatedBy != userId && userRole != "Admin")
            return Forbid();

        if (dto.Title != null) evt.Title = dto.Title;
        if (dto.Description != null) evt.Description = dto.Description;
        if (dto.Category.HasValue) evt.Category = dto.Category.Value;
        if (dto.Date.HasValue) evt.Date = dto.Date.Value;
        if (dto.StartTime.HasValue) evt.StartTime = dto.StartTime.Value;
        if (dto.EndTime.HasValue) evt.EndTime = dto.EndTime.Value;
        if (dto.Location != null) evt.Location = dto.Location;
        if (dto.Capacity.HasValue) evt.Capacity = dto.Capacity.Value;
        if (dto.RegistrationDeadline.HasValue) evt.RegistrationDeadline = dto.RegistrationDeadline.Value;
        if (dto.IsPaid.HasValue) evt.IsPaid = dto.IsPaid.Value;
        if (dto.Price.HasValue) evt.Price = dto.Price.Value;
        if (dto.Status.HasValue) evt.Status = dto.Status.Value;

        evt.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Etkinlik güncellendi" });
    }

    /// <summary>
    /// Delete an event
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var evt = await _context.Events.FindAsync(id);
        if (evt == null)
            return NotFound(new { message = "Etkinlik bulunamadı" });

        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (evt.CreatedBy != userId && userRole != "Admin")
            return Forbid();

        _context.Events.Remove(evt);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Etkinlik silindi" });
    }

    // ========== REGISTRATION ENDPOINTS ==========

    /// <summary>
    /// Register for an event
    /// </summary>
    [HttpPost("{id}/register")]
    public async Task<ActionResult<object>> RegisterForEvent(Guid id, [FromBody] EventRegistrationDto? dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var evt = await _context.Events.FindAsync(id);
        if (evt == null)
            return NotFound(new { message = "Etkinlik bulunamadı" });

        if (evt.Status != EventStatus.Published)
            return BadRequest(new { message = "Bu etkinliğe kayıt yapılamaz" });

        if (DateTime.UtcNow > evt.RegistrationDeadline)
            return BadRequest(new { message = "Kayıt süresi dolmuş" });

        // Check if already registered
        var existingRegistration = await _context.EventRegistrations
            .AnyAsync(r => r.EventId == id && r.UserId == userId);

        if (existingRegistration)
            return BadRequest(new { message = "Bu etkinliğe zaten kayıtlısınız" });

        // Check if already on waitlist
        var existingWaitlist = await _context.EventWaitlists
            .AnyAsync(w => w.EventId == id && w.UserId == userId && w.Status == WaitlistStatus.Pending);

        if (existingWaitlist)
            return BadRequest(new { message = "Bu etkinliğin bekleme listesinde zaten varsınız" });

        // Use transaction for atomic operations
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Atomic capacity increment with row-level locking
            // This prevents race conditions when multiple users register simultaneously
            var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "UPDATE Events SET RegisteredCount = RegisteredCount + 1, UpdatedAt = {1} WHERE Id = {0} AND RegisteredCount < Capacity",
                id, DateTime.UtcNow);

            if (rowsAffected == 0)
            {
                // Capacity is full - add to waitlist
                var maxPosition = await _context.EventWaitlists
                    .Where(w => w.EventId == id && w.Status == WaitlistStatus.Pending)
                    .MaxAsync(w => (int?)w.Position) ?? 0;

                var waitlistEntry = new EventWaitlist
                {
                    EventId = id,
                    UserId = userId,
                    Position = maxPosition + 1,
                    Status = WaitlistStatus.Pending,
                    AddedAt = DateTime.UtcNow
                };

                _context.EventWaitlists.Add(waitlistEntry);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    isWaitlisted = true,
                    waitlistPosition = waitlistEntry.Position,
                    message = "Etkinlik kapasitesi dolu. Bekleme listesine eklendiniz."
                });
            }

            // Generate QR code
            var qrCode = $"EVENT-{Guid.NewGuid():N}";

            var registration = new EventRegistration
            {
                EventId = id,
                UserId = userId,
                QrCode = qrCode,
                CustomFieldsJson = dto?.CustomFieldsJson
            };

            _context.EventRegistrations.Add(registration);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            // Refresh the event to get updated values
            await _context.Entry(evt).ReloadAsync();

            return Ok(new
            {
                isWaitlisted = false,
                registration.Id,
                registration.QrCode,
                EventTitle = evt.Title,
                EventDate = evt.Date,
                message = "Etkinliğe kaydoldunuz"
            });
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Cancel event registration
    /// </summary>
    [HttpDelete("{eventId}/registrations/{regId}")]
    public async Task<IActionResult> CancelRegistration(Guid eventId, Guid regId)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var registration = await _context.EventRegistrations
            .Include(r => r.Event)
            .FirstOrDefaultAsync(r => r.Id == regId && r.EventId == eventId && r.UserId == userId);

        if (registration == null)
            return NotFound(new { message = "Kayıt bulunamadı" });

        if (registration.CheckedIn)
            return BadRequest(new { message = "Giriş yapılmış kayıt iptal edilemez" });

        // Use transaction for atomic operations
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.EventRegistrations.Remove(registration);

            // Check if there's someone on the waitlist to promote
            var nextInWaitlist = await _context.EventWaitlists
                .Include(w => w.User)
                .Where(w => w.EventId == eventId && w.Status == WaitlistStatus.Pending)
                .OrderBy(w => w.Position)
                .FirstOrDefaultAsync();

            if (nextInWaitlist != null)
            {
                // Auto-register the next person from waitlist
                var qrCode = $"EVENT-{Guid.NewGuid():N}";
                var newRegistration = new EventRegistration
                {
                    EventId = eventId,
                    UserId = nextInWaitlist.UserId,
                    QrCode = qrCode
                };

                _context.EventRegistrations.Add(newRegistration);

                // Update waitlist entry status
                nextInWaitlist.Status = WaitlistStatus.Registered;
                nextInWaitlist.UpdatedAt = DateTime.UtcNow;

                // Capacity stays the same (one out, one in)
                // Just update the timestamp
                await _context.Database.ExecuteSqlRawAsync(
                    "UPDATE Events SET UpdatedAt = {1} WHERE Id = {0}",
                    eventId, DateTime.UtcNow);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // TODO: Send notification to the user who was promoted from waitlist
                // await _notificationService.SendEventRegistrationFromWaitlistAsync(nextInWaitlist.User, registration.Event);

                return Ok(new { 
                    message = "Kayıt iptal edildi. Bekleme listesinden bir kullanıcı otomatik kaydedildi.",
                    promotedUser = $"{nextInWaitlist.User.FirstName} {nextInWaitlist.User.LastName}"
                });
            }
            else
            {
                // No one on waitlist - just decrement capacity
                await _context.Database.ExecuteSqlRawAsync(
                    "UPDATE Events SET RegisteredCount = CASE WHEN RegisteredCount > 0 THEN RegisteredCount - 1 ELSE 0 END, UpdatedAt = {1} WHERE Id = {0}",
                    eventId, DateTime.UtcNow);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Kayıt iptal edildi" });
            }
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Get registrations for an event (Event manager only)
    /// </summary>
    [HttpGet("{id}/registrations")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<IEnumerable<object>>> GetEventRegistrations(Guid id)
    {
        var registrations = await _context.EventRegistrations
            .Include(r => r.User)
            .Where(r => r.EventId == id)
            .Select(r => new
            {
                r.Id,
                r.UserId,
                UserName = $"{r.User.FirstName} {r.User.LastName}",
                UserEmail = r.User.Email,
                r.RegistrationDate,
                r.QrCode,
                r.CheckedIn,
                r.CheckedInAt,
                r.CustomFieldsJson
            })
            .ToListAsync();

        return Ok(registrations);
    }

    /// <summary>
    /// Check in a participant via QR code
    /// </summary>
    [HttpPost("{eventId}/registrations/{regId}/checkin")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<object>> CheckIn(Guid eventId, Guid regId, [FromBody] CheckInDto? dto)
    {
        var qrCodeValue = dto?.QrCode;
        var registration = await _context.EventRegistrations
            .Include(r => r.User)
            .Include(r => r.Event)
            .FirstOrDefaultAsync(r => 
                (r.Id == regId || r.QrCode == qrCodeValue) && r.EventId == eventId);

        if (registration == null)
            return NotFound(new { message = "Kayıt bulunamadı" });

        if (registration.CheckedIn)
            return BadRequest(new { message = "Bu katılımcı zaten giriş yapmış" });

        registration.CheckedIn = true;
        registration.CheckedInAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Giriş kaydedildi",
            UserName = $"{registration.User.FirstName} {registration.User.LastName}",
            registration.CheckedInAt,
            EventTitle = registration.Event.Title
        });
    }

    /// <summary>
    /// Get current user's event registrations
    /// </summary>
    [HttpGet("my-events")]
    public async Task<ActionResult<IEnumerable<object>>> GetMyEvents()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var registrations = await _context.EventRegistrations
            .Include(r => r.Event)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.Event.Date)
            .Select(r => new
            {
                RegistrationId = r.Id,
                r.QrCode,
                r.CheckedIn,
                r.CheckedInAt,
                r.RegistrationDate,
                    Event = r.Event != null ? new
                    {
                        r.Event.Id,
                        r.Event.Title,
                        r.Event.Description,
                        r.Event.Category,
                        r.Event.Date,
                        r.Event.StartTime,
                        r.Event.EndTime,
                        r.Event.Location,
                        r.Event.Status
                    } : null
            })
            .ToListAsync();

        return Ok(registrations);
    }

    // ========== WAITLIST ENDPOINTS ==========

    /// <summary>
    /// Get waitlist for an event (Admin/Faculty only)
    /// </summary>
    [HttpGet("{id}/waitlist")]
    [Authorize(Roles = "Admin,Faculty")]
    public async Task<ActionResult<IEnumerable<object>>> GetEventWaitlist(Guid id)
    {
        var waitlist = await _context.EventWaitlists
            .Include(w => w.User)
            .Where(w => w.EventId == id && w.Status == WaitlistStatus.Pending)
            .OrderBy(w => w.Position)
            .Select(w => new
            {
                w.Id,
                w.UserId,
                UserName = $"{w.User.FirstName} {w.User.LastName}",
                UserEmail = w.User.Email,
                w.Position,
                w.Status,
                w.AddedAt
            })
            .ToListAsync();

        return Ok(waitlist);
    }

    /// <summary>
    /// Get current user's waitlist entries
    /// </summary>
    [HttpGet("my-waitlist")]
    public async Task<ActionResult<IEnumerable<object>>> GetMyWaitlist()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var waitlistEntries = await _context.EventWaitlists
            .Include(w => w.Event)
            .Where(w => w.UserId == userId && w.Status == WaitlistStatus.Pending)
            .OrderBy(w => w.Event.Date)
            .Select(w => new
            {
                WaitlistId = w.Id,
                w.Position,
                w.AddedAt,
                w.Status,
                Event = w.Event != null ? new
                {
                    w.Event.Id,
                    w.Event.Title,
                    w.Event.Description,
                    w.Event.Category,
                    w.Event.Date,
                    w.Event.StartTime,
                    w.Event.EndTime,
                    w.Event.Location,
                    w.Event.Capacity,
                    w.Event.RegisteredCount
                } : null
            })
            .ToListAsync();

        return Ok(waitlistEntries);
    }

    /// <summary>
    /// Remove yourself from an event's waitlist
    /// </summary>
    [HttpDelete("{eventId}/waitlist/{waitlistId}")]
    public async Task<IActionResult> RemoveFromWaitlist(Guid eventId, Guid waitlistId)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var waitlistEntry = await _context.EventWaitlists
            .FirstOrDefaultAsync(w => w.Id == waitlistId && w.EventId == eventId && w.UserId == userId);

        if (waitlistEntry == null)
            return NotFound(new { message = "Bekleme listesi kaydı bulunamadı" });

        if (waitlistEntry.Status != WaitlistStatus.Pending)
            return BadRequest(new { message = "Bu kayıt artık beklemede değil" });

        waitlistEntry.Status = WaitlistStatus.Cancelled;
        waitlistEntry.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Bekleme listesinden çıkartıldınız" });
    }

    /// <summary>
    /// Get waitlist status for an event (public info)
    /// </summary>
    [HttpGet("{id}/waitlist/status")]
    [AllowAnonymous]
    public async Task<ActionResult<object>> GetWaitlistStatus(Guid id)
    {
        var evt = await _context.Events.FindAsync(id);
        if (evt == null)
            return NotFound(new { message = "Etkinlik bulunamadı" });

        var waitlistCount = await _context.EventWaitlists
            .CountAsync(w => w.EventId == id && w.Status == WaitlistStatus.Pending);

        return Ok(new
        {
            eventId = id,
            eventTitle = evt.Title,
            capacity = evt.Capacity,
            registeredCount = evt.RegisteredCount,
            isFull = evt.RegisteredCount >= evt.Capacity,
            waitlistCount
        });
    }
}

// DTOs
public record CreateEventDto(
    string Title,
    string? Description,
    EventCategory Category,
    DateTime Date,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string? Location,
    int Capacity,
    DateTime RegistrationDeadline,
    bool IsPaid = false,
    decimal? Price = null,
    EventStatus Status = EventStatus.Draft
);

public record UpdateEventDto(
    string? Title,
    string? Description,
    EventCategory? Category,
    DateTime? Date,
    TimeSpan? StartTime,
    TimeSpan? EndTime,
    string? Location,
    int? Capacity,
    DateTime? RegistrationDeadline,
    bool? IsPaid,
    decimal? Price,
    EventStatus? Status
);

public record EventRegistrationDto(string? CustomFieldsJson);

public record CheckInDto(string? QrCode);
