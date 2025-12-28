using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

/// <summary>
/// Interface for course scheduling service with CSP (Constraint Satisfaction Problem) algorithm
/// </summary>
public interface ISchedulingService
{
    /// <summary>
    /// Generates an optimized schedule using CSP with backtracking
    /// </summary>
    Task<SchedulingResult> GenerateScheduleAsync(string semester, int year, List<Guid>? sectionIds = null, SchedulingOptions? options = null);

    /// <summary>
    /// Validates a schedule against all constraints
    /// </summary>
    Task<ValidationResult> ValidateScheduleAsync(IEnumerable<Schedule> schedules);

    /// <summary>
    /// Checks if a specific schedule assignment is valid
    /// </summary>
    Task<bool> IsValidAssignmentAsync(Schedule schedule, IEnumerable<Schedule> existingSchedules);
}

/// <summary>
/// Result of schedule generation
/// </summary>
public class SchedulingResult
{
    public bool Success { get; set; }
    public List<Schedule> Schedules { get; set; } = new();
    public int ScheduledSections { get; set; }
    public int UnscheduledSections { get; set; }
    public List<string> ConflictMessages { get; set; } = new();
    public int TotalSoftConstraintScore { get; set; }
    public string? ErrorMessage { get; set; }
    public TimeSpan ExecutionTime { get; set; }
}

/// <summary>
/// Result of schedule validation
/// </summary>
public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Violations { get; set; } = new();
    public int HardConstraintViolations { get; set; }
    public int SoftConstraintScore { get; set; }
}

/// <summary>
/// Options for schedule generation
/// </summary>
public class SchedulingOptions
{
    /// <summary>
    /// Maximum time in milliseconds for the algorithm to run
    /// </summary>
    public int TimeoutMs { get; set; } = 30000;

    /// <summary>
    /// Whether to use heuristic ordering for better performance
    /// </summary>
    public bool UseHeuristics { get; set; } = true;

    /// <summary>
    /// Instructor time preferences (JSON format)
    /// Key: InstructorId, Value: List of preferred time slots
    /// </summary>
    public Dictionary<Guid, List<string>>? InstructorPreferences { get; set; }

    /// <summary>
    /// Weight for instructor preference soft constraint (0-10)
    /// </summary>
    public int InstructorPreferenceWeight { get; set; } = 10;

    /// <summary>
    /// Weight for minimizing gaps in student schedules (0-10)
    /// </summary>
    public int GapMinimizationWeight { get; set; } = 5;

    /// <summary>
    /// Weight for even distribution across week (0-10)
    /// </summary>
    public int EvenDistributionWeight { get; set; } = 5;

    /// <summary>
    /// Weight for morning slots for required courses (0-10)
    /// </summary>
    public int MorningSlotWeight { get; set; } = 8;

    /// <summary>
    /// Random seed for generating different alternatives (null = use current time)
    /// </summary>
    public int? RandomSeed { get; set; }
}
