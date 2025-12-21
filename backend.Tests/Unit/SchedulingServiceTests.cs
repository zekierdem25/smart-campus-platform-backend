using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using SmartCampus.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class SchedulingServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<ILogger<SchedulingService>> _mockLogger;
    private readonly SchedulingService _service;

    public SchedulingServiceTests()
    {
        // Setup InMemory Database
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);

        // Setup Mocks
        _mockLogger = new Mock<ILogger<SchedulingService>>();

        _service = new SchedulingService(_context, _mockLogger.Object);
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_WithValidDependencies_ShouldInitialize()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var logger = new Mock<ILogger<SchedulingService>>();

        // Act
        var service = new SchedulingService(context, logger.Object);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_ShouldSetContextField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var logger = new Mock<ILogger<SchedulingService>>();

        // Act
        var service = new SchedulingService(context, logger.Object);

        // Assert
        var contextField = typeof(SchedulingService)
            .GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
        var contextValue = contextField?.GetValue(service);

        Assert.NotNull(contextField);
        Assert.Same(context, contextValue);
    }

    [Fact]
    public void Constructor_ShouldSetLoggerField()
    {
        // Arrange
        var context = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);
        var logger = new Mock<ILogger<SchedulingService>>();

        // Act
        var service = new SchedulingService(context, logger.Object);

        // Assert
        var loggerField = typeof(SchedulingService)
            .GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance);
        var loggerValue = loggerField?.GetValue(service);

        Assert.NotNull(loggerField);
        Assert.Same(logger.Object, loggerValue);
    }

    #endregion

    #region GenerateTimeSlots Tests

    [Fact]
    public void GenerateTimeSlots_ShouldReturnCorrectNumberOfSlots()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("GenerateTimeSlots", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, null) as List<TimeSlot>;

        // Assert
        Assert.NotNull(result);
        // 5 days * 4 time slots per day (9, 11, 13, 15) = 20 slots
        Assert.Equal(20, result.Count);
    }

    [Fact]
    public void GenerateTimeSlots_ShouldGenerateSlotsForAllWeekdays()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("GenerateTimeSlots", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, null) as List<TimeSlot>;

        // Assert
        Assert.NotNull(result);
        var days = result.Select(s => s.Day).Distinct().ToList();
        Assert.Contains(ScheduleDayOfWeek.Monday, days);
        Assert.Contains(ScheduleDayOfWeek.Tuesday, days);
        Assert.Contains(ScheduleDayOfWeek.Wednesday, days);
        Assert.Contains(ScheduleDayOfWeek.Thursday, days);
        Assert.Contains(ScheduleDayOfWeek.Friday, days);
    }

    [Fact]
    public void GenerateTimeSlots_ShouldGenerateCorrectTimeRanges()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("GenerateTimeSlots", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = method!.Invoke(_service, null) as List<TimeSlot>;

        // Assert
        Assert.NotNull(result);
        // Check first slot (Monday 9:00)
        var firstSlot = result.First();
        Assert.Equal(TimeSpan.FromHours(9), firstSlot.Start);
        Assert.Equal(TimeSpan.FromHours(10.5), firstSlot.End);
        
        // Check last slot (Friday 15:00)
        var lastSlot = result.Last();
        Assert.Equal(TimeSpan.FromHours(15), lastSlot.Start);
        Assert.Equal(TimeSpan.FromHours(16.5), lastSlot.End);
    }

    [Fact]
    public void GenerateTimeSlots_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("GenerateTimeSlots", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region TimeOverlaps Tests

    [Fact]
    public void TimeOverlaps_WithOverlappingTimes_ShouldReturnTrue()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("TimeOverlaps", BindingFlags.NonPublic | BindingFlags.Instance);
        var start1 = TimeSpan.FromHours(9);
        var end1 = TimeSpan.FromHours(10.5);
        var start2 = TimeSpan.FromHours(10);
        var end2 = TimeSpan.FromHours(11.5);

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { start1, end1, start2, end2 })!;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TimeOverlaps_WithNonOverlappingTimes_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("TimeOverlaps", BindingFlags.NonPublic | BindingFlags.Instance);
        var start1 = TimeSpan.FromHours(9);
        var end1 = TimeSpan.FromHours(10.5);
        var start2 = TimeSpan.FromHours(11);
        var end2 = TimeSpan.FromHours(12.5);

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { start1, end1, start2, end2 })!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TimeOverlaps_WithAdjacentTimes_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("TimeOverlaps", BindingFlags.NonPublic | BindingFlags.Instance);
        var start1 = TimeSpan.FromHours(9);
        var end1 = TimeSpan.FromHours(10.5);
        var start2 = TimeSpan.FromHours(10.5);
        var end2 = TimeSpan.FromHours(12);

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { start1, end1, start2, end2 })!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TimeOverlaps_WithSameTimes_ShouldReturnTrue()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("TimeOverlaps", BindingFlags.NonPublic | BindingFlags.Instance);
        var start1 = TimeSpan.FromHours(9);
        var end1 = TimeSpan.FromHours(10.5);
        var start2 = TimeSpan.FromHours(9);
        var end2 = TimeSpan.FromHours(10.5);

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { start1, end1, start2, end2 })!;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TimeOverlaps_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("TimeOverlaps", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region HasStudentConflict Tests

    [Fact]
    public void HasStudentConflict_WithConflict_ShouldReturnTrue()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("HasStudentConflict", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var newSchedule = new Schedule
        {
            SectionId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(10.5)
        };

        var existingAssignments = new List<Schedule>
        {
            new Schedule
            {
                SectionId = Guid.NewGuid(),
                DayOfWeek = ScheduleDayOfWeek.Monday,
                StartTime = TimeSpan.FromHours(9.5),
                EndTime = TimeSpan.FromHours(11)
            }
        };

        var studentOtherSectionIds = new List<Guid> { existingAssignments[0].SectionId };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { newSchedule, existingAssignments, studentOtherSectionIds })!;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasStudentConflict_WithoutConflict_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("HasStudentConflict", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var newSchedule = new Schedule
        {
            SectionId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(10.5)
        };

        var existingAssignments = new List<Schedule>
        {
            new Schedule
            {
                SectionId = Guid.NewGuid(),
                DayOfWeek = ScheduleDayOfWeek.Tuesday,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(10.5)
            }
        };

        var studentOtherSectionIds = new List<Guid> { existingAssignments[0].SectionId };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { newSchedule, existingAssignments, studentOtherSectionIds })!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasStudentConflict_WithEmptyExistingAssignments_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("HasStudentConflict", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var newSchedule = new Schedule
        {
            SectionId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(10.5)
        };

        var existingAssignments = new List<Schedule>();
        var studentOtherSectionIds = new List<Guid> { Guid.NewGuid() };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { newSchedule, existingAssignments, studentOtherSectionIds })!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasStudentConflict_WithDifferentDay_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("HasStudentConflict", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var newSchedule = new Schedule
        {
            SectionId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(10.5)
        };

        var existingAssignments = new List<Schedule>
        {
            new Schedule
            {
                SectionId = Guid.NewGuid(),
                DayOfWeek = ScheduleDayOfWeek.Tuesday,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(10.5)
            }
        };

        var studentOtherSectionIds = new List<Guid> { existingAssignments[0].SectionId };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { newSchedule, existingAssignments, studentOtherSectionIds })!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasStudentConflict_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("HasStudentConflict", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region ClassroomFeaturesMatch Tests

    [Fact]
    public void ClassroomFeaturesMatch_WithNoRequirements_ShouldReturnTrue()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("ClassroomFeaturesMatch", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var course = new Course { RequirementsJson = null };
        var classroom = new Classroom { FeaturesJson = "[\"Projector\", \"Whiteboard\"]" };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { course, classroom })!;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ClassroomFeaturesMatch_WithMatchingFeatures_ShouldReturnTrue()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("ClassroomFeaturesMatch", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var course = new Course { RequirementsJson = "[\"Projector\", \"Whiteboard\"]" };
        var classroom = new Classroom { FeaturesJson = "[\"Projector\", \"Whiteboard\", \"Computer\"]" };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { course, classroom })!;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ClassroomFeaturesMatch_WithMissingFeatures_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("ClassroomFeaturesMatch", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var course = new Course { RequirementsJson = "[\"Projector\", \"Whiteboard\", \"Computer\"]" };
        var classroom = new Classroom { FeaturesJson = "[\"Projector\", \"Whiteboard\"]" };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { course, classroom })!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ClassroomFeaturesMatch_WithEmptyClassroomFeatures_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("ClassroomFeaturesMatch", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var course = new Course { RequirementsJson = "[\"Projector\"]" };
        var classroom = new Classroom { FeaturesJson = null };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { course, classroom })!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ClassroomFeaturesMatch_WithCaseInsensitiveMatch_ShouldReturnTrue()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("ClassroomFeaturesMatch", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var course = new Course { RequirementsJson = "[\"projector\"]" };
        var classroom = new Classroom { FeaturesJson = "[\"PROJECTOR\"]" };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { course, classroom })!;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ClassroomFeaturesMatch_WithInvalidCourseJson_ShouldReturnTrue()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("ClassroomFeaturesMatch", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var course = new Course { RequirementsJson = "invalid json" };
        var classroom = new Classroom { FeaturesJson = "[\"Projector\"]" };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { course, classroom })!;

        // Assert
        Assert.True(result); // Invalid JSON returns true (skips constraint)
    }

    [Fact]
    public void ClassroomFeaturesMatch_WithInvalidClassroomJson_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("ClassroomFeaturesMatch", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var course = new Course { RequirementsJson = "[\"Projector\"]" };
        var classroom = new Classroom { FeaturesJson = "invalid json" };

        // Act
        var result = (bool)method!.Invoke(_service, new object[] { course, classroom })!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ClassroomFeaturesMatch_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("ClassroomFeaturesMatch", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region CalculateGapScore Tests

    [Fact]
    public void CalculateGapScore_WithNoExistingAssignments_ShouldReturnWeight()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateGapScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(10.5)
        };
        var existingAssignments = new List<Schedule>();
        var instructorId = Guid.NewGuid();
        var weight = 5;

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, existingAssignments, instructorId, weight })!;

        // Assert
        Assert.Equal(weight, result);
    }

    [Fact]
    public void CalculateGapScore_WithAdjacentSlot_ShouldReturnFullWeight()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateGapScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(11),
            EndTime = TimeSpan.FromHours(12.5)
        };
        var existingAssignments = new List<Schedule>
        {
            new Schedule
            {
                DayOfWeek = ScheduleDayOfWeek.Monday,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(10.5)
            }
        };
        var instructorId = Guid.NewGuid();
        var weight = 5;

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, existingAssignments, instructorId, weight })!;

        // Assert
        Assert.Equal(weight, result);
    }

    [Fact]
    public void CalculateGapScore_WithSmallGap_ShouldReturnHalfWeight()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateGapScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(12),
            EndTime = TimeSpan.FromHours(13.5)
        };
        var existingAssignments = new List<Schedule>
        {
            new Schedule
            {
                DayOfWeek = ScheduleDayOfWeek.Monday,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(10.5)
            }
        };
        var instructorId = Guid.NewGuid();
        var weight = 6;

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, existingAssignments, instructorId, weight })!;

        // Assert
        Assert.Equal(weight / 2, result);
    }

    [Fact]
    public void CalculateGapScore_WithLargeGap_ShouldReturnZero()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateGapScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(15),
            EndTime = TimeSpan.FromHours(16.5)
        };
        var existingAssignments = new List<Schedule>
        {
            new Schedule
            {
                DayOfWeek = ScheduleDayOfWeek.Monday,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(10.5)
            }
        };
        var instructorId = Guid.NewGuid();
        var weight = 5;

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, existingAssignments, instructorId, weight })!;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateGapScore_WithDifferentDay_ShouldReturnWeight()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateGapScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            DayOfWeek = ScheduleDayOfWeek.Tuesday,
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(10.5)
        };
        var existingAssignments = new List<Schedule>
        {
            new Schedule
            {
                DayOfWeek = ScheduleDayOfWeek.Monday,
                StartTime = TimeSpan.FromHours(9),
                EndTime = TimeSpan.FromHours(10.5)
            }
        };
        var instructorId = Guid.NewGuid();
        var weight = 5;

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, existingAssignments, instructorId, weight })!;

        // Assert
        Assert.Equal(weight, result);
    }

    [Fact]
    public void CalculateGapScore_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateGapScore", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region CalculateDistributionScore Tests

    [Fact]
    public void CalculateDistributionScore_WithEmptyAssignments_ShouldReturnZero()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateDistributionScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule { DayOfWeek = ScheduleDayOfWeek.Monday };
        var existingAssignments = new List<Schedule>();
        var weight = 5;

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, existingAssignments, weight })!;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateDistributionScore_WithFewerClassesOnDay_ShouldReturnWeight()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateDistributionScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule { DayOfWeek = ScheduleDayOfWeek.Monday };
        var existingAssignments = new List<Schedule>
        {
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Tuesday },
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Tuesday },
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Wednesday },
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Wednesday },
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Thursday },
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Thursday }
        };
        var weight = 5;

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, existingAssignments, weight })!;

        // Assert
        Assert.Equal(weight, result);
    }

    [Fact]
    public void CalculateDistributionScore_WithMoreClassesOnDay_ShouldReturnZero()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateDistributionScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule { DayOfWeek = ScheduleDayOfWeek.Monday };
        var existingAssignments = new List<Schedule>
        {
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Monday },
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Monday },
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Monday },
            new Schedule { DayOfWeek = ScheduleDayOfWeek.Tuesday }
        };
        var weight = 5;

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, existingAssignments, weight })!;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateDistributionScore_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateDistributionScore", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region CalculateSoftConstraintScore Tests

    [Fact]
    public void CalculateSoftConstraintScore_WithInstructorPreference_ShouldAddWeight()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateSoftConstraintScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9)
        };
        var instructorId = Guid.NewGuid();
        var section = new CourseSection
        {
            InstructorId = instructorId,
            Course = new Course { IsActive = true }
        };
        var existingAssignments = new List<Schedule>();
        var options = new SchedulingOptions
        {
            InstructorPreferences = new Dictionary<Guid, List<string>>
            {
                { instructorId, new List<string> { "Monday-09:00:00" } }
            },
            InstructorPreferenceWeight = 10,
            GapMinimizationWeight = 5,
            EvenDistributionWeight = 5,
            MorningSlotWeight = 8
        };

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, section, existingAssignments, options })!;

        // Assert
        Assert.True(result >= 10); // At least instructor preference weight
    }

    [Fact]
    public void CalculateSoftConstraintScore_WithMorningSlot_ShouldAddWeight()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateSoftConstraintScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9) // Morning slot
        };
        var section = new CourseSection
        {
            Course = new Course { IsActive = true }
        };
        var existingAssignments = new List<Schedule>();
        var options = new SchedulingOptions
        {
            MorningSlotWeight = 8,
            GapMinimizationWeight = 5,
            EvenDistributionWeight = 5
        };

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, section, existingAssignments, options })!;

        // Assert
        Assert.True(result >= 8); // At least morning slot weight
    }

    [Fact]
    public void CalculateSoftConstraintScore_WithAfternoonSlot_ShouldNotAddMorningWeight()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateSoftConstraintScore", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(13) // Afternoon slot
        };
        var section = new CourseSection
        {
            Course = new Course { IsActive = true }
        };
        var existingAssignments = new List<Schedule>();
        var options = new SchedulingOptions
        {
            MorningSlotWeight = 8,
            GapMinimizationWeight = 5,
            EvenDistributionWeight = 5
        };

        // Act
        var result = (int)method!.Invoke(_service, new object[] { schedule, section, existingAssignments, options })!;

        // Assert
        Assert.True(result < 8); // Should not include morning slot weight
    }

    [Fact]
    public void CalculateSoftConstraintScore_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("CalculateSoftConstraintScore", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region MarkSlotsUsed Tests

    [Fact]
    public void MarkSlotsUsed_WithUsedTrue_ShouldMarkSlotAsUsed()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("MarkSlotsUsed", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            ClassroomId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9)
        };
        var usedSlots = new Dictionary<string, bool>();

        // Act
        method!.Invoke(_service, new object[] { schedule, usedSlots, true });

        // Assert
        var key = $"ROOM_{schedule.ClassroomId}_{schedule.DayOfWeek}_{schedule.StartTime}";
        Assert.True(usedSlots.ContainsKey(key));
        Assert.True(usedSlots[key]);
    }

    [Fact]
    public void MarkSlotsUsed_WithUsedFalse_ShouldMarkSlotAsUnused()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("MarkSlotsUsed", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            ClassroomId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9)
        };
        var usedSlots = new Dictionary<string, bool>();

        // Act
        method!.Invoke(_service, new object[] { schedule, usedSlots, false });

        // Assert
        var key = $"ROOM_{schedule.ClassroomId}_{schedule.DayOfWeek}_{schedule.StartTime}";
        Assert.True(usedSlots.ContainsKey(key));
        Assert.False(usedSlots[key]);
    }

    [Fact]
    public void MarkSlotsUsed_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("MarkSlotsUsed", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region OrderSectionsByHeuristics Tests

    [Fact]
    public void OrderSectionsByHeuristics_ShouldOrderByEnrolledCount()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("OrderSectionsByHeuristics", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var sections = new List<CourseSection>
        {
            new CourseSection { EnrolledCount = 10, Course = new Course { RequirementsJson = null } },
            new CourseSection { EnrolledCount = 30, Course = new Course { RequirementsJson = null } },
            new CourseSection { EnrolledCount = 20, Course = new Course { RequirementsJson = null } }
        };
        var classrooms = new List<Classroom>
        {
            new Classroom { Capacity = 50 }
        };

        // Act
        var result = method!.Invoke(_service, new object[] { sections, classrooms }) as IEnumerable<CourseSection>;
        var orderedList = result!.ToList();

        // Assert
        Assert.Equal(30, orderedList[0].EnrolledCount);
        Assert.Equal(20, orderedList[1].EnrolledCount);
        Assert.Equal(10, orderedList[2].EnrolledCount);
    }

    [Fact]
    public void OrderSectionsByHeuristics_ShouldOrderByRequirements()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("OrderSectionsByHeuristics", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var sections = new List<CourseSection>
        {
            new CourseSection { EnrolledCount = 20, Course = new Course { RequirementsJson = null } },
            new CourseSection { EnrolledCount = 20, Course = new Course { RequirementsJson = "[\"Projector\"]" } },
            new CourseSection { EnrolledCount = 20, Course = new Course { RequirementsJson = null } }
        };
        var classrooms = new List<Classroom>
        {
            new Classroom { Capacity = 50 }
        };

        // Act
        var result = method!.Invoke(_service, new object[] { sections, classrooms }) as IEnumerable<CourseSection>;
        var orderedList = result!.ToList();

        // Assert
        Assert.NotNull(orderedList[0].Course.RequirementsJson);
    }

    [Fact]
    public void OrderSectionsByHeuristics_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("OrderSectionsByHeuristics", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion

    #region IsValidAssignmentInternal Tests

    [Fact]
    public async Task IsValidAssignmentInternal_WithValidAssignment_ShouldReturnTrue()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("IsValidAssignmentInternal", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            ClassroomId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9)
        };
        var existingAssignments = new List<Schedule>();
        var usedSlots = new Dictionary<string, bool>();
        var studentOtherSectionIds = new List<Guid>();
        var section = new CourseSection { InstructorId = Guid.NewGuid() };

        // Act
        var task = method!.Invoke(_service, new object[] { schedule, existingAssignments, usedSlots, studentOtherSectionIds, section }) as Task<bool>;
        var result = await task!;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsValidAssignmentInternal_WithClassroomConflict_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("IsValidAssignmentInternal", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            ClassroomId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9)
        };
        var existingAssignments = new List<Schedule>();
        var usedSlots = new Dictionary<string, bool>
        {
            { $"ROOM_{schedule.ClassroomId}_{schedule.DayOfWeek}_{schedule.StartTime}", true }
        };
        var studentOtherSectionIds = new List<Guid>();
        var section = new CourseSection { InstructorId = Guid.NewGuid() };

        // Act
        var task = method!.Invoke(_service, new object[] { schedule, existingAssignments, usedSlots, studentOtherSectionIds, section }) as Task<bool>;
        var result = await task!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task IsValidAssignmentInternal_WithInstructorConflict_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("IsValidAssignmentInternal", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var instructorId = Guid.NewGuid();
        var schedule = new Schedule
        {
            ClassroomId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9)
        };
        var existingAssignments = new List<Schedule>();
        var usedSlots = new Dictionary<string, bool>
        {
            { $"INS_{instructorId}_{schedule.DayOfWeek}_{schedule.StartTime}", true }
        };
        var studentOtherSectionIds = new List<Guid>();
        var section = new CourseSection { InstructorId = instructorId };

        // Act
        var task = method!.Invoke(_service, new object[] { schedule, existingAssignments, usedSlots, studentOtherSectionIds, section }) as Task<bool>;
        var result = await task!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task IsValidAssignmentInternal_WithStudentConflict_ShouldReturnFalse()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("IsValidAssignmentInternal", BindingFlags.NonPublic | BindingFlags.Instance);
        
        var schedule = new Schedule
        {
            ClassroomId = Guid.NewGuid(),
            DayOfWeek = ScheduleDayOfWeek.Monday,
            StartTime = TimeSpan.FromHours(9),
            EndTime = TimeSpan.FromHours(10.5)
        };
        var conflictingSectionId = Guid.NewGuid();
        var existingAssignments = new List<Schedule>
        {
            new Schedule
            {
                SectionId = conflictingSectionId,
                DayOfWeek = ScheduleDayOfWeek.Monday,
                StartTime = TimeSpan.FromHours(9.5),
                EndTime = TimeSpan.FromHours(11)
            }
        };
        var usedSlots = new Dictionary<string, bool>();
        var studentOtherSectionIds = new List<Guid> { conflictingSectionId };
        var section = new CourseSection { InstructorId = Guid.NewGuid() };

        // Act
        var task = method!.Invoke(_service, new object[] { schedule, existingAssignments, usedSlots, studentOtherSectionIds, section }) as Task<bool>;
        var result = await task!;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValidAssignmentInternal_IsPrivateMethod_ShouldNotBePubliclyAccessible()
    {
        // Arrange
        var method = typeof(SchedulingService)
            .GetMethod("IsValidAssignmentInternal", BindingFlags.NonPublic | BindingFlags.Instance);

        // Assert
        Assert.NotNull(method);
        Assert.True(method!.IsPrivate);
    }

    #endregion
}

