using SmartCampus.API.DTOs;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class AcademicDTOsTests
{
    [Fact]
    public void ClassroomDto_FullName_ShouldReturnFormattedString()
    {
        // Arrange
        var classroom = new ClassroomDto
        {
            Building = "Engineering Block",
            RoomNumber = "101"
        };

        // Act
        var fullName = classroom.FullName;

        // Assert
        Assert.Equal("Engineering Block - 101", fullName);
    }

    [Fact]
    public void ClassroomDto_FullName_ShouldHandleEmptyBuildingOrRoom()
    {
        // Arrange
        var classroom = new ClassroomDto
        {
            Building = "",
            RoomNumber = ""
        };

        // Act
        var fullName = classroom.FullName;

        // Assert
        Assert.Equal(" - ", fullName);
    }
}

public class CourseSectionDtoTests
{
    [Fact]
    public void AvailableSeats_ShouldCalculateCorrectly()
    {
        // Arrange
        var section = new CourseSectionDto
        {
            Capacity = 30,
            EnrolledCount = 10
        };

        // Act
        var availableSeats = section.AvailableSeats;

        // Assert
        Assert.Equal(20, availableSeats);
    }

    [Fact]
    public void AvailableSeats_ShouldReturnNegative_WhenOverCapacity()
    {
        // Arrange
        var section = new CourseSectionDto
        {
            Capacity = 30,
            EnrolledCount = 35
        };

        // Act
        var availableSeats = section.AvailableSeats;

        // Assert
        Assert.Equal(-5, availableSeats);
    }
}

public class CourseSectionSummaryDtoTests
{
    [Fact]
    public void AvailableSeats_ShouldCalculateCorrectly()
    {
        // Arrange
        var section = new CourseSectionSummaryDto
        {
            Capacity = 30,
            EnrolledCount = 10
        };

        // Act
        var availableSeats = section.AvailableSeats;

        // Assert
        Assert.Equal(20, availableSeats);
    }

    [Fact]
    public void AvailableSeats_ShouldReturnNegative_WhenOverCapacity()
    {
        // Arrange
        var section = new CourseSectionSummaryDto
        {
            Capacity = 30,
            EnrolledCount = 35
        };

        // Act
        var availableSeats = section.AvailableSeats;

        // Assert
        Assert.Equal(-5, availableSeats);
    }
}
