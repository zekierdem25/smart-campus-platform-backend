using System;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.Services;
using System.Text;
using System.Text.Json;

namespace SmartCampus.API.Tests.Unit;

public class ExportServiceTests
{
    private readonly Mock<ILogger<ExportService>> _mockLogger;
    private readonly ExportService _service;

    public ExportServiceTests()
    {
        _mockLogger = new Mock<ILogger<ExportService>>();
        _service = new ExportService(_mockLogger.Object);
    }

    #region ExportAnalyticsToExcelAsync Tests

    [Fact]
    public async Task ExportAnalyticsToExcelAsync_WithValidData_ShouldReturnExcelBytes()
    {
        // Arrange
        var analyticsData = new
        {
            TotalStudents = 100,
            TotalCourses = 50,
            AverageGpa = 3.2
        };

        // Act
        var result = await _service.ExportAnalyticsToExcelAsync(analyticsData, "dashboard");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
        // Excel files start with PK (ZIP signature)
        Assert.Equal((byte)0x50, result[0]); // 'P'
        Assert.Equal((byte)0x4B, result[1]); // 'K'
    }

    [Fact]
    public async Task ExportAnalyticsToExcelAsync_WithComplexData_ShouldReturnExcelBytes()
    {
        // Arrange
        var analyticsData = new
        {
            Dashboard = new
            {
                TotalUsers = 500,
                ActiveUsers = 300
            },
            Performance = new[] { 3.5, 3.7, 3.2, 3.9 }
        };

        // Act
        var result = await _service.ExportAnalyticsToExcelAsync(analyticsData, "academic");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }

    [Fact]
    public async Task ExportAnalyticsToExcelAsync_WithEmptyData_ShouldReturnExcelBytes()
    {
        // Arrange
        var analyticsData = new { };

        // Act
        var result = await _service.ExportAnalyticsToExcelAsync(analyticsData, "empty");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }

    #endregion

    #region ExportAnalyticsToPdfAsync Tests

    [Fact]
    public async Task ExportAnalyticsToPdfAsync_WithValidData_ShouldReturnPdfBytes()
    {
        // Arrange
        var analyticsData = new
        {
            TotalStudents = 100,
            AverageGpa = 3.5
        };

        // Act
        var result = await _service.ExportAnalyticsToPdfAsync(analyticsData, "academic", "Academic Report");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
        // PDF files start with %PDF
        Assert.Equal((byte)0x25, result[0]); // '%'
        Assert.Equal((byte)0x50, result[1]); // 'P'
        Assert.Equal((byte)0x44, result[2]); // 'D'
        Assert.Equal((byte)0x46, result[3]); // 'F'
    }

    [Fact]
    public async Task ExportAnalyticsToPdfAsync_WithLongContent_ShouldHandleMultiplePages()
    {
        // Arrange
        var longData = new
        {
            Students = Enumerable.Range(1, 100).Select(i => new
            {
                Id = i,
                Name = $"Student {i}",
                Gpa = 3.0 + (i % 10) * 0.1
            }).ToArray()
        };

        // Act
        var result = await _service.ExportAnalyticsToPdfAsync(longData, "students", "Student Report");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }

    [Fact]
    public async Task ExportAnalyticsToPdfAsync_WithSpecialCharacters_ShouldHandleCorrectly()
    {
        // Arrange
        var analyticsData = new
        {
            Department = "Bilgisayar Mühendisliği",
            Students = "Öğrenci Sayısı: 150"
        };

        // Act
        var result = await _service.ExportAnalyticsToPdfAsync(analyticsData, "department", "Departman Raporu");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }

    #endregion

    #region ExportAnalyticsToCsvAsync Tests

    [Fact]
    public async Task ExportAnalyticsToCsvAsync_WithValidData_ShouldReturnCsvBytes()
    {
        // Arrange
        var analyticsData = new
        {
            TotalStudents = 100,
            TotalCourses = 50,
            AverageGpa = 3.2
        };

        // Act
        var result = await _service.ExportAnalyticsToCsvAsync(analyticsData, "dashboard");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
        
        var csvContent = System.Text.Encoding.UTF8.GetString(result);
        Assert.Contains("Analytics Report - dashboard", csvContent);
        Assert.Contains("Generated:", csvContent);
    }

    [Fact]
    public async Task ExportAnalyticsToCsvAsync_WithNestedData_ShouldFlattenCorrectly()
    {
        // Arrange
        var analyticsData = new
        {
            Dashboard = new
            {
                Users = 500,
                Courses = 100
            },
            Performance = new
            {
                AverageGpa = 3.5,
                PassRate = 95.5
            }
        };

        // Act
        var result = await _service.ExportAnalyticsToCsvAsync(analyticsData, "complex");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
        
        var csvContent = System.Text.Encoding.UTF8.GetString(result);
        Assert.Contains("Dashboard.Users", csvContent);
        Assert.Contains("Performance.AverageGpa", csvContent);
    }

    [Fact]
    public async Task ExportAnalyticsToCsvAsync_WithArrayData_ShouldHandleArrays()
    {
        // Arrange
        var analyticsData = new
        {
            Grades = new[] { "AA", "BA", "BB", "CB" },
            Counts = new[] { 10, 20, 30, 15 }
        };

        // Act
        var result = await _service.ExportAnalyticsToCsvAsync(analyticsData, "grades");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
        
        var csvContent = System.Text.Encoding.UTF8.GetString(result);
        Assert.Contains("Grades[0]", csvContent);
        Assert.Contains("Counts[0]", csvContent);
    }

    [Fact]
    public async Task ExportAnalyticsToCsvAsync_WithEmptyData_ShouldReturnHeaderOnly()
    {
        // Arrange
        var analyticsData = new { };

        // Act
        var result = await _service.ExportAnalyticsToCsvAsync(analyticsData, "empty");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
        
        var csvContent = System.Text.Encoding.UTF8.GetString(result);
        Assert.Contains("Analytics Report - empty", csvContent);
        Assert.Contains("Generated:", csvContent);
    }

    #endregion

    #region Edge Cases & Error Handling

    [Fact]
    public async Task Export_WithNullReportType_ShouldNotThrow()
    {
        // Arrange
        var data = new { Value = 123 };

        // Act & Assert
        var excelResult = await _service.ExportAnalyticsToExcelAsync(data, null!);
        var pdfResult = await _service.ExportAnalyticsToPdfAsync(data, null!, "Title");
        var csvResult = await _service.ExportAnalyticsToCsvAsync(data, null!);

        Assert.NotNull(excelResult);
        Assert.NotNull(pdfResult);
        Assert.NotNull(csvResult);
    }

    [Fact]
    public async Task Export_WithVeryLargeData_ShouldComplete()
    {
        // Arrange
        var largeData = new
        {
            Records = Enumerable.Range(1, 1000).Select(i => new
            {
                Id = i,
                Value = i * 2.5,
                Description = $"Record {i} with some description text"
            }).ToArray()
        };

        // Act
        var excelTask = _service.ExportAnalyticsToExcelAsync(largeData, "large");
        var pdfTask = _service.ExportAnalyticsToPdfAsync(largeData, "large", "Large Report");
        var csvTask = _service.ExportAnalyticsToCsvAsync(largeData, "large");

        await Task.WhenAll(excelTask, pdfTask, csvTask);

        // Assert
        Assert.True(excelTask.Result.Length > 0);
        Assert.True(pdfTask.Result.Length > 0);
        Assert.True(csvTask.Result.Length > 0);
    }

    #endregion
}
