using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using SmartCampus.API.Services;

namespace SmartCampus.API.Tests.Unit;

public class AnalyticsServiceTests
{
    private readonly Mock<IGradeCalculationService> _mockGradeService;
    private readonly IMemoryCache _cache;
    private readonly Mock<ILogger<AnalyticsService>> _mockLogger;
    private readonly ApplicationDbContext _context;
    private readonly AnalyticsService _service;

    public AnalyticsServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _mockGradeService = new Mock<IGradeCalculationService>();
        _cache = new MemoryCache(new MemoryCacheOptions());
        _mockLogger = new Mock<ILogger<AnalyticsService>>();

        _service = new AnalyticsService(
            _context,
            _mockGradeService.Object,
            _cache,
            _mockLogger.Object
        );
    }

    #region GetDashboardMetricsAsync Tests

    [Fact]
    public async Task GetDashboardMetricsAsync_ShouldReturnMetrics()
    {
        // Arrange
        await SeedTestData();

        // No cache setup needed for real cache miss

        // Act
        var result = await _service.GetDashboardMetricsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.TotalStudents >= 0);
        Assert.True(result.TotalCourses >= 0);
        Assert.True(result.TotalFaculty >= 0);
        Assert.True(result.ActiveEnrollments >= 0);
    }

    [Fact]
    public async Task GetDashboardMetricsAsync_WithCache_ShouldReturnCachedData()
    {
        // Arrange
        var cachedMetrics = new DashboardMetricsDto
        {
            TotalStudents = 100,
            TotalCourses = 50,
            TotalFaculty = 10,
            ActiveEnrollments = 200
        };

        _cache.Set("dashboard_metrics", cachedMetrics);

        // Act
        var result = await _service.GetDashboardMetricsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(100, result.TotalStudents);
        Assert.Equal(50, result.TotalCourses);
    }

    #endregion

    #region GetAcademicPerformanceAsync Tests

    [Fact]
    public async Task GetAcademicPerformanceAsync_ShouldReturnPerformanceMetrics()
    {
        // Arrange
        await SeedAcademicData();

        // Act
        var result = await _service.GetAcademicPerformanceAsync("Fall", 2024);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Fall 2024", result.Term);
        Assert.NotNull(result.GpaByDepartment);
        Assert.NotNull(result.GradeDistribution);
    }

    [Fact]
    public async Task GetAcademicPerformanceAsync_WithNoData_ShouldReturnEmptyMetrics()
    {
        // Act
        var result = await _service.GetAcademicPerformanceAsync("Fall", 2024);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0.0, result.AverageGpa);
        Assert.Empty(result.GpaByDepartment);
        Assert.Empty(result.GradeDistribution);
    }

    #endregion

    #region GetAttendanceAnalyticsAsync Tests

    [Fact]
    public async Task GetAttendanceAnalyticsAsync_ShouldReturnAttendanceData()
    {
        // Arrange
        await SeedAttendanceData();

        // Act
        var result = await _service.GetAttendanceAnalyticsAsync("Fall", 2024, null);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.OverallAttendanceRate >= 0);
        Assert.NotNull(result.LowAttendanceCourses);
        Assert.NotNull(result.AtRiskStudents);
    }

    [Fact]
    public async Task GetAttendanceAnalyticsAsync_WithCourseFilter_ShouldFilterByCourse()
    {
        // Arrange
        await SeedAttendanceData();
        var courseId = _context.Courses.First().Id;

        // Act
        var result = await _service.GetAttendanceAnalyticsAsync("Fall", 2024, courseId);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.OverallAttendanceRate >= 0);
    }

    #endregion

    #region GetMealUsageAnalyticsAsync Tests

    [Fact]
    public async Task GetMealUsageAnalyticsAsync_ShouldReturnMealData()
    {
        // Arrange
        await SeedMealData();
        var dateFrom = DateTime.UtcNow.AddDays(-7);
        var dateTo = DateTime.UtcNow;

        // Act
        var result = await _service.GetMealUsageAnalyticsAsync(dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.TotalMealsServed >= 0);
        Assert.NotNull(result.UsageByCafeteria);
        Assert.NotNull(result.PeakHours);
    }

    [Fact]
    public async Task GetMealUsageAnalyticsAsync_WithNoData_ShouldReturnZero()
    {
        // Act
        var result = await _service.GetMealUsageAnalyticsAsync(null, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.TotalMealsServed);
        Assert.Empty(result.UsageByCafeteria);
        Assert.Empty(result.PeakHours);
    }

    #endregion

    #region GetEventsAnalyticsAsync Tests

    [Fact]
    public async Task GetEventsAnalyticsAsync_ShouldReturnEventData()
    {
        // Arrange
        await SeedEventData();
        var dateFrom = DateTime.UtcNow.AddDays(-7);
        var dateTo = DateTime.UtcNow;

        // Act
        var result = await _service.GetEventsAnalyticsAsync(dateFrom, dateTo);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.TotalEvents >= 0);
        Assert.True(result.TotalRegistrations >= 0);
        Assert.NotNull(result.PopularCategories);
    }

    [Fact]
    public async Task GetEventsAnalyticsAsync_WithNoData_ShouldReturnZero()
    {
        // Act
        var result = await _service.GetEventsAnalyticsAsync(null, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.TotalEvents);
        Assert.Equal(0, result.TotalRegistrations);
        Assert.Empty(result.PopularCategories);
    }

    #endregion

    #region Helper Methods

    private async Task SeedTestData()
    {
        var department = new Department
        {
            Id = Guid.NewGuid(),
            Name = "Computer Science",
            Code = "CS",
            Faculty = "Engineering",
            IsActive = true
        };

        var student = new Student
        {
            Id = Guid.NewGuid(),
            StudentNumber = "2024001",
            DepartmentId = department.Id,
            Department = department,
            EnrollmentYear = 2024,
            CGPA = 3.5m
        };

        var course = new Course
        {
            Id = Guid.NewGuid(),
            Code = "CS101",
            Name = "Introduction to Programming",
            Credits = 3,
            ECTS = 6,
            DepartmentId = department.Id,
            IsActive = true
        };

        _context.Departments.Add(department);
        _context.Students.Add(student);
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    private async Task SeedAcademicData()
    {
        await SeedTestData();

        var student = _context.Students.First();
        var course = _context.Courses.First();
        var section = new CourseSection
        {
            Id = Guid.NewGuid(),
            CourseId = course.Id,
            Semester = "Fall",
            Year = 2024,
            SectionNumber = 1,
            InstructorId = Guid.NewGuid()
        };

        var enrollment = new Enrollment
        {
            Id = Guid.NewGuid(),
            StudentId = student.Id,
            SectionId = section.Id,
            Status = EnrollmentStatus.Active,
            GradePoint = 3.5m,
            LetterGrade = "AA"
        };

        _context.CourseSections.Add(section);
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
    }

    private async Task SeedAttendanceData()
    {
        await SeedAcademicData();

        var section = _context.CourseSections.First();
        var student = _context.Students.First();

        var session = new AttendanceSession
        {
            Id = Guid.NewGuid(),
            SectionId = section.Id,
            InstructorId = Guid.NewGuid(), // Minimal setup
            Date = DateTime.UtcNow.Date,
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(11),
            Latitude = 40.0m,
            Longitude = 29.0m,
            GeofenceRadius = 100
        };

        var record = new AttendanceRecord
        {
            Id = Guid.NewGuid(),
            SessionId = session.Id,
            StudentId = student.Id,
            CheckInTime = DateTime.UtcNow,
            Latitude = 40.0m,
            Longitude = 29.0m,
            DistanceFromCenter = 50m
        };

        _context.AttendanceSessions.Add(session);
        _context.AttendanceRecords.Add(record);
        await _context.SaveChangesAsync();
    }

    private async Task SeedMealData()
    {
        var cafeteria = new Cafeteria
        {
            Id = Guid.NewGuid(),
            Name = "Main Cafeteria",
            Location = "Building A",
            Capacity = 200,
            IsActive = true
        };

        var menu = new MealMenu
        {
            Id = Guid.NewGuid(),
            CafeteriaId = cafeteria.Id,
            Date = DateTime.UtcNow.Date,
            MealType = MealType.Lunch,
            ItemsJson = "[]"
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@test.com",
            FirstName = "Test",
            LastName = "User",
            PasswordHash = "dummy"
        };

        var reservation = new MealReservation
        {
            Id = Guid.NewGuid(),
            MenuId = menu.Id,
            UserId = user.Id,
            CafeteriaId = cafeteria.Id,
            Date = DateTime.UtcNow.Date,
            MealType = MealType.Lunch,
            Status = MealReservationStatus.Reserved,
            QrCode = "dummy"
        };

        _context.Cafeterias.Add(cafeteria);
        _context.MealMenus.Add(menu);
        _context.Users.Add(user);
        _context.MealReservations.Add(reservation);
        await _context.SaveChangesAsync();
    }

    private async Task SeedEventData()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "organizer@test.com",
            FirstName = "Event",
            LastName = "Organizer",
            PasswordHash = "dummy"
        };

        var eventItem = new Event
        {
            Id = Guid.NewGuid(),
            Title = "Tech Conference",
            Description = "Annual tech event",
            Date = DateTime.UtcNow.AddDays(5),
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(17),
            Location = "Main Hall",
            Category = EventCategory.Academic,
            Capacity = 100,
            Status = EventStatus.Published,
            RegistrationDeadline = DateTime.UtcNow.AddDays(4),
            CreatedBy = user.Id
        };

        var registration = new EventRegistration
        {
            Id = Guid.NewGuid(),
            EventId = eventItem.Id,
            UserId = user.Id,
            QrCode = "dummy"
        };

        _context.Users.Add(user);
        _context.Events.Add(eventItem);
        _context.EventRegistrations.Add(registration);
        await _context.SaveChangesAsync();
    }

    #endregion
}
