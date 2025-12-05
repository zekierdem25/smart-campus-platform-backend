using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;

namespace SmartCampus.API.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        ApplicationDbContext context,
        IJwtService jwtService,
        IEmailService emailService,
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _context = context;
        _jwtService = jwtService;
        _emailService = emailService;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        // Email kontrolü
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Bu email adresi zaten kullanılıyor"
            };
        }

        // Bölüm kontrolü
        var department = await _context.Departments.FindAsync(request.DepartmentId);
        if (department == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Geçersiz bölüm seçimi"
            };
        }

        // Kullanıcı tipine göre rol belirleme
        var role = request.UserType.ToLower() switch
        {
            "faculty" => UserRole.Faculty,
            _ => UserRole.Student
        };

        // Kullanıcı oluştur
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = role,
            IsEmailVerified = false,
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Öğrenci veya öğretim üyesi kaydı
        if (role == UserRole.Student)
        {
            if (string.IsNullOrEmpty(request.StudentNumber))
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Öğrenci numarası zorunludur"
                };
            }

            // Öğrenci numarası kontrolü
            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == request.StudentNumber);
            if (existingStudent != null)
            {
                // Kullanıcıyı sil
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Bu öğrenci numarası zaten kullanılıyor"
                };
            }

            var student = new Student
            {
                UserId = user.Id,
                StudentNumber = request.StudentNumber,
                DepartmentId = request.DepartmentId,
                EnrollmentYear = DateTime.Now.Year,
                CurrentSemester = 1
            };
            _context.Students.Add(student);
        }
        else if (role == UserRole.Faculty)
        {
            // Öğretim üyesi numarası otomatik oluştur
            var lastFaculty = await _context.Faculties.OrderByDescending(f => f.EmployeeNumber).FirstOrDefaultAsync();
            var employeeNumber = lastFaculty != null 
                ? $"F{(int.Parse(lastFaculty.EmployeeNumber.Substring(1)) + 1):D3}"
                : "F001";

            var faculty = new Faculty
            {
                UserId = user.Id,
                EmployeeNumber = employeeNumber,
                DepartmentId = request.DepartmentId,
                Title = AcademicTitle.Lecturer
            };
            _context.Faculties.Add(faculty);
        }

        // Email doğrulama token'ı oluştur
        var verificationToken = new EmailVerificationToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
        _context.EmailVerificationTokens.Add(verificationToken);

        await _context.SaveChangesAsync();

        // Email gönder
        await _emailService.SendEmailVerificationAsync(user.Email, user.FullName, verificationToken.Token);

        _logger.LogInformation("Yeni kullanıcı kaydedildi: {Email}", user.Email);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Kayıt başarılı. Email adresinize doğrulama linki gönderildi."
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users
            .Include(u => u.Student)
                .ThenInclude(s => s!.Department)
            .Include(u => u.Faculty)
                .ThenInclude(f => f!.Department)
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Email veya şifre hatalı"
            };
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Email veya şifre hatalı"
            };
        }

        if (!user.IsActive)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Hesabınız deaktif edilmiş"
            };
        }

        if (!user.IsEmailVerified)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Email adresinizi doğrulamanız gerekmektedir"
            };
        }

        // Access token oluştur
        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Refresh token kaydet
        var refreshTokenExpDays = int.Parse(_configuration["JWT:RefreshTokenExpirationDays"] ?? "7");
        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpDays)
        };
        _context.RefreshTokens.Add(refreshTokenEntity);

        // Son giriş zamanını güncelle
        user.LastLoginAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        _logger.LogInformation("Kullanıcı giriş yaptı: {Email}", user.Email);

        var accessTokenExpMinutes = int.Parse(_configuration["JWT:AccessTokenExpirationMinutes"] ?? "15");

        return new AuthResponseDto
        {
            Success = true,
            Message = "Giriş başarılı",
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiration = DateTime.UtcNow.AddMinutes(accessTokenExpMinutes),
            User = MapUserToDto(user)
        };
    }

    public async Task<AuthResponseDto> VerifyEmailAsync(string token)
    {
        var verificationToken = await _context.EmailVerificationTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == token);

        if (verificationToken == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Geçersiz doğrulama linki"
            };
        }

        if (!verificationToken.IsValid)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Doğrulama linki geçersiz veya süresi dolmuş"
            };
        }

        // Kullanıcıyı doğrula
        verificationToken.User.IsEmailVerified = true;
        verificationToken.User.UpdatedAt = DateTime.UtcNow;
        verificationToken.UsedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Hoşgeldin emaili gönder
        await _emailService.SendWelcomeEmailAsync(verificationToken.User.Email, verificationToken.User.FullName);

        _logger.LogInformation("Email doğrulandı: {Email}", verificationToken.User.Email);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Email adresiniz başarıyla doğrulandı. Artık giriş yapabilirsiniz."
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
    {
        var tokenEntity = await _context.RefreshTokens
            .Include(t => t.User)
                .ThenInclude(u => u.Student)
                    .ThenInclude(s => s!.Department)
            .Include(t => t.User)
                .ThenInclude(u => u.Faculty)
                    .ThenInclude(f => f!.Department)
            .FirstOrDefaultAsync(t => t.Token == refreshToken);

        if (tokenEntity == null || !tokenEntity.IsActive)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Geçersiz veya süresi dolmuş token"
            };
        }

        var user = tokenEntity.User;

        // Eski token'ı iptal et
        tokenEntity.RevokedAt = DateTime.UtcNow;
        tokenEntity.RevokedReason = "Yenilendi";

        // Yeni token'lar oluştur
        var newAccessToken = _jwtService.GenerateAccessToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenExpDays = int.Parse(_configuration["JWT:RefreshTokenExpirationDays"] ?? "7");
        var newRefreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpDays)
        };
        _context.RefreshTokens.Add(newRefreshTokenEntity);

        tokenEntity.ReplacedByToken = newRefreshToken;

        await _context.SaveChangesAsync();

        var accessTokenExpMinutes = int.Parse(_configuration["JWT:AccessTokenExpirationMinutes"] ?? "15");

        return new AuthResponseDto
        {
            Success = true,
            Message = "Token yenilendi",
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            AccessTokenExpiration = DateTime.UtcNow.AddMinutes(accessTokenExpMinutes),
            User = MapUserToDto(user)
        };
    }

    public async Task<AuthResponseDto> LogoutAsync(Guid userId, string refreshToken)
    {
        var tokenEntity = await _context.RefreshTokens
            .FirstOrDefaultAsync(t => t.UserId == userId && t.Token == refreshToken);

        if (tokenEntity != null)
        {
            tokenEntity.RevokedAt = DateTime.UtcNow;
            tokenEntity.RevokedReason = "Çıkış yapıldı";
            await _context.SaveChangesAsync();
        }

        return new AuthResponseDto
        {
            Success = true,
            Message = "Çıkış yapıldı"
        };
    }

    public async Task<AuthResponseDto> ForgotPasswordAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        // Güvenlik için kullanıcı bulunamasa bile başarılı döndür
        if (user == null)
        {
            return new AuthResponseDto
            {
                Success = true,
                Message = "Eğer bu email adresi kayıtlıysa, şifre sıfırlama linki gönderilecektir"
            };
        }

        // Mevcut aktif token'ları iptal et
        var existingTokens = await _context.PasswordResetTokens
            .Where(t => t.UserId == user.Id && t.UsedAt == null && t.ExpiresAt > DateTime.UtcNow)
            .ToListAsync();

        foreach (var token in existingTokens)
        {
            token.UsedAt = DateTime.UtcNow;
        }

        // Yeni token oluştur
        var resetToken = new PasswordResetToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
        _context.PasswordResetTokens.Add(resetToken);

        await _context.SaveChangesAsync();

        // Email gönder
        await _emailService.SendPasswordResetAsync(user.Email, user.FullName, resetToken.Token);

        _logger.LogInformation("Şifre sıfırlama talebi: {Email}", email);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Eğer bu email adresi kayıtlıysa, şifre sıfırlama linki gönderilecektir"
        };
    }

    public async Task<AuthResponseDto> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        var resetToken = await _context.PasswordResetTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == request.Token);

        if (resetToken == null || !resetToken.IsValid)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Geçersiz veya süresi dolmuş şifre sıfırlama linki"
            };
        }

        // Şifreyi güncelle
        resetToken.User.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        resetToken.User.UpdatedAt = DateTime.UtcNow;
        resetToken.UsedAt = DateTime.UtcNow;

        // Tüm refresh token'ları iptal et (güvenlik için)
        var activeTokens = await _context.RefreshTokens
            .Where(t => t.UserId == resetToken.UserId && t.RevokedAt == null)
            .ToListAsync();

        foreach (var token in activeTokens)
        {
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedReason = "Şifre sıfırlandı";
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("Şifre sıfırlandı: {Email}", resetToken.User.Email);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Şifreniz başarıyla sıfırlandı. Artık giriş yapabilirsiniz."
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

