using SmartCampus.API.Models;
using SmartCampus.API.DTOs;

namespace SmartCampus.API.Tests.Helpers;

/// <summary>
/// Test verileri oluşturmak için helper metodlar
/// </summary>
public static class TestHelpers
{
    /// <summary>
    /// Test kullanıcısı oluşturur
    /// </summary>
    public static User CreateTestUser(
        string email = "test@example.com",
        string password = "Test1234!",
        string role = "Student",
        bool isEmailVerified = true)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            FullName = "Test User",
            Role = role,
            IsEmailVerified = isEmailVerified,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Register request oluşturur
    /// </summary>
    public static RegisterRequest CreateRegisterRequest(
        string email = "newuser@example.com",
        string password = "Test1234!",
        string fullName = "New User",
        string role = "Student")
    {
        return new RegisterRequest
        {
            Email = email,
            Password = password,
            FullName = fullName,
            Role = role
        };
    }

    /// <summary>
    /// Login request oluşturur
    /// </summary>
    public static LoginRequest CreateLoginRequest(
        string email = "test@example.com",
        string password = "Test1234!")
    {
        return new LoginRequest
        {
            Email = email,
            Password = password
        };
    }

    /// <summary>
    /// Department oluşturur
    /// </summary>
    public static Department CreateTestDepartment(
        string name = "Computer Science",
        string code = "CS")
    {
        return new Department
        {
            Id = Guid.NewGuid(),
            Name = name,
            Code = code,
            Faculty = "Engineering"
        };
    }
}

