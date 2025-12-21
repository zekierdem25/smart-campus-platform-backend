using SmartCampus.API.Controllers;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class CreateEventDtoTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
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
        var dto = new CreateEventDto(
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
    public void Constructor_WithDefaultValues_ShouldUseDefaults()
    {
        // Arrange
        var title = "Workshop Event";
        var category = EventCategory.Workshop;
        var date = DateTime.UtcNow.AddDays(15).Date;
        var startTime = TimeSpan.FromHours(10);
        var endTime = TimeSpan.FromHours(12);
        var capacity = 50;
        var registrationDeadline = DateTime.UtcNow.AddDays(10);

        // Act - Not specifying IsPaid, Price, and Status
        var dto = new CreateEventDto(
            title,
            null, // Description
            category,
            date,
            startTime,
            endTime,
            null, // Location
            capacity,
            registrationDeadline
        );

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Equal(category, dto.Category);
        Assert.Equal(date, dto.Date);
        Assert.Equal(startTime, dto.StartTime);
        Assert.Equal(endTime, dto.EndTime);
        Assert.Null(dto.Location);
        Assert.Equal(capacity, dto.Capacity);
        Assert.Equal(registrationDeadline, dto.RegistrationDeadline);
        Assert.False(dto.IsPaid); // Default value
        Assert.Null(dto.Price); // Default value
        Assert.Equal(EventStatus.Draft, dto.Status); // Default value
    }

    [Fact]
    public void Constructor_WithNullOptionalParameters_ShouldSetNullsCorrectly()
    {
        // Arrange
        var title = "Social Event";
        var category = EventCategory.Social;
        var date = DateTime.UtcNow.AddDays(20).Date;
        var startTime = TimeSpan.FromHours(18);
        var endTime = TimeSpan.FromHours(22);
        var capacity = 200;
        var registrationDeadline = DateTime.UtcNow.AddDays(15);

        // Act
        var dto = new CreateEventDto(
            title,
            null, // Description
            category,
            date,
            startTime,
            endTime,
            null, // Location
            capacity,
            registrationDeadline,
            false, // IsPaid
            null,  // Price
            EventStatus.Draft // Status
        );

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Location);
        Assert.Null(dto.Price);
    }

    [Fact]
    public void Constructor_WithAllEventCategories_ShouldSetCategoryCorrectly()
    {
        // Arrange & Act & Assert
        foreach (EventCategory category in Enum.GetValues<EventCategory>())
        {
            var dto = new CreateEventDto(
                "Test Event",
                null,
                category,
                DateTime.UtcNow.AddDays(10).Date,
                TimeSpan.FromHours(10),
                TimeSpan.FromHours(12),
                null,
                100,
                DateTime.UtcNow.AddDays(5)
            );

            Assert.Equal(category, dto.Category);
        }
    }

    [Fact]
    public void Constructor_WithAllEventStatuses_ShouldSetStatusCorrectly()
    {
        // Arrange & Act & Assert
        foreach (EventStatus status in Enum.GetValues<EventStatus>())
        {
            var dto = new CreateEventDto(
                "Test Event",
                null,
                EventCategory.Academic,
                DateTime.UtcNow.AddDays(10).Date,
                TimeSpan.FromHours(10),
                TimeSpan.FromHours(12),
                null,
                100,
                DateTime.UtcNow.AddDays(5),
                false,
                null,
                status
            );

            Assert.Equal(status, dto.Status);
        }
    }

    [Fact]
    public void Constructor_WithPaidEvent_ShouldSetPriceCorrectly()
    {
        // Arrange
        var title = "Paid Workshop";
        var price = 250.50m;

        // Act
        var dto = new CreateEventDto(
            title,
            null,
            EventCategory.Workshop,
            DateTime.UtcNow.AddDays(25).Date,
            TimeSpan.FromHours(14),
            TimeSpan.FromHours(16),
            "Room 101",
            30,
            DateTime.UtcNow.AddDays(20),
            true, // IsPaid
            price,
            EventStatus.Published
        );

        // Assert
        Assert.True(dto.IsPaid);
        Assert.Equal(price, dto.Price);
    }

    [Fact]
    public void Constructor_WithFreeEvent_ShouldSetIsPaidToFalse()
    {
        // Arrange
        var title = "Free Event";

        // Act
        var dto = new CreateEventDto(
            title,
            "Free entry event",
            EventCategory.Social,
            DateTime.UtcNow.AddDays(30).Date,
            TimeSpan.FromHours(19),
            TimeSpan.FromHours(21),
            "Outdoor Area",
            500,
            DateTime.UtcNow.AddDays(25),
            false, // IsPaid
            null,  // Price
            EventStatus.Published
        );

        // Assert
        Assert.False(dto.IsPaid);
        Assert.Null(dto.Price);
    }

    [Fact]
    public void Constructor_WithEmptyTitle_ShouldSetEmptyString()
    {
        // Arrange
        var category = EventCategory.Academic;

        // Act
        var dto = new CreateEventDto(
            string.Empty,
            null,
            category,
            DateTime.UtcNow.AddDays(10).Date,
            TimeSpan.FromHours(9),
            TimeSpan.FromHours(11),
            null,
            50,
            DateTime.UtcNow.AddDays(5)
        );

        // Assert
        Assert.Equal(string.Empty, dto.Title);
    }

    [Fact]
    public void Constructor_WithLongStrings_ShouldSetValuesCorrectly()
    {
        // Arrange
        var longTitle = new string('A', 200);
        var longDescription = new string('B', 2000);
        var longLocation = new string('C', 500);

        // Act
        var dto = new CreateEventDto(
            longTitle,
            longDescription,
            EventCategory.Conference,
            DateTime.UtcNow.AddDays(40).Date,
            TimeSpan.FromHours(8),
            TimeSpan.FromHours(18),
            longLocation,
            1000,
            DateTime.UtcNow.AddDays(35)
        );

        // Assert
        Assert.Equal(longTitle, dto.Title);
        Assert.Equal(longDescription, dto.Description);
        Assert.Equal(longLocation, dto.Location);
    }

    [Fact]
    public void Constructor_WithTimeSpanValues_ShouldSetCorrectly()
    {
        // Arrange
        var startTime = new TimeSpan(14, 30, 0); // 14:30:00
        var endTime = new TimeSpan(17, 45, 0);   // 17:45:00

        // Act
        var dto = new CreateEventDto(
            "Time Test Event",
            null,
            EventCategory.Workshop,
            DateTime.UtcNow.AddDays(20).Date,
            startTime,
            endTime,
            null,
            25,
            DateTime.UtcNow.AddDays(15)
        );

        // Assert
        Assert.Equal(startTime, dto.StartTime);
        Assert.Equal(endTime, dto.EndTime);
        Assert.True(dto.EndTime > dto.StartTime);
    }

    [Fact]
    public void Constructor_WithFutureDates_ShouldSetDatesCorrectly()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(60).Date;
        var futureDeadline = DateTime.UtcNow.AddDays(50);

        // Act
        var dto = new CreateEventDto(
            "Future Event",
            null,
            EventCategory.Conference,
            futureDate,
            TimeSpan.FromHours(10),
            TimeSpan.FromHours(16),
            null,
            300,
            futureDeadline
        );

        // Assert
        Assert.Equal(futureDate, dto.Date);
        Assert.Equal(futureDeadline, dto.RegistrationDeadline);
        Assert.True(dto.Date > DateTime.UtcNow.Date);
        Assert.True(dto.RegistrationDeadline > DateTime.UtcNow);
    }

    [Fact]
    public void Constructor_WithDifferentCapacityValues_ShouldSetCorrectly()
    {
        // Arrange
        var smallCapacity = 1;
        var largeCapacity = 10000;

        // Act
        var dto1 = new CreateEventDto(
            "Small Event",
            null,
            EventCategory.Workshop,
            DateTime.UtcNow.AddDays(10).Date,
            TimeSpan.FromHours(10),
            TimeSpan.FromHours(12),
            null,
            smallCapacity,
            DateTime.UtcNow.AddDays(5)
        );

        var dto2 = new CreateEventDto(
            "Large Event",
            null,
            EventCategory.Conference,
            DateTime.UtcNow.AddDays(10).Date,
            TimeSpan.FromHours(9),
            TimeSpan.FromHours(17),
            null,
            largeCapacity,
            DateTime.UtcNow.AddDays(5)
        );

        // Assert
        Assert.Equal(smallCapacity, dto1.Capacity);
        Assert.Equal(largeCapacity, dto2.Capacity);
    }

    [Fact]
    public void RecordEquality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var title = "Equal Event";
        var category = EventCategory.Sports;
        var date = DateTime.UtcNow.AddDays(25).Date;
        var startTime = TimeSpan.FromHours(10);
        var endTime = TimeSpan.FromHours(12);
        var capacity = 100;
        var registrationDeadline = DateTime.UtcNow.AddDays(20);

        // Act
        var dto1 = new CreateEventDto(
            title,
            null,
            category,
            date,
            startTime,
            endTime,
            null,
            capacity,
            registrationDeadline
        );

        var dto2 = new CreateEventDto(
            title,
            null,
            category,
            date,
            startTime,
            endTime,
            null,
            capacity,
            registrationDeadline
        );

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentRequiredValues_ShouldNotBeEqual()
    {
        // Arrange
        var category = EventCategory.Academic;
        var date = DateTime.UtcNow.AddDays(15).Date;
        var startTime = TimeSpan.FromHours(9);
        var endTime = TimeSpan.FromHours(11);
        var capacity = 50;
        var registrationDeadline = DateTime.UtcNow.AddDays(10);

        // Act
        var dto1 = new CreateEventDto(
            "Event 1",
            null,
            category,
            date,
            startTime,
            endTime,
            null,
            capacity,
            registrationDeadline
        );

        var dto2 = new CreateEventDto(
            "Event 2",
            null,
            category,
            date,
            startTime,
            endTime,
            null,
            capacity,
            registrationDeadline
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentCategories_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Same Title";
        var date = DateTime.UtcNow.AddDays(20).Date;
        var startTime = TimeSpan.FromHours(10);
        var endTime = TimeSpan.FromHours(12);
        var capacity = 100;
        var registrationDeadline = DateTime.UtcNow.AddDays(15);

        // Act
        var dto1 = new CreateEventDto(
            title,
            null,
            EventCategory.Conference,
            date,
            startTime,
            endTime,
            null,
            capacity,
            registrationDeadline
        );

        var dto2 = new CreateEventDto(
            title,
            null,
            EventCategory.Workshop,
            date,
            startTime,
            endTime,
            null,
            capacity,
            registrationDeadline
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentOptionalValues_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Event";
        var category = EventCategory.Social;
        var date = DateTime.UtcNow.AddDays(30).Date;
        var startTime = TimeSpan.FromHours(18);
        var endTime = TimeSpan.FromHours(20);
        var capacity = 200;
        var registrationDeadline = DateTime.UtcNow.AddDays(25);

        // Act
        var dto1 = new CreateEventDto(
            title,
            null,
            category,
            date,
            startTime,
            endTime,
            "Location 1",
            capacity,
            registrationDeadline
        );

        var dto2 = new CreateEventDto(
            title,
            null,
            category,
            date,
            startTime,
            endTime,
            "Location 2",
            capacity,
            registrationDeadline
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentDefaultValues_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Event";
        var category = EventCategory.Conference;
        var date = DateTime.UtcNow.AddDays(20).Date;
        var startTime = TimeSpan.FromHours(10);
        var endTime = TimeSpan.FromHours(12);
        var capacity = 100;
        var registrationDeadline = DateTime.UtcNow.AddDays(15);

        // Act
        var dto1 = new CreateEventDto(
            title,
            null,
            category,
            date,
            startTime,
            endTime,
            null,
            capacity,
            registrationDeadline,
            false, // IsPaid
            null,  // Price
            EventStatus.Draft // Status
        );

        var dto2 = new CreateEventDto(
            title,
            null,
            category,
            date,
            startTime,
            endTime,
            null,
            capacity,
            registrationDeadline,
            true,  // IsPaid - different
            null,
            EventStatus.Draft
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void Constructor_WithZeroCapacity_ShouldSetZero()
    {
        // Arrange
        var title = "Zero Capacity Event";

        // Act
        var dto = new CreateEventDto(
            title,
            null,
            EventCategory.Academic,
            DateTime.UtcNow.AddDays(10).Date,
            TimeSpan.FromHours(10),
            TimeSpan.FromHours(12),
            null,
            0, // Zero capacity
            DateTime.UtcNow.AddDays(5)
        );

        // Assert
        Assert.Equal(0, dto.Capacity);
    }

    [Fact]
    public void Constructor_WithDecimalPrice_ShouldSetPriceCorrectly()
    {
        // Arrange
        var price1 = 99.99m;
        var price2 = 0.01m;
        var price3 = 1000.50m;

        // Act
        var dto1 = new CreateEventDto(
            "Event 1",
            null,
            EventCategory.Workshop,
            DateTime.UtcNow.AddDays(10).Date,
            TimeSpan.FromHours(10),
            TimeSpan.FromHours(12),
            null,
            50,
            DateTime.UtcNow.AddDays(5),
            true,
            price1,
            EventStatus.Published
        );

        var dto2 = new CreateEventDto(
            "Event 2",
            null,
            EventCategory.Social,
            DateTime.UtcNow.AddDays(10).Date,
            TimeSpan.FromHours(10),
            TimeSpan.FromHours(12),
            null,
            50,
            DateTime.UtcNow.AddDays(5),
            true,
            price2,
            EventStatus.Published
        );

        var dto3 = new CreateEventDto(
            "Event 3",
            null,
            EventCategory.Conference,
            DateTime.UtcNow.AddDays(10).Date,
            TimeSpan.FromHours(10),
            TimeSpan.FromHours(12),
            null,
            50,
            DateTime.UtcNow.AddDays(5),
            true,
            price3,
            EventStatus.Published
        );

        // Assert
        Assert.Equal(price1, dto1.Price);
        Assert.Equal(price2, dto2.Price);
        Assert.Equal(price3, dto3.Price);
    }
}

