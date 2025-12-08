using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Models;

namespace SmartCampus.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Role).HasConversion<string>();
        });

        // Student configuration
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.StudentNumber).IsUnique();
            entity.HasIndex(e => e.UserId).IsUnique();

            entity.HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Faculty configuration
        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasIndex(e => e.EmployeeNumber).IsUnique();
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.Property(e => e.Title).HasConversion<string>();

            entity.HasOne(f => f.User)
                .WithOne(u => u.Faculty)
                .HasForeignKey<Faculty>(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(f => f.Department)
                .WithMany(d => d.FacultyMembers)
                .HasForeignKey(f => f.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Department configuration
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
        });

        // RefreshToken configuration
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasIndex(e => e.Token).IsUnique();

            entity.HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // EmailVerificationToken configuration
        modelBuilder.Entity<EmailVerificationToken>(entity =>
        {
            entity.HasIndex(e => e.Token).IsUnique();

            entity.HasOne(e => e.User)
                .WithMany(u => u.EmailVerificationTokens)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false); // UserId nullable - email doğrulanmadan önce kullanıcı yok
        });

        // PasswordResetToken configuration
        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.HasIndex(e => e.Token).IsUnique();

            entity.HasOne(p => p.User)
                .WithMany(u => u.PasswordResetTokens)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Departments
        var csDepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var eeDepartmentId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var meDepartmentId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        modelBuilder.Entity<Department>().HasData(
            new Department
            {
                Id = csDepartmentId,
                Name = "Bilgisayar Mühendisliği",
                Code = "BM",
                Faculty = "Mühendislik Fakültesi",
                Description = "Bilgisayar Mühendisliği Bölümü",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Department
            {
                Id = eeDepartmentId,
                Name = "Elektrik-Elektronik Mühendisliği",
                Code = "EEM",
                Faculty = "Mühendislik Fakültesi",
                Description = "Elektrik-Elektronik Mühendisliği Bölümü",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Department
            {
                Id = meDepartmentId,
                Name = "Makine Mühendisliği",
                Code = "MM",
                Faculty = "Mühendislik Fakültesi",
                Description = "Makine Mühendisliği Bölümü",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Admin User
        var adminUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminUserId,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@smartcampus.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = UserRole.Admin,
                IsEmailVerified = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Faculty Users
        var faculty1UserId = Guid.Parse("f1111111-1111-1111-1111-111111111111");
        var faculty2UserId = Guid.Parse("f2222222-2222-2222-2222-222222222222");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = faculty1UserId,
                FirstName = "Mehmet",
                LastName = "Sevri",
                Email = "mehmet.sevri@smartcampus.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Faculty123!"),
                Role = UserRole.Faculty,
                IsEmailVerified = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = faculty2UserId,
                FirstName = "Ayşe",
                LastName = "Yılmaz",
                Email = "ayse.yilmaz@smartcampus.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Faculty123!"),
                Role = UserRole.Faculty,
                IsEmailVerified = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Faculty>().HasData(
            new Faculty
            {
                Id = Guid.Parse("fa111111-1111-1111-1111-111111111111"),
                UserId = faculty1UserId,
                EmployeeNumber = "F001",
                DepartmentId = csDepartmentId,
                Title = AcademicTitle.AssociateProfessor,
                OfficeLocation = "A-101",
                OfficeHours = "Pazartesi 10:00-12:00, Çarşamba 14:00-16:00",
                Specialization = "Web Programlama, Yazılım Mühendisliği",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Faculty
            {
                Id = Guid.Parse("fa222222-2222-2222-2222-222222222222"),
                UserId = faculty2UserId,
                EmployeeNumber = "F002",
                DepartmentId = eeDepartmentId,
                Title = AcademicTitle.Professor,
                OfficeLocation = "B-205",
                OfficeHours = "Salı 09:00-11:00, Perşembe 13:00-15:00",
                Specialization = "Elektronik, Sinyal İşleme",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Student Users
        var student1UserId = Guid.Parse("c1111111-1111-1111-1111-111111111111");
        var student2UserId = Guid.Parse("c2222222-2222-2222-2222-222222222222");
        var student3UserId = Guid.Parse("c3333333-3333-3333-3333-333333333333");
        var student4UserId = Guid.Parse("c4444444-4444-4444-4444-444444444444");
        var student5UserId = Guid.Parse("c5555555-5555-5555-5555-555555555555");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = student1UserId,
                FirstName = "Zeki",
                LastName = "Erdem",
                Email = "zeki.erdem@smartcampus.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student123!"),
                Role = UserRole.Student,
                IsEmailVerified = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = student2UserId,
                FirstName = "Mert",
                LastName = "Abdullahoğlu",
                Email = "mert.abdullahoglu@smartcampus.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student123!"),
                Role = UserRole.Student,
                IsEmailVerified = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = student3UserId,
                FirstName = "Sena",
                LastName = "Kamiloğlu",
                Email = "sena.kamiloglu@smartcampus.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student123!"),
                Role = UserRole.Student,
                IsEmailVerified = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = student4UserId,
                FirstName = "Şevval",
                LastName = "Asi",
                Email = "sevval.asi@smartcampus.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student123!"),
                Role = UserRole.Student,
                IsEmailVerified = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new User
            {
                Id = student5UserId,
                FirstName = "Ali",
                LastName = "Veli",
                Email = "ali.veli@smartcampus.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student123!"),
                Role = UserRole.Student,
                IsEmailVerified = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = Guid.Parse("d1111111-1111-1111-1111-111111111111"),
                UserId = student1UserId,
                StudentNumber = "2021001",
                DepartmentId = csDepartmentId,
                GPA = 3.50m,
                CGPA = 3.45m,
                CurrentSemester = 7,
                EnrollmentYear = 2021,
                IsScholarship = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Student
            {
                Id = Guid.Parse("d2222222-2222-2222-2222-222222222222"),
                UserId = student2UserId,
                StudentNumber = "2021002",
                DepartmentId = csDepartmentId,
                GPA = 3.20m,
                CGPA = 3.15m,
                CurrentSemester = 7,
                EnrollmentYear = 2021,
                IsScholarship = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Student
            {
                Id = Guid.Parse("d3333333-3333-3333-3333-333333333333"),
                UserId = student3UserId,
                StudentNumber = "2021003",
                DepartmentId = csDepartmentId,
                GPA = 3.80m,
                CGPA = 3.75m,
                CurrentSemester = 7,
                EnrollmentYear = 2021,
                IsScholarship = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Student
            {
                Id = Guid.Parse("d4444444-4444-4444-4444-444444444444"),
                UserId = student4UserId,
                StudentNumber = "2021004",
                DepartmentId = csDepartmentId,
                GPA = 3.60m,
                CGPA = 3.55m,
                CurrentSemester = 7,
                EnrollmentYear = 2021,
                IsScholarship = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Student
            {
                Id = Guid.Parse("d5555555-5555-5555-5555-555555555555"),
                UserId = student5UserId,
                StudentNumber = "2022001",
                DepartmentId = eeDepartmentId,
                GPA = 2.90m,
                CGPA = 2.85m,
                CurrentSemester = 5,
                EnrollmentYear = 2022,
                IsScholarship = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
