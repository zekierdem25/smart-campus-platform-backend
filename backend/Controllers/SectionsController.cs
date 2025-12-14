using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/sections")]
[Authorize]
public class SectionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IScheduleConflictService _scheduleConflictService;

    public SectionsController(
        ApplicationDbContext context,
        IScheduleConflictService scheduleConflictService)
    {
        _context = context;
        _scheduleConflictService = scheduleConflictService;
    }

    /// <summary>
    /// Get all sections with filtering
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<object>> GetSections(
        [FromQuery] Guid? courseId = null,
        [FromQuery] Guid? instructorId = null,
        [FromQuery] string? semester = null,
        [FromQuery] int? year = null,
        [FromQuery] bool? isActive = true)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        UserRole? userRole = null;
        Guid? facultyDepartmentId = null;

        if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var userIdGuid))
        {
            var user = await _context.Users
                .Include(u => u.Faculty)
                    .ThenInclude(f => f!.Department)
                .FirstOrDefaultAsync(u => u.Id == userIdGuid);

            if (user != null)
            {
                userRole = user.Role;
                if (user.Role == UserRole.Faculty && user.Faculty != null)
                {
                    facultyDepartmentId = user.Faculty.DepartmentId;
                }
            }
        }

        var query = _context.CourseSections
            .Include(s => s.Course)
                .ThenInclude(c => c.Department)
            .Include(s => s.Instructor)
                .ThenInclude(i => i.User)
            .Include(s => s.Classroom)
            .AsQueryable();

        // Faculty can only see sections for courses in their department
        if (userRole == UserRole.Faculty && facultyDepartmentId.HasValue)
        {
            query = query.Where(s => s.Course.DepartmentId == facultyDepartmentId.Value);
        }

        if (courseId.HasValue)
            query = query.Where(s => s.CourseId == courseId.Value);

        if (instructorId.HasValue)
            query = query.Where(s => s.InstructorId == instructorId.Value);

        if (!string.IsNullOrEmpty(semester))
            query = query.Where(s => s.Semester == semester);

        if (year.HasValue)
            query = query.Where(s => s.Year == year.Value);

        if (isActive.HasValue)
            query = query.Where(s => s.IsActive == isActive.Value);

        var sections = await query
            .OrderBy(s => s.Course.Code)
            .ThenBy(s => s.SectionNumber)
            .ToListAsync();

        var result = sections.Select(s => new CourseSectionDto
        {
            Id = s.Id,
            CourseId = s.CourseId,
            CourseCode = s.Course.Code,
            CourseName = s.Course.Name,
            SectionNumber = s.SectionNumber,
            Semester = s.Semester,
            Year = s.Year,
            InstructorId = s.InstructorId,
            InstructorName = $"{s.Instructor.User.FirstName} {s.Instructor.User.LastName}",
            ClassroomId = s.ClassroomId,
            ClassroomName = s.Classroom != null ? $"{s.Classroom.Building} - {s.Classroom.RoomNumber}" : null,
            Capacity = s.Capacity,
            EnrolledCount = s.EnrolledCount,
            Schedule = _scheduleConflictService.ParseScheduleJson(s.ScheduleJson),
            IsActive = s.IsActive
        });

        return Ok(result);
    }

    /// <summary>
    /// Get section by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseSectionDto>> GetSection(Guid id)
    {
        var section = await _context.CourseSections
            .Include(s => s.Course)
            .Include(s => s.Instructor)
                .ThenInclude(i => i.User)
            .Include(s => s.Classroom)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (section == null)
            return NotFound(new { message = "Section not found" });

        return Ok(new CourseSectionDto
        {
            Id = section.Id,
            CourseId = section.CourseId,
            CourseCode = section.Course.Code,
            CourseName = section.Course.Name,
            SectionNumber = section.SectionNumber,
            Semester = section.Semester,
            Year = section.Year,
            InstructorId = section.InstructorId,
            InstructorName = $"{section.Instructor.User.FirstName} {section.Instructor.User.LastName}",
            ClassroomId = section.ClassroomId,
            ClassroomName = section.Classroom != null ? $"{section.Classroom.Building} - {section.Classroom.RoomNumber}" : null,
            Capacity = section.Capacity,
            EnrolledCount = section.EnrolledCount,
            Schedule = _scheduleConflictService.ParseScheduleJson(section.ScheduleJson),
            IsActive = section.IsActive
        });
    }

    /// <summary>
    /// Create a new section (Admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateSection([FromBody] CreateSectionRequest request)
    {
        // Check if course exists
        var course = await _context.Courses.FindAsync(request.CourseId);
        if (course == null)
            return BadRequest(new { message = "Course not found" });

        // Check if instructor exists
        var instructor = await _context.Faculties.FindAsync(request.InstructorId);
        if (instructor == null)
            return BadRequest(new { message = "Instructor not found" });

        // Check if section number already exists for this course/semester/year
        var exists = await _context.CourseSections
            .AnyAsync(s => s.CourseId == request.CourseId && 
                          s.SectionNumber == request.SectionNumber && 
                          s.Semester == request.Semester && 
                          s.Year == request.Year);

        if (exists)
            return BadRequest(new { message = "Section already exists for this course in the specified semester" });

        var section = new CourseSection
        {
            CourseId = request.CourseId,
            SectionNumber = request.SectionNumber,
            Semester = request.Semester,
            Year = request.Year,
            InstructorId = request.InstructorId,
            ClassroomId = request.ClassroomId,
            Capacity = request.Capacity,
            ScheduleJson = _scheduleConflictService.SerializeSchedule(request.Schedule),
            IsActive = true
        };

        _context.CourseSections.Add(section);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSection), new { id = section.Id }, new { id = section.Id, message = "Section created successfully" });
    }

    /// <summary>
    /// Update a section (Admin only)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateSection(Guid id, [FromBody] UpdateSectionRequest request)
    {
        var section = await _context.CourseSections.FindAsync(id);
        if (section == null)
            return NotFound(new { message = "Section not found" });

        if (request.InstructorId.HasValue)
            section.InstructorId = request.InstructorId.Value;

        if (request.ClassroomId.HasValue)
            section.ClassroomId = request.ClassroomId.Value;

        if (request.Capacity.HasValue)
            section.Capacity = request.Capacity.Value;

        if (request.Schedule != null)
            section.ScheduleJson = _scheduleConflictService.SerializeSchedule(request.Schedule);

        section.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Section updated successfully" });
    }

    /// <summary>
    /// Get classrooms list
    /// </summary>
    [HttpGet("/api/v1/classrooms")]
    public async Task<ActionResult<List<ClassroomDto>>> GetClassrooms()
    {
        var classrooms = await _context.Classrooms
            .Where(c => c.IsActive)
            .OrderBy(c => c.Building)
            .ThenBy(c => c.RoomNumber)
            .ToListAsync();

        var result = classrooms.Select(c => new ClassroomDto
        {
            Id = c.Id,
            Building = c.Building,
            RoomNumber = c.RoomNumber,
            Capacity = c.Capacity,
            Latitude = c.Latitude,
            Longitude = c.Longitude,
            Features = !string.IsNullOrEmpty(c.FeaturesJson) 
                ? System.Text.Json.JsonSerializer.Deserialize<List<string>>(c.FeaturesJson) ?? new List<string>()
                : new List<string>(),
            IsActive = c.IsActive
        });

        return Ok(result);
    }
}
