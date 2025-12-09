using Microsoft.EntityFrameworkCore;
using SmartCampus.API.Data;
using SmartCampus.API.DTOs;
using SmartCampus.API.Models;
using System.Text.Json;

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
        try
        {
            // Email domain kontrolü - sadece .edu uzantılı veya @smartcampus.com email'ler kabul edilir
            var emailDomain = request.Email.Split('@').LastOrDefault()?.ToLower();
            var isValidDomain = !string.IsNullOrEmpty(emailDomain) && 
                               (emailDomain.Contains(".edu") || emailDomain == "smartcampus.com");
            
            if (!isValidDomain)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Sadece .edu uzantılı eğitim kurumu email adresleri veya @smartcampus.com email adresleri ile kayıt olunabilir. Lütfen geçerli bir email adresi kullanın (örn: @university.edu, @university.edu.tr veya @smartcampus.com)."
                };
            }

        // Email kontrolü - sadece doğrulanmış kullanıcıları kontrol et
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email && u.IsEmailVerified);
        if (existingUser != null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Bu email adresi zaten sistemde kayıtlı. Lütfen farklı bir email adresi kullanın veya giriş yapmayı deneyin."
            };
        }

        // Email doğrulanmamış token var mı kontrol et (tekrar kayıt olmaya çalışıyorsa)
        var now = DateTime.UtcNow;
        
        // Önce RegistrationData ile kontrol et (yeni sistem)
        var existingTokensWithData = await _context.EmailVerificationTokens
            .Where(t => t.UsedAt == null && t.ExpiresAt > now && !string.IsNullOrEmpty(t.RegistrationData))
            .ToListAsync();
        
        var existingToken = existingTokensWithData.FirstOrDefault(t => 
        {
            try
            {
                var data = JsonSerializer.Deserialize<RegisterRequestDto>(t.RegistrationData);
                return data?.Email?.ToLower() == request.Email.ToLower();
            }
            catch
            {
                return false;
            }
        });
        
        // Eğer bulunamadıysa, eski kayıtları kontrol et (UserId ile)
        // Ama sadece kullanıcı doğrulanmamışsa geçerli say
        if (existingToken == null)
        {
            var existingUserWithToken = await _context.EmailVerificationTokens
                .Include(t => t.User)
                .Where(t => t.UsedAt == null && t.ExpiresAt > now && t.UserId.HasValue && t.User != null && t.User.Email.ToLower() == request.Email.ToLower() && !t.User.IsEmailVerified)
                .FirstOrDefaultAsync();
            
            if (existingUserWithToken != null)
            {
                existingToken = existingUserWithToken;
            }
        }
        
        if (existingToken != null)
        {
            // Eğer token'ın UserId'si varsa ve kullanıcı zaten doğrulanmışsa, token'ı iptal et ve devam et
            if (existingToken.UserId.HasValue)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == existingToken.UserId.Value);
                if (user != null && user.IsEmailVerified)
                {
                    // Kullanıcı zaten doğrulanmış, eski token'ı iptal et ve devam et
                    existingToken.UsedAt = now;
                    await _context.SaveChangesAsync();
                    // Devam et, yeni kayıt oluşturulabilir
                }
                else
                {
                    // Kullanıcı doğrulanmamış, eski token'ı iptal et ve yeni token oluştur
                    existingToken.UsedAt = now;
                    await _context.SaveChangesAsync();
                    // Devam et, yeni kayıt oluşturulabilir
                }
            }
            else
            {
                // RegistrationData ile token var, eski token'ı iptal et ve yeni token oluştur
                existingToken.UsedAt = now;
                await _context.SaveChangesAsync();
                // Devam et, yeni kayıt oluşturulabilir
            }
        }

        // Kullanıcı tipine göre rol belirleme
        var role = request.UserType.ToLower() switch
        {
            "faculty" => UserRole.Faculty,
            "admin" => UserRole.Admin,
            _ => UserRole.Student
        };

        // İsim-soyad kontrolü (tüm roller için) - sadece doğrulanmış kullanıcıları kontrol et
        var existingUserByName = await _context.Users
            .Where(u => u.FirstName.ToLower() == request.FirstName.ToLower() && 
                       u.LastName.ToLower() == request.LastName.ToLower() &&
                       u.Role == role &&
                       u.IsEmailVerified)
            .FirstOrDefaultAsync();
        
        if (existingUserByName != null)
        {
            var roleName = role switch
            {
                UserRole.Student => "öğrenci",
                UserRole.Faculty => "öğretim üyesi",
                UserRole.Admin => "admin",
                _ => "kullanıcı"
            };
            
            return new AuthResponseDto
            {
                Success = false,
                Message = $"{request.FirstName} {request.LastName} isimli bir {roleName} zaten sistemde kayıtlı. Lütfen farklı bir email adresi veya bilgilerle kayıt olmayı deneyin."
            };
        }

        // Bölüm kontrolü (Admin için gerekli değil)
        if (role != UserRole.Admin)
        {
        var department = await _context.Departments.FindAsync(request.DepartmentId);
        if (department == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Geçersiz bölüm seçimi"
            };
        }
        }

        // Öğrenci numarası kontrolü (sadece Student için)
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
            var existingStudent = await _context.Students
                .Include(s => s.User)
                .Where(s => s.User.IsEmailVerified)
                .FirstOrDefaultAsync(s => s.StudentNumber == request.StudentNumber);
            if (existingStudent != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = $"Bu öğrenci numarası ({request.StudentNumber}) zaten başka bir öğrenci tarafından kullanılıyor. Lütfen doğru öğrenci numaranızı giriniz."
                };
            }
        }

        // Kayıt bilgilerini JSON olarak hazırla
        var registrationData = new
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password, // Şifreyi hash'lemeden sakla, doğrulama sırasında hash'lenecek
            UserType = request.UserType,
            StudentNumber = request.StudentNumber,
            DepartmentId = request.DepartmentId
        };
        var registrationDataJson = JsonSerializer.Serialize(registrationData);

        // Email doğrulama token'ı oluştur (kullanıcı henüz oluşturulmadı)
        var verificationToken = new EmailVerificationToken
        {
            UserId = null, // Email doğrulanmadan kullanıcı yok
            Token = Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            RegistrationData = registrationDataJson
        };
        _context.EmailVerificationTokens.Add(verificationToken);

        await _context.SaveChangesAsync();

        // Email gönder (FullName için FirstName + LastName kullan)
        var fullName = $"{request.FirstName} {request.LastName}";
        await _emailService.SendEmailVerificationAsync(request.Email, fullName, verificationToken.Token);

        _logger.LogInformation("Email doğrulama token'ı oluşturuldu: {Email}", request.Email);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Kayıt başarılı. Email adresinize doğrulama linki gönderildi."
        };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Kayıt işlemi sırasında hata oluştu: {Email}", request.Email);
            return new AuthResponseDto
            {
                Success = false,
                Message = "Kayıt işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin."
            };
        }
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

        // Email doğrulama kontrolü şifre kontrolünden önce yapılmalı
        if (!user.IsEmailVerified)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Hesabınız doğrulanmamış. Lütfen email adresinizi doğrulayın."
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
        try
        {
            var verificationToken = await _context.EmailVerificationTokens
                .FirstOrDefaultAsync(t => t.Token == token);

        if (verificationToken == null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Geçersiz doğrulama linki"
            };
        }

        // IsValid kontrolü (computed property EF Core'da çalışmaz, manuel kontrol)
        var now = DateTime.UtcNow;
        if (verificationToken.UsedAt != null || verificationToken.ExpiresAt <= now)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Doğrulama linki geçersiz veya süresi dolmuş"
            };
        }

        // RegistrationData'dan kayıt bilgilerini parse et
        if (string.IsNullOrEmpty(verificationToken.RegistrationData))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Geçersiz doğrulama linki"
            };
        }

        RegisterRequestDto? registrationData;
        try
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            registrationData = JsonSerializer.Deserialize<RegisterRequestDto>(verificationToken.RegistrationData, jsonOptions);
            _logger.LogInformation("RegistrationData parsed: {Data}", verificationToken.RegistrationData);
            if (registrationData == null)
            {
                _logger.LogError("Registration data is null after deserialization");
                throw new JsonException("Registration data is null");
            }
            _logger.LogInformation("Parsed email: {Email}, UserType: {UserType}", registrationData.Email, registrationData.UserType);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON parse error for RegistrationData: {Data}", verificationToken.RegistrationData);
            return new AuthResponseDto
            {
                Success = false,
                Message = "Geçersiz doğrulama linki"
            };
        }

        // Email kontrolü - doğrulanmış kullanıcı var mı?
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registrationData.Email && u.IsEmailVerified);
        if (existingUser != null)
        {
            verificationToken.UsedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            
            return new AuthResponseDto
            {
                Success = false,
                Message = "Bu email adresi zaten doğrulanmış ve sistemde kayıtlı."
            };
        }

        // Kullanıcı tipine göre rol belirleme
        var role = registrationData.UserType.ToLower() switch
        {
            "faculty" => UserRole.Faculty,
            "admin" => UserRole.Admin,
            _ => UserRole.Student
        };

        // İsim-soyad kontrolü (tüm roller için)
        var existingUserByName = await _context.Users
            .Where(u => u.FirstName.ToLower() == registrationData.FirstName.ToLower() && 
                       u.LastName.ToLower() == registrationData.LastName.ToLower() &&
                       u.Role == role &&
                       u.IsEmailVerified)
            .FirstOrDefaultAsync();
        
        if (existingUserByName != null)
        {
            var roleName = role switch
            {
                UserRole.Student => "öğrenci",
                UserRole.Faculty => "öğretim üyesi",
                UserRole.Admin => "admin",
                _ => "kullanıcı"
            };
            
            verificationToken.UsedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            
            return new AuthResponseDto
            {
                Success = false,
                Message = $"{registrationData.FirstName} {registrationData.LastName} isimli bir {roleName} zaten sistemde kayıtlı."
            };
        }

        // Bölüm kontrolü (Admin için gerekli değil)
        if (role != UserRole.Admin)
        {
            var department = await _context.Departments.FindAsync(registrationData.DepartmentId);
            if (department == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Geçersiz bölüm seçimi"
                };
            }
        }

        // Doğrulanmamış aynı email'li kullanıcı varsa sil (eski kayıtlar için)
        var unverifiedUser = await _context.Users
            .Include(u => u.Student)
            .Include(u => u.Faculty)
            .FirstOrDefaultAsync(u => u.Email == registrationData.Email && !u.IsEmailVerified);
        
        if (unverifiedUser != null)
        {
            _logger.LogInformation("Doğrulanmamış kullanıcı siliniyor: {Email}", unverifiedUser.Email);
            
            // Önce ilişkili kayıtları sil
            if (unverifiedUser.Student != null)
            {
                _context.Students.Remove(unverifiedUser.Student);
            }
            if (unverifiedUser.Faculty != null)
            {
                _context.Faculties.Remove(unverifiedUser.Faculty);
            }
            
            // Eski token'ları sil
            var oldTokens = await _context.EmailVerificationTokens
                .Where(t => t.UserId == unverifiedUser.Id)
                .ToListAsync();
            _context.EmailVerificationTokens.RemoveRange(oldTokens);
            
            // Kullanıcıyı sil
            _context.Users.Remove(unverifiedUser);
            await _context.SaveChangesAsync();
        }

        // Kullanıcı oluştur (email doğrulandı, artık veritabanına kaydedilebilir)
        var user = new User
        {
            FirstName = registrationData.FirstName,
            LastName = registrationData.LastName,
            Email = registrationData.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrationData.Password),
            Role = role,
            IsEmailVerified = true, // Email doğrulandı
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Öğrenci, öğretim üyesi veya admin kaydı
        if (role == UserRole.Student)
        {
            // Öğrenci numarası kontrolü
            var existingStudent = await _context.Students
                .Include(s => s.User)
                .Where(s => s.User.IsEmailVerified)
                .FirstOrDefaultAsync(s => s.StudentNumber == registrationData.StudentNumber);
            if (existingStudent != null)
            {
                // Kullanıcıyı sil
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                
                verificationToken.UsedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                
                return new AuthResponseDto
                {
                    Success = false,
                    Message = $"Bu öğrenci numarası ({registrationData.StudentNumber}) zaten başka bir öğrenci tarafından kullanılıyor."
                };
            }

            var student = new Student
            {
                UserId = user.Id,
                StudentNumber = registrationData.StudentNumber!,
                DepartmentId = registrationData.DepartmentId,
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
                DepartmentId = registrationData.DepartmentId,
                Title = AcademicTitle.Lecturer
            };
            _context.Faculties.Add(faculty);
        }
        // Admin için ekstra tablo kaydı gerekmez

        // Token'ı güncelle ve işaretle
        verificationToken.UserId = user.Id;
        verificationToken.UsedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Hoşgeldin emaili gönder
        await _emailService.SendWelcomeEmailAsync(user.Email, user.FullName);

        _logger.LogInformation("Email doğrulandı ve kullanıcı oluşturuldu: {Email}", user.Email);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Email adresiniz başarıyla doğrulandı. Artık giriş yapabilirsiniz."
        };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Email doğrulama sırasında hata oluştu. Token: {Token}", token);
            // Development'ta hata detayını göster
            var errorMessage = $"Email doğrulama hatası: {ex.Message}";
            if (ex.InnerException != null)
            {
                errorMessage += $" Inner: {ex.InnerException.Message}";
            }
            return new AuthResponseDto
            {
                Success = false,
                Message = errorMessage
            };
        }
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

