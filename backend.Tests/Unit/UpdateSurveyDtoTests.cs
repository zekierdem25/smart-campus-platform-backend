using SmartCampus.API.Controllers;
using System.Text.Json;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class UpdateSurveyDtoTests
{
    [Fact]
    public void Constructor_WithAllNullValues_ShouldSetAllPropertiesToNull()
    {
        // Act
        var dto = new UpdateSurveyDto(null, null, null, null, null, null);

        // Assert
        Assert.Null(dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Schema);
        Assert.Null(dto.IsActive);
        Assert.Null(dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithAllValues_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var title = "Updated Survey Title";
        var description = "Updated survey description";
        var schema = new Dictionary<string, object>
        {
            { "type", "object" },
            { "properties", new Dictionary<string, object>
                {
                    { "rating", new Dictionary<string, object> { { "type", "number" } } }
                }
            }
        };
        var isActive = true;
        var startsAt = DateTime.UtcNow.AddDays(1);
        var endsAt = DateTime.UtcNow.AddDays(7);

        // Act
        var dto = new UpdateSurveyDto(title, description, schema, isActive, startsAt, endsAt);

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Equal(description, dto.Description);
        Assert.Equal(schema, dto.Schema);
        Assert.Equal(isActive, dto.IsActive);
        Assert.Equal(startsAt, dto.StartsAt);
        Assert.Equal(endsAt, dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithPartialValues_ShouldSetOnlyProvidedProperties()
    {
        // Arrange
        var title = "Updated Title";
        var isActive = false;

        // Act
        var dto = new UpdateSurveyDto(title, null, null, isActive, null, null);

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Schema);
        Assert.Equal(isActive, dto.IsActive);
        Assert.Null(dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithOnlyTitle_ShouldSetOnlyTitle()
    {
        // Arrange
        var title = "New Title";

        // Act
        var dto = new UpdateSurveyDto(title, null, null, null, null, null);

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Schema);
        Assert.Null(dto.IsActive);
        Assert.Null(dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithOnlyDescription_ShouldSetOnlyDescription()
    {
        // Arrange
        var description = "New Description";

        // Act
        var dto = new UpdateSurveyDto(null, description, null, null, null, null);

        // Assert
        Assert.Null(dto.Title);
        Assert.Equal(description, dto.Description);
        Assert.Null(dto.Schema);
        Assert.Null(dto.IsActive);
        Assert.Null(dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithOnlySchema_ShouldSetOnlySchema()
    {
        // Arrange
        var schema = new { type = "object" };

        // Act
        var dto = new UpdateSurveyDto(null, null, schema, null, null, null);

        // Assert
        Assert.Null(dto.Title);
        Assert.Null(dto.Description);
        Assert.Equal(schema, dto.Schema);
        Assert.Null(dto.IsActive);
        Assert.Null(dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithOnlyIsActive_ShouldSetOnlyIsActive()
    {
        // Arrange
        var isActive = true;

        // Act
        var dto = new UpdateSurveyDto(null, null, null, isActive, null, null);

        // Assert
        Assert.Null(dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Schema);
        Assert.Equal(isActive, dto.IsActive);
        Assert.Null(dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithBooleanIsActive_ShouldSetCorrectly()
    {
        // Act
        var dto1 = new UpdateSurveyDto(null, null, null, true, null, null);
        var dto2 = new UpdateSurveyDto(null, null, null, false, null, null);

        // Assert
        Assert.True(dto1.IsActive);
        Assert.False(dto2.IsActive);
    }

    [Fact]
    public void Constructor_WithDateTimeValues_ShouldSetCorrectly()
    {
        // Arrange
        var startsAt = DateTime.UtcNow.AddDays(1);
        var endsAt = DateTime.UtcNow.AddDays(7);

        // Act
        var dto = new UpdateSurveyDto(null, null, null, null, startsAt, endsAt);

        // Assert
        Assert.Equal(startsAt, dto.StartsAt);
        Assert.Equal(endsAt, dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithPastAndFutureDates_ShouldSetCorrectly()
    {
        // Arrange
        var pastStartsAt = DateTime.UtcNow.AddDays(-30);
        var pastEndsAt = DateTime.UtcNow.AddDays(-20);
        var futureStartsAt = DateTime.UtcNow.AddDays(10);
        var futureEndsAt = DateTime.UtcNow.AddDays(20);

        // Act
        var dto1 = new UpdateSurveyDto(null, null, null, null, pastStartsAt, pastEndsAt);
        var dto2 = new UpdateSurveyDto(null, null, null, null, futureStartsAt, futureEndsAt);

        // Assert
        Assert.Equal(pastStartsAt, dto1.StartsAt);
        Assert.Equal(pastEndsAt, dto1.EndsAt);
        Assert.Equal(futureStartsAt, dto2.StartsAt);
        Assert.Equal(futureEndsAt, dto2.EndsAt);
    }

    [Fact]
    public void Constructor_WithOnlyStartsAt_ShouldSetOnlyStartsAt()
    {
        // Arrange
        var startsAt = DateTime.UtcNow.AddDays(5);

        // Act
        var dto = new UpdateSurveyDto(null, null, null, null, startsAt, null);

        // Assert
        Assert.Null(dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Schema);
        Assert.Null(dto.IsActive);
        Assert.Equal(startsAt, dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithOnlyEndsAt_ShouldSetOnlyEndsAt()
    {
        // Arrange
        var endsAt = DateTime.UtcNow.AddDays(10);

        // Act
        var dto = new UpdateSurveyDto(null, null, null, null, null, endsAt);

        // Assert
        Assert.Null(dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Schema);
        Assert.Null(dto.IsActive);
        Assert.Null(dto.StartsAt);
        Assert.Equal(endsAt, dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithEmptyStrings_ShouldSetEmptyStrings()
    {
        // Act
        var dto = new UpdateSurveyDto("", "", null, null, null, null);

        // Assert
        Assert.Equal("", dto.Title);
        Assert.Equal("", dto.Description);
        Assert.Null(dto.Schema);
    }

    [Fact]
    public void Constructor_WithLongStrings_ShouldSetCorrectly()
    {
        // Arrange
        var longTitle = new string('A', 1000);
        var longDescription = new string('B', 2000);

        // Act
        var dto = new UpdateSurveyDto(longTitle, longDescription, null, null, null, null);

        // Assert
        Assert.Equal(longTitle, dto.Title);
        Assert.Equal(longDescription, dto.Description);
    }

    [Fact]
    public void Constructor_WithDifferentSchemaTypes_ShouldSetCorrectly()
    {
        // Arrange
        var dictSchema = new Dictionary<string, object> { { "type", "object" } };
        var anonymousSchema = new { type = "object", properties = new { } };
        var jsonElementSchema = JsonSerializer.Deserialize<JsonElement>("{\"type\":\"object\"}");

        // Act
        var dto1 = new UpdateSurveyDto(null, null, dictSchema, null, null, null);
        var dto2 = new UpdateSurveyDto(null, null, anonymousSchema, null, null, null);
        var dto3 = new UpdateSurveyDto(null, null, jsonElementSchema, null, null, null);

        // Assert
        Assert.Equal(dictSchema, dto1.Schema);
        Assert.Equal(anonymousSchema, dto2.Schema);
        Assert.Equal(jsonElementSchema, dto3.Schema);
    }

    [Fact]
    public void Constructor_WithComplexSchema_ShouldSetCorrectly()
    {
        // Arrange
        var complexSchema = new Dictionary<string, object>
        {
            { "type", "object" },
            { "properties", new Dictionary<string, object>
                {
                    { "question1", new Dictionary<string, object>
                        {
                            { "type", "string" },
                            { "required", true }
                        }
                    },
                    { "question2", new Dictionary<string, object>
                        {
                            { "type", "number" },
                            { "minimum", 1 },
                            { "maximum", 10 }
                        }
                    }
                }
            }
        };

        // Act
        var dto = new UpdateSurveyDto(null, null, complexSchema, null, null, null);

        // Assert
        Assert.Equal(complexSchema, dto.Schema);
    }

    [Fact]
    public void Constructor_WithMixedNullAndNonNullValues_ShouldSetCorrectly()
    {
        // Arrange
        var title = "Test Survey";
        var isActive = true;
        var startsAt = DateTime.UtcNow.AddDays(1);

        // Act
        var dto = new UpdateSurveyDto(title, null, null, isActive, startsAt, null);

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Null(dto.Schema);
        Assert.Equal(isActive, dto.IsActive);
        Assert.Equal(startsAt, dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Equality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var title = "Test Survey";
        var description = "Test Description";
        var schema = new Dictionary<string, object> { { "type", "object" } };
        var isActive = true;
        var startsAt = DateTime.UtcNow.AddDays(1);
        var endsAt = DateTime.UtcNow.AddDays(7);

        // Act
        var dto1 = new UpdateSurveyDto(title, description, schema, isActive, startsAt, endsAt);
        var dto2 = new UpdateSurveyDto(title, description, schema, isActive, startsAt, endsAt);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithAllNullValues_ShouldBeEqual()
    {
        // Act
        var dto1 = new UpdateSurveyDto(null, null, null, null, null, null);
        var dto2 = new UpdateSurveyDto(null, null, null, null, null, null);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentTitle_ShouldNotBeEqual()
    {
        // Arrange
        var description = "Test Description";

        // Act
        var dto1 = new UpdateSurveyDto("Title1", description, null, null, null, null);
        var dto2 = new UpdateSurveyDto("Title2", description, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentDescription_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Survey";

        // Act
        var dto1 = new UpdateSurveyDto(title, "Description1", null, null, null, null);
        var dto2 = new UpdateSurveyDto(title, "Description2", null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentSchema_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Survey";
        var schema1 = new Dictionary<string, object> { { "type", "object" } };
        var schema2 = new Dictionary<string, object> { { "type", "array" } };

        // Act
        var dto1 = new UpdateSurveyDto(title, null, schema1, null, null, null);
        var dto2 = new UpdateSurveyDto(title, null, schema2, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentIsActive_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Survey";

        // Act
        var dto1 = new UpdateSurveyDto(title, null, null, true, null, null);
        var dto2 = new UpdateSurveyDto(title, null, null, false, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentStartsAt_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Survey";
        var startsAt1 = DateTime.UtcNow.AddDays(1);
        var startsAt2 = DateTime.UtcNow.AddDays(2);

        // Act
        var dto1 = new UpdateSurveyDto(title, null, null, null, startsAt1, null);
        var dto2 = new UpdateSurveyDto(title, null, null, null, startsAt2, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithDifferentEndsAt_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Test Survey";
        var endsAt1 = DateTime.UtcNow.AddDays(7);
        var endsAt2 = DateTime.UtcNow.AddDays(8);

        // Act
        var dto1 = new UpdateSurveyDto(title, null, null, null, null, endsAt1);
        var dto2 = new UpdateSurveyDto(title, null, null, null, null, endsAt2);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullTitle_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateSurveyDto(null, null, null, null, null, null);
        var dto2 = new UpdateSurveyDto("Title", null, null, null, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullIsActive_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateSurveyDto(null, null, null, null, null, null);
        var dto2 = new UpdateSurveyDto(null, null, null, true, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullStartsAt_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateSurveyDto(null, null, null, null, null, null);
        var dto2 = new UpdateSurveyDto(null, null, null, null, DateTime.UtcNow, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Equality_WithNullVsNonNullEndsAt_ShouldNotBeEqual()
    {
        // Act
        var dto1 = new UpdateSurveyDto(null, null, null, null, null, null);
        var dto2 = new UpdateSurveyDto(null, null, null, null, null, DateTime.UtcNow);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void Constructor_WithSameStartsAtAndEndsAt_ShouldSetCorrectly()
    {
        // Arrange
        var dateTime = DateTime.UtcNow.AddDays(5);

        // Act
        var dto = new UpdateSurveyDto(null, null, null, null, dateTime, dateTime);

        // Assert
        Assert.Equal(dateTime, dto.StartsAt);
        Assert.Equal(dateTime, dto.EndsAt);
    }
}

