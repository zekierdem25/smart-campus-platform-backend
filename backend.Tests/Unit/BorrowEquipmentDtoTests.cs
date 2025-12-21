using SmartCampus.API.Controllers;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class BorrowEquipmentDtoTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var expectedReturnDate = DateTime.UtcNow.AddDays(7);
        var purpose = "Ders projesi için";
        var notes = "Öğleden sonra teslim alacağım";

        // Act
        var dto = new BorrowEquipmentDto(
            expectedReturnDate,
            purpose,
            notes
        );

        // Assert
        Assert.Equal(expectedReturnDate, dto.ExpectedReturnDate);
        Assert.Equal(purpose, dto.Purpose);
        Assert.Equal(notes, dto.Notes);
    }

    [Fact]
    public void Constructor_WithNullNotes_ShouldSetNotesToNull()
    {
        // Arrange
        var expectedReturnDate = DateTime.UtcNow.AddDays(5);
        var purpose = "Araştırma için";

        // Act
        var dto = new BorrowEquipmentDto(
            expectedReturnDate,
            purpose,
            null
        );

        // Assert
        Assert.Equal(expectedReturnDate, dto.ExpectedReturnDate);
        Assert.Equal(purpose, dto.Purpose);
        Assert.Null(dto.Notes);
    }

    [Fact]
    public void Constructor_WithEmptyPurpose_ShouldSetEmptyString()
    {
        // Arrange
        var expectedReturnDate = DateTime.UtcNow.AddDays(3);
        var purpose = string.Empty;

        // Act
        var dto = new BorrowEquipmentDto(
            expectedReturnDate,
            purpose,
            null
        );

        // Assert
        Assert.Equal(expectedReturnDate, dto.ExpectedReturnDate);
        Assert.Equal(string.Empty, dto.Purpose);
        Assert.Null(dto.Notes);
    }

    [Fact]
    public void Constructor_WithFutureDate_ShouldSetExpectedReturnDate()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddDays(30);
        var purpose = "Test amaçlı";

        // Act
        var dto = new BorrowEquipmentDto(
            futureDate,
            purpose,
            null
        );

        // Assert
        Assert.Equal(futureDate, dto.ExpectedReturnDate);
        Assert.True(dto.ExpectedReturnDate > DateTime.UtcNow);
    }

    [Fact]
    public void Constructor_WithPastDate_ShouldStillSetExpectedReturnDate()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddDays(-5);
        var purpose = "Geçmiş tarih testi";

        // Act
        var dto = new BorrowEquipmentDto(
            pastDate,
            purpose,
            null
        );

        // Assert
        Assert.Equal(pastDate, dto.ExpectedReturnDate);
    }

    [Fact]
    public void Constructor_WithLongPurpose_ShouldSetPurposeCorrectly()
    {
        // Arrange
        var expectedReturnDate = DateTime.UtcNow.AddDays(10);
        var longPurpose = new string('A', 500); // 500 karakterlik string

        // Act
        var dto = new BorrowEquipmentDto(
            expectedReturnDate,
            longPurpose,
            null
        );

        // Assert
        Assert.Equal(longPurpose, dto.Purpose);
        Assert.Equal(500, dto.Purpose.Length);
    }

    [Fact]
    public void Constructor_WithLongNotes_ShouldSetNotesCorrectly()
    {
        // Arrange
        var expectedReturnDate = DateTime.UtcNow.AddDays(15);
        var purpose = "Test";
        var longNotes = new string('B', 1000); // 1000 karakterlik string

        // Act
        var dto = new BorrowEquipmentDto(
            expectedReturnDate,
            purpose,
            longNotes
        );

        // Assert
        Assert.Equal(longNotes, dto.Notes);
        Assert.Equal(1000, dto.Notes?.Length);
    }

    [Fact]
    public void RecordEquality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var date = DateTime.UtcNow.AddDays(7);
        var purpose = "Aynı amaç";
        var notes = "Aynı notlar";

        // Act
        var dto1 = new BorrowEquipmentDto(date, purpose, notes);
        var dto2 = new BorrowEquipmentDto(date, purpose, notes);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var date1 = DateTime.UtcNow.AddDays(7);
        var date2 = DateTime.UtcNow.AddDays(14);
        var purpose = "Aynı amaç";

        // Act
        var dto1 = new BorrowEquipmentDto(date1, purpose, null);
        var dto2 = new BorrowEquipmentDto(date2, purpose, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentNotes_ShouldNotBeEqual()
    {
        // Arrange
        var date = DateTime.UtcNow.AddDays(7);
        var purpose = "Aynı amaç";

        // Act
        var dto1 = new BorrowEquipmentDto(date, purpose, "Notlar 1");
        var dto2 = new BorrowEquipmentDto(date, purpose, "Notlar 2");

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullNotes_ShouldNotBeEqual()
    {
        // Arrange
        var date = DateTime.UtcNow.AddDays(7);
        var purpose = "Aynı amaç";

        // Act
        var dto1 = new BorrowEquipmentDto(date, purpose, null);
        var dto2 = new BorrowEquipmentDto(date, purpose, "Notlar");

        // Assert
        Assert.NotEqual(dto1, dto2);
    }
}

