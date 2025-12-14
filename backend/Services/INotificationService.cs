namespace SmartCampus.API.Services;

/// <summary>
/// Service interface for sending notifications (email-based) for critical events
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Notify faculty when a student drops a course
    /// </summary>
    Task NotifyFacultyOnCourseDropAsync(Guid enrollmentId);

    /// <summary>
    /// Notify student when grades are entered/updated
    /// </summary>
    Task NotifyStudentOnGradeEntryAsync(Guid enrollmentId);

    /// <summary>
    /// Notify all enrolled students when an attendance session starts
    /// </summary>
    Task NotifyStudentsOnAttendanceSessionStartAsync(Guid sessionId);
}
