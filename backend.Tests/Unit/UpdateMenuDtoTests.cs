using SmartCampus.API.Controllers;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class UpdateMenuDtoTests
{
    [Fact]
    public void Constructor_WithAllNullValues_ShouldSetAllPropertiesToNull()
    {
        // Act
        var dto = new UpdateMenuDto(null, null, null, null, null);

        // Assert
        Assert.Null(dto.ItemsJson);
        Assert.Null(dto.NutritionJson);
        Assert.Null(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithAllValues_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var itemsJson = "[\"Soup\", \"Main Dish\", \"Salad\"]";
        var nutritionJson = "{\"calories\": 500, \"protein\": 30, \"carbs\": 50, \"fat\": 20}";
        var isPublished = true;

        // Act
        var dto = new UpdateMenuDto(itemsJson, nutritionJson, null, null, isPublished);

        // Assert
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.Equal(nutritionJson, dto.NutritionJson);
        Assert.Equal(isPublished, dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithPartialValues_ShouldSetOnlyProvidedProperties()
    {
        // Arrange
        var itemsJson = "[\"Soup\", \"Main Dish\"]";

        // Act
        var dto = new UpdateMenuDto(itemsJson, null, null, null, null);

        // Assert
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.Null(dto.NutritionJson);
        Assert.Null(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithOnlyItemsJson_ShouldSetOnlyItemsJson()
    {
        // Arrange
        var itemsJson = "[\"Item1\", \"Item2\"]";

        // Act
        var dto = new UpdateMenuDto(itemsJson, null, null, null, null);

        // Assert
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.Null(dto.NutritionJson);
        Assert.Null(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithOnlyNutritionJson_ShouldSetOnlyNutritionJson()
    {
        // Arrange
        var nutritionJson = "{\"calories\": 600}";

        // Act
        var dto = new UpdateMenuDto(null, nutritionJson, null, null, null);

        // Assert
        Assert.Null(dto.ItemsJson);
        Assert.Equal(nutritionJson, dto.NutritionJson);
        Assert.Null(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithOnlyIsPublished_ShouldSetOnlyIsPublished()
    {
        // Arrange
        var isPublished = true;

        // Act
        var dto = new UpdateMenuDto(null, null, null, null, isPublished);

        // Assert
        Assert.Null(dto.ItemsJson);
        Assert.Null(dto.NutritionJson);
        Assert.Equal(isPublished, dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithBooleanIsPublished_ShouldSetCorrectly()
    {
        // Act
        var dto1 = new UpdateMenuDto(null, null, null, null, true);
        var dto2 = new UpdateMenuDto(null, null, null, null, false);

        // Assert
        Assert.True(dto1.IsPublished);
        Assert.False(dto2.IsPublished);
    }

    [Fact]
    public void Constructor_WithEmptyStrings_ShouldSetEmptyStrings()
    {
        // Act
        var dto = new UpdateMenuDto("", "", null, null, null);

        // Assert
        Assert.Equal("", dto.ItemsJson);
        Assert.Equal("", dto.NutritionJson);
        Assert.Null(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithLongJsonStrings_ShouldSetCorrectly()
    {
        // Arrange
        var longItemsJson = "[" + string.Join(", ", Enumerable.Range(1, 100).Select(i => $"\"Item{i}\"")) + "]";
        var longNutritionJson = "{" + string.Join(", ", Enumerable.Range(1, 50).Select(i => $"\"key{i}\": \"value{i}\"")) + "}";

        // Act
        var dto = new UpdateMenuDto(longItemsJson, longNutritionJson, null, null, null);

        // Assert
        Assert.Equal(longItemsJson, dto.ItemsJson);
        Assert.Equal(longNutritionJson, dto.NutritionJson);
    }

    [Fact]
    public void Constructor_WithValidJsonStrings_ShouldSetCorrectly()
    {
        // Arrange
        var itemsJson = "[\"Soup\", \"Main Dish\", \"Salad\", \"Dessert\"]";
        var nutritionJson = "{\"calories\": 500, \"protein\": 30, \"carbs\": 50, \"fat\": 20, \"fiber\": 10}";

        // Act
        var dto = new UpdateMenuDto(itemsJson, nutritionJson, null, null, true);

        // Assert
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.Equal(nutritionJson, dto.NutritionJson);
        Assert.True(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithInvalidJsonStrings_ShouldStillSetCorrectly()
    {
        // Arrange
        var invalidItemsJson = "not a valid json";
        var invalidNutritionJson = "{invalid json}";

        // Act
        var dto = new UpdateMenuDto(invalidItemsJson, invalidNutritionJson, null, null, null);

        // Assert
        Assert.Equal(invalidItemsJson, dto.ItemsJson);
        Assert.Equal(invalidNutritionJson, dto.NutritionJson);
    }

    [Fact]
    public void Constructor_WithMixedNullAndNonNullValues_ShouldSetCorrectly()
    {
        // Arrange
        var itemsJson = "[\"Item1\"]";
        var isPublished = false;

        // Act
        var dto = new UpdateMenuDto(itemsJson, null, null, null, isPublished);

        // Assert
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.Null(dto.NutritionJson);
        Assert.Equal(isPublished, dto.IsPublished);
    }

    [Fact]
    public void Equality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var itemsJson = "[\"Soup\", \"Main Dish\"]";
        var nutritionJson = "{\"calories\": 500}";
        var isPublished = true;

        // Act
        var dto1 = new UpdateMenuDto(itemsJson, nutritionJson, null, null, isPublished);
        var dto2 = new UpdateMenuDto(itemsJson, nutritionJson, null, null, isPublished);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithAllNullValues_ShouldBeEqual()
    {
        // Act
        var dto1 = new UpdateMenuDto(null, null, null, null, null);
        var dto2 = new UpdateMenuDto(null, null, null, null, null);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentItemsJson_ShouldNotBeEqual()
    {
        // Arrange
        var nutritionJson = "{\"calories\": 500}";
        var isPublished = true;

        // Act
        var dto1 = new UpdateMenuDto("[\"Item1\"]", nutritionJson, null, null, isPublished);
        var dto2 = new UpdateMenuDto("[\"Item2\"]", nutritionJson, null, null, isPublished);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentNutritionJson_ShouldNotBeEqual()
    {
        // Arrange
        var itemsJson = "[\"Item1\"]";
        var isPublished = true;

        // Act
        var dto1 = new UpdateMenuDto(itemsJson, "{\"calories\": 500}", null, null, isPublished);
        var dto2 = new UpdateMenuDto(itemsJson, "{\"calories\": 600}", null, null, isPublished);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentIsPublished_ShouldNotBeEqual()
    {
        // Arrange
        var itemsJson = "[\"Item1\"]";
        var nutritionJson = "{\"calories\": 500}";

        // Act
        var dto1 = new UpdateMenuDto(itemsJson, nutritionJson, null, null, true);
        var dto2 = new UpdateMenuDto(itemsJson, nutritionJson, null, null, false);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullItemsJson_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateMenuDto(null, null, null, null, null);
        var dto2 = new UpdateMenuDto("[\"Item1\"]", null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullNutritionJson_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateMenuDto(null, null, null, null, null);
        var dto2 = new UpdateMenuDto(null, "{\"calories\": 500}", null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullIsPublished_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateMenuDto(null, null, null, null, null);
        var dto2 = new UpdateMenuDto(null, null, null, null, true);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Constructor_WithComplexJsonStructures_ShouldSetCorrectly()
    {
        // Arrange
        var complexItemsJson = "{\"breakfast\": [\"Eggs\", \"Toast\"], \"lunch\": [\"Soup\", \"Salad\"], \"dinner\": [\"Pasta\", \"Dessert\"]}";
        var complexNutritionJson = "{\"breakfast\": {\"calories\": 300, \"protein\": 20}, \"lunch\": {\"calories\": 400, \"protein\": 25}, \"dinner\": {\"calories\": 600, \"protein\": 30}}";

        // Act
        var dto = new UpdateMenuDto(complexItemsJson, complexNutritionJson, null, null, true);

        // Assert
        Assert.Equal(complexItemsJson, dto.ItemsJson);
        Assert.Equal(complexNutritionJson, dto.NutritionJson);
        Assert.True(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithWhitespaceOnlyStrings_ShouldSetCorrectly()
    {
        // Arrange
        var whitespaceItemsJson = "   ";
        var whitespaceNutritionJson = "\t\n";

        // Act
        var dto = new UpdateMenuDto(whitespaceItemsJson, whitespaceNutritionJson, null, null, null);

        // Assert
        Assert.Equal(whitespaceItemsJson, dto.ItemsJson);
        Assert.Equal(whitespaceNutritionJson, dto.NutritionJson);
    }
}

