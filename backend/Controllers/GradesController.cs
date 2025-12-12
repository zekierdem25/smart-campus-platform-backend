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
[Route("api/v1/grades")]
[Authorize]
public class GradesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IGradeCalculationService _gradeCalculationService;

    public GradesController(
        ApplicationDbContext context,
        IGradeCalculationService gradeCalculationService)
    {
        _context = context;
        _gradeCalculationService = gradeCalculationService;
    }

    /// <summary>
    /// Get my grades (Student only)
    /// </summary>
    [HttpGet("my-grades")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<List<GradeDto>>> GetMyGrades()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        var enrollments = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Course)
            .Where(e => e.StudentId == student.Id)
            .OrderByDescending(e => e.Section.Year)
            .ThenByDescending(e => e.Section.Semester)
            .ToListAsync();

        var grades = enrollments.Select(e => new GradeDto
        {
            EnrollmentId = e.Id,
            CourseCode = e.Section.Course.Code,
            CourseName = e.Section.Course.Name,
            Credits = e.Section.Course.Credits,
            Semester = e.Section.Semester,
            Year = e.Section.Year,
            MidtermGrade = e.MidtermGrade,
            FinalGrade = e.FinalGrade,
            HomeworkGrade = e.HomeworkGrade,
            LetterGrade = e.LetterGrade,
            GradePoint = e.GradePoint
        }).ToList();

        return Ok(grades);
    }

    /// <summary>
    /// Get transcript as JSON (Student only)
    /// </summary>
    [HttpGet("transcript")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<TranscriptDto>> GetTranscript()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students
            .Include(s => s.User)
            .Include(s => s.Department)
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        var enrollments = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Course)
            .Where(e => e.StudentId == student.Id && 
                       (e.Status == EnrollmentStatus.Completed || e.Status == EnrollmentStatus.Active))
            .OrderBy(e => e.Section.Year)
            .ThenBy(e => e.Section.Semester)
            .ToListAsync();

        // Group by semester
        var semesterGroups = enrollments
            .GroupBy(e => new { e.Section.Semester, e.Section.Year })
            .Select(g => new SemesterGradesDto
            {
                Semester = g.Key.Semester,
                Year = g.Key.Year,
                GPA = _gradeCalculationService.CalculateGPA(g.ToList()),
                Credits = g.Sum(e => e.Section.Course.Credits),
                Courses = g.Select(e => new GradeDto
                {
                    EnrollmentId = e.Id,
                    CourseCode = e.Section.Course.Code,
                    CourseName = e.Section.Course.Name,
                    Credits = e.Section.Course.Credits,
                    Semester = e.Section.Semester,
                    Year = e.Section.Year,
                    MidtermGrade = e.MidtermGrade,
                    FinalGrade = e.FinalGrade,
                    HomeworkGrade = e.HomeworkGrade,
                    LetterGrade = e.LetterGrade,
                    GradePoint = e.GradePoint
                }).ToList()
            }).ToList();

        var transcript = new TranscriptDto
        {
            StudentId = student.Id,
            StudentName = $"{student.User.FirstName} {student.User.LastName}",
            StudentNumber = student.StudentNumber,
            DepartmentName = student.Department?.Name ?? "",
            EnrollmentYear = student.EnrollmentYear,
            CGPA = _gradeCalculationService.CalculateCGPA(enrollments),
            TotalCredits = enrollments.Sum(e => e.Section.Course.Credits),
            TotalECTS = enrollments.Sum(e => e.Section.Course.ECTS),
            Semesters = semesterGroups
        };

        return Ok(transcript);
    }

    /// <summary>
    /// Get transcript as PDF (Student only)
    /// </summary>
    [HttpGet("transcript/pdf")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult> GetTranscriptPdf()
    {
        // For now, return transcript data - PDF generation would require PdfSharpCore
        // This is a placeholder that returns JSON with a note
        var transcript = await GetTranscript();
        
        // TODO: Implement PDF generation with PdfSharpCore
        // For now, return the JSON with content type note
        return Ok(new 
        { 
            message = "PDF generation requires PdfSharpCore package. Use /transcript for JSON format.",
            data = (transcript.Result as OkObjectResult)?.Value 
        });
    }

    /// <summary>
    /// Input grades for a student (Faculty only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult> InputGrades([FromBody] GradeInputRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var enrollment = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Instructor)
            .FirstOrDefaultAsync(e => e.Id == request.EnrollmentId);

        if (enrollment == null)
            return NotFound(new { message = "Enrollment not found" });

        // Verify faculty owns this section
        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId));

        if (faculty == null)
            return BadRequest(new { message = "Faculty profile not found" });

        if (enrollment.Section.InstructorId != faculty.Id && User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
            return Forbid();

        // Update grades
        if (request.MidtermGrade.HasValue)
            enrollment.MidtermGrade = request.MidtermGrade;
        if (request.FinalGrade.HasValue)
            enrollment.FinalGrade = request.FinalGrade;
        if (request.HomeworkGrade.HasValue)
            enrollment.HomeworkGrade = request.HomeworkGrade;

        // Calculate letter grade and grade point
        if (enrollment.MidtermGrade.HasValue && enrollment.FinalGrade.HasValue)
        {
            var (letterGrade, gradePoint) = _gradeCalculationService.CalculateLetterGrade(
                enrollment.MidtermGrade,
                enrollment.FinalGrade,
                enrollment.HomeworkGrade);

            enrollment.LetterGrade = letterGrade;
            enrollment.GradePoint = gradePoint;

            // If passing grade, mark as completed
            if (gradePoint >= 1.0m)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }
            else if (gradePoint > 0)
            {
                enrollment.Status = EnrollmentStatus.Failed;
            }
        }

        enrollment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Update student's CGPA
        var student = await _context.Students
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Section)
                    .ThenInclude(s => s.Course)
            .FirstOrDefaultAsync(s => s.Id == enrollment.StudentId);

        if (student != null)
        {
            student.CGPA = _gradeCalculationService.CalculateCGPA(student.Enrollments.ToList());
            await _context.SaveChangesAsync();
        }

        return Ok(new 
        { 
            message = "Grades updated successfully",
            letterGrade = enrollment.LetterGrade,
            gradePoint = enrollment.GradePoint
        });
    }

    /// <summary>
    /// Bulk input grades for a section (Faculty only)
    /// </summary>
    [HttpPost("bulk")]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult> BulkInputGrades([FromBody] BulkGradeInputRequest request)
    {
        var results = new List<object>();

        foreach (var gradeInput in request.Grades)
        {
            try
            {
                var result = await InputGrades(gradeInput);
                results.Add(new { enrollmentId = gradeInput.EnrollmentId, success = true });
            }
            catch (Exception ex)
            {
                results.Add(new { enrollmentId = gradeInput.EnrollmentId, success = false, error = ex.Message });
            }
        }

        return Ok(new { message = "Bulk grade input completed", results });
    }
}
