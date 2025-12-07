// frontend/src/pages/Profile.jsx
import { useContext, useEffect, useState, useRef } from "react";
import { AuthContext } from "../context/AuthContext";
import api from "../api/axios";
import "../App.css"; // CSS dosyasının import edildiğinden emin olalım

export default function Profile() {
  const { user, updateUser } = useContext(AuthContext);
  const fileInputRef = useRef(null);

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    phone: "",
  });

  const [isSaving, setIsSaving] = useState(false);
  const [photoFile, setPhotoFile] = useState(null);
  const [photoMessage, setPhotoMessage] = useState("");

  // Şifre state'leri
  const [passForm, setPassForm] = useState({ current: "", new: "", newAgain: "" });
  const [passMessage, setPassMessage] = useState("");

  useEffect(() => {
    if (user) {
      setForm({
        firstName: user.firstName || "",
        lastName: user.lastName || "",
        phone: user.phone || "",
      });
    }
  }, [user]);

  if (!user) return <div className="profile-page">Yükleniyor...</div>;

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handlePassChange = (e) => {
    setPassForm({ ...passForm, [e.target.name]: e.target.value });
  };

  // --- KAYDET ---
  const handleSaveProfile = async (e) => {
    e.preventDefault();
    setIsSaving(true);
    try {
      const res = await api.put("/users/me", form);
      updateUser(res.data?.user || { ...user, ...form });
      alert("Profil başarıyla güncellendi!");
    } catch (err) {
      alert("Hata oluştu: " + (err.response?.data?.message || err.message));
    } finally {
      setIsSaving(false);
    }
  };

  // --- FOTOĞRAF SEÇME ---
  const handleFileSelect = (e) => {
    if (e.target.files && e.target.files[0]) {
      setPhotoFile(e.target.files[0]);
      setPhotoMessage("Fotoğraf seçildi. Yüklemek için butona basınız.");
    }
  };

  // --- FOTOĞRAF YÜKLEME ---
const handleUploadPhoto = async () => {
  if (!photoFile) {
    setPhotoMessage("Lütfen önce bir fotoğraf seçin.");
    return;
  }

  try {
      const formData = new FormData();

      // Backend tarafında hangisi kullanılıyorsa biri tutacaktır
      formData.append("profilePicture", photoFile);
      formData.append("file", photoFile);

      // 1) Fotoğrafı yükle
      await api.post("/users/me/profile-picture", formData);

      // 2) Ardından kullanıcıyı tekrar çek
      const meRes = await api.get("/users/me");

      if (meRes.data?.user) {
        updateUser(meRes.data.user);
        setPhotoMessage("Profil fotoğrafı güncellendi!");
      } else {
        setPhotoMessage(
          "Yükleme başarılı ama kullanıcı bilgisi alınamadı. Lütfen sayfayı yenileyin."
        );
      }

      setPhotoFile(null);
    } catch (err) {
      console.error("Upload error:", err.response || err);
      setPhotoMessage(
        "Yükleme başarısız: " +
          (err.response?.data?.message ||
            err.response?.data ||
            err.message ||
            "Bilinmeyen hata")
      );
    }
  };

  const fullName = `${user.firstName || ""} ${user.lastName || ""}`.trim();

  const avatarUrl =
    user.profilePictureUrl ||
    user.profilePictureURL ||
    user.profilePicture ||
    user.avatarUrl ||
    user.avatar ||
    null;
    
  return (
    <div className="profile-page">
      <div className="profile-card">
        <h2 className="profile-title">Profil Ayarları</h2>

        {/* --- ÜST BÖLÜM --- */}
        <div className="profile-header">
          <div className="avatar-section">
            <div className="avatar-circle">
              {user.profilePictureUrl ? (
                <img src={user.profilePictureUrl} alt="Avatar" className="avatar-img" />
              ) : (
                <span>{user.firstName?.[0]?.toUpperCase()}</span>
              )}
            </div>
            
            {/* GİZLİ INPUT VE ÖZEL BUTON */}
            <input 
              type="file" 
              ref={fileInputRef} 
              onChange={handleFileSelect} 
              className="hidden-input" 
              accept="image/*"
            />
            
            {!photoFile ? (
              <button 
                type="button" 
                className="custom-file-upload"
                onClick={() => fileInputRef.current.click()}
              >
                Fotoğraf Değiştir
              </button>
            ) : (
              <button 
                type="button" 
                className="btn-save" 
                style={{fontSize: '12px', padding: '8px 16px', marginTop: '5px'}}
                onClick={handleUploadPhoto}
              >
                Onayla ve Yükle
              </button>
            )}
            
            {photoMessage && <p style={{fontSize: '12px', marginTop: 5, color: '#e67e22'}}>{photoMessage}</p>}
          </div>

          <div className="info-section">
            <h3>{fullName || "Kullanıcı"}</h3>
            <span className="role-badge">
              {user.role === "admin" ? "Yönetici" : user.role === "faculty" ? "Öğretim Üyesi" : "Öğrenci"}
            </span>
            <p className="info-text"><strong>E-posta:</strong> {user.email}</p>
            {user.studentInfo && (
              <p className="info-text">
                <strong>No:</strong> {user.studentInfo.studentNumber} | <strong>Bölüm:</strong> {user.studentInfo.departmentName}
              </p>
            )}
          </div>
        </div>

        <hr style={{border: '0', borderTop: '1px solid #eee', margin: '0 0 30px 0'}} />

        {/* --- FORM BÖLÜMÜ --- */}
        <form onSubmit={handleSaveProfile}>
          <h4 className="section-header">Kişisel Bilgiler</h4>
          
          <div className="form-row">
            <div className="form-group">
              <label>Ad</label>
              <input className="form-input" name="firstName" value={form.firstName} onChange={handleChange} />
            </div>
            <div className="form-group">
              <label>Soyad</label>
              <input className="form-input" name="lastName" value={form.lastName} onChange={handleChange} />
            </div>
          </div>

          <div className="form-row">
            <div className="form-group">
              <label>Telefon</label>
              <input className="form-input" name="phone" value={form.phone} onChange={handleChange} placeholder="0555..." />
            </div>
            <div className="form-group">
              <label>E-posta (Değiştirilemez)</label>
              <input className="form-input" value={user.email} disabled />
            </div>
          </div>

          <div style={{textAlign: 'right', marginBottom: '30px'}}>
            <button type="submit" className="btn-save" disabled={isSaving}>
              {isSaving ? "Kaydediliyor..." : "Bilgileri Güncelle"}
            </button>
          </div>
        </form>

        <hr style={{border: '0', borderTop: '1px solid #eee', margin: '0 0 30px 0'}} />

        {/* --- ŞİFRE BÖLÜMÜ --- */}
        <h4 className="section-header">Güvenlik</h4>
        <div className="form-row">
            <div className="form-group">
                <label>Mevcut Şifre</label>
                <input className="form-input" type="password" name="current" value={passForm.current} onChange={handlePassChange} />
            </div>
        </div>
        <div className="form-row">
            <div className="form-group">
                <label>Yeni Şifre</label>
                <input className="form-input" type="password" name="new" value={passForm.new} onChange={handlePassChange} />
            </div>
            <div className="form-group">
                <label>Yeni Şifre (Tekrar)</label>
                <input className="form-input" type="password" name="newAgain" value={passForm.newAgain} onChange={handlePassChange} />
            </div>
        </div>
        <button 
            type="button" 
            className="btn-save" 
            style={{backgroundColor: '#607d8b'}}
            onClick={() => setPassMessage("Şifre işlemleri için lütfen 'Şifremi Unuttum' ekranını kullanınız.")}
        >
            Şifreyi Güncelle
        </button>
        {passMessage && <p style={{marginTop: 10, color: '#e74c3c'}}>{passMessage}</p>}

      </div>
    </div>
  );
}