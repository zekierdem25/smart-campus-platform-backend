using SmartCampus.API.Controllers;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class ScheduleItemDtoTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.Equal(sectionId, dto.SectionId);
        Assert.Equal(dayOfWeek, dto.DayOfWeek);
        Assert.Equal(startTime, dto.StartTime);
        Assert.Equal(endTime, dto.EndTime);
        Assert.Equal(classroomId, dto.ClassroomId);
    }

    [Theory]
    [InlineData(ScheduleDayOfWeek.Monday)]
    [InlineData(ScheduleDayOfWeek.Tuesday)]
    [InlineData(ScheduleDayOfWeek.Wednesday)]
    [InlineData(ScheduleDayOfWeek.Thursday)]
    [InlineData(ScheduleDayOfWeek.Friday)]
    public void Constructor_WithAllDayOfWeekValues_ShouldSetCorrectly(ScheduleDayOfWeek dayOfWeek)
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.Equal(dayOfWeek, dto.DayOfWeek);
    }

    [Fact]
    public void Constructor_WithDifferentTimeSpans_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var classroomId = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId, dayOfWeek, TimeSpan.FromHours(9), TimeSpan.FromHours(11), classroomId);
        var dto2 = new ScheduleItemDto(sectionId, dayOfWeek, TimeSpan.FromHours(13), TimeSpan.FromHours(15), classroomId);
        var dto3 = new ScheduleItemDto(sectionId, dayOfWeek, TimeSpan.FromHours(15), TimeSpan.FromHours(17), classroomId);

        // Assert
        Assert.Equal(TimeSpan.FromHours(9), dto1.StartTime);
        Assert.Equal(TimeSpan.FromHours(11), dto1.EndTime);
        Assert.Equal(TimeSpan.FromHours(13), dto2.StartTime);
        Assert.Equal(TimeSpan.FromHours(15), dto2.EndTime);
        Assert.Equal(TimeSpan.FromHours(15), dto3.StartTime);
        Assert.Equal(TimeSpan.FromHours(17), dto3.EndTime);
    }

    [Fact]
    public void Constructor_WithDifferentGuids_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId1 = Guid.NewGuid();
        var sectionId2 = Guid.NewGuid();
        var classroomId1 = Guid.NewGuid();
        var classroomId2 = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);

        // Act
        var dto1 = new ScheduleItemDto(sectionId1, dayOfWeek, startTime, endTime, classroomId1);
        var dto2 = new ScheduleItemDto(sectionId2, dayOfWeek, startTime, endTime, classroomId2);

        // Assert
        Assert.Equal(sectionId1, dto1.SectionId);
        Assert.Equal(classroomId1, dto1.ClassroomId);
        Assert.Equal(sectionId2, dto2.SectionId);
        Assert.Equal(classroomId2, dto2.ClassroomId);
    }

    [Fact]
    public void Constructor_WithSameStartAndEndTime_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var time = TimeSpan.FromHours(9);
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, time, time, classroomId);

        // Assert
        Assert.Equal(time, dto.StartTime);
        Assert.Equal(time, dto.EndTime);
    }

    [Fact]
    public void Constructor_WithZeroTimeSpan_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, TimeSpan.Zero, TimeSpan.Zero, classroomId);

        // Assert
        Assert.Equal(TimeSpan.Zero, dto.StartTime);
        Assert.Equal(TimeSpan.Zero, dto.EndTime);
    }

    [Fact]
    public void Constructor_WithNegativeTimeSpan_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, TimeSpan.FromHours(-1), TimeSpan.FromHours(-2), classroomId);

        // Assert
        Assert.Equal(TimeSpan.FromHours(-1), dto.StartTime);
        Assert.Equal(TimeSpan.FromHours(-2), dto.EndTime);
    }

    [Fact]
    public void Constructor_WithEmptyGuid_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId = Guid.Empty;
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.Empty;

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.Equal(Guid.Empty, dto.SectionId);
        Assert.Equal(Guid.Empty, dto.ClassroomId);
    }

    [Fact]
    public void Constructor_WithLongTimeSpan_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromDays(1);
        var endTime = TimeSpan.FromDays(2);
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.Equal(TimeSpan.FromDays(1), dto.StartTime);
        Assert.Equal(TimeSpan.FromDays(2), dto.EndTime);
    }

    [Fact]
    public void Equality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);
        var dto2 = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentSectionId_ShouldNotBeEqual()
    {
        // Arrange
        var sectionId1 = Guid.NewGuid();
        var sectionId2 = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId1, dayOfWeek, startTime, endTime, classroomId);
        var dto2 = new ScheduleItemDto(sectionId2, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentDayOfWeek_ShouldNotBeEqual()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId, ScheduleDayOfWeek.Monday, startTime, endTime, classroomId);
        var dto2 = new ScheduleItemDto(sectionId, ScheduleDayOfWeek.Tuesday, startTime, endTime, classroomId);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentStartTime_ShouldNotBeEqual()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId, dayOfWeek, TimeSpan.FromHours(9), endTime, classroomId);
        var dto2 = new ScheduleItemDto(sectionId, dayOfWeek, TimeSpan.FromHours(10), endTime, classroomId);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentEndTime_ShouldNotBeEqual()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var classroomId = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId, dayOfWeek, startTime, TimeSpan.FromHours(11), classroomId);
        var dto2 = new ScheduleItemDto(sectionId, dayOfWeek, startTime, TimeSpan.FromHours(12), classroomId);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentClassroomId_ShouldNotBeEqual()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId1 = Guid.NewGuid();
        var classroomId2 = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId1);
        var dto2 = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId2);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithAllDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var sectionId1 = Guid.NewGuid();
        var sectionId2 = Guid.NewGuid();
        var classroomId1 = Guid.NewGuid();
        var classroomId2 = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId1, ScheduleDayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(11), classroomId1);
        var dto2 = new ScheduleItemDto(sectionId2, ScheduleDayOfWeek.Tuesday, TimeSpan.FromHours(13), TimeSpan.FromHours(15), classroomId2);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void GetHashCode_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);
        var dto2 = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.Equal(dto1.GetHashCode(), dto2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var sectionId1 = Guid.NewGuid();
        var sectionId2 = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto1 = new ScheduleItemDto(sectionId1, dayOfWeek, startTime, endTime, classroomId);
        var dto2 = new ScheduleItemDto(sectionId2, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.NotEqual(dto1.GetHashCode(), dto2.GetHashCode());
    }

    [Fact]
    public void ToString_ShouldContainAllProperties()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);
        var toString = dto.ToString();

        // Assert
        Assert.Contains(sectionId.ToString(), toString);
        Assert.Contains(dayOfWeek.ToString(), toString);
        Assert.Contains(classroomId.ToString(), toString);
    }

    [Fact]
    public void Constructor_WithMinutePrecisionTimeSpans_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = new TimeSpan(9, 30, 0); // 9:30 AM
        var endTime = new TimeSpan(11, 45, 0); // 11:45 AM
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.Equal(new TimeSpan(9, 30, 0), dto.StartTime);
        Assert.Equal(new TimeSpan(11, 45, 0), dto.EndTime);
    }

    [Fact]
    public void Constructor_WithSecondPrecisionTimeSpans_ShouldSetCorrectly()
    {
        // Arrange
        var sectionId = Guid.NewGuid();
        var dayOfWeek = ScheduleDayOfWeek.Monday;
        var startTime = new TimeSpan(9, 30, 15); // 9:30:15 AM
        var endTime = new TimeSpan(11, 45, 30); // 11:45:30 AM
        var classroomId = Guid.NewGuid();

        // Act
        var dto = new ScheduleItemDto(sectionId, dayOfWeek, startTime, endTime, classroomId);

        // Assert
        Assert.Equal(new TimeSpan(9, 30, 15), dto.StartTime);
        Assert.Equal(new TimeSpan(11, 45, 30), dto.EndTime);
    }
}

