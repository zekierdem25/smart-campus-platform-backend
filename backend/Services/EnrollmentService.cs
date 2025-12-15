using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public interface IEnrollmentService
{
    Task<EnrollmentCheckResult> CheckEnrollmentEligibilityAsync(Guid studentId, Guid sectionId);
    Task<Enrollment> EnrollStudentAsync(Guid studentId, Guid sectionId);
    Task<bool> DropCourseAsync(Guid enrollmentId, Guid studentId);
    Task<List<EnrollmentDto>> GetStudentEnrollmentsAsync(Guid studentId);
}

public class EnrollmentService : IEnrollmentService
{
    private readonly ApplicationDbContext _context;
    private readonly IPrerequisiteService _prerequisiteService;
    private readonly IScheduleConflictService _scheduleConflictService;

    // Drop period: first 4 weeks from semester start
    private const int DropPeriodWeeks = 4;

    public EnrollmentService(
        ApplicationDbContext context,
        IPrerequisiteService prerequisiteService,
        IScheduleConflictService scheduleConflictService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _prerequisiteService = prerequisiteService ?? throw new ArgumentNullException(nameof(prerequisiteService));
        _scheduleConflictService = scheduleConflictService ?? throw new ArgumentNullException(nameof(scheduleConflictService));
    }

    /// <summary>
    /// Comprehensive check for enrollment eligibility.
    /// Checks: prerequisites, schedule conflicts, capacity.
    /// </summary>
    public async Task<EnrollmentCheckResult> CheckEnrollmentEligibilityAsync(Guid studentId, Guid sectionId)
    {
        var result = new EnrollmentCheckResult { CanEnroll = true };

        // Get section with course info
        var section = await _context.CourseSections
            .Include(s => s.Course)
            .Include(s => s.Classroom)
            .FirstOrDefaultAsync(s => s.Id == sectionId);

        if (section == null)
        {
            result.CanEnroll = false;
            result.Errors.Add("Ders şubesi bulunamadı");
            return result;
        }

        if (!section.IsActive)
        {
            result.CanEnroll = false;
            result.Errors.Add("Ders şubesi aktif değil");
            return result;
        }

        // Check 1: Already enrolled?
        var existingEnrollment = await _context.Enrollments
            .AnyAsync(e => e.StudentId == studentId && 
                          e.SectionId == sectionId && 
                          e.Status == EnrollmentStatus.Active);

        if (existingEnrollment)
        {
            result.CanEnroll = false;
            result.Errors.Add("Bu ders şubesine zaten kayıtlısınız");
            return result;
        }

        // Check if enrolled in another section of same course
        var enrolledInSameCourse = await _context.Enrollments
            .AnyAsync(e => e.StudentId == studentId && 
                          e.Section.CourseId == section.CourseId && 
                          e.Status == EnrollmentStatus.Active);

        if (enrolledInSameCourse)
        {
            result.CanEnroll = false;
            result.Errors.Add("Bu dersin başka bir şubesine zaten kayıtlısınız");
            return result;
        }

        // Check 2: Prerequisites
        var missingPrereqs = await _prerequisiteService.GetMissingPrerequisitesAsync(section.CourseId, studentId);
        if (missingPrereqs.Any())
        {
            result.CanEnroll = false;
            result.Errors.Add($"Eksik önkoşullar: {string.Join(", ", missingPrereqs)}");
        }

        // Check 3: Schedule conflict
        var studentSections = await _context.Enrollments
            .Where(e => e.StudentId == studentId && 
                       e.Status == EnrollmentStatus.Active &&
                       e.Section.Semester == section.Semester &&
                       e.Section.Year == section.Year)
            .Select(e => e.Section.ScheduleJson)
            .ToListAsync();

        var existingSchedule = studentSections
            .SelectMany(s => _scheduleConflictService.ParseScheduleJson(s))
            .ToList();

        var newSchedule = _scheduleConflictService.ParseScheduleJson(section.ScheduleJson);

        if (_scheduleConflictService.HasScheduleConflict(existingSchedule, newSchedule))
        {
            result.CanEnroll = false;
            result.Errors.Add("Mevcut derslerle program çakışması var");
        }

        // Check 4: Capacity (with some buffer warning)
        if (section.EnrolledCount >= section.Capacity)
        {
            result.CanEnroll = false;
            result.Errors.Add("Kontenjan dolu");
        }
        else if (section.EnrolledCount >= section.Capacity * 0.9)
        {
            result.Warnings.Add($"Sadece {section.Capacity - section.EnrolledCount} kontenjan kaldı");
        }

        return result;
    }

    /// <summary>
    /// Enrolls a student in a section.
    /// Uses atomic increment for capacity control.
    /// </summary>
    public async Task<Enrollment> EnrollStudentAsync(Guid studentId, Guid sectionId)
    {
        // Double-check eligibility
        var eligibility = await CheckEnrollmentEligibilityAsync(studentId, sectionId);
        if (!eligibility.CanEnroll)
        {
            throw new InvalidOperationException(string.Join("; ", eligibility.Errors));
        }

        // Atomic capacity increment with row-level locking
        // This prevents race conditions
        var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
            "UPDATE CourseSections SET EnrolledCount = EnrolledCount + 1 WHERE Id = {0} AND EnrolledCount < Capacity",
            sectionId);

        if (rowsAffected == 0)
        {
            throw new InvalidOperationException("Section is full or enrollment failed");
        }

        // Create enrollment record
        var enrollment = new Enrollment
        {
            StudentId = studentId,
            SectionId = sectionId,
            Status = EnrollmentStatus.Active,
            EnrollmentDate = DateTime.UtcNow
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return enrollment;
    }

    /// <summary>
    /// Drops a course (removes enrollment).
    /// Only allowed within drop period.
    /// </summary>
    public async Task<bool> DropCourseAsync(Guid enrollmentId, Guid studentId)
    {
        var enrollment = await _context.Enrollments
            .Include(e => e.Section)
            .FirstOrDefaultAsync(e => e.Id == enrollmentId && e.StudentId == studentId);

        if (enrollment == null)
            throw new InvalidOperationException("Enrollment not found");

        if (enrollment.Status != EnrollmentStatus.Active)
            throw new InvalidOperationException("Cannot drop - enrollment is not active");

        // Check drop period (4 weeks from enrollment)
        var dropDeadline = enrollment.EnrollmentDate.AddDays(DropPeriodWeeks * 7);
        if (DateTime.UtcNow > dropDeadline)
        {
            throw new InvalidOperationException($"Drop period has ended (deadline was {dropDeadline:yyyy-MM-dd})");
        }

        // Update enrollment status
        enrollment.Status = EnrollmentStatus.Dropped;
        enrollment.UpdatedAt = DateTime.UtcNow;

        // Decrease section count
        await _context.Database.ExecuteSqlRawAsync(
            "UPDATE CourseSections SET EnrolledCount = EnrolledCount - 1 WHERE Id = {0} AND EnrolledCount > 0",
            enrollment.SectionId);

        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Gets all active enrollments for a student.
    /// </summary>
    public async Task<List<EnrollmentDto>> GetStudentEnrollmentsAsync(Guid studentId)
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Course)
            .Include(e => e.Section)
                .ThenInclude(s => s.Instructor)
                    .ThenInclude(i => i.User)
            .Where(e => e.StudentId == studentId && e.Status == EnrollmentStatus.Active)
            .ToListAsync();

        return enrollments.Select(e => new EnrollmentDto
        {
            Id = e.Id,
            StudentId = e.StudentId,
            SectionId = e.SectionId,
            CourseCode = e.Section.Course.Code,
            CourseName = e.Section.Course.Name,
            SectionNumber = e.Section.SectionNumber,
            InstructorName = $"{e.Section.Instructor.User.FirstName} {e.Section.Instructor.User.LastName}",
            Status = e.Status.ToString(),
            EnrollmentDate = e.EnrollmentDate,
            MidtermGrade = e.MidtermGrade,
            FinalGrade = e.FinalGrade,
            HomeworkGrade = e.HomeworkGrade,
            LetterGrade = e.LetterGrade,
            GradePoint = e.GradePoint,
            Schedule = _scheduleConflictService.ParseScheduleJson(e.Section.ScheduleJson)
        }).ToList();
    }
}
