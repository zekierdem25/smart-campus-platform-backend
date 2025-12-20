namespace SmartCampus.API.Services;

/// <summary>
/// Service interface for sending notifications (email-based) for critical events
/// </summary>
public interface INotificationService
{
    // ========== Part 1-2 Notifications ==========

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

    // ========== Part 3: Meal Notifications ==========

    /// <summary>
    /// Send meal reservation confirmation email
    /// </summary>
    Task SendMealReservationConfirmationAsync(Guid reservationId);

    /// <summary>
    /// Send meal reservation cancellation email
    /// </summary>
    Task SendMealReservationCancellationAsync(Guid reservationId);

    // ========== Part 3: Event Notifications ==========

    /// <summary>
    /// Send event registration confirmation email with QR code
    /// </summary>
    Task SendEventRegistrationConfirmationAsync(Guid registrationId);

    /// <summary>
    /// Send event registration cancellation email
    /// </summary>
    Task SendEventRegistrationCancellationAsync(Guid registrationId);

    /// <summary>
    /// Send notification when user is registered from waitlist
    /// </summary>
    Task SendEventWaitlistPromotionAsync(Guid registrationId);

    // ========== Part 3: Classroom Reservation Notifications ==========

    /// <summary>
    /// Send classroom reservation pending notification
    /// </summary>
    Task SendClassroomReservationPendingAsync(Guid reservationId);

    /// <summary>
    /// Send classroom reservation approval notification
    /// </summary>
    Task SendClassroomReservationApprovalAsync(Guid reservationId);

    /// <summary>
    /// Send classroom reservation rejection notification
    /// </summary>
    Task SendClassroomReservationRejectionAsync(Guid reservationId, string? reason = null);

    /// <summary>
    /// Notify admin about pending classroom reservation
    /// </summary>
    Task NotifyAdminClassroomReservationPendingAsync(Guid reservationId);

    // ========== Part 3: Equipment Notifications ==========

    /// <summary>
    /// Send equipment borrowing request confirmation
    /// </summary>
    Task SendEquipmentBorrowingRequestAsync(Guid borrowingId);

    /// <summary>
    /// Send equipment borrowing approval notification
    /// </summary>
    Task SendEquipmentBorrowingApprovalAsync(Guid borrowingId);

    /// <summary>
    /// Send equipment borrowing rejection notification
    /// </summary>
    Task SendEquipmentBorrowingRejectionAsync(Guid borrowingId, string? reason = null);

    /// <summary>
    /// Send equipment return confirmation
    /// </summary>
    Task SendEquipmentReturnConfirmationAsync(Guid borrowingId);
}
