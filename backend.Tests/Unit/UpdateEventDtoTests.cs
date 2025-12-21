using SmartCampus.API.Controllers;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class UpdateEventDtoTests
{
    [Fact]
    public void Constructor_WithAllNullValues_ShouldSetAllPropertiesToNull()
    {
        // Act
        var dto = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.Null(dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Category);
        Assert.Null(dto.Date);
        Assert.Null(dto.StartTime);
        Assert.Null(dto.EndTime);
        Assert.Null(dto.Location);
        Assert.Null(dto.Capacity);
        Assert.Null(dto.RegistrationDeadline);
        Assert.Null(dto.IsPaid);
        Assert.Null(dto.Price);
        Assert.Null(dto.Status);
    }

    [Fact]
    public void Constructor_WithAllValues_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var title = "Tech Conference 2025";
        var description = "Annual technology conference";
        var category = EventCategory.Conference;
        var date = DateTime.UtcNow.AddDays(30).Date;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(17);
        var location = "Main Auditorium";
        var capacity = 500;
        var registrationDeadline = DateTime.UtcNow.AddDays(20);
        var isPaid = true;
        var price = 100m;
        var status = EventStatus.Published;

        // Act
        var dto = new UpdateEventDto(
            title,
            description,
            category,
            date,
            startTime,
            endTime,
            location,
            capacity,
            registrationDeadline,
            isPaid,
            price,
            status
        );

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Equal(description, dto.Description);
        Assert.Equal(category, dto.Category);
        Assert.Equal(date, dto.Date);
        Assert.Equal(startTime, dto.StartTime);
        Assert.Equal(endTime, dto.EndTime);
        Assert.Equal(location, dto.Location);
        Assert.Equal(capacity, dto.Capacity);
        Assert.Equal(registrationDeadline, dto.RegistrationDeadline);
        Assert.Equal(isPaid, dto.IsPaid);
        Assert.Equal(price, dto.Price);
        Assert.Equal(status, dto.Status);
    }

    [Fact]
    public void Constructor_WithPartialValues_ShouldSetOnlyProvidedProperties()
    {
        // Arrange
        var title = "Updated Event Title";
        var category = EventCategory.Workshop;
        var capacity = 100;

        // Act
        var dto = new UpdateEventDto(title, null, category, null, null, null, null, capacity, null, null, null, null);

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Equal(category, dto.Category);
        Assert.Null(dto.Date);
        Assert.Null(dto.StartTime);
        Assert.Null(dto.EndTime);
        Assert.Null(dto.Location);
        Assert.Equal(capacity, dto.Capacity);
        Assert.Null(dto.RegistrationDeadline);
        Assert.Null(dto.IsPaid);
        Assert.Null(dto.Price);
        Assert.Null(dto.Status);
    }

    [Theory]
    [InlineData(EventCategory.Conference)]
    [InlineData(EventCategory.Workshop)]
    [InlineData(EventCategory.Social)]
    [InlineData(EventCategory.Sports)]
    [InlineData(EventCategory.Academic)]
    public void Constructor_WithAllEventCategoryValues_ShouldSetCorrectly(EventCategory category)
    {
        // Act
        var dto = new UpdateEventDto(null, null, category, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.Equal(category, dto.Category);
    }

    [Theory]
    [InlineData(EventStatus.Draft)]
    [InlineData(EventStatus.Published)]
    [InlineData(EventStatus.Ongoing)]
    [InlineData(EventStatus.Completed)]
    [InlineData(EventStatus.Cancelled)]
    public void Constructor_WithAllEventStatusValues_ShouldSetCorrectly(EventStatus status)
    {
        // Act
        var dto = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, status);

        // Assert
        Assert.Equal(status, dto.Status);
    }

    [Fact]
    public void Constructor_WithDateTimeValues_ShouldSetCorrectly()
    {
        // Arrange
        var date = DateTime.UtcNow.AddDays(30).Date;
        var registrationDeadline = DateTime.UtcNow.AddDays(20);

        // Act
        var dto = new UpdateEventDto(null, null, null, date, null, null, null, null, registrationDeadline, null, null, null);

        // Assert
        Assert.Equal(date, dto.Date);
        Assert.Equal(registrationDeadline, dto.RegistrationDeadline);
    }

    [Fact]
    public void Constructor_WithTimeSpanValues_ShouldSetCorrectly()
    {
        // Arrange
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(17);

        // Act
        var dto = new UpdateEventDto(null, null, null, null, startTime, endTime, null, null, null, null, null, null);

        // Assert
        Assert.Equal(startTime, dto.StartTime);
        Assert.Equal(endTime, dto.EndTime);
    }

    [Fact]
    public void Constructor_WithBooleanIsPaid_ShouldSetCorrectly()
    {
        // Act
        var dto1 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, true, null, null);
        var dto2 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, false, null, null);

        // Assert
        Assert.True(dto1.IsPaid);
        Assert.False(dto2.IsPaid);
    }

    [Fact]
    public void Constructor_WithDecimalPrice_ShouldSetCorrectly()
    {
        // Arrange
        var price1 = 100m;
        var price2 = 50.99m;
        var price3 = 0m;

        // Act
        var dto1 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, price1, null);
        var dto2 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, price2, null);
        var dto3 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, price3, null);

        // Assert
        Assert.Equal(price1, dto1.Price);
        Assert.Equal(price2, dto2.Price);
        Assert.Equal(price3, dto3.Price);
    }

    [Fact]
    public void Constructor_WithIntCapacity_ShouldSetCorrectly()
    {
        // Arrange
        var capacity1 = 100;
        var capacity2 = 500;
        var capacity3 = 0;

        // Act
        var dto1 = new UpdateEventDto(null, null, null, null, null, null, null, capacity1, null, null, null, null);
        var dto2 = new UpdateEventDto(null, null, null, null, null, null, null, capacity2, null, null, null, null);
        var dto3 = new UpdateEventDto(null, null, null, null, null, null, null, capacity3, null, null, null, null);

        // Assert
        Assert.Equal(capacity1, dto1.Capacity);
        Assert.Equal(capacity2, dto2.Capacity);
        Assert.Equal(capacity3, dto3.Capacity);
    }

    [Fact]
    public void Constructor_WithEmptyStrings_ShouldSetEmptyStrings()
    {
        // Act
        var dto = new UpdateEventDto("", "", null, null, null, null, "", null, null, null, null, null);

        // Assert
        Assert.Equal("", dto.Title);
        Assert.Equal("", dto.Description);
        Assert.Equal("", dto.Location);
    }

    [Fact]
    public void Constructor_WithLongStrings_ShouldSetCorrectly()
    {
        // Arrange
        var longTitle = new string('A', 1000);
        var longDescription = new string('B', 2000);
        var longLocation = new string('C', 500);

        // Act
        var dto = new UpdateEventDto(longTitle, longDescription, null, null, null, null, longLocation, null, null, null, null, null);

        // Assert
        Assert.Equal(longTitle, dto.Title);
        Assert.Equal(longDescription, dto.Description);
        Assert.Equal(longLocation, dto.Location);
    }

    [Fact]
    public void Constructor_WithOnlyTitle_ShouldSetOnlyTitle()
    {
        // Arrange
        var title = "Updated Title";

        // Act
        var dto = new UpdateEventDto(title, null, null, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Category);
        Assert.Null(dto.Date);
        Assert.Null(dto.StartTime);
        Assert.Null(dto.EndTime);
        Assert.Null(dto.Location);
        Assert.Null(dto.Capacity);
        Assert.Null(dto.RegistrationDeadline);
        Assert.Null(dto.IsPaid);
        Assert.Null(dto.Price);
        Assert.Null(dto.Status);
    }

    [Fact]
    public void Constructor_WithOnlyCategory_ShouldSetOnlyCategory()
    {
        // Arrange
        var category = EventCategory.Social;

        // Act
        var dto = new UpdateEventDto(null, null, category, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.Null(dto.Title);
        Assert.Null(dto.Description);
        Assert.Equal(category, dto.Category);
        Assert.Null(dto.Date);
        Assert.Null(dto.StartTime);
        Assert.Null(dto.EndTime);
        Assert.Null(dto.Location);
        Assert.Null(dto.Capacity);
        Assert.Null(dto.RegistrationDeadline);
        Assert.Null(dto.IsPaid);
        Assert.Null(dto.Price);
        Assert.Null(dto.Status);
    }

    [Fact]
    public void Constructor_WithOnlyStatus_ShouldSetOnlyStatus()
    {
        // Arrange
        var status = EventStatus.Ongoing;

        // Act
        var dto = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, status);

        // Assert
        Assert.Null(dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Category);
        Assert.Null(dto.Date);
        Assert.Null(dto.StartTime);
        Assert.Null(dto.EndTime);
        Assert.Null(dto.Location);
        Assert.Null(dto.Capacity);
        Assert.Null(dto.RegistrationDeadline);
        Assert.Null(dto.IsPaid);
        Assert.Null(dto.Price);
        Assert.Equal(status, dto.Status);
    }

    [Fact]
    public void Equality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var title = "Test Event";
        var description = "Test Description";
        var category = EventCategory.Conference;
        var date = DateTime.UtcNow.AddDays(30).Date;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(17);
        var location = "Test Location";
        var capacity = 100;
        var registrationDeadline = DateTime.UtcNow.AddDays(20);
        var isPaid = true;
        var price = 50m;
        var status = EventStatus.Published;

        // Act
        var dto1 = new UpdateEventDto(title, description, category, date, startTime, endTime, location, capacity, registrationDeadline, isPaid, price, status);
        var dto2 = new UpdateEventDto(title, description, category, date, startTime, endTime, location, capacity, registrationDeadline, isPaid, price, status);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithAllNullValues_ShouldBeEqual()
    {
        // Act
        var dto1 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentTitle_ShouldNotBeEqual()
    {
        // Arrange
        var category = EventCategory.Conference;

        // Act
        var dto1 = new UpdateEventDto("Title1", null, category, null, null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEventDto("Title2", null, category, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentCategory_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Event";

        // Act
        var dto1 = new UpdateEventDto(title, null, EventCategory.Conference, null, null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEventDto(title, null, EventCategory.Workshop, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentStatus_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Event";

        // Act
        var dto1 = new UpdateEventDto(title, null, null, null, null, null, null, null, null, null, null, EventStatus.Draft);
        var dto2 = new UpdateEventDto(title, null, null, null, null, null, null, null, null, null, null, EventStatus.Published);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullTitle_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEventDto("Title", null, null, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullCategory_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEventDto(null, null, EventCategory.Conference, null, null, null, null, null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullStatus_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEventDto(null, null, null, null, null, null, null, null, null, null, null, EventStatus.Published);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Constructor_WithMixedNullAndNonNullValues_ShouldSetCorrectly()
    {
        // Arrange
        var title = "Test Event";
        var category = EventCategory.Social;
        var capacity = 200;
        var isPaid = false;
        var status = EventStatus.Ongoing;

        // Act
        var dto = new UpdateEventDto(title, null, category, null, null, null, null, capacity, null, isPaid, null, status);

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Equal(category, dto.Category);
        Assert.Null(dto.Date);
        Assert.Null(dto.StartTime);
        Assert.Null(dto.EndTime);
        Assert.Null(dto.Location);
        Assert.Equal(capacity, dto.Capacity);
        Assert.Null(dto.RegistrationDeadline);
        Assert.Equal(isPaid, dto.IsPaid);
        Assert.Null(dto.Price);
        Assert.Equal(status, dto.Status);
    }

    [Fact]
    public void Constructor_WithPastAndFutureDates_ShouldSetCorrectly()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddDays(-30).Date;
        var futureDate = DateTime.UtcNow.AddDays(30).Date;
        var pastDeadline = DateTime.UtcNow.AddDays(-10);
        var futureDeadline = DateTime.UtcNow.AddDays(20);

        // Act
        var dto1 = new UpdateEventDto(null, null, null, pastDate, null, null, null, null, pastDeadline, null, null, null);
        var dto2 = new UpdateEventDto(null, null, null, futureDate, null, null, null, null, futureDeadline, null, null, null);

        // Assert
        Assert.Equal(pastDate, dto1.Date);
        Assert.Equal(pastDeadline, dto1.RegistrationDeadline);
        Assert.Equal(futureDate, dto2.Date);
        Assert.Equal(futureDeadline, dto2.RegistrationDeadline);
    }

    [Fact]
    public void Constructor_WithDifferentTimeSpans_ShouldSetCorrectly()
    {
        // Arrange
        var morningStart = TimeSpan.FromHours(9);
        var morningEnd = TimeSpan.FromHours(11);
        var afternoonStart = TimeSpan.FromHours(13);
        var afternoonEnd = TimeSpan.FromHours(15);
        var eveningStart = TimeSpan.FromHours(17);
        var eveningEnd = TimeSpan.FromHours(19);

        // Act
        var dto1 = new UpdateEventDto(null, null, null, null, morningStart, morningEnd, null, null, null, null, null, null);
        var dto2 = new UpdateEventDto(null, null, null, null, afternoonStart, afternoonEnd, null, null, null, null, null, null);
        var dto3 = new UpdateEventDto(null, null, null, null, eveningStart, eveningEnd, null, null, null, null, null, null);

        // Assert
        Assert.Equal(morningStart, dto1.StartTime);
        Assert.Equal(morningEnd, dto1.EndTime);
        Assert.Equal(afternoonStart, dto2.StartTime);
        Assert.Equal(afternoonEnd, dto2.EndTime);
        Assert.Equal(eveningStart, dto3.StartTime);
        Assert.Equal(eveningEnd, dto3.EndTime);
    }
}

