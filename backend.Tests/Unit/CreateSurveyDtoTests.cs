using SmartCampus.API.Controllers;
using System.Text.Json;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

public class CreateSurveyDtoTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var title = "Event Feedback Survey";
        var description = "Please provide your feedback about the event";
        var schema = new Dictionary<string, object>
        {
            { "type", "object" },
            { "properties", new Dictionary<string, object>
                {
                    { "rating", new Dictionary<string, object> { { "type", "number" }, { "minimum", 1 }, { "maximum", 5 } } },
                    { "comments", new Dictionary<string, object> { { "type", "string" } } }
                }
            }
        };
        var startsAt = DateTime.UtcNow.AddDays(1);
        var endsAt = DateTime.UtcNow.AddDays(7);

        // Act
        var dto = new CreateSurveyDto(
            title,
            description,
            schema,
            startsAt,
            endsAt
        );

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Equal(description, dto.Description);
        Assert.Equal(schema, dto.Schema);
        Assert.Equal(startsAt, dto.StartsAt);
        Assert.Equal(endsAt, dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithNullOptionalParameters_ShouldSetNullsCorrectly()
    {
        // Arrange
        var title = "Simple Survey";
        var schema = new { type = "object" };

        // Act
        var dto = new CreateSurveyDto(
            title,
            null, // Description
            schema,
            null, // StartsAt
            null  // EndsAt
        );

        // Assert
        Assert.Equal(title, dto.Title);
        Assert.Null(dto.Description);
        Assert.Equal(schema, dto.Schema);
        Assert.Null(dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithEmptyTitle_ShouldSetEmptyString()
    {
        // Arrange
        var schema = new Dictionary<string, object> { { "type", "form" } };

        // Act
        var dto = new CreateSurveyDto(
            string.Empty,
            null,
            schema,
            null,
            null
        );

        // Assert
        Assert.Equal(string.Empty, dto.Title);
    }

    [Fact]
    public void Constructor_WithDictionarySchema_ShouldSetSchemaCorrectly()
    {
        // Arrange
        var title = "Survey with Dictionary Schema";
        var schema = new Dictionary<string, object>
        {
            { "title", "Feedback Form" },
            { "fields", new List<object>
                {
                    new Dictionary<string, object> { { "name", "rating" }, { "type", "number" } },
                    new Dictionary<string, object> { { "name", "feedback" }, { "type", "text" } }
                }
            }
        };

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            null,
            null
        );

        // Assert
        Assert.Equal(schema, dto.Schema);
        Assert.IsType<Dictionary<string, object>>(dto.Schema);
    }

    [Fact]
    public void Constructor_WithAnonymousObjectSchema_ShouldSetSchemaCorrectly()
    {
        // Arrange
        var title = "Survey with Anonymous Schema";
        var schema = new
        {
            type = "survey",
            questions = new[]
            {
                new { id = 1, text = "How satisfied are you?", type = "rating" },
                new { id = 2, text = "Any comments?", type = "text" }
            }
        };

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            null,
            null
        );

        // Assert
        Assert.Equal(schema, dto.Schema);
    }

    [Fact]
    public void Constructor_WithJsonElementSchema_ShouldSetSchemaCorrectly()
    {
        // Arrange
        var title = "Survey with JSON Schema";
        var jsonString = "{\"type\":\"object\",\"properties\":{\"rating\":{\"type\":\"number\"}}}";
        var schema = JsonSerializer.Deserialize<JsonElement>(jsonString);

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            null,
            null
        );

        // Assert
        Assert.Equal(schema, dto.Schema);
        Assert.IsType<JsonElement>(dto.Schema);
    }

    [Fact]
    public void Constructor_WithListSchema_ShouldSetSchemaCorrectly()
    {
        // Arrange
        var title = "Survey with List Schema";
        var schema = new List<object>
        {
            new Dictionary<string, object> { { "question", "Q1" }, { "type", "text" } },
            new Dictionary<string, object> { { "question", "Q2" }, { "type", "choice" } }
        };

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            null,
            null
        );

        // Assert
        Assert.Equal(schema, dto.Schema);
        Assert.IsType<List<object>>(dto.Schema);
    }

    [Fact]
    public void Constructor_WithFutureDates_ShouldSetDatesCorrectly()
    {
        // Arrange
        var title = "Future Survey";
        var schema = new { type = "form" };
        var startsAt = DateTime.UtcNow.AddDays(5);
        var endsAt = DateTime.UtcNow.AddDays(30);

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            startsAt,
            endsAt
        );

        // Assert
        Assert.Equal(startsAt, dto.StartsAt);
        Assert.Equal(endsAt, dto.EndsAt);
        Assert.True(dto.StartsAt > DateTime.UtcNow);
        Assert.True(dto.EndsAt > DateTime.UtcNow);
        Assert.True(dto.EndsAt > dto.StartsAt);
    }

    [Fact]
    public void Constructor_WithPastDates_ShouldStillSetDatesCorrectly()
    {
        // Arrange
        var title = "Past Survey";
        var schema = new { type = "form" };
        var startsAt = DateTime.UtcNow.AddDays(-10);
        var endsAt = DateTime.UtcNow.AddDays(-5);

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            startsAt,
            endsAt
        );

        // Assert
        Assert.Equal(startsAt, dto.StartsAt);
        Assert.Equal(endsAt, dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithOnlyStartsAt_ShouldSetStartsAtOnly()
    {
        // Arrange
        var title = "Survey with Start Date";
        var schema = new { type = "survey" };
        var startsAt = DateTime.UtcNow.AddDays(3);

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            startsAt,
            null // EndsAt
        );

        // Assert
        Assert.Equal(startsAt, dto.StartsAt);
        Assert.Null(dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithOnlyEndsAt_ShouldSetEndsAtOnly()
    {
        // Arrange
        var title = "Survey with End Date";
        var schema = new { type = "survey" };
        var endsAt = DateTime.UtcNow.AddDays(10);

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            null, // StartsAt
            endsAt
        );

        // Assert
        Assert.Null(dto.StartsAt);
        Assert.Equal(endsAt, dto.EndsAt);
    }

    [Fact]
    public void Constructor_WithLongTitle_ShouldSetTitleCorrectly()
    {
        // Arrange
        var longTitle = new string('A', 500);
        var schema = new { type = "form" };

        // Act
        var dto = new CreateSurveyDto(
            longTitle,
            null,
            schema,
            null,
            null
        );

        // Assert
        Assert.Equal(longTitle, dto.Title);
        Assert.Equal(500, dto.Title.Length);
    }

    [Fact]
    public void Constructor_WithLongDescription_ShouldSetDescriptionCorrectly()
    {
        // Arrange
        var title = "Survey";
        var longDescription = new string('B', 2000);
        var schema = new { type = "form" };

        // Act
        var dto = new CreateSurveyDto(
            title,
            longDescription,
            schema,
            null,
            null
        );

        // Assert
        Assert.Equal(longDescription, dto.Description);
        Assert.Equal(2000, dto.Description?.Length);
    }

    [Fact]
    public void Constructor_WithComplexNestedSchema_ShouldSetSchemaCorrectly()
    {
        // Arrange
        var title = "Complex Survey";
        var schema = new Dictionary<string, object>
        {
            { "version", "1.0" },
            { "sections", new List<object>
                {
                    new Dictionary<string, object>
                    {
                        { "id", "section1" },
                        { "title", "Personal Info" },
                        { "questions", new List<object>
                            {
                                new Dictionary<string, object> { { "id", "q1" }, { "text", "Name?" }, { "required", true } },
                                new Dictionary<string, object> { { "id", "q2" }, { "text", "Email?" }, { "type", "email" } }
                            }
                        }
                    }
                }
            }
        };

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            null,
            null
        );

        // Assert
        Assert.Equal(schema, dto.Schema);
        var schemaDict = Assert.IsType<Dictionary<string, object>>(dto.Schema);
        Assert.True(schemaDict.ContainsKey("version"));
        Assert.True(schemaDict.ContainsKey("sections"));
    }

    [Fact]
    public void RecordEquality_WithSameValues_ShouldBeEqual()
    {
        // Arrange
        var title = "Equal Survey";
        var description = "Same description";
        var schema = new { type = "form" };
        var startsAt = DateTime.UtcNow.AddDays(5);
        var endsAt = DateTime.UtcNow.AddDays(10);

        // Act
        var dto1 = new CreateSurveyDto(title, description, schema, startsAt, endsAt);
        var dto2 = new CreateSurveyDto(title, description, schema, startsAt, endsAt);

        // Assert
        Assert.Equal(dto1, dto2);
        Assert.True(dto1 == dto2);
        Assert.False(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentTitles_ShouldNotBeEqual()
    {
        // Arrange
        var schema = new { type = "form" };
        var startsAt = DateTime.UtcNow.AddDays(5);
        var endsAt = DateTime.UtcNow.AddDays(10);

        // Act
        var dto1 = new CreateSurveyDto("Survey 1", null, schema, startsAt, endsAt);
        var dto2 = new CreateSurveyDto("Survey 2", null, schema, startsAt, endsAt);

        // Assert
        Assert.NotEqual(dto1, dto2);
        Assert.False(dto1 == dto2);
        Assert.True(dto1 != dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentDescriptions_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Same Title";
        var schema = new { type = "form" };

        // Act
        var dto1 = new CreateSurveyDto(title, "Description 1", schema, null, null);
        var dto2 = new CreateSurveyDto(title, "Description 2", schema, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullDescription_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Same Title";
        var schema = new { type = "form" };

        // Act
        var dto1 = new CreateSurveyDto(title, null, schema, null, null);
        var dto2 = new CreateSurveyDto(title, "Some description", schema, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentSchemas_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Same Title";
        var schema1 = new { type = "form", version = 1 };
        var schema2 = new { type = "form", version = 2 };

        // Act
        var dto1 = new CreateSurveyDto(title, null, schema1, null, null);
        var dto2 = new CreateSurveyDto(title, null, schema2, null, null);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentStartsAt_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Same Title";
        var schema = new { type = "form" };
        var endsAt = DateTime.UtcNow.AddDays(10);

        // Act
        var dto1 = new CreateSurveyDto(title, null, schema, DateTime.UtcNow.AddDays(5), endsAt);
        var dto2 = new CreateSurveyDto(title, null, schema, DateTime.UtcNow.AddDays(6), endsAt);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithDifferentEndsAt_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Same Title";
        var schema = new { type = "form" };
        var startsAt = DateTime.UtcNow.AddDays(5);

        // Act
        var dto1 = new CreateSurveyDto(title, null, schema, startsAt, DateTime.UtcNow.AddDays(10));
        var dto2 = new CreateSurveyDto(title, null, schema, startsAt, DateTime.UtcNow.AddDays(15));

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullStartsAt_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Same Title";
        var schema = new { type = "form" };
        var endsAt = DateTime.UtcNow.AddDays(10);

        // Act
        var dto1 = new CreateSurveyDto(title, null, schema, null, endsAt);
        var dto2 = new CreateSurveyDto(title, null, schema, DateTime.UtcNow.AddDays(5), endsAt);

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void RecordEquality_WithNullAndNotNullEndsAt_ShouldNotBeEqual()
    {
        // Arrange
        var title = "Same Title";
        var schema = new { type = "form" };
        var startsAt = DateTime.UtcNow.AddDays(5);

        // Act
        var dto1 = new CreateSurveyDto(title, null, schema, startsAt, null);
        var dto2 = new CreateSurveyDto(title, null, schema, startsAt, DateTime.UtcNow.AddDays(10));

        // Assert
        Assert.NotEqual(dto1, dto2);
    }

    [Fact]
    public void Constructor_WithSameDateTimeForStartsAndEnds_ShouldSetBothCorrectly()
    {
        // Arrange
        var title = "One Day Survey";
        var schema = new { type = "form" };
        var sameDateTime = DateTime.UtcNow.AddDays(5);

        // Act
        var dto = new CreateSurveyDto(
            title,
            null,
            schema,
            sameDateTime,
            sameDateTime
        );

        // Assert
        Assert.Equal(sameDateTime, dto.StartsAt);
        Assert.Equal(sameDateTime, dto.EndsAt);
        Assert.Equal(dto.StartsAt, dto.EndsAt);
    }
}

