# Test Coverage Script (ASCII Version)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Test Coverage Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 1. Eski test sonuclarini sil
Write-Host "[1/4] Eski test sonuclari temizleniyor..." -ForegroundColor Yellow
if (Test-Path "TestResults") {
    Remove-Item -Path "TestResults" -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "  [OK] TestResults klasoru silindi" -ForegroundColor Green
} else {
    Write-Host "  [INFO] TestResults klasoru bulunamadi (zaten temiz)" -ForegroundColor Gray
}
Write-Host ""

# 2. Testleri calistir
Write-Host "[2/4] Testler calistiriliyor (coverage toplaniyor)..." -ForegroundColor Yellow
$testResult = dotnet test --collect:"XPlat Code Coverage" --settings:coverlet.runsettings --verbosity:minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "  [ERR] Testler basarisiz oldu!" -ForegroundColor Red
    exit $LASTEXITCODE
}
Write-Host "  [OK] Testler basariyla tamamlandi" -ForegroundColor Green
Write-Host ""

# 3. Coverage raporu olustur
Write-Host "[3/4] HTML coverage raporu olusturuluyor..." -ForegroundColor Yellow

$reportGeneratorExe = "reportgenerator"
if (-not (Get-Command "reportgenerator" -ErrorAction SilentlyContinue)) {
    $globalToolPath = "$env:USERPROFILE\.dotnet\tools\reportgenerator.exe"
    if (Test-Path $globalToolPath) {
        $reportGeneratorExe = $globalToolPath
        Write-Host "  [OK] ReportGenerator manual path uzerinden bulundu" -ForegroundColor Green
    } else {
        Write-Host "  [WARN] ReportGenerator bulunamadi. Yukleniyor..." -ForegroundColor Yellow
        dotnet tool install -g dotnet-reportgenerator-globaltool --verbosity:quiet
        if (Test-Path $globalToolPath) {
            $reportGeneratorExe = $globalToolPath
            Write-Host "  [OK] ReportGenerator yuklendi" -ForegroundColor Green
        } else {
            Write-Host "  [ERR] ReportGenerator yuklenemedi veya bulunamadi!" -ForegroundColor Red
            exit 1
        }
    }
}

$coverageXml = Get-ChildItem -Path "TestResults" -Recurse -Filter "coverage.cobertura.xml" | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if ($coverageXml) {
    & $reportGeneratorExe "-reports:$($coverageXml.FullName)" "-targetdir:TestResults/CoverageReport" "-reporttypes:Html" "-classfilters:-*DesignTimeDbContextFactory*;-*Migrations*" "-verbosity:Warning"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  [OK] Coverage raporu olusturuldu" -ForegroundColor Green
    } else {
        Write-Host "  [ERR] Coverage raporu olusturulamadi!" -ForegroundColor Red
        exit $LASTEXITCODE
    }
} else {
    Write-Host "  [ERR] Coverage XML dosyasi bulunamadi!" -ForegroundColor Red
    exit 1
}
Write-Host ""

# 4. Raporu ac
Write-Host "[4/4] Coverage raporu aciliyor..." -ForegroundColor Yellow
$reportPath = Join-Path $PSScriptRoot "TestResults\CoverageReport\index.html"

if (Test-Path $reportPath) {
    Write-Host "  [OK] Rapor aciliyor: $reportPath" -ForegroundColor Green
    Start-Process $reportPath
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "  Islem tamamlandi!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Cyan
} else {
    Write-Host "  [ERR] Rapor dosyasi bulunamadi: $reportPath" -ForegroundColor Red
    exit 1
}