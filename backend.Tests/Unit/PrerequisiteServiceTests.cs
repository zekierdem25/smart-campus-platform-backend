using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class PrerequisiteServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly PrerequisiteService _service;

    public PrerequisiteServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _service = new PrerequisiteService(_context);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new PrerequisiteService(null!));
    }

    [Fact]
    public async Task CheckPrerequisitesAsync_NoPrerequisites_ShouldReturnTrue()
    {
        // Arrange
        var course = new Course { Id = Guid.NewGuid(), Name = "Basic Course", Code = "BS101" };
        _context.Courses.Add(course);
        var student = new Student { Id = Guid.NewGuid(), User = new User { Id = Guid.NewGuid(), Email = "s@s.com" } };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.CheckPrerequisitesAsync(course.Id, student.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckPrerequisitesAsync_WithCompletedPrerequisite_ShouldReturnTrue()
    {
        // Arrange
        var prerequisite = new Course { Id = Guid.NewGuid(), Name = "Prereq Course", Code = "PRE101" };
        var course = new Course { Id = Guid.NewGuid(), Name = "Advanced Course", Code = "ADV101" };
        _context.Courses.AddRange(prerequisite, course);

        var coursePrereq = new CoursePrerequisite 
        { 
            CourseId = course.Id, 
            PrerequisiteCourseId = prerequisite.Id 
        };
        _context.CoursePrerequisites.Add(coursePrereq);

        var student = new Student { Id = Guid.NewGuid(), User = new User { Id = Guid.NewGuid(), Email = "s@s.com" } };
        _context.Students.Add(student);

        // Enroll student in prerequisite and complete it
        var section = new CourseSection { Id = Guid.NewGuid(), Course = prerequisite };
        var enrollment = new Enrollment 
        { 
            Id = Guid.NewGuid(), 
            StudentId = student.Id, 
            Section = section, 
            SectionId = section.Id,
            Status = EnrollmentStatus.Completed,
            LetterGrade = "AA"
        };
        _context.Enrollments.Add(enrollment);

        await _context.SaveChangesAsync();

        // Act
        var result = await _service.CheckPrerequisitesAsync(course.Id, student.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckPrerequisitesAsync_WithMissingPrerequisite_ShouldReturnFalse()
    {
        // Arrange
        var prerequisite = new Course { Id = Guid.NewGuid(), Name = "Prereq Course", Code = "PRE101" };
        var course = new Course { Id = Guid.NewGuid(), Name = "Advanced Course", Code = "ADV101" };
        _context.Courses.AddRange(prerequisite, course);

        var coursePrereq = new CoursePrerequisite 
        { 
            CourseId = course.Id, 
            PrerequisiteCourseId = prerequisite.Id 
        };
        _context.CoursePrerequisites.Add(coursePrereq);

        var student = new Student { Id = Guid.NewGuid(), User = new User { Id = Guid.NewGuid(), Email = "s@s.com" } };
        _context.Students.Add(student);

        await _context.SaveChangesAsync();

        // Act
        var result = await _service.CheckPrerequisitesAsync(course.Id, student.Id);
        var missing = await _service.GetMissingPrerequisitesAsync(course.Id, student.Id);

        // Assert
        Assert.False(result);
        Assert.Contains($"{prerequisite.Code} - {prerequisite.Name}", missing);
    }

    [Fact]
    public async Task CheckPrerequisitesAsync_WithFailedPrerequisite_ShouldReturnFalse()
    {
        // Arrange
        var prerequisite = new Course { Id = Guid.NewGuid(), Name = "Prereq Course", Code = "PRE101" };
        var course = new Course { Id = Guid.NewGuid(), Name = "Advanced Course", Code = "ADV101" };
        _context.Courses.AddRange(prerequisite, course);

        var coursePrereq = new CoursePrerequisite 
        { 
            CourseId = course.Id, 
            PrerequisiteCourseId = prerequisite.Id 
        };
        _context.CoursePrerequisites.Add(coursePrereq);

        var student = new Student { Id = Guid.NewGuid(), User = new User { Id = Guid.NewGuid(), Email = "s@s.com" } };
        _context.Students.Add(student);

        // Enroll student in prerequisite but fail it
        var section = new CourseSection { Id = Guid.NewGuid(), Course = prerequisite };
        var enrollment = new Enrollment 
        { 
            Id = Guid.NewGuid(), 
            StudentId = student.Id, 
            Section = section, 
            SectionId = section.Id,
            Status = EnrollmentStatus.Completed,
            LetterGrade = "FF" // Failed
        };
        _context.Enrollments.Add(enrollment);

        await _context.SaveChangesAsync();

        // Act
        var result = await _service.CheckPrerequisitesAsync(course.Id, student.Id);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetAllPrerequisitesAsync_ShouldRecurseCorrectly()
    {
        // Graph: C -> B -> A (C depends on B, B depends on A)
        var courseA = new Course { Id = Guid.NewGuid(), Name = "Course A", Code = "A" };
        var courseB = new Course { Id = Guid.NewGuid(), Name = "Course B", Code = "B" };
        var courseC = new Course { Id = Guid.NewGuid(), Name = "Course C", Code = "C" };
        _context.Courses.AddRange(courseA, courseB, courseC);

        _context.CoursePrerequisites.Add(new CoursePrerequisite { CourseId = courseB.Id, PrerequisiteCourseId = courseA.Id });
        _context.CoursePrerequisites.Add(new CoursePrerequisite { CourseId = courseC.Id, PrerequisiteCourseId = courseB.Id });

        await _context.SaveChangesAsync();

        // Act
        var prerequisitesRec = await _service.GetAllPrerequisitesAsync(courseC.Id);

        // Assert
        Assert.Equal(2, prerequisitesRec.Count);
        Assert.Contains(prerequisitesRec, c => c.Id == courseA.Id);
        Assert.Contains(prerequisitesRec, c => c.Id == courseB.Id);
    }
}
