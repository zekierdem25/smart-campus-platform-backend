# Smart Campus API Tests

Bu klasör test projesini içerir. Sena test metodlarını buraya yazacak.

## Yapı

```
SmartCampus.API.Tests/
├── Unit/              # Unit testler (Service'ler için)
│   └── Services/
│       ├── AuthServiceTests.cs
│       └── UserServiceTests.cs
├── Integration/        # Integration testler (Controller'lar için)
│   ├── AuthControllerTests.cs
│   └── UsersControllerTests.cs
└── Helpers/           # Test helper metodları
    └── TestHelpers.cs
```

## Test Çalıştırma

```bash
# Tüm testler
dotnet test

# Belirli bir test sınıfı
dotnet test --filter "FullyQualifiedName~AuthServiceTests"

# Coverage raporu
dotnet test /p:CollectCoverage=true
```

## Minimum Test Gereksinimleri (Part 1)

### Unit Tests (minimum 5):
- `Register_ValidUser_ReturnsSuccess`
- `Register_InvalidEmail_ThrowsException`
- `Login_ValidCredentials_ReturnsToken`
- `Login_InvalidPassword_ThrowsException`
- `GenerateToken_ValidUser_ReturnsJwtToken`

### Integration Tests (minimum 10):
- `POST /api/v1/auth/register` - 201 Created
- `POST /api/v1/auth/register` - 400 Bad Request
- `POST /api/v1/auth/login` - 200 OK
- `POST /api/v1/auth/login` - 401 Unauthorized
- `POST /api/v1/auth/refresh` - 200 OK
- `POST /api/v1/auth/refresh` - 401 Unauthorized
- `POST /api/v1/auth/forgot-password` - 200 OK
- `POST /api/v1/auth/reset-password` - 200 OK
- `GET /api/v1/users/me` - 200 OK (authenticated)
- `GET /api/v1/users/me` - 401 Unauthorized

## Notlar

- Test dosyaları şu anda template olarak hazırlanmıştır
- Sena test metodlarını yazacak
- In-memory database kullanılacak (hızlı test için)
- Mock kullanılacak (email servisi, JWT servisi)

