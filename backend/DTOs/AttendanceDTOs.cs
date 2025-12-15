namespace SmartCampus.API.DTOs;

// ========== Attendance Session DTOs ==========

public class AttendanceSessionDto
{
    public Guid Id { get; set; }
    public Guid SectionId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int SectionNumber { get; set; }
    public Guid InstructorId { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int GeofenceRadius { get; set; }
    public string? QrCode { get; set; }
    public DateTime? QrCodeExpiresAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public int TotalStudents { get; set; }
    public int PresentStudents { get; set; }
    public int FlaggedStudents { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool HasCheckedIn { get; set; } // Whether the current student has checked in to this session
}

public class CreateAttendanceSessionRequest
{
    public Guid SectionId { get; set; }
    public int DurationMinutes { get; set; } = 30;
    public int GeofenceRadius { get; set; } = 15;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}

public class AttendanceSessionListDto
{
    public Guid Id { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int SectionNumber { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public string Status { get; set; } = string.Empty;
    public int PresentCount { get; set; }
    public int TotalCount { get; set; }
}

// ========== Attendance Record DTOs ==========

public class AttendanceRecordDto
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public DateTime CheckInTime { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public decimal DistanceFromCenter { get; set; }
    public decimal Accuracy { get; set; }
    public bool IsFlagged { get; set; }
    public string? FlagReason { get; set; }
    public bool IsQrVerified { get; set; }
}

public class SensorDataDto
{
    public decimal? X { get; set; }
    public decimal? Y { get; set; }
    public decimal? Z { get; set; }
    public bool Unavailable { get; set; } = false;
}

public class CheckInRequest
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public decimal Accuracy { get; set; }
    public bool? IsMockLocation { get; set; } // From browser API
    public SensorDataDto? SensorData { get; set; } // Accelerometer data for spoofing detection
}

public class CheckInResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public decimal DistanceFromCenter { get; set; }
    public bool IsFlagged { get; set; }
    public string? FlagReason { get; set; }
}

public class QrCheckInRequest
{
    public string QrCode { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public decimal Accuracy { get; set; }
    public SensorDataDto? SensorData { get; set; } // Accelerometer data for spoofing detection
}

// ========== Attendance Statistics DTOs ==========

public class StudentAttendanceDto
{
    public Guid CourseId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int TotalSessions { get; set; }
    public int AttendedSessions { get; set; }
    public int ExcusedAbsences { get; set; }
    public decimal AttendancePercentage { get; set; }
    public string Status { get; set; } = string.Empty; // "OK", "Warning", "Critical"
    public List<AttendanceHistoryDto> History { get; set; } = new();
}

public class AttendanceHistoryDto
{
    public Guid SessionId { get; set; }
    public DateTime Date { get; set; }
    public bool IsPresent { get; set; }
    public bool IsExcused { get; set; }
    public DateTime? CheckInTime { get; set; }
}

public class AttendanceReportDto
{
    public Guid SectionId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public int SectionNumber { get; set; }
    public int TotalSessions { get; set; }
    public List<StudentAttendanceReportDto> Students { get; set; } = new();
}

public class StudentAttendanceReportDto
{
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public int AttendedSessions { get; set; }
    public int ExcusedAbsences { get; set; }
    public decimal AttendancePercentage { get; set; }
    public string Status { get; set; } = string.Empty;
    public int FlagCount { get; set; }
}

// ========== Excuse Request DTOs ==========

public class ExcuseRequestDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public Guid SessionId { get; set; }
    public string CourseCode { get; set; } = string.Empty;
    public string CourseName { get; set; } = string.Empty;
    public DateTime SessionDate { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? DocumentUrl { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid? ReviewedBy { get; set; }
    public string? ReviewerName { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateExcuseRequestDto
{
    public Guid SessionId { get; set; }
    public string Reason { get; set; } = string.Empty;
    // Document will be uploaded separately and URL set via file upload
}

public class ReviewExcuseRequestDto
{
    public string? Notes { get; set; }
}

// ========== GPS & Spoofing Detection DTOs ==========

public class SpoofingCheckResult
{
    public bool IsSuspicious { get; set; }
    public string? Reason { get; set; }
    public decimal? VelocityMps { get; set; } // meters per second
}

public class CampusSettings
{
    public List<string> AllowedIpRanges { get; set; } = new();
    public bool BypassIpCheck { get; set; } = true; // Default true for development
}

// ========== Real-time DTOs (for WebSocket) ==========

public class AttendanceUpdateDto
{
    public Guid SessionId { get; set; }
    public int PresentCount { get; set; }
    public int TotalCount { get; set; }
    public Guid? LatestStudentId { get; set; }
    public string? LatestStudentName { get; set; }
    public DateTime Timestamp { get; set; }
}

public class QrCodeUpdateDto
{
    public Guid SessionId { get; set; }
    public string QrCode { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
