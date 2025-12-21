using SmartCampus.API.Controllers;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class CreateEquipmentDtoTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var name = "Dell Laptop XPS 15";
        var type = EquipmentType.Laptop;
        var serialNumber = "SN-12345";
        var location = "Engineering Building, Room 205";
        var description = "High-performance laptop for presentations";
        var brand = "Dell";
        var model = "XPS 15";

        // Act
        var dto = new CreateEquipmentDto(
            name,
            type,
            serialNumber,
            location,
            description,
            brand,
            model
        );

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(type, dto.Type);
        Assert.Equal(serialNumber, dto.SerialNumber);
        Assert.Equal(location, dto.Location);
        Assert.Equal(description, dto.Description);
        Assert.Equal(brand, dto.Brand);
        Assert.Equal(model, dto.Model);
    }

    [Fact]
    public void Constructor_WithNullOptionalParameters_ShouldSetNullsCorrectly()
    {
        // Arrange
        var name = "Canon Camera";
        var type = EquipmentType.Camera;
        var serialNumber = "SN-67890";

        // Act
        var dto = new CreateEquipmentDto(
            name,
            type,
            serialNumber,
            null, // Location
            null, // Description
            null, // Brand
            null  // Model
        );

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(type, dto.Type);
        Assert.Equal(serialNumber, dto.SerialNumber);
        Assert.Null(dto.Location);
        Assert.Null(dto.Description);
        Assert.Null(dto.Brand);
        Assert.Null(dto.Model);
    }

    [Fact]
    public void Constructor_WithAllEquipmentTypes_ShouldSetTypeCorrectly()
    {
        // Arrange & Act & Assert
        foreach (EquipmentType equipmentType in Enum.GetValues<EquipmentType>())
        {
            var dto = new CreateEquipmentDto(
                "Test Equipment",
                equipmentType,
                "SN-TEST",
                null,
                null,
                null,
                null
            );

            Assert.Equal(equipmentType, dto.Type);
        }
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldSetEmptyString()
    {
        // Arrange
        var serialNumber = "SN-11111";

        // Act
        var dto = new CreateEquipmentDto(
            string.Empty,
            EquipmentType.Other,
            serialNumber,
            null,
            null,
            null,
            null
        );

        // Assert
        Assert.Equal(string.Empty, dto.Name);
        Assert.Equal(serialNumber, dto.SerialNumber);
    }

    [Fact]
    public void Constructor_WithEmptySerialNumber_ShouldSetEmptyString()
    {
        // Arrange
        var name = "Test Equipment";

        // Act
        var dto = new CreateEquipmentDto(
            name,
            EquipmentType.Other,
            string.Empty,
            null,
            null,
            null,
            null
        );

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(string.Empty, dto.SerialNumber);
    }

    [Fact]
    public void Constructor_WithLongStrings_ShouldSetValuesCorrectly()
    {
        // Arrange
        var longName = new string('A', 200);
        var longSerialNumber = new string('B', 100);
        var longLocation = new string('C', 200);
        var longDescription = new string('D', 1000);
        var longBrand = new string('E', 100);
        var longModel = new string('F', 100);

        // Act
        var dto = new CreateEquipmentDto(
            longName,
            EquipmentType.Projector,
            longSerialNumber,
            longLocation,
            longDescription,
            longBrand,
            longModel
        );

        // Assert
        Assert.Equal(longName, dto.Name);
        Assert.Equal(longSerialNumber, dto.SerialNumber);
        Assert.Equal(longLocation, dto.Location);
        Assert.Equal(longDescription, dto.Description);
        Assert.Equal(longBrand, dto.Brand);
        Assert.Equal(longModel, dto.Model);
    }

    [Fact]
    public void Constructor_WithPartialOptionalParameters_ShouldSetCorrectly()
    {
        // Arrange
        var name = "Sony Microphone";
        var type = EquipmentType.Microphone;
        var serialNumber = "SN-MIC-001";
        var brand = "Sony";
        var model = "ECM-123";

        // Act - Only setting some optional parameters
        var dto = new CreateEquipmentDto(
            name,
            type,
            serialNumber,
            null, // Location
            null, // Description
            brand, // Brand
            model  // Model
        );

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(type, dto.Type);
        Assert.Equal(serialNumber, dto.SerialNumber);
        Assert.Null(dto.Location);
        Assert.Null(dto.Description);
        Assert.Equal(brand, dto.Brand);
        Assert.Equal(model, dto.Model);
    }

    [Fact]
    public void RecordEquality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var name = "Test Equipment";
        var type = EquipmentType.Tablet;
        var serialNumber = "SN-EQUAL-001";

        // Act
        var dto1 = new CreateEquipmentDto(name, type, serialNumber, null, null, null, null);
        var dto2 = new CreateEquipmentDto(name, type, serialNumber, null, null, null, null);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentRequiredValues_ShouldNotBeEqual()
    {
        // Arrange
        var type = EquipmentType.Tripod;

        // Act
        var dto1 = new CreateEquipmentDto("Equipment 1", type, "SN-001", null, null, null, null);
        var dto2 = new CreateEquipmentDto("Equipment 2", type, "SN-001", null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentTypes_ShouldNotBeEqual()
    {
        // Arrange
        var name = "Same Name";
        var serialNumber = "SN-SAME";

        // Act
        var dto1 = new CreateEquipmentDto(name, EquipmentType.Laptop, serialNumber, null, null, null, null);
        var dto2 = new CreateEquipmentDto(name, EquipmentType.Projector, serialNumber, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentSerialNumbers_ShouldNotBeEqual()
    {
        // Arrange
        var name = "Same Equipment";
        var type = EquipmentType.SoundSystem;

        // Act
        var dto1 = new CreateEquipmentDto(name, type, "SN-001", null, null, null, null);
        var dto2 = new CreateEquipmentDto(name, type, "SN-002", null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentOptionalValues_ShouldNotBeEqual()
    {
        // Arrange
        var name = "Test Equipment";
        var type = EquipmentType.Other;
        var serialNumber = "SN-TEST";

        // Act
        var dto1 = new CreateEquipmentDto(name, type, serialNumber, "Location 1", null, null, null);
        var dto2 = new CreateEquipmentDto(name, type, serialNumber, "Location 2", null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullOptionalValues_ShouldNotBeEqual()
    {
        // Arrange
        var name = "Test Equipment";
        var type = EquipmentType.Laptop;
        var serialNumber = "SN-TEST";

        // Act
        var dto1 = new CreateEquipmentDto(name, type, serialNumber, null, null, null, null);
        var dto2 = new CreateEquipmentDto(name, type, serialNumber, "Some Location", null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void Constructor_WithAllOptionalParametersSet_ShouldSetAllValues()
    {
        // Arrange
        var name = "Complete Equipment";
        var type = EquipmentType.Camera;
        var serialNumber = "SN-COMPLETE-001";
        var location = "Main Building, Room 101";
        var description = "Professional camera with all accessories";
        var brand = "Canon";
        var model = "EOS R5";

        // Act
        var dto = new CreateEquipmentDto(
            name,
            type,
            serialNumber,
            location,
            description,
            brand,
            model
        );

        // Assert
        Assert.Equal(name, dto.Name);
        Assert.Equal(type, dto.Type);
        Assert.Equal(serialNumber, dto.SerialNumber);
        Assert.Equal(location, dto.Location);
        Assert.Equal(description, dto.Description);
        Assert.Equal(brand, dto.Brand);
        Assert.Equal(model, dto.Model);
    }
}

