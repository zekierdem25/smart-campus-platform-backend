using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public UserService(
        ApplicationDbContext context,
        ILogger<UserService> logger,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
        _environment = environment;
    }

    public async Task<ApiResponseDto<UserResponseDto>> GetCurrentUserAsync(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.Student)
                .ThenInclude(s => s!.Department)
            .Include(u => u.Faculty)
                .ThenInclude(f => f!.Department)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new ApiResponseDto<UserResponseDto>
            {
                Success = false,
                Message = "Kullanıcı bulunamadı"
            };
        }

        return new ApiResponseDto<UserResponseDto>
        {
            Success = true,
            Data = MapUserToDto(user)
        };
    }

    public async Task<ApiResponseDto<UserResponseDto>> UpdateProfileAsync(Guid userId, UpdateProfileRequestDto request)
    {
        var user = await _context.Users
            .Include(u => u.Student)
                .ThenInclude(s => s!.Department)
            .Include(u => u.Faculty)
                .ThenInclude(f => f!.Department)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new ApiResponseDto<UserResponseDto>
            {
                Success = false,
                Message = "Kullanıcı bulunamadı"
            };
        }

        // Sadece gönderilen alanları güncelle
        if (!string.IsNullOrEmpty(request.FirstName))
            user.FirstName = request.FirstName;

        if (!string.IsNullOrEmpty(request.LastName))
            user.LastName = request.LastName;

        if (request.Phone != null)
            user.Phone = request.Phone;

        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Profil güncellendi: {UserId}", userId);

        return new ApiResponseDto<UserResponseDto>
        {
            Success = true,
            Message = "Profil başarıyla güncellendi",
            Data = MapUserToDto(user)
        };
    }

    public async Task<ApiResponseDto<string>> UpdateProfilePictureAsync(Guid userId, IFormFile file)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return new ApiResponseDto<string>
            {
                Success = false,
                Message = "Kullanıcı bulunamadı"
            };
        }

        // Dosya boyutu kontrolü (5MB)
        var maxFileSize = int.Parse(_configuration["FileUpload:MaxFileSize"] ?? "5242880");
        if (file.Length > maxFileSize)
        {
            return new ApiResponseDto<string>
            {
                Success = false,
                Message = "Dosya boyutu 5MB'dan büyük olamaz"
            };
        }

        // Dosya uzantısı kontrolü
        var allowedExtensions = (_configuration["FileUpload:AllowedExtensions"] ?? "jpg,jpeg,png")
            .Split(',');
        var extension = Path.GetExtension(file.FileName).ToLower().TrimStart('.');
        
        if (!allowedExtensions.Contains(extension))
        {
            return new ApiResponseDto<string>
            {
                Success = false,
                Message = "Sadece JPG ve PNG dosyaları yüklenebilir"
            };
        }

        // Dosya kaydet
        var uploadsFolder = Path.Combine(_environment.ContentRootPath, "uploads", "profiles");
        Directory.CreateDirectory(uploadsFolder);

        // Eski dosyayı sil
        if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
        {
            var oldFileName = Path.GetFileName(user.ProfilePictureUrl);
            var oldFilePath = Path.Combine(uploadsFolder, oldFileName);
            if (File.Exists(oldFilePath))
            {
                File.Delete(oldFilePath);
            }
        }

        // Yeni dosyayı kaydet
        var fileName = $"{userId}_{DateTime.UtcNow.Ticks}.{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // URL'i güncelle
        var fileUrl = $"/uploads/profiles/{fileName}";
        user.ProfilePictureUrl = fileUrl;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Profil fotoğrafı güncellendi: {UserId}", userId);

        return new ApiResponseDto<string>
        {
            Success = true,
            Message = "Profil fotoğrafı başarıyla güncellendi",
            Data = fileUrl
        };
    }

    public async Task<ApiResponseDto<bool>> ChangePasswordAsync(Guid userId, ChangePasswordRequestDto request)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Kullanıcı bulunamadı"
            };
        }

        // Mevcut şifre kontrolü
        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
        {
            return new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Mevcut şifre hatalı"
            };
        }

        // Yeni şifre eski şifreyle aynı olmamalı
        if (BCrypt.Net.BCrypt.Verify(request.NewPassword, user.PasswordHash))
        {
            return new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Yeni şifre eski şifreyle aynı olamaz"
            };
        }

        // Şifreyi güncelle
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Şifre değiştirildi: {UserId}", userId);

        return new ApiResponseDto<bool>
        {
            Success = true,
            Message = "Şifre başarıyla değiştirildi",
            Data = true
        };
    }

    public async Task<UserListResponseDto> GetUsersAsync(UserListRequestDto request)
    {
        var query = _context.Users
            .Include(u => u.Student)
                .ThenInclude(s => s!.Department)
            .Include(u => u.Faculty)
                .ThenInclude(f => f!.Department)
            .AsQueryable();

        // Filtreleme
        if (!string.IsNullOrEmpty(request.Role))
        {
            if (Enum.TryParse<UserRole>(request.Role, true, out var role))
            {
                query = query.Where(u => u.Role == role);
            }
        }

        if (request.DepartmentId.HasValue)
        {
            query = query.Where(u => 
                (u.Student != null && u.Student.DepartmentId == request.DepartmentId) ||
                (u.Faculty != null && u.Faculty.DepartmentId == request.DepartmentId));
        }

        // Arama
        if (!string.IsNullOrEmpty(request.Search))
        {
            var search = request.Search.ToLower();
            query = query.Where(u => 
                u.FirstName.ToLower().Contains(search) ||
                u.LastName.ToLower().Contains(search) ||
                u.Email.ToLower().Contains(search));
        }

        // Toplam sayı
        var totalCount = await query.CountAsync();

        // Sıralama
        query = request.SortBy.ToLower() switch
        {
            "firstname" => request.SortDescending ? query.OrderByDescending(u => u.FirstName) : query.OrderBy(u => u.FirstName),
            "lastname" => request.SortDescending ? query.OrderByDescending(u => u.LastName) : query.OrderBy(u => u.LastName),
            "email" => request.SortDescending ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
            _ => request.SortDescending ? query.OrderByDescending(u => u.CreatedAt) : query.OrderBy(u => u.CreatedAt)
        };

        // Sayfalama
        var users = await query
            .Skip((request.Page - 1) * request.Limit)
            .Take(request.Limit)
            .ToListAsync();

        return new UserListResponseDto
        {
            Users = users.Select(MapUserToDto).ToList(),
            TotalCount = totalCount,
            Page = request.Page,
            Limit = request.Limit,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.Limit)
        };
    }

    public async Task<ApiResponseDto<List<DepartmentResponseDto>>> GetDepartmentsAsync()
    {
        var departments = await _context.Departments
            .Where(d => d.IsActive)
            .OrderBy(d => d.Name)
            .Select(d => new DepartmentResponseDto
            {
                Id = d.Id,
                Name = d.Name,
                Code = d.Code,
                Faculty = d.Faculty
            })
            .ToListAsync();

        return new ApiResponseDto<List<DepartmentResponseDto>>
        {
            Success = true,
            Data = departments
        };
    }

    private UserResponseDto MapUserToDto(User user)
    {
        var dto = new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString(),
            Phone = user.Phone,
            ProfilePictureUrl = user.ProfilePictureUrl,
            IsEmailVerified = user.IsEmailVerified,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt
        };

        if (user.Student != null)
        {
            dto.StudentInfo = new StudentInfoDto
            {
                StudentNumber = user.Student.StudentNumber,
                DepartmentName = user.Student.Department?.Name ?? "",
                GPA = user.Student.GPA,
                CGPA = user.Student.CGPA,
                CurrentSemester = user.Student.CurrentSemester,
                EnrollmentYear = user.Student.EnrollmentYear,
                IsScholarship = user.Student.IsScholarship
            };
        }

        if (user.Faculty != null)
        {
            dto.FacultyInfo = new FacultyInfoDto
            {
                EmployeeNumber = user.Faculty.EmployeeNumber,
                DepartmentName = user.Faculty.Department?.Name ?? "",
                Title = user.Faculty.Title.ToString(),
                OfficeLocation = user.Faculty.OfficeLocation,
                OfficeHours = user.Faculty.OfficeHours,
                Specialization = user.Faculty.Specialization
            };
        }

        return dto;
    }
}

