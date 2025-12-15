using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System.Security.Claims;
using System.IO;

namespace SmartCampus.API.Controllers;

[ApiController]
[Route("api/v1/attendance")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IAttendanceService _attendanceService;
    private readonly ISpoofingDetectionService _spoofingDetectionService;
    private readonly INotificationService _notificationService;
    private readonly ILogger<AttendanceController> _logger;
    private readonly IWebHostEnvironment _environment;

    public AttendanceController(
        ApplicationDbContext context,
        IAttendanceService attendanceService,
        ISpoofingDetectionService spoofingDetectionService,
        INotificationService notificationService,
        ILogger<AttendanceController> logger,
        IWebHostEnvironment environment)
    {
        _context = context;
        _attendanceService = attendanceService;
        _spoofingDetectionService = spoofingDetectionService;
        _notificationService = notificationService;
        _logger = logger;
        _environment = environment;
    }

    // ========== Session Management (Faculty) ==========

    /// <summary>
    /// Start an attendance session (Faculty only)
    /// </summary>
    [HttpPost("sessions")]
    [Authorize(Roles = "Faculty")]
    public async Task<ActionResult<AttendanceSessionDto>> StartSession([FromBody] CreateAttendanceSessionRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId));
        if (faculty == null)
            return BadRequest(new { message = "Faculty profile not found" });

        // Get section with classroom
        var section = await _context.CourseSections
            .Include(s => s.Course)
            .Include(s => s.Classroom)
            .FirstOrDefaultAsync(s => s.Id == request.SectionId);

        if (section == null)
            return NotFound(new { message = "Section not found" });

        if (section.InstructorId != faculty.Id)
            return Forbid();

        if (section.Classroom == null)
            return BadRequest(new { message = "Section has no assigned classroom" });

        // Check if there's already an active session
        var existingSession = await _context.AttendanceSessions
            .AnyAsync(s => s.SectionId == request.SectionId && s.Status == AttendanceSessionStatus.Active);

        if (existingSession)
            return BadRequest(new { message = "An active session already exists for this section" });

        // Create session using teacher's current location
        var session = new AttendanceSession
        {
            SectionId = request.SectionId,
            InstructorId = faculty.Id,
            Date = DateTime.UtcNow.Date,
            StartTime = DateTime.UtcNow.TimeOfDay,
            EndTime = DateTime.UtcNow.AddMinutes(request.DurationMinutes).TimeOfDay,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            GeofenceRadius = request.GeofenceRadius,
            QrCode = _attendanceService.GenerateQrCode(Guid.NewGuid()), // Will be regenerated
            QrCodeExpiresAt = DateTime.UtcNow.AddSeconds(60),
            Status = AttendanceSessionStatus.Active
        };

        // Regenerate QR with actual session ID
        _context.AttendanceSessions.Add(session);
        await _context.SaveChangesAsync();

        session.QrCode = _attendanceService.GenerateQrCode(session.Id);
        await _context.SaveChangesAsync();

        // Get enrolled student count
        var enrolledCount = await _context.Enrollments
            .CountAsync(e => e.SectionId == request.SectionId && e.Status == EnrollmentStatus.Active);

        // Notify enrolled students asynchronously (fire and forget)
        _ = Task.Run(async () =>
        {
            try
            {
                await _notificationService.NotifyStudentsOnAttendanceSessionStartAsync(session.Id);
            }
            catch (Exception ex)
            {
                // Log error but don't fail the request
                _logger.LogError(ex, "Failed to send attendance session start notifications");
            }
        });

        return CreatedAtAction(nameof(GetSession), new { id = session.Id }, new AttendanceSessionDto
        {
            Id = session.Id,
            SectionId = session.SectionId,
            CourseCode = section.Course.Code,
            CourseName = section.Course.Name,
            SectionNumber = section.SectionNumber,
            InstructorId = faculty.Id,
            Date = session.Date,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Latitude = session.Latitude,
            Longitude = session.Longitude,
            GeofenceRadius = session.GeofenceRadius,
            QrCode = session.QrCode,
            QrCodeExpiresAt = session.QrCodeExpiresAt,
            Status = session.Status.ToString(),
            TotalStudents = enrolledCount,
            PresentStudents = 0,
            FlaggedStudents = 0,
            CreatedAt = session.CreatedAt
        });
    }

    /// <summary>
    /// Get session details (Faculty)
    /// </summary>
    [HttpGet("sessions/{id}")]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult<AttendanceSessionDto>> GetSession(Guid id)
    {
        var session = await _context.AttendanceSessions
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Include(s => s.Instructor)
                .ThenInclude(i => i.User)
            .Include(s => s.Records)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (session == null)
            return NotFound(new { message = "Session not found" });

        var enrolledCount = await _context.Enrollments
            .CountAsync(e => e.SectionId == session.SectionId && e.Status == EnrollmentStatus.Active);

        // Refresh QR code if active and expired
        if (session.Status == AttendanceSessionStatus.Active && 
            session.QrCodeExpiresAt < DateTime.UtcNow)
        {
            session.QrCode = _attendanceService.GenerateQrCode(session.Id);
            session.QrCodeExpiresAt = DateTime.UtcNow.AddSeconds(5);
            await _context.SaveChangesAsync();
        }

        return Ok(new AttendanceSessionDto
        {
            Id = session.Id,
            SectionId = session.SectionId,
            CourseCode = session.Section.Course.Code,
            CourseName = session.Section.Course.Name,
            SectionNumber = session.Section.SectionNumber,
            InstructorId = session.InstructorId,
            InstructorName = $"{session.Instructor.User.FirstName} {session.Instructor.User.LastName}",
            Date = session.Date,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            Latitude = session.Latitude,
            Longitude = session.Longitude,
            GeofenceRadius = session.GeofenceRadius,
            QrCode = session.QrCode,
            QrCodeExpiresAt = session.QrCodeExpiresAt,
            Status = session.Status.ToString(),
            TotalStudents = enrolledCount,
            PresentStudents = session.Records.Count,
            FlaggedStudents = session.Records.Count(r => r.IsFlagged),
            CreatedAt = session.CreatedAt
        });
    }

    /// <summary>
    /// Close an attendance session (Faculty)
    /// </summary>
    [HttpPut("sessions/{id}/close")]
    [Authorize(Roles = "Faculty")]
    public async Task<ActionResult> CloseSession(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId));

        var session = await _context.AttendanceSessions.FindAsync(id);
        if (session == null)
            return NotFound(new { message = "Session not found" });

        if (session.InstructorId != faculty?.Id)
            return Forbid();

        session.Status = AttendanceSessionStatus.Closed;
        session.EndTime = DateTime.UtcNow.TimeOfDay;
        session.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Session closed successfully" });
    }

    /// <summary>
    /// Get my sessions (Faculty)
    /// </summary>
    [HttpGet("sessions/my-sessions")]
    [Authorize(Roles = "Faculty")]
    public async Task<ActionResult<List<AttendanceSessionListDto>>> GetMySessions(
        [FromQuery] Guid? sectionId = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId));
        if (faculty == null)
            return BadRequest(new { message = "Faculty profile not found" });

        var query = _context.AttendanceSessions
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Include(s => s.Records)
            .Where(s => s.InstructorId == faculty.Id);

        if (sectionId.HasValue)
            query = query.Where(s => s.SectionId == sectionId.Value);

        if (startDate.HasValue)
            query = query.Where(s => s.Date >= startDate.Value.Date);

        if (endDate.HasValue)
            query = query.Where(s => s.Date <= endDate.Value.Date);

        var sessions = await query
            .OrderByDescending(s => s.Date)
            .ThenByDescending(s => s.StartTime)
            .ToListAsync();

        var result = sessions.Select(s => new AttendanceSessionListDto
        {
            Id = s.Id,
            CourseCode = s.Section.Course.Code,
            CourseName = s.Section.Course.Name,
            SectionNumber = s.Section.SectionNumber,
            Date = s.Date,
            StartTime = s.StartTime,
            Status = s.Status.ToString(),
            PresentCount = s.Records.Count,
            TotalCount = _context.Enrollments.Count(e => e.SectionId == s.SectionId && e.Status == EnrollmentStatus.Active)
        });

        return Ok(result);
    }

    // ========== Check-in (Student) ==========

    /// <summary>
    /// Check in to an attendance session using GPS (Student)
    /// </summary>
    [HttpPost("sessions/{id}/checkin")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<CheckInResult>> CheckIn(Guid id, [FromBody] CheckInRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        var session = await _context.AttendanceSessions
            .Include(s => s.Section)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (session == null)
            return NotFound(new { message = "Session not found" });

        if (session.Status != AttendanceSessionStatus.Active)
            return BadRequest(new { message = "Session is not active" });

        // Check if student is enrolled
        var isEnrolled = await _context.Enrollments
            .AnyAsync(e => e.StudentId == student.Id && 
                          e.SectionId == session.SectionId && 
                          e.Status == EnrollmentStatus.Active);

        if (!isEnrolled)
            return BadRequest(new { message = "You are not enrolled in this course" });

        // Check if already checked in
        var alreadyCheckedIn = await _context.AttendanceRecords
            .AnyAsync(r => r.SessionId == id && r.StudentId == student.Id);

        if (alreadyCheckedIn)
            return BadRequest(new { message = "You have already checked in to this session" });

        // Calculate distance
        var distance = _attendanceService.CalculateDistance(
            request.Latitude, request.Longitude,
            session.Latitude, session.Longitude);

        var isWithinGeofence = _attendanceService.IsWithinGeofence(
            request.Latitude, request.Longitude,
            session.Latitude, session.Longitude,
            session.GeofenceRadius, request.Accuracy);

        // Get client IP
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = Request.Headers.UserAgent.ToString();

        // Spoofing detection
        var spoofCheck = await _spoofingDetectionService.CheckForSpoofingAsync(
            student.Id,
            request.Latitude,
            request.Longitude,
            ipAddress,
            DateTime.UtcNow,
            request.IsMockLocation,
            request.SensorData,
            userAgent);

        // Create attendance record
        var record = new AttendanceRecord
        {
            SessionId = id,
            StudentId = student.Id,
            CheckInTime = DateTime.UtcNow,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            DistanceFromCenter = (decimal)distance,
            Accuracy = request.Accuracy,
            IpAddress = ipAddress,
            UserAgent = Request.Headers.UserAgent.ToString(),
            IsFlagged = spoofCheck.IsSuspicious || !isWithinGeofence,
            FlagReason = spoofCheck.IsSuspicious 
                ? spoofCheck.Reason 
                : (!isWithinGeofence ? "OUTSIDE_GEOFENCE" : null),
            // Store sensor data for audit trail
            SensorAccelerationX = request.SensorData?.X,
            SensorAccelerationY = request.SensorData?.Y,
            SensorAccelerationZ = request.SensorData?.Z,
            SensorDataUnavailable = request.SensorData?.Unavailable ?? true
        };

        _context.AttendanceRecords.Add(record);
        await _context.SaveChangesAsync();

        // Prepare result
        var result = new CheckInResult
        {
            Success = isWithinGeofence && !spoofCheck.IsSuspicious,
            DistanceFromCenter = (decimal)distance,
            IsFlagged = record.IsFlagged,
            FlagReason = record.FlagReason
        };

        if (!isWithinGeofence)
        {
            result.Message = $"You are {distance:F0}m from the classroom. Maximum allowed: {session.GeofenceRadius}m";
        }
        else if (spoofCheck.IsSuspicious)
        {
            result.Message = $"Check-in flagged for review: {spoofCheck.Reason}";
        }
        else
        {
            result.Message = "Check-in successful!";
        }

        return Ok(result);
    }

    /// <summary>
    /// Check in using QR code (Student) - Bonus feature
    /// </summary>
    [HttpPost("sessions/{id}/checkin/qr")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<CheckInResult>> CheckInWithQr(Guid id, [FromBody] QrCheckInRequest request)
    {
        // Validate QR code
        if (!_attendanceService.ValidateQrCode(request.QrCode, id))
        {
            return BadRequest(new CheckInResult
            {
                Success = false,
                Message = "Invalid or expired QR code"
            });
        }

        // Proceed with regular check-in but mark as QR verified
        var checkInRequest = new CheckInRequest
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Accuracy = request.Accuracy,
            SensorData = request.SensorData
        };

        var result = await CheckIn(id, checkInRequest);
        
        if (result.Result is OkObjectResult okResult && okResult.Value is CheckInResult checkInResult)
        {
            // Mark as QR verified
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId!));

            if (student != null)
            {
                var record = await _context.AttendanceRecords
                    .FirstOrDefaultAsync(r => r.SessionId == id && r.StudentId == student.Id);
                
                if (record != null)
                {
                    record.IsQrVerified = true;
                    await _context.SaveChangesAsync();
                }
            }

            checkInResult.Message = "QR check-in successful!";
            return Ok(checkInResult);
        }

        return result;
    }

    /// <summary>
    /// Get my attendance status (Student)
    /// </summary>
    [HttpGet("my-attendance")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<List<StudentAttendanceDto>>> GetMyAttendance()
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
            .Include(e => e.Section)
                .ThenInclude(s => s.AttendanceSessions)
                    .ThenInclude(a => a.Records)
            .Where(e => e.StudentId == student.Id && e.Status == EnrollmentStatus.Active)
            .ToListAsync();

        var excuseRequests = await _context.ExcuseRequests
            .Where(er => er.StudentId == student.Id && er.Status == ExcuseRequestStatus.Approved)
            .ToListAsync();

        var result = enrollments.Select(e =>
        {
            var closedSessions = e.Section.AttendanceSessions
                .Where(s => s.Status == AttendanceSessionStatus.Closed)
                .ToList();

            var attendedSessions = closedSessions
                .Count(s => s.Records.Any(r => r.StudentId == student.Id));

            var excusedAbsences = closedSessions
                .Count(s => excuseRequests.Any(er => er.SessionId == s.Id));

            var totalSessions = closedSessions.Count;
            var percentage = totalSessions > 0 
                ? Math.Round((decimal)(attendedSessions + excusedAbsences) / totalSessions * 100, 1) 
                : 100;

            var status = percentage >= 80 ? "OK" : percentage >= 70 ? "Warning" : "Critical";

            return new StudentAttendanceDto
            {
                CourseId = e.Section.CourseId,
                CourseCode = e.Section.Course.Code,
                CourseName = e.Section.Course.Name,
                TotalSessions = totalSessions,
                AttendedSessions = attendedSessions,
                ExcusedAbsences = excusedAbsences,
                AttendancePercentage = percentage,
                Status = status,
                History = closedSessions.Select(s => new AttendanceHistoryDto
                {
                    SessionId = s.Id,
                    Date = s.Date,
                    IsPresent = s.Records.Any(r => r.StudentId == student.Id),
                    IsExcused = excuseRequests.Any(er => er.SessionId == s.Id),
                    CheckInTime = s.Records.FirstOrDefault(r => r.StudentId == student.Id)?.CheckInTime
                }).OrderByDescending(h => h.Date).ToList()
            };
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Get active attendance sessions for enrolled courses (Student)
    /// </summary>
    [HttpGet("active-sessions")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult<List<AttendanceSessionDto>>> GetActiveSessions()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        // Get all enrollments for this student
        var enrollments = await _context.Enrollments
            .Include(e => e.Section)
                .ThenInclude(s => s.Course)
            .Where(e => e.StudentId == student.Id && e.Status == EnrollmentStatus.Active)
            .Select(e => e.SectionId)
            .ToListAsync();

        if (!enrollments.Any())
            return Ok(new List<AttendanceSessionDto>());

        // Get active sessions for enrolled sections
        var activeSessions = await _context.AttendanceSessions
            .Include(s => s.Section)
                .ThenInclude(sec => sec.Course)
            .Include(s => s.Instructor)
                .ThenInclude(i => i.User)
            .Include(s => s.Records)
            .Where(s => enrollments.Contains(s.SectionId) && 
                       s.Status == AttendanceSessionStatus.Active &&
                       s.Date == DateTime.UtcNow.Date &&
                       s.EndTime > DateTime.UtcNow.TimeOfDay)
            .ToListAsync();

        var result = activeSessions.Select(s => new AttendanceSessionDto
        {
            Id = s.Id,
            SectionId = s.SectionId,
            CourseCode = s.Section.Course.Code,
            CourseName = s.Section.Course.Name,
            SectionNumber = s.Section.SectionNumber,
            InstructorId = s.InstructorId,
            InstructorName = $"{s.Instructor.User.FirstName} {s.Instructor.User.LastName}",
            Date = s.Date,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Latitude = s.Latitude,
            Longitude = s.Longitude,
            GeofenceRadius = s.GeofenceRadius,
            QrCode = s.QrCode,
            QrCodeExpiresAt = s.QrCodeExpiresAt,
            Status = s.Status.ToString(),
            TotalStudents = _context.Enrollments.Count(e => e.SectionId == s.SectionId && e.Status == EnrollmentStatus.Active),
            PresentStudents = s.Records.Count,
            FlaggedStudents = s.Records.Count(r => r.IsFlagged),
            CreatedAt = s.CreatedAt,
            HasCheckedIn = s.Records.Any(r => r.StudentId == student.Id) // Check if student has already checked in
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Get attendance report for a section (Faculty)
    /// </summary>
    [HttpGet("report/{sectionId}")]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult<AttendanceReportDto>> GetAttendanceReport(
        Guid sectionId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var section = await _context.CourseSections
            .Include(s => s.Course)
            .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                    .ThenInclude(st => st.User)
            .Include(s => s.AttendanceSessions)
                .ThenInclude(a => a.Records)
            .FirstOrDefaultAsync(s => s.Id == sectionId);

        if (section == null)
            return NotFound(new { message = "Section not found" });

        var sessions = section.AttendanceSessions
            .Where(s => s.Status == AttendanceSessionStatus.Closed);

        if (startDate.HasValue)
            sessions = sessions.Where(s => s.Date >= startDate.Value.Date);
        if (endDate.HasValue)
            sessions = sessions.Where(s => s.Date <= endDate.Value.Date);

        var sessionList = sessions.ToList();
        var totalSessions = sessionList.Count;

        var excuseRequests = await _context.ExcuseRequests
            .Where(er => sessionList.Select(s => s.Id).Contains(er.SessionId) && 
                        er.Status == ExcuseRequestStatus.Approved)
            .ToListAsync();

        var students = section.Enrollments
            .Where(e => e.Status == EnrollmentStatus.Active)
            .Select(e =>
            {
                var attended = sessionList.Count(s => s.Records.Any(r => r.StudentId == e.StudentId));
                var excused = sessionList.Count(s => excuseRequests.Any(er => er.StudentId == e.StudentId && er.SessionId == s.Id));
                var flagged = sessionList.Sum(s => s.Records.Count(r => r.StudentId == e.StudentId && r.IsFlagged));
                var percentage = totalSessions > 0 ? Math.Round((decimal)(attended + excused) / totalSessions * 100, 1) : 100;

                return new StudentAttendanceReportDto
                {
                    StudentId = e.StudentId,
                    StudentName = $"{e.Student.User.FirstName} {e.Student.User.LastName}",
                    StudentNumber = e.Student.StudentNumber,
                    AttendedSessions = attended,
                    ExcusedAbsences = excused,
                    AttendancePercentage = percentage,
                    Status = percentage >= 80 ? "OK" : percentage >= 70 ? "Warning" : "Critical",
                    FlagCount = flagged
                };
            })
            .OrderBy(s => s.StudentNumber)
            .ToList();

        return Ok(new AttendanceReportDto
        {
            SectionId = sectionId,
            CourseCode = section.Course.Code,
            CourseName = section.Course.Name,
            SectionNumber = section.SectionNumber,
            TotalSessions = totalSessions,
            Students = students
        });
    }

    // ========== Excuse Requests ==========

    /// <summary>
    /// Submit an excuse request (Student)
    /// </summary>
    [HttpPost("excuse-requests")]
    [Authorize(Roles = "Student")]
    public async Task<ActionResult> CreateExcuseRequest([FromForm] CreateExcuseRequestDto request, [FromForm] IFormFile? file)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var student = await _context.Students
            .FirstOrDefaultAsync(s => s.UserId == Guid.Parse(userId));
        if (student == null)
            return BadRequest(new { message = "Student profile not found" });

        var session = await _context.AttendanceSessions.FindAsync(request.SessionId);
        if (session == null)
            return NotFound(new { message = "Session not found" });

        // Check if already has an excuse request
        var existing = await _context.ExcuseRequests
            .AnyAsync(er => er.StudentId == student.Id && er.SessionId == request.SessionId);
        if (existing)
            return BadRequest(new { message = "Excuse request already submitted for this session" });

        string? documentUrl = null;

        // Handle file upload if provided
        if (file != null && file.Length > 0)
        {
            // Validate file extension
            var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest(new { message = "Invalid file type. Only PDF, JPG, and PNG files are allowed." });
            }

            // Validate file size (5MB max)
            const long maxFileSize = 5 * 1024 * 1024; // 5MB in bytes
            if (file.Length > maxFileSize)
            {
                return BadRequest(new { message = "File size exceeds the maximum limit of 5MB." });
            }

            try
            {
                // Create uploads/excuses directory if it doesn't exist
                var uploadsPath = Path.Combine(_environment.ContentRootPath, "uploads", "excuses");
                Directory.CreateDirectory(uploadsPath);

                // Generate unique filename
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Store relative path for URL access
                documentUrl = $"/uploads/excuses/{uniqueFileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading excuse document");
                return StatusCode(500, new { message = "Error uploading file. Please try again." });
            }
        }

        var excuseRequest = new ExcuseRequest
        {
            StudentId = student.Id,
            SessionId = request.SessionId,
            Reason = request.Reason,
            DocumentUrl = documentUrl,
            Status = ExcuseRequestStatus.Pending
        };

        _context.ExcuseRequests.Add(excuseRequest);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExcuseRequests), new { id = excuseRequest.Id }, new { message = "Excuse request submitted" });
    }

    /// <summary>
    /// Get excuse requests for review (Faculty)
    /// </summary>
    [HttpGet("excuse-requests")]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult<List<ExcuseRequestDto>>> GetExcuseRequests(
        [FromQuery] Guid? sectionId = null,
        [FromQuery] string? status = null)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId));

        var query = _context.ExcuseRequests
            .Include(er => er.Student)
                .ThenInclude(s => s.User)
            .Include(er => er.Session)
                .ThenInclude(s => s.Section)
                    .ThenInclude(sec => sec.Course)
            .AsQueryable();

        // Filter by faculty's sections
        if (faculty != null && User.FindFirst(ClaimTypes.Role)?.Value != "Admin")
        {
            query = query.Where(er => er.Session.InstructorId == faculty.Id);
        }

        if (sectionId.HasValue)
            query = query.Where(er => er.Session.SectionId == sectionId.Value);

        if (!string.IsNullOrEmpty(status) && Enum.TryParse<ExcuseRequestStatus>(status, true, out var statusEnum))
            query = query.Where(er => er.Status == statusEnum);

        var requests = await query
            .OrderByDescending(er => er.CreatedAt)
            .ToListAsync();

        var result = requests.Select(er => new ExcuseRequestDto
        {
            Id = er.Id,
            StudentId = er.StudentId,
            StudentName = $"{er.Student.User.FirstName} {er.Student.User.LastName}",
            StudentNumber = er.Student.StudentNumber,
            SessionId = er.SessionId,
            CourseCode = er.Session.Section.Course.Code,
            CourseName = er.Session.Section.Course.Name,
            SessionDate = er.Session.Date,
            Reason = er.Reason,
            DocumentUrl = er.DocumentUrl,
            Status = er.Status.ToString(),
            ReviewedBy = er.ReviewedBy,
            ReviewedAt = er.ReviewedAt,
            Notes = er.Notes,
            CreatedAt = er.CreatedAt
        });

        return Ok(result);
    }

    /// <summary>
    /// Approve an excuse request (Faculty)
    /// </summary>
    [HttpPut("excuse-requests/{id}/approve")]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult> ApproveExcuseRequest(Guid id, [FromBody] ReviewExcuseRequestDto? review)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId));

        var excuseRequest = await _context.ExcuseRequests
            .Include(er => er.Session)
            .FirstOrDefaultAsync(er => er.Id == id);

        if (excuseRequest == null)
            return NotFound(new { message = "Excuse request not found" });

        excuseRequest.Status = ExcuseRequestStatus.Approved;
        excuseRequest.ReviewedBy = faculty?.Id;
        excuseRequest.ReviewedAt = DateTime.UtcNow;
        excuseRequest.Notes = review?.Notes;
        excuseRequest.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Excuse request approved" });
    }

    /// <summary>
    /// Reject an excuse request (Faculty)
    /// </summary>
    [HttpPut("excuse-requests/{id}/reject")]
    [Authorize(Roles = "Faculty,Admin")]
    public async Task<ActionResult> RejectExcuseRequest(Guid id, [FromBody] ReviewExcuseRequestDto? review)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var faculty = await _context.Faculties
            .FirstOrDefaultAsync(f => f.UserId == Guid.Parse(userId));

        var excuseRequest = await _context.ExcuseRequests.FindAsync(id);
        if (excuseRequest == null)
            return NotFound(new { message = "Excuse request not found" });

        excuseRequest.Status = ExcuseRequestStatus.Rejected;
        excuseRequest.ReviewedBy = faculty?.Id;
        excuseRequest.ReviewedAt = DateTime.UtcNow;
        excuseRequest.Notes = review?.Notes;
        excuseRequest.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Excuse request rejected" });
    }

    // ========== QR Code Refresh (Real-time) ==========

    /// <summary>
    /// Get refreshed QR code for active session (Faculty)
    /// </summary>
    [HttpGet("sessions/{id}/qr")]
    [Authorize(Roles = "Faculty")]
    public async Task<ActionResult<QrCodeUpdateDto>> GetRefreshedQrCode(Guid id)
    {
        var session = await _context.AttendanceSessions.FindAsync(id);
        if (session == null)
            return NotFound(new { message = "Session not found" });

        if (session.Status != AttendanceSessionStatus.Active)
            return BadRequest(new { message = "Session is not active" });

        // Always generate new QR
        session.QrCode = _attendanceService.GenerateQrCode(session.Id);
        session.QrCodeExpiresAt = DateTime.UtcNow.AddSeconds(60);
        await _context.SaveChangesAsync();

        return Ok(new QrCodeUpdateDto
        {
            SessionId = session.Id,
            QrCode = session.QrCode,
            ExpiresAt = session.QrCodeExpiresAt.Value
        });
    }
}
