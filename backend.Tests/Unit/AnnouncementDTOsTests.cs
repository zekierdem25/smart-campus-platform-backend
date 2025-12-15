using SmartCampus.API.DTOs;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class AnnouncementDtoTests
{
    [Fact]
    public void IsGeneral_ShouldBeTrue_WhenCourseIdIsNull()
    {
        // Arrange
        var announcement = new AnnouncementDto
        {
            CourseId = null
        };

        // Act & Assert
        Assert.True(announcement.IsGeneral);
    }

    [Fact]
    public void IsGeneral_ShouldBeFalse_WhenCourseIdIsNotNull()
    {
        // Arrange
        var announcement = new AnnouncementDto
        {
            CourseId = Guid.NewGuid()
        };

        // Act & Assert
        Assert.False(announcement.IsGeneral);
    }
}
