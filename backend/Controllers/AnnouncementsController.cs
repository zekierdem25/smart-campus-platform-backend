using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/announcements")]
[Authorize]
public class AnnouncementsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AnnouncementsController(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Get announcements visible to the current user (General + enrolled courses)
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<AnnouncementDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<AnnouncementDto>>> GetAnnouncements(
        [FromQuery] Guid? courseId = null,
        [FromQuery] bool? isImportant = null)
    {
        var userId = GetCurrentUserId();
        if (userId == null)
        {
            return Unauthorized(new { message = "Kullanıcı kimliği bulunamadı" });
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId.Value);

        if (user == null)
        {
            return Unauthorized(new { message = "Kullanıcı bulunamadı" });
        }

        // Base query
        var query = _context.Announcements
            .Include(a => a.Author)
            .Include(a => a.Course)
            .AsQueryable();

        // Filter by course if specified
        if (courseId.HasValue)
        {
            query = query.Where(a => a.CourseId == courseId.Value);
        }

        // Filter by importance if specified
        if (isImportant.HasValue)
        {
            query = query.Where(a => a.IsImportant == isImportant.Value);
        }

        // Role-based filtering
        if (user.Role == UserRole.Student)
        {
            // Students see: General announcements + announcements for courses they're enrolled in
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId.Value);

            if (student != null)
            {
                var enrolledCourseIds = await _context.Enrollments
                    .Where(e => e.StudentId == student.Id && e.Status == EnrollmentStatus.Active)
                    .Select(e => e.Section.CourseId)
                    .Distinct()
                    .ToListAsync();

                query = query.Where(a => 
                    a.CourseId == null || // General announcements
                    enrolledCourseIds.Contains(a.CourseId.Value)); // Or enrolled courses
            }
            else
            {
                // If student profile not found, only show general announcements
                query = query.Where(a => a.CourseId == null);
            }
        }
        else if (user.Role == UserRole.Faculty)
        {
            // Faculty see: General announcements + announcements for courses in their department
            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(f => f.UserId == userId.Value);

            if (faculty != null)
            {
                var departmentCourseIds = await _context.Courses
                    .Where(c => c.DepartmentId == faculty.DepartmentId)
                    .Select(c => c.Id)
                    .ToListAsync();

                query = query.Where(a => 
                    a.CourseId == null || // General announcements
                    departmentCourseIds.Contains(a.CourseId.Value)); // Or courses in their department
            }
            else
            {
                // If faculty profile not found, only show general announcements
                query = query.Where(a => a.CourseId == null);
            }
        }
        // Admin can see all announcements

        var announcements = await query
            .OrderByDescending(a => a.IsImportant)
            .ThenByDescending(a => a.CreatedAt)
            .ToListAsync();

        var result = announcements.Select(a => new AnnouncementDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            CreatedAt = a.CreatedAt,
            AuthorId = a.AuthorId,
            AuthorName = $"{a.Author.FirstName} {a.Author.LastName}",
            CourseId = a.CourseId,
            CourseName = a.Course?.Name,
            CourseCode = a.Course?.Code,
            IsImportant = a.IsImportant
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Create a new announcement (Faculty and Admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Faculty,Admin")]
    [ProducesResponseType(typeof(AnnouncementDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<AnnouncementDto>> CreateAnnouncement([FromBody] CreateAnnouncementDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Geçersiz veri", errors = ModelState });
        }

        var userId = GetCurrentUserId();
        if (userId == null)
        {
            return Unauthorized(new { message = "Kullanıcı kimliği bulunamadı" });
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId.Value);

        if (user == null)
        {
            return Unauthorized(new { message = "Kullanıcı bulunamadı" });
        }

        // If CourseId is provided, verify the course exists and check permissions
        if (request.CourseId.HasValue)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == request.CourseId.Value);

            if (course == null)
            {
                return BadRequest(new { message = "Belirtilen ders bulunamadı" });
            }

            // Faculty can only create announcements for courses in their department
            if (user.Role == UserRole.Faculty)
            {
                var faculty = await _context.Faculties
                    .FirstOrDefaultAsync(f => f.UserId == userId.Value);

                if (faculty == null)
                {
                    return BadRequest(new { message = "Faculty profili bulunamadı" });
                }

                if (course.DepartmentId != faculty.DepartmentId)
                {
                    return StatusCode(403, new { message = "Sadece kendi bölümünüzün dersleri için duyuru oluşturabilirsiniz" });
                }
            }
        }

        // Create announcement
        var announcement = new Announcement
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = userId.Value,
            CourseId = request.CourseId,
            IsImportant = request.IsImportant,
            CreatedAt = DateTime.UtcNow
        };

        _context.Announcements.Add(announcement);
        await _context.SaveChangesAsync();

        // Load related data for response
        await _context.Entry(announcement)
            .Reference(a => a.Author)
            .LoadAsync();

        if (announcement.CourseId.HasValue)
        {
            await _context.Entry(announcement)
                .Reference(a => a.Course)
                .LoadAsync();
        }

        var result = new AnnouncementDto
        {
            Id = announcement.Id,
            Title = announcement.Title,
            Content = announcement.Content,
            CreatedAt = announcement.CreatedAt,
            AuthorId = announcement.AuthorId,
            AuthorName = $"{announcement.Author.FirstName} {announcement.Author.LastName}",
            CourseId = announcement.CourseId,
            CourseName = announcement.Course?.Name,
            CourseCode = announcement.Course?.Code,
            IsImportant = announcement.IsImportant
        };

        return StatusCode(StatusCodes.Status201Created, result);
    }

    private Guid? GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }
        return null;
    }
}
