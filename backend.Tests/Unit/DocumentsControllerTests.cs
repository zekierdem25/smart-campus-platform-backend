using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Controllers;
using SmartCampus.API.Data;
using SmartCampus.API.Models;
using Xunit;

namespace SmartCampus.API.Tests.Unit;

[Collection("PdfGeneration")]
public class DocumentsControllerTests
{
    private readonly ApplicationDbContext _context;

    public DocumentsControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
    }

    [Fact]
    public void Constructor_WithValidContext_ShouldInitializeCorrectly()
    {
        // Act
        var controller = new DocumentsController(_context);

        // Assert
        Assert.NotNull(controller);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange
        ApplicationDbContext? context = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new DocumentsController(context!));
    }

    [Fact]
    public void GenerateCertificatePdf_WithValidStudent_ShouldReturnByteArray()
    {
        // Arrange
        var controller = new DocumentsController(_context);
        var student = new Student
        {
            StudentNumber = "123456789",
            EnrollmentYear = 2023,
            CreatedAt = DateTime.UtcNow,
            CurrentSemester = 3,
            User = new User
            {
                FirstName = "Ali",
                LastName = "Veli",
                IsActive = true
            },
            Department = new Department
            {
                Name = "Computer Engineering"
            }
        };

        var methodInfo = typeof(DocumentsController).GetMethod("GenerateCertificatePdf", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        var result = methodInfo?.Invoke(controller, new object[] { student }) as byte[];

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result!.Length > 100); // Check if it has substantial content
    }
}
