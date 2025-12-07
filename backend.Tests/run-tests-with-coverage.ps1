# Test Coverage Script
# Bu script eski test sonuçlarını siler, testleri çalıştırır, coverage raporu oluşturur ve açar

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Test Coverage Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 1. Eski test sonuçlarını sil
Write-Host "[1/4] Eski test sonuçları temizleniyor..." -ForegroundColor Yellow
if (Test-Path "TestResults") {
    Remove-Item -Path "TestResults" -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "  ✓ TestResults klasörü silindi" -ForegroundColor Green
} else {
    Write-Host "  ℹ TestResults klasörü bulunamadı (zaten temiz)" -ForegroundColor Gray
}
Write-Host ""

# 2. Testleri çalıştır (coverage ile)
Write-Host "[2/4] Testler çalıştırılıyor (coverage toplanıyor)..." -ForegroundColor Yellow
$testResult = dotnet test --collect:"XPlat Code Coverage" --settings:coverlet.runsettings --verbosity:minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "  ✗ Testler başarısız oldu!" -ForegroundColor Red
    exit $LASTEXITCODE
}
Write-Host "  ✓ Testler başarıyla tamamlandı" -ForegroundColor Green
Write-Host ""

# 3. Coverage raporu oluştur
Write-Host "[3/4] HTML coverage raporu oluşturuluyor..." -ForegroundColor Yellow

# ReportGenerator'ın yüklü olup olmadığını kontrol et
$reportGeneratorInstalled = Get-Command reportgenerator -ErrorAction SilentlyContinue

if (-not $reportGeneratorInstalled) {
    Write-Host "  ⚠ ReportGenerator bulunamadı. Yükleniyor..." -ForegroundColor Yellow
    dotnet tool install -g dotnet-reportgenerator-globaltool --verbosity:quiet
    Write-Host "  ✓ ReportGenerator yüklendi" -ForegroundColor Green
}

# Coverage raporu oluştur
$coverageXml = Get-ChildItem -Path "TestResults" -Recurse -Filter "coverage.cobertura.xml" | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if ($coverageXml) {
    reportgenerator -reports:"$($coverageXml.FullName)" -targetdir:"TestResults/CoverageReport" -reporttypes:Html -classfilters:"-*DesignTimeDbContextFactory*;-*Migrations*" -verbosity:Warning
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  ✓ Coverage raporu oluşturuldu" -ForegroundColor Green
    } else {
        Write-Host "  ✗ Coverage raporu oluşturulamadı!" -ForegroundColor Red
        exit $LASTEXITCODE
    }
} else {
    Write-Host "  ✗ Coverage XML dosyası bulunamadı!" -ForegroundColor Red
    exit 1
}
Write-Host ""

# 4. Coverage raporunu aç
Write-Host "[4/4] Coverage raporu açılıyor..." -ForegroundColor Yellow
$reportPath = Join-Path $PSScriptRoot "TestResults\CoverageReport\index.html"

if (Test-Path $reportPath) {
    Write-Host "  ✓ Rapor açılıyor: $reportPath" -ForegroundColor Green
    Start-Process $reportPath
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "  İşlem tamamlandı!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Cyan
} else {
    Write-Host "  ✗ Rapor dosyası bulunamadı: $reportPath" -ForegroundColor Red
    exit 1
}
