using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using System.Security.Claims;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/reservations")]
[Authorize]
public class ClassroomReservationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ClassroomReservationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create a classroom reservation
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<object>> CreateReservation([FromBody] CreateClassroomReservationDto dto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var classroom = await _context.Classrooms.FindAsync(dto.ClassroomId);
        if (classroom == null)
            return BadRequest(new { message = "Derslik bulunamadı" });

        if (!classroom.IsActive)
            return BadRequest(new { message = "Bu derslik aktif değil" });

        if (dto.Date.Date < DateTime.UtcNow.Date)
            return BadRequest(new { message = "Geçmiş tarih için rezervasyon yapılamaz" });

        if (dto.StartTime >= dto.EndTime)
            return BadRequest(new { message = "Bitiş saati başlangıç saatinden sonra olmalıdır" });

        // Check for conflicting reservations
        var hasConflict = await _context.ClassroomReservations
            .AnyAsync(r => r.ClassroomId == dto.ClassroomId &&
                          r.Date.Date == dto.Date.Date &&
                          r.Status == ReservationStatus.Approved &&
                          ((r.StartTime <= dto.StartTime && r.EndTime > dto.StartTime) ||
                           (r.StartTime < dto.EndTime && r.EndTime >= dto.EndTime) ||
                           (r.StartTime >= dto.StartTime && r.EndTime <= dto.EndTime)));

        if (hasConflict)
            return BadRequest(new { message = "Bu zaman dilimi için derslik zaten rezerve edilmiş" });

        // Check for schedule conflicts
        var dayOfWeek = (ScheduleDayOfWeek)((int)dto.Date.DayOfWeek == 0 ? 7 : (int)dto.Date.DayOfWeek);
        var hasScheduleConflict = await _context.Schedules
            .AnyAsync(s => s.ClassroomId == dto.ClassroomId &&
                          s.DayOfWeek == dayOfWeek &&
                          ((s.StartTime <= dto.StartTime && s.EndTime > dto.StartTime) ||
                           (s.StartTime < dto.EndTime && s.EndTime >= dto.EndTime)));

        if (hasScheduleConflict)
            return BadRequest(new { message = "Bu zaman diliminde derslikteki ders programı ile çakışma var" });

        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        var status = userRole == "Admin" ? ReservationStatus.Approved : ReservationStatus.Pending;

        var reservation = new ClassroomReservation
        {
            ClassroomId = dto.ClassroomId,
            UserId = userId,
            Date = dto.Date.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Purpose = dto.Purpose,
            Status = status,
            ApprovedBy = status == ReservationStatus.Approved ? userId : null,
            ApprovedAt = status == ReservationStatus.Approved ? DateTime.UtcNow : null
        };

        _context.ClassroomReservations.Add(reservation);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, new
        {
            reservation.Id,
            reservation.Date,
            reservation.StartTime,
            reservation.EndTime,
            reservation.Status,
            ClassroomName = $"{classroom.Building} {classroom.RoomNumber}",
            message = status == ReservationStatus.Pending 
                ? "Rezervasyon talebi oluşturuldu, onay bekleniyor" 
                : "Rezervasyon oluşturuldu"
        });
    }

    /// <summary>
    /// Get reservation by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetReservation(Guid id)
    {
        var reservation = await _context.ClassroomReservations
            .Include(r => r.Classroom)
            .Include(r => r.User)
            .Include(r => r.Approver)
            .Where(r => r.Id == id)
            .Select(r => new
            {
                r.Id,
                r.Date,
                r.StartTime,
                r.EndTime,
                r.Purpose,
                r.Status,
                r.ApprovedAt,
                Classroom = new
                {
                    r.Classroom.Id,
                    r.Classroom.Building,
                    r.Classroom.RoomNumber,
                    r.Classroom.Capacity
                },
                User = new
                {
                    r.User.Id,
                    Name = $"{r.User.FirstName} {r.User.LastName}",
                    r.User.Email
                },
                ApproverName = r.Approver == null 
                    ? null
                    : r.Approver.FirstName + " " + r.Approver.LastName,
                r.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (reservation == null)
            return NotFound(new { message = "Rezervasyon bulunamadı" });

        return Ok(reservation);
    }

    /// <summary>
    /// Get all reservations with filtering
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<object>> GetReservations(
        [FromQuery] DateTime? date = null,
        [FromQuery] Guid? classroomId = null,
        [FromQuery] Guid? userId = null,
        [FromQuery] ReservationStatus? status = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        var query = _context.ClassroomReservations
            .Include(r => r.Classroom)
            .Include(r => r.User)
            .AsQueryable();

        // Non-admin users can only see their own reservations or all approved ones
        if (userRole != "Admin")
        {
            query = query.Where(r => r.UserId == currentUserId || r.Status == ReservationStatus.Approved);
        }

        if (date.HasValue)
            query = query.Where(r => r.Date.Date == date.Value.Date);

        if (classroomId.HasValue)
            query = query.Where(r => r.ClassroomId == classroomId.Value);

        if (userId.HasValue && userRole == "Admin")
            query = query.Where(r => r.UserId == userId.Value);

        if (status.HasValue)
            query = query.Where(r => r.Status == status.Value);

        var totalCount = await query.CountAsync();

        var reservations = await query
            .OrderByDescending(r => r.Date)
            .ThenBy(r => r.StartTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new
            {
                r.Id,
                r.Date,
                r.StartTime,
                r.EndTime,
                r.Purpose,
                r.Status,
                ClassroomName = $"{r.Classroom.Building} {r.Classroom.RoomNumber}",
                UserName = $"{r.User.FirstName} {r.User.LastName}",
                r.CreatedAt
            })
            .ToListAsync();

        return Ok(new
        {
            reservations,
            totalCount,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        });
    }

    /// <summary>
    /// Approve a reservation (Admin only)
    /// </summary>
    [HttpPut("{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveReservation(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var reservation = await _context.ClassroomReservations.FindAsync(id);
        if (reservation == null)
            return NotFound(new { message = "Rezervasyon bulunamadı" });

        if (reservation.Status != ReservationStatus.Pending)
            return BadRequest(new { message = "Sadece bekleyen rezervasyonlar onaylanabilir" });

        reservation.Status = ReservationStatus.Approved;
        reservation.ApprovedBy = userId;
        reservation.ApprovedAt = DateTime.UtcNow;
        reservation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Rezervasyon onaylandı" });
    }

    /// <summary>
    /// Reject a reservation (Admin only)
    /// </summary>
    [HttpPut("{id}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectReservation(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var reservation = await _context.ClassroomReservations.FindAsync(id);
        if (reservation == null)
            return NotFound(new { message = "Rezervasyon bulunamadı" });

        if (reservation.Status != ReservationStatus.Pending)
            return BadRequest(new { message = "Sadece bekleyen rezervasyonlar reddedilebilir" });

        reservation.Status = ReservationStatus.Rejected;
        reservation.ApprovedBy = userId;
        reservation.ApprovedAt = DateTime.UtcNow;
        reservation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Rezervasyon reddedildi" });
    }

    /// <summary>
    /// Cancel own reservation
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelReservation(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        var reservation = await _context.ClassroomReservations.FindAsync(id);
        if (reservation == null)
            return NotFound(new { message = "Rezervasyon bulunamadı" });

        // Only owner or admin can cancel
        if (reservation.UserId != userId && userRole != "Admin")
            return Forbid();

        if (reservation.Status == ReservationStatus.Cancelled)
            return BadRequest(new { message = "Rezervasyon zaten iptal edilmiş" });

        reservation.Status = ReservationStatus.Cancelled;
        reservation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return Ok(new { message = "Rezervasyon iptal edildi" });
    }

    /// <summary>
    /// Get classroom availability for a specific date
    /// </summary>
    [HttpGet("availability")]
    public async Task<ActionResult<object>> GetClassroomAvailability(
        [FromQuery] Guid classroomId,
        [FromQuery] DateTime date)
    {
        var classroom = await _context.Classrooms.FindAsync(classroomId);
        if (classroom == null)
            return NotFound(new { message = "Derslik bulunamadı" });

        // Get approved reservations for this date
        var reservations = await _context.ClassroomReservations
            .Where(r => r.ClassroomId == classroomId &&
                       r.Date.Date == date.Date &&
                       r.Status == ReservationStatus.Approved)
            .Select(r => new
            {
                r.StartTime,
                r.EndTime,
                r.Purpose
            })
            .ToListAsync();

        // Get scheduled classes for this day
        var dayOfWeek = (ScheduleDayOfWeek)((int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek);
        var schedules = await _context.Schedules
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Where(s => s.ClassroomId == classroomId && s.DayOfWeek == dayOfWeek)
            .Select(s => new
            {
                s.StartTime,
                s.EndTime,
                Purpose = $"{s.Section.Course.Code} - {s.Section.Course.Name}"
            })
            .ToListAsync();

        return Ok(new
        {
            Classroom = new
            {
                classroom.Id,
                classroom.Building,
                classroom.RoomNumber,
                classroom.Capacity
            },
            Date = date.Date,
            Reservations = reservations,
            ScheduledClasses = schedules
        });
    }
}

// DTOs
public record CreateClassroomReservationDto(
    Guid ClassroomId,
    DateTime Date,
    TimeSpan StartTime,
    TimeSpan EndTime,
    string? Purpose
);
