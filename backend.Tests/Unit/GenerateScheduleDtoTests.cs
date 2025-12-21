using SmartCampus.API.Controllers;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class GenerateScheduleDtoTests
{
    [Fact]
    public void Constructor_WithValidRequiredParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;

        // Act
        var dto = new GenerateScheduleDto(semester, year);

        // Assert
        Assert.Equal(semester, dto.Semester);
        Assert.Equal(year, dto.Year);
    }

    [Fact]
    public void Constructor_WithDefaultValues_ShouldUseDefaults()
    {
        // Arrange
        var semester = "Spring";
        var year = 2024;

        // Act - Not specifying optional parameters
        var dto = new GenerateScheduleDto(semester, year);

        // Assert
        Assert.Equal(semester, dto.Semester);
        Assert.Equal(year, dto.Year);
        Assert.Null(dto.SectionIds);
        Assert.Null(dto.TimeoutMs);
        Assert.Null(dto.UseHeuristics);
        Assert.Null(dto.InstructorPreferences);
        Assert.Null(dto.InstructorPreferenceWeight);
        Assert.Null(dto.GapMinimizationWeight);
        Assert.Null(dto.EvenDistributionWeight);
        Assert.Null(dto.MorningSlotWeight);
    }

    [Fact]
    public void Constructor_WithAllParameters_ShouldSetAllProperties()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var sectionIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var timeoutMs = 5000;
        var useHeuristics = true;
        var instructorPreferences = new Dictionary<Guid, List<string>>
        {
            { Guid.NewGuid(), new List<string> { "Monday", "Wednesday" } },
            { Guid.NewGuid(), new List<string> { "Tuesday", "Thursday" } }
        };
        var instructorPreferenceWeight = 10;
        var gapMinimizationWeight = 5;
        var evenDistributionWeight = 3;
        var morningSlotWeight = 2;

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            sectionIds,
            timeoutMs,
            useHeuristics,
            instructorPreferences,
            instructorPreferenceWeight,
            gapMinimizationWeight,
            evenDistributionWeight,
            morningSlotWeight
        );

        // Assert
        Assert.Equal(semester, dto.Semester);
        Assert.Equal(year, dto.Year);
        Assert.Equal(sectionIds, dto.SectionIds);
        Assert.Equal(timeoutMs, dto.TimeoutMs);
        Assert.Equal(useHeuristics, dto.UseHeuristics);
        Assert.Equal(instructorPreferences, dto.InstructorPreferences);
        Assert.Equal(instructorPreferenceWeight, dto.InstructorPreferenceWeight);
        Assert.Equal(gapMinimizationWeight, dto.GapMinimizationWeight);
        Assert.Equal(evenDistributionWeight, dto.EvenDistributionWeight);
        Assert.Equal(morningSlotWeight, dto.MorningSlotWeight);
    }

    [Fact]
    public void Constructor_WithPartialOptionalParameters_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Summer";
        var year = 2024;
        var sectionIds = new List<Guid> { Guid.NewGuid() };
        var timeoutMs = 3000;

        // Act - Only setting some optional parameters
        var dto = new GenerateScheduleDto(
            semester,
            year,
            sectionIds,
            timeoutMs
        );

        // Assert
        Assert.Equal(semester, dto.Semester);
        Assert.Equal(year, dto.Year);
        Assert.Equal(sectionIds, dto.SectionIds);
        Assert.Equal(timeoutMs, dto.TimeoutMs);
        Assert.Null(dto.UseHeuristics);
        Assert.Null(dto.InstructorPreferences);
        Assert.Null(dto.InstructorPreferenceWeight);
        Assert.Null(dto.GapMinimizationWeight);
        Assert.Null(dto.EvenDistributionWeight);
        Assert.Null(dto.MorningSlotWeight);
    }

    [Fact]
    public void Constructor_WithEmptySemester_ShouldSetEmptyString()
    {
        // Arrange
        var year = 2025;

        // Act
        var dto = new GenerateScheduleDto(string.Empty, year);

        // Assert
        Assert.Equal(string.Empty, dto.Semester);
        Assert.Equal(year, dto.Year);
    }

    [Fact]
    public void Constructor_WithDifferentSemesterValues_ShouldSetCorrectly()
    {
        // Arrange
        var semesters = new[] { "Fall", "Spring", "Summer", "Winter" };
        var year = 2025;

        // Act & Assert
        foreach (var semester in semesters)
        {
            var dto = new GenerateScheduleDto(semester, year);
            Assert.Equal(semester, dto.Semester);
            Assert.Equal(year, dto.Year);
        }
    }

    [Fact]
    public void Constructor_WithDifferentYearValues_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var years = new[] { 2020, 2021, 2022, 2023, 2024, 2025, 2030 };

        // Act & Assert
        foreach (var year in years)
        {
            var dto = new GenerateScheduleDto(semester, year);
            Assert.Equal(semester, dto.Semester);
            Assert.Equal(year, dto.Year);
        }
    }

    [Fact]
    public void Constructor_WithSectionIds_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var sectionIds = new List<Guid>
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        // Act
        var dto = new GenerateScheduleDto(semester, year, sectionIds);

        // Assert
        Assert.Equal(sectionIds, dto.SectionIds);
        Assert.Equal(3, dto.SectionIds?.Count);
    }

    [Fact]
    public void Constructor_WithEmptySectionIds_ShouldSetEmptyList()
    {
        // Arrange
        var semester = "Spring";
        var year = 2024;
        var sectionIds = new List<Guid>();

        // Act
        var dto = new GenerateScheduleDto(semester, year, sectionIds);

        // Assert
        Assert.Equal(sectionIds, dto.SectionIds);
        Assert.Empty(dto.SectionIds!);
    }

    [Fact]
    public void Constructor_WithTimeoutMs_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var timeoutValues = new[] { 1000, 5000, 10000, 30000, 60000 };

        // Act & Assert
        foreach (var timeout in timeoutValues)
        {
            var dto = new GenerateScheduleDto(semester, year, null, timeout);
            Assert.Equal(timeout, dto.TimeoutMs);
        }
    }

    [Fact]
    public void Constructor_WithUseHeuristics_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;

        // Act
        var dtoTrue = new GenerateScheduleDto(semester, year, null, null, true);
        var dtoFalse = new GenerateScheduleDto(semester, year, null, null, false);

        // Assert
        Assert.True(dtoTrue.UseHeuristics);
        Assert.False(dtoFalse.UseHeuristics);
    }

    [Fact]
    public void Constructor_WithInstructorPreferences_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var instructorPreferences = new Dictionary<Guid, List<string>>
        {
            { Guid.NewGuid(), new List<string> { "Monday", "Wednesday", "Friday" } },
            { Guid.NewGuid(), new List<string> { "Tuesday", "Thursday" } },
            { Guid.NewGuid(), new List<string> { "Monday" } }
        };

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            null,
            null,
            null,
            instructorPreferences
        );

        // Assert
        Assert.Equal(instructorPreferences, dto.InstructorPreferences);
        Assert.Equal(3, dto.InstructorPreferences?.Count);
    }

    [Fact]
    public void Constructor_WithEmptyInstructorPreferences_ShouldSetEmptyDictionary()
    {
        // Arrange
        var semester = "Spring";
        var year = 2024;
        var instructorPreferences = new Dictionary<Guid, List<string>>();

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            null,
            null,
            null,
            instructorPreferences
        );

        // Assert
        Assert.Equal(instructorPreferences, dto.InstructorPreferences);
        Assert.Empty(dto.InstructorPreferences!);
    }

    [Fact]
    public void Constructor_WithWeightParameters_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var instructorPreferenceWeight = 10;
        var gapMinimizationWeight = 5;
        var evenDistributionWeight = 3;
        var morningSlotWeight = 2;

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            null,
            null,
            null,
            null,
            instructorPreferenceWeight,
            gapMinimizationWeight,
            evenDistributionWeight,
            morningSlotWeight
        );

        // Assert
        Assert.Equal(instructorPreferenceWeight, dto.InstructorPreferenceWeight);
        Assert.Equal(gapMinimizationWeight, dto.GapMinimizationWeight);
        Assert.Equal(evenDistributionWeight, dto.EvenDistributionWeight);
        Assert.Equal(morningSlotWeight, dto.MorningSlotWeight);
    }

    [Fact]
    public void Constructor_WithZeroWeights_ShouldSetZero()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            null,
            null,
            null,
            null,
            0, // InstructorPreferenceWeight
            0, // GapMinimizationWeight
            0, // EvenDistributionWeight
            0  // MorningSlotWeight
        );

        // Assert
        Assert.Equal(0, dto.InstructorPreferenceWeight);
        Assert.Equal(0, dto.GapMinimizationWeight);
        Assert.Equal(0, dto.EvenDistributionWeight);
        Assert.Equal(0, dto.MorningSlotWeight);
    }

    [Fact]
    public void Constructor_WithNegativeWeights_ShouldSetNegativeValues()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            null,
            null,
            null,
            null,
            -1, // InstructorPreferenceWeight
            -5, // GapMinimizationWeight
            -10, // EvenDistributionWeight
            -2  // MorningSlotWeight
        );

        // Assert
        Assert.Equal(-1, dto.InstructorPreferenceWeight);
        Assert.Equal(-5, dto.GapMinimizationWeight);
        Assert.Equal(-10, dto.EvenDistributionWeight);
        Assert.Equal(-2, dto.MorningSlotWeight);
    }

    [Fact]
    public void Constructor_WithLargeWeights_ShouldSetLargeValues()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            null,
            null,
            null,
            null,
            100, // InstructorPreferenceWeight
            200, // GapMinimizationWeight
            300, // EvenDistributionWeight
            400  // MorningSlotWeight
        );

        // Assert
        Assert.Equal(100, dto.InstructorPreferenceWeight);
        Assert.Equal(200, dto.GapMinimizationWeight);
        Assert.Equal(300, dto.EvenDistributionWeight);
        Assert.Equal(400, dto.MorningSlotWeight);
    }

    [Fact]
    public void Constructor_WithLongSemesterString_ShouldSetCorrectly()
    {
        // Arrange
        var longSemester = new string('A', 100);
        var year = 2025;

        // Act
        var dto = new GenerateScheduleDto(longSemester, year);

        // Assert
        Assert.Equal(longSemester, dto.Semester);
        Assert.Equal(100, dto.Semester.Length);
    }

    [Fact]
    public void Constructor_WithComplexInstructorPreferences_ShouldSetCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var instructorPreferences = new Dictionary<Guid, List<string>>
        {
            { Guid.NewGuid(), new List<string> { "Monday", "Wednesday", "Friday", "Morning", "Afternoon" } },
            { Guid.NewGuid(), new List<string> { "Tuesday", "Thursday" } }
        };

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            null,
            null,
            null,
            instructorPreferences
        );

        // Assert
        Assert.Equal(instructorPreferences, dto.InstructorPreferences);
        Assert.Equal(2, dto.InstructorPreferences?.Count);
        Assert.Equal(5, dto.InstructorPreferences?.First().Value.Count);
        Assert.Equal(2, dto.InstructorPreferences?.Last().Value.Count);
    }

    [Fact]
    public void RecordEquality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var sectionIds = new List<Guid> { Guid.NewGuid() };
        var timeoutMs = 5000;

        // Act
        var dto1 = new GenerateScheduleDto(semester, year, sectionIds, timeoutMs);
        var dto2 = new GenerateScheduleDto(semester, year, sectionIds, timeoutMs);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentSemesters_ShouldNotBeEqual()
    {
        // Arrange
        var year = 2025;

        // Act
        var dto1 = new GenerateScheduleDto("Fall", year);
        var dto2 = new GenerateScheduleDto("Spring", year);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentYears_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";

        // Act
        var dto1 = new GenerateScheduleDto(semester, 2024);
        var dto2 = new GenerateScheduleDto(semester, 2025);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentSectionIds_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var sectionIds1 = new List<Guid> { Guid.NewGuid() };
        var sectionIds2 = new List<Guid> { Guid.NewGuid() };

        // Act
        var dto1 = new GenerateScheduleDto(semester, year, sectionIds1);
        var dto2 = new GenerateScheduleDto(semester, year, sectionIds2);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullSectionIds_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var sectionIds = new List<Guid> { Guid.NewGuid() };

        // Act
        var dto1 = new GenerateScheduleDto(semester, year, null);
        var dto2 = new GenerateScheduleDto(semester, year, sectionIds);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentTimeoutMs_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;

        // Act
        var dto1 = new GenerateScheduleDto(semester, year, null, 5000);
        var dto2 = new GenerateScheduleDto(semester, year, null, 10000);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentUseHeuristics_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;

        // Act
        var dto1 = new GenerateScheduleDto(semester, year, null, null, true);
        var dto2 = new GenerateScheduleDto(semester, year, null, null, false);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentInstructorPreferences_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var preferences1 = new Dictionary<Guid, List<string>>
        {
            { Guid.NewGuid(), new List<string> { "Monday" } }
        };
        var preferences2 = new Dictionary<Guid, List<string>>
        {
            { Guid.NewGuid(), new List<string> { "Tuesday" } }
        };

        // Act
        var dto1 = new GenerateScheduleDto(semester, year, null, null, null, preferences1);
        var dto2 = new GenerateScheduleDto(semester, year, null, null, null, preferences2);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentWeights_ShouldNotBeEqual()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;

        // Act
        var dto1 = new GenerateScheduleDto(semester, year, null, null, null, null, 10, 5, 3, 2);
        var dto2 = new GenerateScheduleDto(semester, year, null, null, null, null, 20, 10, 6, 4);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void Constructor_WithNullAndNotNullOptionalParameters_ShouldHandleCorrectly()
    {
        // Arrange
        var semester = "Fall";
        var year = 2025;
        var sectionIds = new List<Guid> { Guid.NewGuid() };
        var instructorPreferences = new Dictionary<Guid, List<string>>
        {
            { Guid.NewGuid(), new List<string> { "Monday" } }
        };

        // Act
        var dto = new GenerateScheduleDto(
            semester,
            year,
            sectionIds,
            null, // TimeoutMs
            null, // UseHeuristics
            instructorPreferences,
            null, // InstructorPreferenceWeight
            null, // GapMinimizationWeight
            null, // EvenDistributionWeight
            null  // MorningSlotWeight
        );

        // Assert
        Assert.NotNull(dto.SectionIds);
        Assert.Null(dto.TimeoutMs);
        Assert.Null(dto.UseHeuristics);
        Assert.NotNull(dto.InstructorPreferences);
        Assert.Null(dto.InstructorPreferenceWeight);
        Assert.Null(dto.GapMinimizationWeight);
        Assert.Null(dto.EvenDistributionWeight);
        Assert.Null(dto.MorningSlotWeight);
    }
}

