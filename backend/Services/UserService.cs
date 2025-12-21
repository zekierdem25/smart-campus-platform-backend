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
    private readonly IActivityLogService _activityLogService;
    private readonly IFileStorageService _fileStorageService;

    public UserService(
        ApplicationDbContext context,
        ILogger<UserService> logger,
        IConfiguration configuration,
        IWebHostEnvironment environment,
        IActivityLogService activityLogService,
        IFileStorageService fileStorageService)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
        _environment = environment;
        _activityLogService = activityLogService;
        _fileStorageService = fileStorageService;
    }

    public async Task<ApiResponseDto<UserResponseDto>> GetCurrentUserAsync(Guid userId)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetCurrentUserAsync hatası: {Message}", ex.Message);
            return new ApiResponseDto<UserResponseDto>
            {
                Success = false,
                Message = $"Sistem hatası: {ex.Message}"
            };
        }
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
        await _activityLogService.RecordAsync(userId, "profile-update", "Profil bilgileri güncellendi");

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

        // Eski dosyayı sil
        if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
        {
            await _fileStorageService.DeleteFileAsync(user.ProfilePictureUrl);
        }

        // Yeni dosyayı Cloud Storage'a yükle
        var fileName = $"{userId}_{DateTime.UtcNow.Ticks}.{extension}";
        var fileUrl = await _fileStorageService.UploadFileAsync(file, fileName);

        // URL'i güncelle
        user.ProfilePictureUrl = fileUrl;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Profil fotoğrafı güncellendi: {UserId}", userId);
        await _activityLogService.RecordAsync(userId, "profile-picture", "Profil fotoğrafı güncellendi");

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
        await _activityLogService.RecordAsync(userId, "change-password", "Şifre değiştirildi");

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

    public async Task<ApiResponseDto<bool>> DeleteUserAsync(Guid userId, Guid deletedByUserId)
    {
        // Kendi kendini silmeyi engelle
        if (userId == deletedByUserId)
        {
            return new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Kendi hesabınızı silemezsiniz"
            };
        }

        var user = await _context.Users
            .Include(u => u.Student)
            .Include(u => u.Faculty)
            .Include(u => u.RefreshTokens)
            .Include(u => u.EmailVerificationTokens)
            .Include(u => u.PasswordResetTokens)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Kullanıcı bulunamadı"
            };
        }

        // Admin kullanıcılarını silmeyi engelle (güvenlik)
        if (user.Role == UserRole.Admin)
        {
            return new ApiResponseDto<bool>
            {
                Success = false,
                Message = "Admin kullanıcıları silinemez"
            };
        }

        // İlişkili verileri sil
        if (user.Student != null)
        {
            _context.Students.Remove(user.Student);
        }

        if (user.Faculty != null)
        {
            _context.Faculties.Remove(user.Faculty);
        }

        // Token'ları sil
        _context.RefreshTokens.RemoveRange(user.RefreshTokens);
        _context.EmailVerificationTokens.RemoveRange(user.EmailVerificationTokens);
        _context.PasswordResetTokens.RemoveRange(user.PasswordResetTokens);

        // Profil fotoğrafını sil
        if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
        {
            await _fileStorageService.DeleteFileAsync(user.ProfilePictureUrl);
        }

        // Kullanıcıyı sil
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Kullanıcı silindi: {UserId} (Silen: {DeletedByUserId})", userId, deletedByUserId);
        await _activityLogService.RecordAsync(deletedByUserId, "delete-user", $"Kullanıcı silindi: {user.Email}");

        return new ApiResponseDto<bool>
        {
            Success = true,
            Message = "Kullanıcı başarıyla silindi",
            Data = true
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
                DepartmentId = user.Student.DepartmentId,
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
                DepartmentId = user.Faculty.DepartmentId,
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

