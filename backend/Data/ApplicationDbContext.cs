using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Models;

namespace SmartCampus.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets - Part 1
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }

    // DbSets - Part 2: Academic Management
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseSection> CourseSections { get; set; }
    public DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }

    // DbSets - Part 2: Attendance System
    public DbSet<AttendanceSession> AttendanceSessions { get; set; }
    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    public DbSet<ExcuseRequest> ExcuseRequests { get; set; }

    // DbSets - Security
    public DbSet<TwoFactorCode> TwoFactorCodes { get; set; }

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

        // ActivityLog configuration
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasIndex(e => new { e.UserId, e.CreatedAt });
            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IpAddress).HasMaxLength(45);
            entity.Property(e => e.UserAgent).HasMaxLength(300);

            entity.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ========== Part 2: Academic Management Configurations ==========

        // Course configuration
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.Code).IsUnique();
            
            entity.HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // CoursePrerequisite configuration (composite key)
        modelBuilder.Entity<CoursePrerequisite>(entity =>
        {
            entity.HasKey(cp => new { cp.CourseId, cp.PrerequisiteCourseId });

            entity.HasOne(cp => cp.Course)
                .WithMany(c => c.Prerequisites)
                .HasForeignKey(cp => cp.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(cp => cp.PrerequisiteCourse)
                .WithMany(c => c.RequiredFor)
                .HasForeignKey(cp => cp.PrerequisiteCourseId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // CourseSection configuration
        modelBuilder.Entity<CourseSection>(entity =>
        {
            entity.HasIndex(e => new { e.CourseId, e.SectionNumber, e.Semester, e.Year }).IsUnique();
            entity.Property(e => e.Semester).HasMaxLength(20);

            entity.HasOne(cs => cs.Course)
                .WithMany(c => c.Sections)
                .HasForeignKey(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(cs => cs.Instructor)
                .WithMany(f => f.TeachingSections)
                .HasForeignKey(cs => cs.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(cs => cs.Classroom)
                .WithMany(c => c.Sections)
                .HasForeignKey(cs => cs.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Enrollment configuration
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasIndex(e => new { e.StudentId, e.SectionId }).IsUnique();
            entity.Property(e => e.Status).HasConversion<string>();

            entity.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Section)
                .WithMany(cs => cs.Enrollments)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Classroom configuration
        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasIndex(e => new { e.Building, e.RoomNumber }).IsUnique();
        });

        // ========== Part 2: Attendance System Configurations ==========

        // AttendanceSession configuration
        modelBuilder.Entity<AttendanceSession>(entity =>
        {
            entity.HasIndex(e => new { e.SectionId, e.Date });
            entity.Property(e => e.Status).HasConversion<string>();

            entity.HasOne(a => a.Section)
                .WithMany(cs => cs.AttendanceSessions)
                .HasForeignKey(a => a.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Instructor)
                .WithMany(f => f.AttendanceSessions)
                .HasForeignKey(a => a.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // AttendanceRecord configuration
        modelBuilder.Entity<AttendanceRecord>(entity =>
        {
            entity.HasIndex(e => new { e.SessionId, e.StudentId }).IsUnique();

            entity.HasOne(ar => ar.Session)
                .WithMany(s => s.Records)
                .HasForeignKey(ar => ar.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ar => ar.Student)
                .WithMany(s => s.AttendanceRecords)
                .HasForeignKey(ar => ar.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ExcuseRequest configuration
        modelBuilder.Entity<ExcuseRequest>(entity =>
        {
            entity.HasIndex(e => new { e.StudentId, e.SessionId }).IsUnique();
            entity.Property(e => e.Status).HasConversion<string>();

            entity.HasOne(er => er.Student)
                .WithMany(s => s.ExcuseRequests)
                .HasForeignKey(er => er.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(er => er.Session)
                .WithMany(s => s.ExcuseRequests)
                .HasForeignKey(er => er.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(er => er.Reviewer)
                .WithMany()
                .HasForeignKey(er => er.ReviewedBy)
                .OnDelete(DeleteBehavior.SetNull);
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

        // ========== Part 2: Seed Data ==========
        SeedPart2Data(modelBuilder, csDepartmentId, eeDepartmentId);
    }

    private void SeedPart2Data(ModelBuilder modelBuilder, Guid csDepartmentId, Guid eeDepartmentId)
    {
        var faculty1Id = Guid.Parse("fa111111-1111-1111-1111-111111111111");
        var faculty2Id = Guid.Parse("fa222222-2222-2222-2222-222222222222");

        // Seed Classrooms with real GPS coordinates (example: Istanbul Technical University area)
        var classroom1Id = Guid.Parse("c1a00001-0001-0001-0001-000000000001");
        var classroom2Id = Guid.Parse("c2a00002-0002-0002-0002-000000000002");
        var classroom3Id = Guid.Parse("c3a00003-0003-0003-0003-000000000003");
        var classroom4Id = Guid.Parse("c4a00004-0004-0004-0004-000000000004");
        var classroom5Id = Guid.Parse("c5a00005-0005-0005-0005-000000000005");

        modelBuilder.Entity<Classroom>().HasData(
            new Classroom
            {
                Id = classroom1Id,
                Building = "A Blok",
                RoomNumber = "101",
                Capacity = 50,
                Latitude = 41.1055m,
                Longitude = 29.0250m,
                FeaturesJson = "[\"projector\", \"whiteboard\", \"ac\", \"computer\"]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Classroom
            {
                Id = classroom2Id,
                Building = "A Blok",
                RoomNumber = "102",
                Capacity = 40,
                Latitude = 41.1056m,
                Longitude = 29.0251m,
                FeaturesJson = "[\"projector\", \"whiteboard\"]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Classroom
            {
                Id = classroom3Id,
                Building = "B Blok",
                RoomNumber = "201",
                Capacity = 60,
                Latitude = 41.1060m,
                Longitude = 29.0255m,
                FeaturesJson = "[\"projector\", \"whiteboard\", \"ac\", \"lab_computers\"]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Classroom
            {
                Id = classroom4Id,
                Building = "B Blok",
                RoomNumber = "202",
                Capacity = 35,
                Latitude = 41.1061m,
                Longitude = 29.0256m,
                FeaturesJson = "[\"projector\", \"whiteboard\", \"ac\"]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Classroom
            {
                Id = classroom5Id,
                Building = "C Blok",
                RoomNumber = "Lab-1",
                Capacity = 30,
                Latitude = 41.1065m,
                Longitude = 29.0260m,
                FeaturesJson = "[\"projector\", \"whiteboard\", \"ac\", \"lab_computers\", \"network\"]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Courses
        var course1Id = Guid.Parse("c0a00001-0001-0001-0001-000000000001"); // BM101 - Programlamaya Giriş
        var course2Id = Guid.Parse("c0a00002-0002-0002-0002-000000000002"); // BM201 - Veri Yapıları
        var course3Id = Guid.Parse("c0a00003-0003-0003-0003-000000000003"); // BM301 - Algoritmalar
        var course4Id = Guid.Parse("c0a00004-0004-0004-0004-000000000004"); // BM302 - Veritabanı Sistemleri
        var course5Id = Guid.Parse("c0a00005-0005-0005-0005-000000000005"); // BM401 - Yazılım Mühendisliği
        var course6Id = Guid.Parse("c0a00006-0006-0006-0006-000000000006"); // BM402 - Web Programlama
        var course7Id = Guid.Parse("c0a00007-0007-0007-0007-000000000007"); // EEM101 - Devre Analizi
        var course8Id = Guid.Parse("c0a00008-0008-0008-0008-000000000008"); // EEM201 - Elektronik

        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = course1Id,
                Code = "BM101",
                Name = "Programlamaya Giriş",
                Description = "Temel programlama kavramları, değişkenler, döngüler, fonksiyonlar ve nesne yönelimli programlamaya giriş.",
                Credits = 4,
                ECTS = 6,
                DepartmentId = csDepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Course
            {
                Id = course2Id,
                Code = "BM201",
                Name = "Veri Yapıları",
                Description = "Diziler, bağlı listeler, yığınlar, kuyruklar, ağaçlar ve graf veri yapıları.",
                Credits = 4,
                ECTS = 6,
                DepartmentId = csDepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Course
            {
                Id = course3Id,
                Code = "BM301",
                Name = "Algoritmalar",
                Description = "Algoritma analizi, sıralama ve arama algoritmaları, dinamik programlama, açgözlü algoritmalar.",
                Credits = 3,
                ECTS = 5,
                DepartmentId = csDepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Course
            {
                Id = course4Id,
                Code = "BM302",
                Name = "Veritabanı Sistemleri",
                Description = "İlişkisel veritabanları, SQL, normalizasyon, indeksleme ve transaction yönetimi.",
                Credits = 3,
                ECTS = 5,
                DepartmentId = csDepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Course
            {
                Id = course5Id,
                Code = "BM401",
                Name = "Yazılım Mühendisliği",
                Description = "Yazılım geliştirme süreçleri, tasarım desenleri, test ve kalite güvencesi.",
                Credits = 3,
                ECTS = 5,
                DepartmentId = csDepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Course
            {
                Id = course6Id,
                Code = "BM402",
                Name = "Web Programlama",
                Description = "HTML, CSS, JavaScript, React, Node.js ve modern web teknolojileri.",
                Credits = 3,
                ECTS = 5,
                DepartmentId = csDepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Course
            {
                Id = course7Id,
                Code = "EEM101",
                Name = "Devre Analizi",
                Description = "Temel elektrik devreleri, Kirchhoff yasaları, Thevenin ve Norton teoremleri.",
                Credits = 4,
                ECTS = 6,
                DepartmentId = eeDepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Course
            {
                Id = course8Id,
                Code = "EEM201",
                Name = "Elektronik",
                Description = "Diyotlar, transistörler, opamp'lar ve temel elektronik devreler.",
                Credits = 4,
                ECTS = 6,
                DepartmentId = eeDepartmentId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Course Prerequisites (creates a dependency chain for recursive checking)
        // BM201 requires BM101
        // BM301 requires BM201 (which requires BM101 - recursive test)
        // BM302 requires BM201
        // BM401 requires BM301 and BM302
        // BM402 requires BM101
        // EEM201 requires EEM101
        modelBuilder.Entity<CoursePrerequisite>().HasData(
            new CoursePrerequisite { CourseId = course2Id, PrerequisiteCourseId = course1Id }, // BM201 <- BM101
            new CoursePrerequisite { CourseId = course3Id, PrerequisiteCourseId = course2Id }, // BM301 <- BM201
            new CoursePrerequisite { CourseId = course4Id, PrerequisiteCourseId = course2Id }, // BM302 <- BM201
            new CoursePrerequisite { CourseId = course5Id, PrerequisiteCourseId = course3Id }, // BM401 <- BM301
            new CoursePrerequisite { CourseId = course5Id, PrerequisiteCourseId = course4Id }, // BM401 <- BM302
            new CoursePrerequisite { CourseId = course6Id, PrerequisiteCourseId = course1Id }, // BM402 <- BM101
            new CoursePrerequisite { CourseId = course8Id, PrerequisiteCourseId = course7Id }  // EEM201 <- EEM101
        );

        // Seed Course Sections (Fall 2025)
        var section1Id = Guid.Parse("5ec00001-0001-0001-0001-000000000001");
        var section2Id = Guid.Parse("5ec00002-0002-0002-0002-000000000002");
        var section3Id = Guid.Parse("5ec00003-0003-0003-0003-000000000003");
        var section4Id = Guid.Parse("5ec00004-0004-0004-0004-000000000004");
        var section5Id = Guid.Parse("5ec00005-0005-0005-0005-000000000005");
        var section6Id = Guid.Parse("5ec00006-0006-0006-0006-000000000006");

        modelBuilder.Entity<CourseSection>().HasData(
            new CourseSection
            {
                Id = section1Id,
                CourseId = course1Id,
                SectionNumber = 1,
                Semester = "Fall",
                Year = 2025,
                InstructorId = faculty1Id,
                ClassroomId = classroom1Id,
                Capacity = 50,
                EnrolledCount = 3,
                ScheduleJson = "[{\"day\":\"Monday\",\"startTime\":\"09:00\",\"endTime\":\"10:50\"},{\"day\":\"Wednesday\",\"startTime\":\"09:00\",\"endTime\":\"10:50\"}]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new CourseSection
            {
                Id = section2Id,
                CourseId = course2Id,
                SectionNumber = 1,
                Semester = "Fall",
                Year = 2025,
                InstructorId = faculty1Id,
                ClassroomId = classroom2Id,
                Capacity = 40,
                EnrolledCount = 2,
                ScheduleJson = "[{\"day\":\"Tuesday\",\"startTime\":\"11:00\",\"endTime\":\"12:50\"},{\"day\":\"Thursday\",\"startTime\":\"11:00\",\"endTime\":\"12:50\"}]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new CourseSection
            {
                Id = section3Id,
                CourseId = course3Id,
                SectionNumber = 1,
                Semester = "Fall",
                Year = 2025,
                InstructorId = faculty1Id,
                ClassroomId = classroom3Id,
                Capacity = 35,
                EnrolledCount = 2,
                ScheduleJson = "[{\"day\":\"Monday\",\"startTime\":\"13:00\",\"endTime\":\"14:50\"},{\"day\":\"Wednesday\",\"startTime\":\"13:00\",\"endTime\":\"14:50\"}]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new CourseSection
            {
                Id = section4Id,
                CourseId = course4Id,
                SectionNumber = 1,
                Semester = "Fall",
                Year = 2025,
                InstructorId = faculty1Id,
                ClassroomId = classroom4Id,
                Capacity = 35,
                EnrolledCount = 1,
                ScheduleJson = "[{\"day\":\"Tuesday\",\"startTime\":\"14:00\",\"endTime\":\"15:50\"},{\"day\":\"Thursday\",\"startTime\":\"14:00\",\"endTime\":\"15:50\"}]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new CourseSection
            {
                Id = section5Id,
                CourseId = course6Id,
                SectionNumber = 1,
                Semester = "Fall",
                Year = 2025,
                InstructorId = faculty1Id,
                ClassroomId = classroom5Id,
                Capacity = 30,
                EnrolledCount = 2,
                ScheduleJson = "[{\"day\":\"Friday\",\"startTime\":\"10:00\",\"endTime\":\"12:50\"}]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new CourseSection
            {
                Id = section6Id,
                CourseId = course7Id,
                SectionNumber = 1,
                Semester = "Fall",
                Year = 2025,
                InstructorId = faculty2Id,
                ClassroomId = classroom1Id,
                Capacity = 45,
                EnrolledCount = 1,
                ScheduleJson = "[{\"day\":\"Monday\",\"startTime\":\"15:00\",\"endTime\":\"16:50\"},{\"day\":\"Wednesday\",\"startTime\":\"15:00\",\"endTime\":\"16:50\"}]",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Seed Enrollments (students enrolled in courses)
        var student1Id = Guid.Parse("d1111111-1111-1111-1111-111111111111");
        var student2Id = Guid.Parse("d2222222-2222-2222-2222-222222222222");
        var student3Id = Guid.Parse("d3333333-3333-3333-3333-333333333333");
        var student4Id = Guid.Parse("d4444444-4444-4444-4444-444444444444");
        var student5Id = Guid.Parse("d5555555-5555-5555-5555-555555555555");

        modelBuilder.Entity<Enrollment>().HasData(
            // Student 1 (Zeki) - enrolled in BM101, BM201, BM301
            new Enrollment
            {
                Id = Guid.Parse("e0a00001-0001-0001-0001-000000000001"),
                StudentId = student1Id,
                SectionId = section1Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.Parse("e0a00002-0002-0002-0002-000000000002"),
                StudentId = student1Id,
                SectionId = section2Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.Parse("e0a00003-0003-0003-0003-000000000003"),
                StudentId = student1Id,
                SectionId = section3Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            // Student 2 (Mert) - enrolled in BM101, BM302
            new Enrollment
            {
                Id = Guid.Parse("e0a00004-0004-0004-0004-000000000004"),
                StudentId = student2Id,
                SectionId = section1Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.Parse("e0a00005-0005-0005-0005-000000000005"),
                StudentId = student2Id,
                SectionId = section4Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            // Student 3 (Sena) - enrolled in BM201, BM301, Web
            new Enrollment
            {
                Id = Guid.Parse("e0a00006-0006-0006-0006-000000000006"),
                StudentId = student3Id,
                SectionId = section2Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.Parse("e0a00007-0007-0007-0007-000000000007"),
                StudentId = student3Id,
                SectionId = section3Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.Parse("e0a00008-0008-0008-0008-000000000008"),
                StudentId = student3Id,
                SectionId = section5Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            // Student 4 (Şevval) - enrolled in BM101, Web
            new Enrollment
            {
                Id = Guid.Parse("e0a00009-0009-0009-0009-000000000009"),
                StudentId = student4Id,
                SectionId = section1Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.Parse("e0a00010-0010-0010-0010-000000000010"),
                StudentId = student4Id,
                SectionId = section5Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            // Student 5 (Ali - EE student) - enrolled in EEM101
            new Enrollment
            {
                Id = Guid.Parse("e0a00011-0011-0011-0011-000000000011"),
                StudentId = student5Id,
                SectionId = section6Id,
                Status = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow.AddDays(-30),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
