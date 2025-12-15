using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.Models;

namespace SmartCampus.API.Tests.Helpers;

public static class TestDatabaseHelper
{
    public static ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        return context;
    }

    public static async Task<ApplicationDbContext> CreateSeededContextAsync()
    {
        var context = CreateInMemoryContext();
        await SeedTestDataAsync(context);
        return context;
    }

    public static async Task SeedTestDataAsync(ApplicationDbContext context)
    {
        // Seed Departments
        var department = new Department
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Bilgisayar Mühendisliği",
            Code = "BM",
            Faculty = "Mühendislik Fakültesi",
            IsActive = true
        };
        context.Departments.Add(department);

        // Seed Admin User
        var adminUser = new User
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@test.edu",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
            Role = UserRole.Admin,
            IsEmailVerified = true,
            IsActive = true
        };
        context.Users.Add(adminUser);

        // Seed Student User
        var studentUser = new User
        {
            Id = Guid.Parse("c1111111-1111-1111-1111-111111111111"),
            FirstName = "Test",
            LastName = "Student",
            Email = "student@test.edu",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student123!"),
            Role = UserRole.Student,
            IsEmailVerified = true,
            IsActive = true
        };
        context.Users.Add(studentUser);

        var student = new Student
        {
            Id = Guid.NewGuid(),
            UserId = studentUser.Id,
            StudentNumber = "2021001",
            DepartmentId = department.Id,
            GPA = 3.50m,
            CGPA = 3.45m,
            CurrentSemester = 7,
            EnrollmentYear = 2021
        };
        context.Students.Add(student);

        // Seed Faculty User
        var facultyUser = new User
        {
            Id = Guid.Parse("f1111111-1111-1111-1111-111111111111"),
            FirstName = "Test",
            LastName = "Faculty",
            Email = "faculty@test.edu",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Faculty123!"),
            Role = UserRole.Faculty,
            IsEmailVerified = true,
            IsActive = true
        };
        context.Users.Add(facultyUser);

        var faculty = new Faculty
        {
            Id = Guid.NewGuid(),
            UserId = facultyUser.Id,
            EmployeeNumber = "F001",
            DepartmentId = department.Id,
            Title = AcademicTitle.AssociateProfessor
        };
        context.Faculties.Add(faculty);

        // Seed Unverified User
        var unverifiedUser = new User
        {
            Id = Guid.Parse("d1111111-1111-1111-1111-111111111111"),
            FirstName = "Unverified",
            LastName = "User",
            Email = "unverified@test.edu",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123!"),
            Role = UserRole.Student,
            IsEmailVerified = false,
            IsActive = true
        };
        context.Users.Add(unverifiedUser);

        // Seed Inactive User
        var inactiveUser = new User
        {
            Id = Guid.Parse("e1111111-1111-1111-1111-111111111111"),
            FirstName = "Inactive",
            LastName = "User",
            Email = "inactive@test.edu",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123!"),
            Role = UserRole.Student,
            IsEmailVerified = true,
            IsActive = false
        };
        context.Users.Add(inactiveUser);

        // Seed Direct Login User (skips 2FA)
        var directLoginUser = new User
        {
            Id = Guid.Parse("b1111111-1111-1111-1111-111111111111"),
            FirstName = "Direct",
            LastName = "Login",
            Email = "direct.login@smartcampus.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("DirectLogin123!"),
            Role = UserRole.Student,
            IsEmailVerified = true,
            IsActive = true
        };
        context.Users.Add(directLoginUser);

        await context.SaveChangesAsync();
    }

    public static User CreateTestUser(string email = "test@test.edu", UserRole role = UserRole.Student, bool isVerified = true, bool isActive = true)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "User",
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test123!"),
            Role = role,
            IsEmailVerified = isVerified,
            IsActive = isActive
        };
    }
}

