using SmartCampus.API.DTOs;
using SmartCampus.API.Services;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class ScheduleConflictServiceTests
{
    private readonly ScheduleConflictService _service;

    public ScheduleConflictServiceTests()
    {
        _service = new ScheduleConflictService();
    }

    // ========== HasScheduleConflict Tests ==========

    [Fact]
    public void HasScheduleConflict_NoOverlap_ShouldReturnFalse()
    {
        // Arrange
        var existing = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "09:00", EndTime = "10:50" }
        };

        var newSchedule = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "11:00", EndTime = "12:50" }
        };

        // Act
        var result = _service.HasScheduleConflict(existing, newSchedule);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasScheduleConflict_DifferentDays_ShouldReturnFalse()
    {
        // Arrange
        var existing = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "09:00", EndTime = "10:50" }
        };

        var newSchedule = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Tuesday", StartTime = "09:00", EndTime = "10:50" }
        };

        // Act
        var result = _service.HasScheduleConflict(existing, newSchedule);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasScheduleConflict_ExactOverlap_ShouldReturnTrue()
    {
        // Arrange
        var existing = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "09:00", EndTime = "10:50" }
        };

        var newSchedule = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "09:00", EndTime = "10:50" }
        };

        // Act
        var result = _service.HasScheduleConflict(existing, newSchedule);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasScheduleConflict_PartialOverlapStart_ShouldReturnTrue()
    {
        // Arrange
        var existing = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "09:00", EndTime = "11:00" }
        };

        // Starts before existing ends (10:30 < 11:00)
        var newSchedule = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "10:30", EndTime = "12:00" }
        };

        // Act
        var result = _service.HasScheduleConflict(existing, newSchedule);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasScheduleConflict_PartialOverlapEnd_ShouldReturnTrue()
    {
        // Arrange
        var existing = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "09:00", EndTime = "11:00" }
        };

        // Ends after existing starts (09:30 > 09:00)
        var newSchedule = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "08:00", EndTime = "09:30" }
        };

        // Act
        var result = _service.HasScheduleConflict(existing, newSchedule);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasScheduleConflict_InsideOverlap_ShouldReturnTrue()
    {
        // Arrange
        var existing = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "09:00", EndTime = "12:00" }
        };

        // Completely inside existing
        var newSchedule = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "10:00", EndTime = "11:00" }
        };

        // Act
        var result = _service.HasScheduleConflict(existing, newSchedule);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasScheduleConflict_EnclosingOverlap_ShouldReturnTrue()
    {
        // Arrange
        var existing = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "10:00", EndTime = "11:00" }
        };

        // Completely encloses existing
        var newSchedule = new List<ScheduleSlotDto>
        {
            new ScheduleSlotDto { Day = "Monday", StartTime = "09:00", EndTime = "12:00" }
        };

        // Act
        var result = _service.HasScheduleConflict(existing, newSchedule);

        // Assert
        Assert.True(result);
    }

    // ========== ParseScheduleJson Tests ==========

    [Fact]
    public void ParseScheduleJson_ValidJson_ShouldReturnList()
    {
        var json = "[{\"day\":\"Monday\",\"startTime\":\"09:00\",\"endTime\":\"10:50\"}]";
        var result = _service.ParseScheduleJson(json);
        
        Assert.Single(result);
        Assert.Equal("Monday", result[0].Day);
        Assert.Equal("09:00", result[0].StartTime);
    }

    [Fact]
    public void ParseScheduleJson_CaseInsensitive_ShouldReturnList()
    {
        var json = "[{\"DAY\":\"Monday\",\"STARTTIME\":\"09:00\",\"ENDTIME\":\"10:50\"}]";
        var result = _service.ParseScheduleJson(json);
        
        Assert.Single(result);
        Assert.Equal("Monday", result[0].Day);
    }

    [Fact]
    public void ParseScheduleJson_NullOrEmpty_ShouldReturnEmptyList()
    {
        Assert.Empty(_service.ParseScheduleJson(null));
        Assert.Empty(_service.ParseScheduleJson(""));
    }

    [Fact]
    public void ParseScheduleJson_InvalidJson_ShouldReturnEmptyList()
    {
        Assert.Empty(_service.ParseScheduleJson("{invalid_json}"));
    }
}
