using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCampus.API.DTOs;
using SmartCampus.API.Services;
using SmartCampus.API.Tests.Helpers;

namespace SmartCampus.API.Tests.Unit;

public class UserServiceTests
{
    private readonly Mock<ILogger<UserService>> _mockLogger;
    private readonly Mock<IWebHostEnvironment> _mockEnvironment;
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

    public UserServiceTests()
    {
        _mockLogger = MockServices.CreateMockLogger<UserService>();
        _mockEnvironment = new Mock<IWebHostEnvironment>();
        _mockEnvironment.Setup(e => e.ContentRootPath).Returns(Path.GetTempPath());
        _configuration = MockServices.CreateMockConfiguration();
    }

    private UserService CreateUserService(SmartCampus.API.Data.ApplicationDbContext context)
    {
        return new UserService(
            context,
            _mockLogger.Object,
            _configuration,
            _mockEnvironment.Object
        );
    }

    #region GetCurrentUser Tests

    [Fact]
    public async Task GetCurrentUserAsync_WithValidUserId_ShouldReturnUser()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");

        // Act
        var result = await userService.GetCurrentUserAsync(userId);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("student@test.edu", result.Data.Email);
    }

    [Fact]
    public async Task GetCurrentUserAsync_WithInvalidUserId_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var invalidUserId = Guid.NewGuid();

        // Act
        var result = await userService.GetCurrentUserAsync(invalidUserId);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("bulunamadı", result.Message.ToLower());
    }

    [Fact]
    public async Task GetCurrentUserAsync_WithStudentUser_ShouldIncludeStudentInfo()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");

        // Act
        var result = await userService.GetCurrentUserAsync(userId);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data?.StudentInfo);
        Assert.Equal("2021001", result.Data.StudentInfo.StudentNumber);
    }

    [Fact]
    public async Task GetCurrentUserAsync_WithFacultyUser_ShouldIncludeFacultyInfo()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("f1111111-1111-1111-1111-111111111111");

        // Act
        var result = await userService.GetCurrentUserAsync(userId);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data?.FacultyInfo);
        Assert.Equal("F001", result.Data.FacultyInfo.EmployeeNumber);
    }

    #endregion

    #region UpdateProfile Tests

    [Fact]
    public async Task UpdateProfileAsync_WithValidData_ShouldSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");
        var request = new UpdateProfileRequestDto
        {
            FirstName = "Updated",
            LastName = "Name",
            Phone = "5551234567"
        };

        // Act
        var result = await userService.UpdateProfileAsync(userId, request);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Updated", result.Data?.FirstName);
        Assert.Equal("Name", result.Data?.LastName);
        Assert.Equal("5551234567", result.Data?.Phone);
    }

    [Fact]
    public async Task UpdateProfileAsync_WithPartialData_ShouldOnlyUpdateProvidedFields()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");
        var request = new UpdateProfileRequestDto
        {
            FirstName = "OnlyFirstName"
        };

        // Act
        var result = await userService.UpdateProfileAsync(userId, request);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("OnlyFirstName", result.Data?.FirstName);
        Assert.Equal("Student", result.Data?.LastName); // Original value
    }

    [Fact]
    public async Task UpdateProfileAsync_WithInvalidUserId_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var invalidUserId = Guid.NewGuid();
        var request = new UpdateProfileRequestDto
        {
            FirstName = "Test"
        };

        // Act
        var result = await userService.UpdateProfileAsync(invalidUserId, request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("bulunamadı", result.Message.ToLower());
    }

    #endregion

    #region ChangePassword Tests

    [Fact]
    public async Task ChangePasswordAsync_WithValidCurrentPassword_ShouldSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");
        var request = new ChangePasswordRequestDto
        {
            CurrentPassword = "Student123!",
            NewPassword = "NewPassword123!",
            ConfirmPassword = "NewPassword123!"
        };

        // Act
        var result = await userService.ChangePasswordAsync(userId, request);

        // Assert
        Assert.True(result.Success);
        Assert.Contains("başarıyla", result.Message.ToLower());
    }

    [Fact]
    public async Task ChangePasswordAsync_WithWrongCurrentPassword_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");
        var request = new ChangePasswordRequestDto
        {
            CurrentPassword = "WrongPassword!",
            NewPassword = "NewPassword123!",
            ConfirmPassword = "NewPassword123!"
        };

        // Act
        var result = await userService.ChangePasswordAsync(userId, request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("hatalı", result.Message.ToLower());
    }

    [Fact]
    public async Task ChangePasswordAsync_WithSamePassword_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");
        var request = new ChangePasswordRequestDto
        {
            CurrentPassword = "Student123!",
            NewPassword = "Student123!",
            ConfirmPassword = "Student123!"
        };

        // Act
        var result = await userService.ChangePasswordAsync(userId, request);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("aynı olamaz", result.Message.ToLower());
    }

    #endregion

    #region GetUsers Tests

    [Fact]
    public async Task GetUsersAsync_WithDefaultPagination_ShouldReturnUsers()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var request = new UserListRequestDto();

        // Act
        var result = await userService.GetUsersAsync(request);

        // Assert
        Assert.NotNull(result.Users);
        Assert.True(result.Users.Count > 0);
        Assert.True(result.TotalCount > 0);
    }

    [Fact]
    public async Task GetUsersAsync_WithRoleFilter_ShouldFilterByRole()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var request = new UserListRequestDto
        {
            Role = "Admin"
        };

        // Act
        var result = await userService.GetUsersAsync(request);

        // Assert
        Assert.All(result.Users, user => Assert.Equal("Admin", user.Role));
    }

    [Fact]
    public async Task GetUsersAsync_WithSearchTerm_ShouldFilterByNameOrEmail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var request = new UserListRequestDto
        {
            Search = "student@test.edu"
        };

        // Act
        var result = await userService.GetUsersAsync(request);

        // Assert
        Assert.True(result.Users.Count > 0);
        Assert.Contains(result.Users, u => u.Email.Contains("student@test.edu"));
    }

    [Fact]
    public async Task GetUsersAsync_WithPagination_ShouldRespectLimits()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var request = new UserListRequestDto
        {
            Page = 1,
            Limit = 2
        };

        // Act
        var result = await userService.GetUsersAsync(request);

        // Assert
        Assert.True(result.Users.Count <= 2);
        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.Limit);
    }

    #endregion

    #region GetDepartments Tests

    [Fact]
    public async Task GetDepartmentsAsync_ShouldReturnActiveDepartments()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);

        // Act
        var result = await userService.GetDepartmentsAsync();

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Count > 0);
    }

    #endregion

    #region ProfilePicture Tests

    [Fact]
    public async Task UpdateProfilePictureAsync_WithValidFile_ShouldSucceed()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");

        var content = new byte[100];
        new Random().NextBytes(content);
        var stream = new MemoryStream(content);
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("test.jpg");
        mockFile.Setup(f => f.Length).Returns(content.Length);
        mockFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Callback<Stream, CancellationToken>((s, ct) => stream.CopyTo(s))
            .Returns(Task.CompletedTask);

        // Act
        var result = await userService.UpdateProfilePictureAsync(userId, mockFile.Object);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Contains("/uploads/profiles/", result.Data);
    }

    [Fact]
    public async Task UpdateProfilePictureAsync_WithOversizedFile_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");

        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("test.jpg");
        mockFile.Setup(f => f.Length).Returns(10 * 1024 * 1024); // 10MB - too large

        // Act
        var result = await userService.UpdateProfilePictureAsync(userId, mockFile.Object);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("5MB", result.Message);
    }

    [Fact]
    public async Task UpdateProfilePictureAsync_WithInvalidExtension_ShouldFail()
    {
        // Arrange
        var context = await TestDatabaseHelper.CreateSeededContextAsync();
        var userService = CreateUserService(context);
        var userId = Guid.Parse("c1111111-1111-1111-1111-111111111111");

        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("test.exe");
        mockFile.Setup(f => f.Length).Returns(1000);

        // Act
        var result = await userService.UpdateProfilePictureAsync(userId, mockFile.Object);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("JPG", result.Message);
    }

    #endregion
}

