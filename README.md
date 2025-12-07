# Akıllı Kampüs Ekosistem Yönetim Platformu

## 📋 Proje Hakkında

Bu proje, bir üniversite kampüsünün günlük operasyonlarını dijitalleştiren ve optimize eden kapsamlı bir web uygulamasıdır.

## 👥 Grup Üyeleri

- Zeki Erdem DURGUN
- Mert ABDULLAHOĞLU
- Sena KAMİLOĞLU
- Şevval ASİ

## 🛠️ Teknoloji Stack

### Backend
- **Framework:** ASP.NET Core 9.0 Web API
- **Language:** C#
- **ORM:** Entity Framework Core (Pomelo MySQL)
- **Database:** MySQL 8.0
- **Authentication:** JWT Bearer Tokens
- **Password Hashing:** BCrypt.Net
- **Email:** MailKit
- **Validation:** FluentValidation
- **API Documentation:** Swagger/OpenAPI

### Frontend
- **Framework:** React 19
- **Routing:** React Router v7
- **State Management:** Context API + useReducer
- **HTTP Client:** Axios
- **Form Handling:** React Hook Form + Yup
- **Styling:** Tailwind CSS
- **Charts:** Recharts
- **Maps:** Leaflet + React Leaflet
- **QR Code:** qrcode.react

### DevOps
- **Containerization:** Docker + Docker Compose
- **Version Control:** Git + GitHub

## 📁 Proje Yapısı

```
web_final/
├── backend/              # ASP.NET Core Web API
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Data/
│   ├── Middleware/
│   └── Program.cs
├── frontend/            # React Application
│   ├── src/
│   │   ├── components/
│   │   ├── pages/
│   │   ├── context/
│   │   ├── services/
│   │   └── utils/
│   └── public/
├── docs/                # Dokümantasyonlar
├── docker-compose.yml
└── README.md
```

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
- .NET 9.0 SDK
- Node.js 18+
- Docker & Docker Compose (opsiyonel)
- MySQL 8.0 (veya Docker ile)

### Docker ile Çalıştırma (Önerilen)

```bash
# Tüm servisleri başlat
docker-compose up -d

# Logları görüntüle
docker-compose logs -f

# Servisleri durdur
docker-compose down
```

### Manuel Kurulum

#### Backend

```bash
cd backend
cp .env.example .env
# .env dosyasını düzenleyin

# Veritabanı migration'larını çalıştır
dotnet ef database update

# Uygulamayı çalıştır
dotnet run
```

Backend: `http://localhost:5000`
Swagger UI: `http://localhost:5000/swagger`

#### Frontend

```bash
cd frontend
cp .env.example .env
# .env dosyasını düzenleyin

# Bağımlılıkları yükle
npm install

# Uygulamayı çalıştır
npm start
```

Frontend: `http://localhost:3000`

## 📚 Dokümantasyon

Tüm dokümantasyonlar `docs/` klasöründe bulunmaktadır:

- `PROJECT_OVERVIEW.md` - Proje genel bakış
- `API_DOCUMENTATION.md` - API endpoint dokümantasyonu
- `DATABASE_SCHEMA.md` - Veritabanı şeması
- `DEPLOYMENT_GUIDE.md` - Deployment rehberi
- `USER_MANUAL.md` - Kullanıcı kılavuzu

## 🧪 Test

### Backend Testleri

Backend testleri xUnit framework'ü kullanılarak yazılmıştır ve iki kategoriye ayrılır:
- **Unit Tests**: Servislerin ve business logic'in test edildiği testler
- **Integration Tests**: API endpoint'lerinin ve controller'ların test edildiği testler

#### Testleri Çalıştırma

**Basit test çalıştırma:**
```bash
cd backend.Tests
dotnet test
```

**Detaylı çıktı ile:**
```bash
cd backend.Tests
dotnet test --verbosity normal
```

**Belirli bir test sınıfını çalıştırma:**
```bash
cd backend.Tests
dotnet test --filter "FullyQualifiedName~AuthControllerTests"
```

#### Code Coverage Raporu Oluşturma

**Otomatik Script ile (Önerilen):**
```powershell
cd backend.Tests
.\run-tests-with-coverage.ps1
```

Bu script:
1. Eski test sonuçlarını temizler
2. Testleri çalıştırır ve coverage verisi toplar
3. HTML coverage raporu oluşturur
4. Raporu tarayıcıda otomatik açar

**Manuel olarak:**

1. **Coverage verisi toplama:**
```bash
cd backend.Tests
dotnet test --collect:"XPlat Code Coverage" --settings:coverlet.runsettings
```

2. **HTML raporu oluşturma:**
```bash
# ReportGenerator'ı yükle (sadece bir kez)
dotnet tool install -g dotnet-reportgenerator-globaltool

# HTML raporu oluştur
reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:"TestResults/CoverageReport" -reporttypes:Html -classfilters:"-*DesignTimeDbContextFactory*;-*Migrations*"
```

3. **Raporu görüntüleme:**
`backend.Tests/TestResults/CoverageReport/index.html` dosyasını tarayıcıda açın.


### Frontend Testleri

```bash
cd frontend
npm test
```

## 📅 Proje Zaman Çizelgesi

- **Part 1:** 5-8 Aralık 2025 (Authentication & User Management)
- **Part 2:** 9-15 Aralık 2025 (Academic Management + GPS Attendance)
- **Part 3:** 16-22 Aralık 2025 (Meal + Event + Scheduling)
- **Part 4:** 23-28 Aralık 2025 (Analytics + Integration)
- **Sunum:** 29 Aralık 2025

## 📝 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## 🤝 Katkıda Bulunma

1. Feature branch oluşturun (`git checkout -b feature/AmazingFeature`)
2. Değişikliklerinizi commit edin (`git commit -m 'Add some AmazingFeature'`)
3. Branch'inizi push edin (`git push origin feature/AmazingFeature`)
4. Pull Request oluşturun

---

**Not:** Bu proje Dr. Öğretim Üyesi Mehmet Sevri'nin Web ve Mobil Programlama dersi kapsamında geliştirilmiştir.

