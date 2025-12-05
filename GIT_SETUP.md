# GitHub Repository Kurulum Rehberi

## 📋 Adımlar

### 1. GitHub'da Repository Oluşturma

1. GitHub.com'a giriş yapın
2. Sağ üstteki **"+"** butonuna tıklayın → **"New repository"**
3. Repository bilgilerini doldurun:
   - **Repository name:** `smart-campus-platform` (veya istediğiniz isim)
   - **Description:** "Akıllı Kampüs Ekosistem Yönetim Platformu - Web Final Projesi"
   - **Visibility:** 
     - **Private** (önerilen - sadece grup üyeleri erişebilir)
     - Veya **Public** (herkes görebilir)
   - **Initialize repository:** ❌ BOŞ BIRAKIN (README, .gitignore, license eklemeyin)
4. **"Create repository"** butonuna tıklayın

### 2. Local Git Repository'yi Hazırlama

Proje klasöründe terminal açın ve şu komutları çalıştırın:

```bash
# Proje root klasörüne gidin
cd C:\Users\zekierdem\Desktop\web_final

# Git repository'yi başlat (eğer yapılmadıysa)
git init

# Git kullanıcı bilgilerinizi ayarlayın (ilk kez ise)
git config user.name "Zeki"
git config user.email "your-email@example.com"

# Tüm dosyaları staging area'ya ekle
git add .

# İlk commit'i yapın
git commit -m "Initial commit: Project setup with ASP.NET Core backend and React frontend"
```

### 3. GitHub Repository'ye Bağlama

GitHub'da oluşturduğunuz repository'nin sayfasına gidin ve **"Quick setup"** bölümünden URL'i kopyalayın.

**HTTPS için:**
```bash
git remote add origin https://github.com/KULLANICI_ADI/REPO_ADI.git
```

**SSH için (eğer SSH key'iniz varsa):**
```bash
git remote add origin git@github.com:KULLANICI_ADI/REPO_ADI.git
```

**Örnek:**
```bash
git remote add origin https://github.com/zekierdem/smart-campus-platform.git
```

### 4. Projeyi GitHub'a Push Etme

```bash
# Main branch'e geçin (veya master)
git branch -M main

# GitHub'a push edin
git push -u origin main
```

### 5. Grup Üyelerini Ekleme

1. GitHub repository sayfasına gidin
2. **Settings** → **Collaborators** → **Add people**
3. Grup üyelerinin GitHub kullanıcı adlarını veya email'lerini ekleyin:
   - Mert
   - Sena
   - Şevval

### 6. Branch Stratejisi (Önerilen)

Her modül için ayrı branch oluşturun:

```bash
# Part 1 için branch
git checkout -b feature/part1-authentication

# Çalışmalarınızı commit edin
git add .
git commit -m "Add: User authentication endpoints"

# Branch'i GitHub'a push edin
git push -u origin feature/part1-authentication
```

**Main branch'e merge için:**
1. GitHub'da **Pull Request** oluşturun
2. Code review yapın
3. Merge edin

## 📝 Commit Mesaj Formatı (Önerilen)

```
Add: Authentication endpoints
Fix: CORS configuration
Update: Database models
Refactor: User service
Docs: API documentation
Test: Authentication unit tests
```

## 🔒 .gitignore Kontrolü

Aşağıdaki dosyaların commit edilmediğinden emin olun:

- `backend/bin/`
- `backend/obj/`
- `frontend/node_modules/`
- `frontend/build/`
- `.env` dosyaları (`.env.example` commit edilmeli)

Kontrol için:
```bash
git status
```

## ✅ Kontrol Listesi

- [ ] GitHub'da repository oluşturuldu
- [ ] Local git repository initialize edildi
- [ ] İlk commit yapıldı
- [ ] Remote repository eklendi
- [ ] Proje GitHub'a push edildi
- [ ] Grup üyeleri collaborator olarak eklendi
- [ ] .gitignore çalışıyor (sensitive dosyalar commit edilmedi)

## 🚨 Önemli Notlar

1. **.env dosyalarını ASLA commit etmeyin!**
   - Sadece `.env.example` commit edilmeli
   - Gerçek şifreler ve API key'ler `.env` dosyasında kalmalı

2. **Düzenli commit yapın:**
   - Her gün en az 2-3 commit
   - Anlamlı commit mesajları kullanın
   - Küçük, atomic commit'ler yapın

3. **Pull before Push:**
   ```bash
   git pull origin main
   git push origin main
   ```

4. **Conflict çözümü:**
   - Conflict'ler olduğunda dikkatli çözün
   - Gerekirse grup arkadaşlarınızla konuşun

## 📞 Yardım

Git komutları hakkında yardım için:
```bash
git help <command>
# Örnek: git help push
```

---

**Hazırlayan:** Zeki  
**Tarih:** 5 Aralık 2025

