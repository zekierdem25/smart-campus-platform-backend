using System.ComponentModel.DataAnnotations;

namespace SmartCampus.API.DTOs;

// ==================== REQUEST DTOs ====================

public class RegisterRequestDto
{
    [Required(ErrorMessage = "Ad alanı zorunludur")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad 2-100 karakter arasında olmalıdır")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad alanı zorunludur")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Soyad 2-100 karakter arasında olmalıdır")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email alanı zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
    [RegularExpression(@"^[^@]+@([^@]+\.edu(\.\w+)?|smartcampus\.com)$", 
        ErrorMessage = "Sadece .edu uzantılı eğitim kurumu email adresleri veya @smartcampus.com email adresleri kabul edilmektedir")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre alanı zorunludur")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Şifre en az 8 karakter olmalıdır")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", 
        ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrar alanı zorunludur")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kullanıcı tipi zorunludur")]
    public string UserType { get; set; } = "Student"; // Student, Faculty, Admin

    // Student için
    public string? StudentNumber { get; set; }

    // Ortak (Admin için gerekli değil)
    public Guid DepartmentId { get; set; }
}

public class LoginRequestDto
{
    [Required(ErrorMessage = "Email alanı zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre alanı zorunludur")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; } = false;
}

public class VerifyEmailRequestDto
{
    [Required(ErrorMessage = "Token zorunludur")]
    [System.Text.Json.Serialization.JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}

public class ForgotPasswordRequestDto
{
    [Required(ErrorMessage = "Email alanı zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
    public string Email { get; set; } = string.Empty;
}

public class ResetPasswordRequestDto
{
    [Required(ErrorMessage = "Token zorunludur")]
    public string Token { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni şifre zorunludur")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Şifre en az 8 karakter olmalıdır")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", 
        ErrorMessage = "Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrar zorunludur")]
    [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class RefreshTokenRequestDto
{
    [Required(ErrorMessage = "Refresh token zorunludur")]
    public string RefreshToken { get; set; } = string.Empty;
}

// ==================== RESPONSE DTOs ====================

public class AuthResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? AccessTokenExpiration { get; set; }
    public UserResponseDto? User { get; set; }
}

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsEmailVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    // Student bilgileri (varsa)
    public StudentInfoDto? StudentInfo { get; set; }

    // Faculty bilgileri (varsa)
    public FacultyInfoDto? FacultyInfo { get; set; }
}

public class StudentInfoDto
{
    public string StudentNumber { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public decimal GPA { get; set; }
    public decimal CGPA { get; set; }
    public int CurrentSemester { get; set; }
    public int EnrollmentYear { get; set; }
    public bool IsScholarship { get; set; }
}

public class FacultyInfoDto
{
    public string EmployeeNumber { get; set; } = string.Empty;
    public string DepartmentName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? OfficeLocation { get; set; }
    public string? OfficeHours { get; set; }
    public string? Specialization { get; set; }
}

public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
}

