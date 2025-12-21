using SmartCampus.API.Controllers;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class SaveScheduleDtoTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var schedules = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(
                Guid.NewGuid(),
                ScheduleDayOfWeek.Monday,
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(11),
                Guid.NewGuid()
            )
        };

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(semester, dto.Semester);
        Assert.Equal(year, dto.Year);
        Assert.Equal(schedules, dto.Schedules);
        Assert.Single(dto.Schedules);
    }

    [Fact]
    public void Constructor_WithEmptySchedulesList_ShouldSetEmptyList()
    {
        // Arrange
        var semester = "Spring";
        var year = 2024;
        var schedules = new List<ScheduleItemDto>();

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(semester, dto.Semester);
        Assert.Equal(year, dto.Year);
        Assert.Empty(dto.Schedules);
    }

    [Fact]
    public void Constructor_WithMultipleSchedules_ShouldSetAllSchedules()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var sectionId1 = Guid.NewGuid();
        var sectionId2 = Guid.NewGuid();
        var classroomId1 = Guid.NewGuid();
        var classroomId2 = Guid.NewGuid();
        var schedules = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(
                sectionId1,
                ScheduleDayOfWeek.Monday,
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(11),
                classroomId1
            ),
            new ScheduleItemDto(
                sectionId2,
                ScheduleDayOfWeek.Wednesday,
                TimeSpan.FromHours(13),
                TimeSpan.FromHours(15),
                classroomId2
            )
        };

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(2, dto.Schedules.Count);
        Assert.Equal(sectionId1, dto.Schedules[0].SectionId);
        Assert.Equal(ScheduleDayOfWeek.Monday, dto.Schedules[0].DayOfWeek);
        Assert.Equal(sectionId2, dto.Schedules[1].SectionId);
        Assert.Equal(ScheduleDayOfWeek.Wednesday, dto.Schedules[1].DayOfWeek);
    }

    [Theory]
    [InlineData("Fall")]
    [InlineData("Spring")]
    [InlineData("Summer")]
    [InlineData("Winter")]
    [InlineData("")]
    [InlineData("Fall 2025")]
    public void Constructor_WithDifferentSemesterValues_ShouldSetCorrectly(string semester)
    {
        // Arrange
        var year = 2025;
        var schedules = new List<ScheduleItemDto>();

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(semester, dto.Semester);
    }

    [Theory]
    [InlineData(2020)]
    [InlineData(2024)]
    [InlineData(2025)]
    [InlineData(2030)]
    [InlineData(1)]
    [InlineData(9999)]
    public void Constructor_WithDifferentYearValues_ShouldSetCorrectly(int year)
    {
        // Arrange
        var semester = "Fall";
        var schedules = new List<ScheduleItemDto>();

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(year, dto.Year);
    }

    [Fact]
    public void Constructor_WithAllScheduleDayOfWeekValues_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var schedules = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid()),
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Tuesday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid()),
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Wednesday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid()),
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Thursday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid()),
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Friday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid())
        };

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(5, dto.Schedules.Count);
        Assert.Equal(ScheduleDayOfWeek.Monday, dto.Schedules[0].DayOfWeek);
        Assert.Equal(ScheduleDayOfWeek.Tuesday, dto.Schedules[1].DayOfWeek);
        Assert.Equal(ScheduleDayOfWeek.Wednesday, dto.Schedules[2].DayOfWeek);
        Assert.Equal(ScheduleDayOfWeek.Thursday, dto.Schedules[3].DayOfWeek);
        Assert.Equal(ScheduleDayOfWeek.Friday, dto.Schedules[4].DayOfWeek);
    }

    [Fact]
    public void Constructor_WithDifferentTimeSpans_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var schedules = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(
                Guid.NewGuid(),
                ScheduleDayOfWeek.Monday,
                TimeSpan.FromHours(9),
                TimeSpan.FromHours(11),
                Guid.NewGuid()
            ),
            new ScheduleItemDto(
                Guid.NewGuid(),
                ScheduleDayOfWeek.Monday,
                TimeSpan.FromHours(13),
                TimeSpan.FromHours(15),
                Guid.NewGuid()
            ),
            new ScheduleItemDto(
                Guid.NewGuid(),
                ScheduleDayOfWeek.Monday,
                TimeSpan.FromHours(15),
                TimeSpan.FromHours(17),
                Guid.NewGuid()
            )
        };

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(3, dto.Schedules.Count);
        Assert.Equal(TimeSpan.FromHours(9), dto.Schedules[0].StartTime);
        Assert.Equal(TimeSpan.FromHours(11), dto.Schedules[0].EndTime);
        Assert.Equal(TimeSpan.FromHours(13), dto.Schedules[1].StartTime);
        Assert.Equal(TimeSpan.FromHours(15), dto.Schedules[1].EndTime);
        Assert.Equal(TimeSpan.FromHours(15), dto.Schedules[2].StartTime);
        Assert.Equal(TimeSpan.FromHours(17), dto.Schedules[2].EndTime);
    }

    [Fact]
    public void Equality_WithSameListReference_ShouldBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var sectionId = Guid.NewGuid();
        var classroomId = Guid.NewGuid();
        var schedules = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(sectionId, ScheduleDayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), classroomId)
        };

        // Act
        var dto1 = new SaveScheduleDto(semester, year, schedules);
        var dto2 = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentSemester_ShouldNotBeEqual()
    {
        // Arrange
        var year = 2025;
        var schedules = new List<ScheduleItemDto>();

        // Act
        var dto1 = new SaveScheduleDto("Fall", year, schedules);
        var dto2 = new SaveScheduleDto("Spring", year, schedules);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentYear_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var schedules = new List<ScheduleItemDto>();

        // Act
        var dto1 = new SaveScheduleDto(semester, 2024, schedules);
        var dto2 = new SaveScheduleDto(semester, 2025, schedules);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentSchedules_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var schedules1 = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid())
        };
        var schedules2 = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Tuesday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid())
        };

        // Act
        var dto1 = new SaveScheduleDto(semester, year, schedules1);
        var dto2 = new SaveScheduleDto(semester, year, schedules2);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithEmptyVsNonEmptySchedules_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var emptySchedules = new List<ScheduleItemDto>();
        var nonEmptySchedules = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid())
        };

        // Act
        var dto1 = new SaveScheduleDto(semester, year, emptySchedules);
        var dto2 = new SaveScheduleDto(semester, year, nonEmptySchedules);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentScheduleCounts_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var schedules1 = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid())
        };
        var schedules2 = new List<ScheduleItemDto>
        {
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid()),
            new ScheduleItemDto(Guid.NewGuid(), ScheduleDayOfWeek.Tuesday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), Guid.NewGuid())
        };

        // Act
        var dto1 = new SaveScheduleDto(semester, year, schedules1);
        var dto2 = new SaveScheduleDto(semester, year, schedules2);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Constructor_WithLongSemesterString_ShouldSetCorrectly()
    {
        // Arrange
        var semester = new string('A', 1000);
        var year = 2025;
        var schedules = new List<ScheduleItemDto>();

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(semester, dto.Semester);
        Assert.Equal(1000, dto.Semester.Length);
    }

    [Fact]
    public void Constructor_WithManySchedules_ShouldSetAllSchedules()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var schedules = new List<ScheduleItemDto>();
        for (int i = 0; i < 100; i++)
        {
            schedules.Add(new ScheduleItemDto(
                Guid.NewGuid(),
                ScheduleDayOfWeek.Monday,
                TimeSpan.FromHours(9 + (i % 8)),
                TimeSpan.FromHours(11 + (i % 8)),
                Guid.NewGuid()
            ));
        }

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(100, dto.Schedules.Count);
    }

    [Fact]
    public void Constructor_WithSameScheduleItemReferences_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var scheduleItem = new ScheduleItemDto(
            Guid.NewGuid(),
            ScheduleDayOfWeek.Monday,
            TimeSpan.FromHours(9),
            TimeSpan.FromHours(11),
            Guid.NewGuid()
        );
        var schedules = new List<ScheduleItemDto> { scheduleItem, scheduleItem };

        // Act
        var dto = new SaveScheduleDto(semester, year, schedules);

        // Assert
        Assert.Equal(2, dto.Schedules.Count);
        Assert.Equal(scheduleItem, dto.Schedules[0]);
        Assert.Equal(scheduleItem, dto.Schedules[1]);
    }
}

