using SmartCampus.API.Controllers;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class UpdateEquipmentDtoTests
{
    [Fact]
    public void Constructor_WithAllNullValues_ShouldSetAllPropertiesToNull()
    {
        // Act
        var dto = new UpdateEquipmentDto(null, null, null, null, null, null, null, null);

        // Assert
        Assert.Null(dto.Name);
        Assert.Null(dto.Type);
        Assert.Null(dto.SerialNumber);
        Assert.Null(dto.Status);
        Assert.Null(dto.Location);
        Assert.Null(dto.Description);
        Assert.Null(dto.Brand);
        Assert.Null(dto.Model);
    }

    [Fact]
    public void Constructor_WithAllValues_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var name = "Dell Laptop XPS 15";
        var type = EquipmentType.Laptop;
        var serialNumber = "SN-12345";
        var status = EquipmentStatus.Available;
        var location = "Engineering Building, Room 205";
        var description = "High-performance laptop for presentations";
        var brand = "Dell";
        var model = "XPS 15";

        // Act
        var dto = new UpdateEquipmentDto(
            name,
            type,
            serialNumber,
            status,
            location,
            description,
            brand,
            model
        );

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(type, dto.Type);
        Assert.Equal(serialNumber, dto.SerialNumber);
        Assert.Equal(status, dto.Status);
        Assert.Equal(location, dto.Location);
        Assert.Equal(description, dto.Description);
        Assert.Equal(brand, dto.Brand);
        Assert.Equal(model, dto.Model);
    }

    [Fact]
    public void Constructor_WithPartialValues_ShouldSetOnlyProvidedProperties()
    {
        // Arrange
        var name = "Canon Camera";
        var type = EquipmentType.Camera;

        // Act
        var dto = new UpdateEquipmentDto(name, type, null, null, null, null, null, null);

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(type, dto.Type);
        Assert.Null(dto.SerialNumber);
        Assert.Null(dto.Status);
        Assert.Null(dto.Location);
        Assert.Null(dto.Description);
        Assert.Null(dto.Brand);
        Assert.Null(dto.Model);
    }

    [Theory]
    [InlineData(EquipmentType.Laptop)]
    [InlineData(EquipmentType.Projector)]
    [InlineData(EquipmentType.Camera)]
    [InlineData(EquipmentType.Microphone)]
    [InlineData(EquipmentType.Tablet)]
    [InlineData(EquipmentType.Tripod)]
    [InlineData(EquipmentType.SoundSystem)]
    [InlineData(EquipmentType.Other)]
    public void Constructor_WithAllEquipmentTypeValues_ShouldSetCorrectly(EquipmentType type)
    {
        // Act
        var dto = new UpdateEquipmentDto(null, type, null, null, null, null, null, null);

        // Assert
        Assert.Equal(type, dto.Type);
    }

    [Theory]
    [InlineData(EquipmentStatus.Available)]
    [InlineData(EquipmentStatus.Borrowed)]
    [InlineData(EquipmentStatus.Maintenance)]
    [InlineData(EquipmentStatus.Retired)]
    public void Constructor_WithAllEquipmentStatusValues_ShouldSetCorrectly(EquipmentStatus status)
    {
        // Act
        var dto = new UpdateEquipmentDto(null, null, null, status, null, null, null, null);

        // Assert
        Assert.Equal(status, dto.Status);
    }

    [Fact]
    public void Constructor_WithEmptyStrings_ShouldSetEmptyStrings()
    {
        // Act
        var dto = new UpdateEquipmentDto("", null, "", null, "", "", "", "");

        // Assert
        Assert.Equal("", dto.Name);
        Assert.Equal("", dto.SerialNumber);
        Assert.Equal("", dto.Location);
        Assert.Equal("", dto.Description);
        Assert.Equal("", dto.Brand);
        Assert.Equal("", dto.Model);
    }

    [Fact]
    public void Constructor_WithLongStrings_ShouldSetCorrectly()
    {
        // Arrange
        var longName = new string('A', 1000);
        var longDescription = new string('B', 2000);
        var longLocation = new string('C', 500);

        // Act
        var dto = new UpdateEquipmentDto(
            longName,
            null,
            null,
            null,
            longLocation,
            longDescription,
            null,
            null
        );

        // Assert
        Assert.Equal(longName, dto.Name);
        Assert.Equal(longDescription, dto.Description);
        Assert.Equal(longLocation, dto.Location);
    }

    [Fact]
    public void Constructor_WithOnlyName_ShouldSetOnlyName()
    {
        // Arrange
        var name = "Updated Equipment Name";

        // Act
        var dto = new UpdateEquipmentDto(name, null, null, null, null, null, null, null);

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Null(dto.Type);
        Assert.Null(dto.SerialNumber);
        Assert.Null(dto.Status);
        Assert.Null(dto.Location);
        Assert.Null(dto.Description);
        Assert.Null(dto.Brand);
        Assert.Null(dto.Model);
    }

    [Fact]
    public void Constructor_WithOnlyType_ShouldSetOnlyType()
    {
        // Arrange
        var type = EquipmentType.Projector;

        // Act
        var dto = new UpdateEquipmentDto(null, type, null, null, null, null, null, null);

        // Assert
        Assert.Null(dto.Name);
        Assert.Equal(type, dto.Type);
        Assert.Null(dto.SerialNumber);
        Assert.Null(dto.Status);
        Assert.Null(dto.Location);
        Assert.Null(dto.Description);
        Assert.Null(dto.Brand);
        Assert.Null(dto.Model);
    }

    [Fact]
    public void Constructor_WithOnlyStatus_ShouldSetOnlyStatus()
    {
        // Arrange
        var status = EquipmentStatus.Maintenance;

        // Act
        var dto = new UpdateEquipmentDto(null, null, null, status, null, null, null, null);

        // Assert
        Assert.Null(dto.Name);
        Assert.Null(dto.Type);
        Assert.Null(dto.SerialNumber);
        Assert.Equal(status, dto.Status);
        Assert.Null(dto.Location);
        Assert.Null(dto.Description);
        Assert.Null(dto.Brand);
        Assert.Null(dto.Model);
    }

    [Fact]
    public void Equality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var name = "Test Equipment";
        var type = EquipmentType.Laptop;
        var serialNumber = "SN-123";
        var status = EquipmentStatus.Available;
        var location = "Room 101";
        var description = "Test Description";
        var brand = "Test Brand";
        var model = "Test Model";

        // Act
        var dto1 = new UpdateEquipmentDto(name, type, serialNumber, status, location, description, brand, model);
        var dto2 = new UpdateEquipmentDto(name, type, serialNumber, status, location, description, brand, model);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithAllNullValues_ShouldBeEqual()
    {
        // Act
        var dto1 = new UpdateEquipmentDto(null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEquipmentDto(null, null, null, null, null, null, null, null);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentName_ShouldNotBeEqual()
    {
        // Arrange
        var type = EquipmentType.Laptop;

        // Act
        var dto1 = new UpdateEquipmentDto("Name1", type, null, null, null, null, null, null);
        var dto2 = new UpdateEquipmentDto("Name2", type, null, null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentType_ShouldNotBeEqual()
    {
        // Arrange
        var name = "Test Equipment";

        // Act
        var dto1 = new UpdateEquipmentDto(name, EquipmentType.Laptop, null, null, null, null, null, null);
        var dto2 = new UpdateEquipmentDto(name, EquipmentType.Projector, null, null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentStatus_ShouldNotBeEqual()
    {
        // Arrange
        var name = "Test Equipment";

        // Act
        var dto1 = new UpdateEquipmentDto(name, null, null, EquipmentStatus.Available, null, null, null, null);
        var dto2 = new UpdateEquipmentDto(name, null, null, EquipmentStatus.Borrowed, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullName_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateEquipmentDto(null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEquipmentDto("Name", null, null, null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullType_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateEquipmentDto(null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEquipmentDto(null, EquipmentType.Laptop, null, null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullStatus_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateEquipmentDto(null, null, null, null, null, null, null, null);
        var dto2 = new UpdateEquipmentDto(null, null, null, EquipmentStatus.Available, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Constructor_WithMixedNullAndNonNullValues_ShouldSetCorrectly()
    {
        // Arrange
        var name = "Test Equipment";
        var type = EquipmentType.Camera;
        var status = EquipmentStatus.Maintenance;
        var brand = "Canon";

        // Act
        var dto = new UpdateEquipmentDto(name, type, null, status, null, null, brand, null);

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(type, dto.Type);
        Assert.Null(dto.SerialNumber);
        Assert.Equal(status, dto.Status);
        Assert.Null(dto.Location);
        Assert.Null(dto.Description);
        Assert.Equal(brand, dto.Brand);
        Assert.Null(dto.Model);
    }

    [Fact]
    public void Constructor_WithOnlyOptionalStringFields_ShouldSetCorrectly()
    {
        // Arrange
        var location = "Room 205";
        var description = "Updated description";
        var brand = "Sony";
        var model = "Model X";

        // Act
        var dto = new UpdateEquipmentDto(null, null, null, null, location, description, brand, model);

        // Assert
        Assert.Null(dto.Name);
        Assert.Null(dto.Type);
        Assert.Null(dto.SerialNumber);
        Assert.Null(dto.Status);
        Assert.Equal(location, dto.Location);
        Assert.Equal(description, dto.Description);
        Assert.Equal(brand, dto.Brand);
        Assert.Equal(model, dto.Model);
    }
}

