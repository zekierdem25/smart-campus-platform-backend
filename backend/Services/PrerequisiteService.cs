using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public interface IPrerequisiteService
{
    Task<bool> CheckPrerequisitesAsync(Guid courseId, Guid studentId);
    Task<List<Course>> GetAllPrerequisitesAsync(Guid courseId);
    Task<List<string>> GetMissingPrerequisitesAsync(Guid courseId, Guid studentId);
}

public class PrerequisiteService : IPrerequisiteService
{
    private readonly ApplicationDbContext _context;

    public PrerequisiteService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Recursively checks if a student has completed all prerequisites for a course.
    /// Uses BFS algorithm to traverse the prerequisite graph.
    /// </summary>
    public async Task<bool> CheckPrerequisitesAsync(Guid courseId, Guid studentId)
    {
        var missingPrerequisites = await GetMissingPrerequisitesAsync(courseId, studentId);
        return missingPrerequisites.Count == 0;
    }

    /// <summary>
    /// Gets all prerequisites for a course (recursively).
    /// Uses BFS to traverse prerequisites.
    /// </summary>
    public async Task<List<Course>> GetAllPrerequisitesAsync(Guid courseId)
    {
        var allPrerequisites = new List<Course>();
        var visited = new HashSet<Guid>();
        var queue = new Queue<Guid>();

        // Start with direct prerequisites
        var directPrereqs = await _context.CoursePrerequisites
            .Where(cp => cp.CourseId == courseId)
            .Select(cp => cp.PrerequisiteCourseId)
            .ToListAsync();

        foreach (var prereqId in directPrereqs)
        {
            queue.Enqueue(prereqId);
        }

        // BFS traversal
        while (queue.Count > 0)
        {
            var currentId = queue.Dequeue();

            if (visited.Contains(currentId))
                continue;

            visited.Add(currentId);

            var course = await _context.Courses.FindAsync(currentId);
            if (course != null)
            {
                allPrerequisites.Add(course);
            }

            // Get next level prerequisites
            var nextPrereqs = await _context.CoursePrerequisites
                .Where(cp => cp.CourseId == currentId)
                .Select(cp => cp.PrerequisiteCourseId)
                .ToListAsync();

            foreach (var nextId in nextPrereqs)
            {
                if (!visited.Contains(nextId))
                {
                    queue.Enqueue(nextId);
                }
            }
        }

        return allPrerequisites;
    }

    /// <summary>
    /// Gets list of missing prerequisites that student hasn't completed.
    /// </summary>
    public async Task<List<string>> GetMissingPrerequisitesAsync(Guid courseId, Guid studentId)
    {
        var missingPrereqs = new List<string>();
        var allPrerequisites = await GetAllPrerequisitesAsync(courseId);

        // Get all courses the student has completed (passed)
        var completedCourseIds = await _context.Enrollments
            .Where(e => e.StudentId == studentId && 
                       (e.Status == EnrollmentStatus.Completed) &&
                       e.LetterGrade != null && 
                       e.LetterGrade != "FF" && 
                       e.LetterGrade != "FD")
            .Select(e => e.Section.CourseId)
            .Distinct()
            .ToListAsync();

        foreach (var prereq in allPrerequisites)
        {
            if (!completedCourseIds.Contains(prereq.Id))
            {
                missingPrereqs.Add($"{prereq.Code} - {prereq.Name}");
            }
        }

        return missingPrereqs;
    }
}
