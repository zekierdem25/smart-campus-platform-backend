using SmartCampus.API.Controllers;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class CreateMenuDtoTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(15).Date;
        var mealType = MealType.Lunch;
        var itemsJson = "[\"Soup\", \"Main Dish\", \"Salad\"]";
        var nutritionJson = "{\"calories\": 500, \"protein\": 30, \"carbs\": 50, \"fat\": 20}";
        var isPublished = true;

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            itemsJson,
            nutritionJson,
            120m, // Price
            500,  // CalorieCount
            isPublished
        );

        // Assert
        Assert.Equal(cafeteriaId, dto.CafeteriaId);
        Assert.Equal(date, dto.Date);
        Assert.Equal(mealType, dto.MealType);
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.Equal(nutritionJson, dto.NutritionJson);
        Assert.Equal(isPublished, dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithDefaultIsPublished_ShouldUseDefaultValue()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(20).Date;
        var mealType = MealType.Dinner;

        // Act - Not specifying IsPublished
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            null, // ItemsJson
            null, // NutritionJson
            120m, // Price
            500   // CalorieCount
        );

        // Assert
        Assert.Equal(cafeteriaId, dto.CafeteriaId);
        Assert.Equal(date, dto.Date);
        Assert.Equal(mealType, dto.MealType);
        Assert.Null(dto.ItemsJson);
        Assert.Null(dto.NutritionJson);
        Assert.False(dto.IsPublished); // Default value
    }

    [Fact]
    public void Constructor_WithNullOptionalParameters_ShouldSetNullsCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(10).Date;
        var mealType = MealType.Lunch;

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            null, // ItemsJson
            null, // NutritionJson
            120m, // Price
            500,  // CalorieCount
            false  // IsPublished
        );

        // Assert
        Assert.Equal(cafeteriaId, dto.CafeteriaId);
        Assert.Equal(date, dto.Date);
        Assert.Equal(mealType, dto.MealType);
        Assert.Null(dto.ItemsJson);
        Assert.Null(dto.NutritionJson);
        Assert.False(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithAllMealTypes_ShouldSetMealTypeCorrectly()
    {
        // Arrange & Act & Assert
        foreach (MealType mealType in Enum.GetValues<MealType>())
        {
            var dto = new CreateMenuDto(
                Guid.NewGuid(),
                DateTime.UtcNow.AddDays(15).Date,
                mealType,
                null,
                null,
                120m,
                500
            );

            Assert.Equal(mealType, dto.MealType);
        }
    }

    [Fact]
    public void Constructor_WithLunchMealType_ShouldSetCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(25).Date;
        var itemsJson = "[\"Soup\", \"Grilled Chicken\", \"Rice\", \"Vegetables\"]";

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Lunch,
            itemsJson,
            null,
            120m,
            500,
            true
        );

        // Assert
        Assert.Equal(MealType.Lunch, dto.MealType);
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.True(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithDinnerMealType_ShouldSetCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(30).Date;
        var itemsJson = "[\"Soup\", \"Fish\", \"Pasta\", \"Dessert\"]";
        var nutritionJson = "{\"calories\": 600, \"protein\": 35}";

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Dinner,
            itemsJson,
            nutritionJson,
            120m,
            500,
            false
        );

        // Assert
        Assert.Equal(MealType.Dinner, dto.MealType);
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.Equal(nutritionJson, dto.NutritionJson);
        Assert.False(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithValidJsonStrings_ShouldSetCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(18).Date;
        var itemsJson = "[\"Item1\", \"Item2\", \"Item3\"]";
        var nutritionJson = "{\"calories\": 450, \"protein\": 25, \"carbs\": 45, \"fat\": 15}";

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Lunch,
            itemsJson,
            nutritionJson,
            120m,
            500
        );

        // Assert
        Assert.Equal(itemsJson, dto.ItemsJson);
        Assert.Equal(nutritionJson, dto.NutritionJson);
    }

    [Fact]
    public void Constructor_WithEmptyJsonStrings_ShouldSetCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(12).Date;

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Dinner,
            "[]",      // Empty array
            "{}",       // Empty object
            120m,
            500
        );

        // Assert
        Assert.Equal("[]", dto.ItemsJson);
        Assert.Equal("{}", dto.NutritionJson);
    }

    [Fact]
    public void Constructor_WithFutureDate_ShouldSetDateCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var futureDate = DateTime.UtcNow.AddDays(60).Date;

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            futureDate,
            MealType.Lunch,
            null,
            null,
            120m,
            500
        );

        // Assert
        Assert.Equal(futureDate, dto.Date);
        Assert.True(dto.Date > DateTime.UtcNow.Date);
    }

    [Fact]
    public void Constructor_WithPastDate_ShouldStillSetDateCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var pastDate = DateTime.UtcNow.AddDays(-5).Date;

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            pastDate,
            MealType.Dinner,
            null,
            null,
            120m,
            500
        );

        // Assert
        Assert.Equal(pastDate, dto.Date);
    }

    [Fact]
    public void Constructor_WithTodayDate_ShouldSetDateCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var today = DateTime.UtcNow.Date;

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            today,
            MealType.Lunch,
            null,
            null,
            120m,
            500
        );

        // Assert
        Assert.Equal(today, dto.Date);
    }

    [Fact]
    public void Constructor_WithDifferentGuidValues_ShouldSetCafeteriaIdCorrectly()
    {
        // Arrange
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(15).Date;

        // Act
        var dto1 = new CreateMenuDto(guid1, date, MealType.Lunch, null, null, 120m, 500);
        var dto2 = new CreateMenuDto(guid2, date, MealType.Lunch, null, null, 120m, 500);

        // Assert
        Assert.Equal(guid1, dto1.CafeteriaId);
        Assert.Equal(guid2, dto2.CafeteriaId);
        Assert.NotEqual(dto1.CafeteriaId, dto2.CafeteriaId);
    }

    [Fact]
    public void Constructor_WithPublishedTrue_ShouldSetIsPublishedToTrue()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(20).Date;

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Lunch,
            "[\"Soup\"]",
            null,
            120m,
            500,
            true // IsPublished
        );

        // Assert
        Assert.True(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithPublishedFalse_ShouldSetIsPublishedToFalse()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(25).Date;

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Dinner,
            "[\"Main Dish\"]",
            null,
            120m,
            500,
            false // IsPublished
        );

        // Assert
        Assert.False(dto.IsPublished);
    }

    [Fact]
    public void Constructor_WithLongJsonStrings_ShouldSetCorrectly()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(30).Date;
        var longItemsJson = "[" + string.Join(", ", Enumerable.Range(1, 50).Select(i => $"\"Item{i}\"")) + "]";
        var longNutritionJson = "{\"calories\": 500, \"protein\": 30, \"carbs\": 50, \"fat\": 20, \"fiber\": 10, \"sugar\": 15}";

        // Act
        var dto = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Lunch,
            longItemsJson,
            longNutritionJson,
            120m,
            500
        );

        // Assert
        Assert.Equal(longItemsJson, dto.ItemsJson);
        Assert.Equal(longNutritionJson, dto.NutritionJson);
        Assert.True(dto.ItemsJson!.Length > 100);
        Assert.True(dto.NutritionJson!.Length > 50);
    }

    [Fact]
    public void RecordEquality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(15).Date;
        var mealType = MealType.Lunch;
        var itemsJson = "[\"Soup\", \"Main\"]";
        var nutritionJson = "{\"calories\": 500}";

        // Act
        var dto1 = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            itemsJson,
            nutritionJson,
            120m,
            500,
            false
        );

        var dto2 = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            itemsJson,
            nutritionJson,
            120m,
            500,
            false
        );

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentCafeteriaIds_ShouldNotBeEqual()
    {
        // Arrange
        var date = DateTime.UtcNow.AddDays(20).Date;
        var mealType = MealType.Dinner;

        // Act
        var dto1 = new CreateMenuDto(
            Guid.NewGuid(),
            date,
            mealType,
            null,
            null,
            120m,
            500
        );

        var dto2 = new CreateMenuDto(
            Guid.NewGuid(),
            date,
            mealType,
            null,
            null,
            120m,
            500
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentDates_ShouldNotBeEqual()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var mealType = MealType.Lunch;

        // Act
        var dto1 = new CreateMenuDto(
            cafeteriaId,
            DateTime.UtcNow.AddDays(10).Date,
            mealType,
            null,
            null,
            120m,
            500
        );

        var dto2 = new CreateMenuDto(
            cafeteriaId,
            DateTime.UtcNow.AddDays(15).Date,
            mealType,
            null,
            null,
            120m,
            500
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentMealTypes_ShouldNotBeEqual()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(25).Date;

        // Act
        var dto1 = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Lunch,
            null,
            null,
            120m,
            500
        );

        var dto2 = new CreateMenuDto(
            cafeteriaId,
            date,
            MealType.Dinner,
            null,
            null,
            120m,
            500
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentJsonValues_ShouldNotBeEqual()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(30).Date;
        var mealType = MealType.Lunch;

        // Act
        var dto1 = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            "[\"Item1\"]",
            null,
            120m,
            500,
            false
        );

        var dto2 = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            "[\"Item2\"]",
            null,
            120m,
            500,
            false
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentIsPublishedValues_ShouldNotBeEqual()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(18).Date;
        var mealType = MealType.Dinner;

        // Act
        var dto1 = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            null,
            null,
            120m,
            500,
            false // IsPublished
        );

        var dto2 = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            null,
            null,
            120m,
            500,
            true // IsPublished - different
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullJsonValues_ShouldNotBeEqual()
    {
        // Arrange
        var cafeteriaId = Guid.NewGuid();
        var date = DateTime.UtcNow.AddDays(22).Date;
        var mealType = MealType.Lunch;

        // Act
        var dto1 = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            null, // ItemsJson
            null,
            120m,
            500,
            false
        );

        var dto2 = new CreateMenuDto(
            cafeteriaId,
            date,
            mealType,
            "[\"Soup\"]", // ItemsJson - not null
            null,
            120m,
            500,
            false
        );

        // Assert
        Assert.NotEqual(dto1, dto2);
    }
}

