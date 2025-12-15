using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/courses")]
[Authorize]
public class CoursesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Get all courses with pagination and filtering
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<object>> GetCourses(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] Guid? departmentId = null,
        [FromQuery] bool? isActive = true)
    {
        // Check if user is Student or Faculty and get their department
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid? userDepartmentId = null;

        if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var userIdGuid))
        {
            var user = await _context.Users
                .Include(u => u.Student)
                    .ThenInclude(s => s!.Department)
                .Include(u => u.Faculty)
                    .ThenInclude(f => f!.Department)
                .FirstOrDefaultAsync(u => u.Id == userIdGuid);

            if (user != null)
            {
                if (user.Role == UserRole.Student && user.Student != null)
                {
                    userDepartmentId = user.Student.DepartmentId;
                }
                else if (user.Role == UserRole.Faculty && user.Faculty != null)
                {
                    userDepartmentId = user.Faculty.DepartmentId;
                }
            }
        }

        var query = _context.Courses
            .Include(c => c.Department)
            .Include(c => c.Prerequisites)
            .Include(c => c.Sections.Where(s => s.IsActive))
            .AsQueryable();

        // Student and Faculty can only see courses in their department (always enforce, ignore departmentId param)
        if (userDepartmentId.HasValue)
        {
            query = query.Where(c => c.DepartmentId == userDepartmentId.Value);
        }
        else if (departmentId.HasValue)
        {
            // Only Admin can filter by departmentId (if no userDepartmentId)
            query = query.Where(c => c.DepartmentId == departmentId.Value);
        }

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            query = query.Where(c => c.Code.ToLower().Contains(search) || 
                                     c.Name.ToLower().Contains(search));
        }

        if (isActive.HasValue)
        {
            query = query.Where(c => c.IsActive == isActive.Value);
        }

        var totalCount = await query.CountAsync();
        var courses = await query
            .OrderBy(c => c.Code)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var result = courses.Select(c => new CourseListDto
        {
            Id = c.Id,
            Code = c.Code,
            Name = c.Name,
            Credits = c.Credits,
            ECTS = c.ECTS,
            DepartmentName = c.Department?.Name ?? "",
            PrerequisiteCount = c.Prerequisites.Count,
            AvailableSectionCount = c.Sections.Count
        });

        return Ok(new
        {
            data = result,
            page,
            pageSize,
            totalCount,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
        });
    }

    /// <summary>
    /// Get course details by ID with prerequisites and sections
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(Guid id)
    {
        // Check if user is Faculty and get their department
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid? facultyDepartmentId = null;

        if (!string.IsNullOrEmpty(userId) && Guid.TryParse(userId, out var userIdGuid))
        {
            var user = await _context.Users
                .Include(u => u.Faculty)
                    .ThenInclude(f => f!.Department)
                .FirstOrDefaultAsync(u => u.Id == userIdGuid);

            if (user?.Role == UserRole.Faculty && user.Faculty != null)
            {
                facultyDepartmentId = user.Faculty.DepartmentId;
            }
        }

        var course = await _context.Courses
            .Include(c => c.Department)
            .Include(c => c.Prerequisites)
                .ThenInclude(p => p.PrerequisiteCourse)
            .Include(c => c.Sections.Where(s => s.IsActive))
                .ThenInclude(s => s.Instructor)
                    .ThenInclude(i => i.User)
            .Include(c => c.Sections)
                .ThenInclude(s => s.Classroom)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
            return NotFound(new { message = "Course not found" });

        // Faculty can only access courses in their department
        if (facultyDepartmentId.HasValue && course.DepartmentId != facultyDepartmentId.Value)
        {
            return StatusCode(403, new { message = "Bu derse erişim yetkiniz yok. Sadece kendi bölümünüzün derslerine erişebilirsiniz." });
        }

        var dto = new CourseDto
        {
            Id = course.Id,
            Code = course.Code,
            Name = course.Name,
            Description = course.Description,
            Credits = course.Credits,
            ECTS = course.ECTS,
            SyllabusUrl = course.SyllabusUrl,
            DepartmentId = course.DepartmentId,
            DepartmentName = course.Department?.Name ?? "",
            IsActive = course.IsActive,
            Prerequisites = course.Prerequisites.Select(p => new CoursePrerequisiteDto
            {
                CourseId = p.PrerequisiteCourseId,
                CourseCode = p.PrerequisiteCourse?.Code ?? "",
                CourseName = p.PrerequisiteCourse?.Name ?? ""
            }).ToList(),
            AvailableSections = course.Sections.Select(s => new CourseSectionSummaryDto
            {
                Id = s.Id,
                SectionNumber = s.SectionNumber,
                InstructorName = $"{s.Instructor?.User?.FirstName} {s.Instructor?.User?.LastName}",
                ClassroomName = s.Classroom != null ? $"{s.Classroom.Building} - {s.Classroom.RoomNumber}" : null,
                Capacity = s.Capacity,
                EnrolledCount = s.EnrolledCount,
                Schedule = ParseSchedule(s.ScheduleJson)
            }).ToList()
        };

        return Ok(dto);
    }

    /// <summary>
    /// Create a new course (Admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CreateCourseRequest request)
    {
        // Check if code already exists
        if (await _context.Courses.AnyAsync(c => c.Code == request.Code))
        {
            return BadRequest(new { message = "Course code already exists" });
        }

        var course = new Course
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            Credits = request.Credits,
            ECTS = request.ECTS,
            SyllabusUrl = request.SyllabusUrl,
            DepartmentId = request.DepartmentId,
            IsActive = true
        };

        _context.Courses.Add(course);

        // Add prerequisites
        foreach (var prereqId in request.PrerequisiteCourseIds)
        {
            _context.CoursePrerequisites.Add(new CoursePrerequisite
            {
                CourseId = course.Id,
                PrerequisiteCourseId = prereqId
            });
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, new { id = course.Id, message = "Course created successfully" });
    }

    /// <summary>
    /// Update a course (Admin only)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateCourse(Guid id, [FromBody] UpdateCourseRequest request)
    {
        var course = await _context.Courses
            .Include(c => c.Prerequisites)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
            return NotFound(new { message = "Course not found" });

        if (request.Name != null) course.Name = request.Name;
        if (request.Description != null) course.Description = request.Description;
        if (request.Credits.HasValue) course.Credits = request.Credits.Value;
        if (request.ECTS.HasValue) course.ECTS = request.ECTS.Value;
        if (request.SyllabusUrl != null) course.SyllabusUrl = request.SyllabusUrl;

        // Update prerequisites if provided
        if (request.PrerequisiteCourseIds != null)
        {
            _context.CoursePrerequisites.RemoveRange(course.Prerequisites);
            foreach (var prereqId in request.PrerequisiteCourseIds)
            {
                _context.CoursePrerequisites.Add(new CoursePrerequisite
                {
                    CourseId = course.Id,
                    PrerequisiteCourseId = prereqId
                });
            }
        }

        course.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Course updated successfully" });
    }

    /// <summary>
    /// Delete a course (soft delete, Admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteCourse(Guid id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return NotFound(new { message = "Course not found" });

        course.IsActive = false;
        course.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Course deleted successfully" });
    }

    private List<ScheduleSlotDto> ParseSchedule(string? json)
    {
        if (string.IsNullOrEmpty(json)) return new List<ScheduleSlotDto>();
        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<ScheduleSlotDto>>(json, 
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ScheduleSlotDto>();
        }
        catch { return new List<ScheduleSlotDto>(); }
    }
}
