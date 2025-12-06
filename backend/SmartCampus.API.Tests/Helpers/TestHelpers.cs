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
    UserRole role = UserRole.Student,
    bool isEmailVerified = true)
{
    return new User
    {
        Id = Guid.NewGuid(),

        // FullName set edilemediği için FirstName / LastName dolduruyoruz
        FirstName = "Test",
        LastName = "User",

        Email = email,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),

        // Role artık enum tipinde
        Role = role,

        IsEmailVerified = isEmailVerified,
        CreatedAt = DateTime.UtcNow
    };
    }

    /// <summary>
    /// Register request oluşturur
    /// </summary>
    public static RegisterRequestDto CreateRegisterRequest(
    string email = "newuser@example.com",
    string password = "Test1234!",
    string fullName = "New User",
    string role = "Student")
{
    return new RegisterRequestDto
    {
        // FullName parametresini basitçe FirstName'e yazıyoruz
        FirstName = fullName,
        LastName = string.Empty,
        Email = email,
        Password = password,
        ConfirmPassword = password,
        Role = role
    };
    }

    /// <summary>
    /// Login request oluşturur
    /// </summary>
    public static LoginRequestDto CreateLoginRequest(
    string email = "test@example.com",
    string password = "Test1234!")
{
    return new LoginRequestDto
    {
        Email = email,
        Password = password,
        // İstersen RememberMe'yi de set edebilirsin
        RememberMe = false
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

