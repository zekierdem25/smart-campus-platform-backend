using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System.Security.Claims;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/enrollments")]
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IEnrollmentService _enrollmentService;
    private readonly IScheduleConflictService _scheduleConflictService;
    private readonly INotificationService _notificationService;
    private readonly ILogger<EnrollmentsController> _logger;

    public EnrollmentsController(
        ApplicationDbContext context,
        IEnrollmentService enrollmentService,
        IScheduleConflictService scheduleConflictService,
        INotificationService notificationService,
        ILogger<EnrollmentsController> logger)
    {
        _context = context;
        _enrollmentService = enrollmentService;
        _scheduleConflictService = scheduleConflictService;
        _notificationService = notificationService;
        _logger = logger;
    }

    /// <summary>
    /// Enroll in a course section (Student only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult> Enroll([FromBody] EnrollRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        try
        {
            // Check eligibility first
            var eligibility = await _enrollmentService.CheckEnrollmentEligibilityAsync(student.Id, request.SectionId);
            if (!eligibility.CanEnroll)
            {
                return BadRequest(new 
                { 
                    message = "Kayıt işlemi gerçekleştirilemedi", 
                    errors = eligibility.Errors,
                    warnings = eligibility.Warnings
                });
            }

            // Perform enrollment
            var enrollment = await _enrollmentService.EnrollStudentAsync(student.Id, request.SectionId);

            return CreatedAtAction(nameof(GetMyCourses), new { id = enrollment.Id }, new 
            { 
                message = "Enrolled successfully",
                enrollmentId = enrollment.Id,
                warnings = eligibility.Warnings
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Check enrollment eligibility before enrolling
    /// </summary>
    [HttpPost("check")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<EnrollmentCheckResult>> CheckEnrollment([FromBody] EnrollRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        var result = await _enrollmentService.CheckEnrollmentEligibilityAsync(student.Id, request.SectionId);
        return Ok(result);
    }

    /// <summary>
    /// Drop a course (Student only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult> DropCourse(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        try
        {
            await _enrollmentService.DropCourseAsync(id, student.Id);
            
            // Notify faculty asynchronously (fire and forget)
            _ = Task.Run(async () =>
            {
                try
                {
                    await _notificationService.NotifyFacultyOnCourseDropAsync(id);
                }
                catch (Exception ex)
                {
                    // Log error but don't fail the request
                    _logger.LogError(ex, "Failed to send course drop notification");
                }
            });
            
            return Ok(new { message = "Course dropped successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Get my enrolled courses (Student only)
    /// </summary>
    [HttpGet("my-courses")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<List<EnrollmentDto>>> GetMyCourses()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        var enrollments = await _enrollmentService.GetStudentEnrollmentsAsync(student.Id);

        // Calculate attendance percentage for each enrollment
        foreach (var enrollment in enrollments)
        {
            var section = await _context.CourseSections
                .Include(s => s.AttendanceSessions)
                    .ThenInclude(a => a.Records)
                .FirstOrDefaultAsync(s => s.Id == enrollment.SectionId);

            if (section != null)
            {
                var totalSessions = section.AttendanceSessions.Count(a => a.Status == AttendanceSessionStatus.Closed);
                var attendedSessions = section.AttendanceSessions
                    .SelectMany(a => a.Records)
                    .Count(r => r.StudentId == student.Id);

                enrollment.AttendancePercentage = totalSessions > 0 
                    ? Math.Round((decimal)attendedSessions / totalSessions * 100, 1) 
                    : 100;
            }
        }

        return Ok(enrollments);
    }

    /// <summary>
    /// Get students enrolled in a section (Faculty only)
    /// </summary>
    [HttpGet("students/{sectionId}")]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult<object>> GetSectionStudents(Guid sectionId)
    {
        var section = await _context.CourseSections
            .Include(s => s.Course)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                    .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == sectionId);

        if (section == null)
            return NotFound(new { message = "Section not found" });

        var students = section.Enrollments
            .Where(e => e.Status == EnrollmentStatus.Active)
            .Select(e => new
            {
                enrollmentId = e.Id,
                studentId = e.StudentId,
                studentNumber = e.Student.StudentNumber,
                studentName = $"{e.Student.User.FirstName} {e.Student.User.LastName}",
                email = e.Student.User.Email,
                enrollmentDate = e.EnrollmentDate,
                midtermGrade = e.MidtermGrade,
                finalGrade = e.FinalGrade,
                homeworkGrade = e.HomeworkGrade,
                letterGrade = e.LetterGrade,
                gradePoint = e.GradePoint
            })
            .OrderBy(s => s.studentNumber)
            .ToList();

        return Ok(new
        {
            sectionId,
            courseCode = section.Course.Code,
            courseName = section.Course.Name,
            sectionNumber = section.SectionNumber,
            totalStudents = students.Count,
            students
        });
    }
}
