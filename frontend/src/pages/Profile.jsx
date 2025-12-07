// frontend/src/pages/Profile.jsx
import { useContext, useEffect, useState, useRef } from "react";
import { AuthContext } from "../context/AuthContext";
import api from "../api/axios";
import "../App.css";

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
  const [previewUrl, setPreviewUrl] = useState(null);

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

  // Clean up object URL when component unmounts or previewUrl changes
  useEffect(() => {
    return () => {
      if (previewUrl && previewUrl.startsWith("blob:")) {
        URL.revokeObjectURL(previewUrl);
      }
    };
  }, [previewUrl]);

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
      const file = e.target.files[0];
      setPhotoFile(file);

      // Create preview
      const objectUrl = URL.createObjectURL(file);
      setPreviewUrl(objectUrl);

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
      formData.append("profilePicture", photoFile);
      formData.append("file", photoFile);

      // 1) Upload photo
      await api.post("/users/me/profile-picture", formData);

      // 2) Fetch user again
      const meRes = await api.get("/users/me");

      // Check for .data.data per strict DTO
      if (meRes.data?.data) {
        // Cache busting logic
        const updatedUser = { ...meRes.data.data };
        if (updatedUser.profilePictureUrl) {
          updatedUser.profilePictureUrl = `${updatedUser.profilePictureUrl}?t=${new Date().getTime()}`;
        }

        updateUser(updatedUser);
        setPhotoMessage("Profil fotoğrafı güncellendi!");
        setPreviewUrl(null); // Reset preview after successful upload
        setPhotoFile(null);
      } else {
        setPhotoMessage(
          "Yükleme başarılı ama kullanıcı bilgisi alınamadı. Lütfen sayfayı yenileyin."
        );
      }
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

  // --- ŞİFRE DEĞİŞTİRME ---
  const handlePasswordSubmit = async () => {
    setPassMessage("");
    if (!passForm.current || !passForm.new || !passForm.newAgain) {
      setPassMessage("Lütfen tüm alanları doldurunuz.");
      return;
    }
    if (passForm.new !== passForm.newAgain) {
      setPassMessage("Yeni şifreler eşleşmiyor.");
      return;
    }

    try {
      await api.post("/users/me/change-password", {
        currentPassword: passForm.current,
        newPassword: passForm.new,
        confirmPassword: passForm.newAgain,
      });
      alert("Şifreniz başarıyla güncellendi!");
      setPassForm({ current: "", new: "", newAgain: "" });
    } catch (err) {
      console.error(err);
      setPassMessage(err.response?.data?.message || "Şifre değiştirilirken hata oluştu.");
    }
  };

  const fullName = `${user.firstName || ""} ${user.lastName || ""}`.trim();

  // Determine Avatar URL
  // Priority: Preview -> User's Profile Picture -> Fallback to null
  let avatarUrl = previewUrl ||
    user.profilePictureUrl ||
    user.profilePictureURL ||
    user.profilePicture ||
    user.avatarUrl ||
    user.avatar ||
    null;

  // Fix URL if it's from backend and relative
  if (avatarUrl && !avatarUrl.startsWith("http") && !avatarUrl.startsWith("blob:")) {
    avatarUrl = `http://localhost:5000${avatarUrl.startsWith("/") ? "" : "/"}${avatarUrl}`;
  }

  return (
    <div className="profile-page">
      <div className="profile-card">
        <h2 className="profile-title">Profil Ayarları</h2>

        {/* --- ÜST BÖLÜM --- */}
        <div className="profile-header">
          <div className="avatar-section">
            <div className="avatar-circle">
              {avatarUrl ? (
                <img src={avatarUrl} alt="Avatar" className="avatar-img" />
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
                style={{ fontSize: '12px', padding: '8px 16px', marginTop: '5px' }}
                onClick={handleUploadPhoto}
              >
                Onayla ve Yükle
              </button>
            )}

            {photoMessage && <p style={{ fontSize: '12px', marginTop: 5, color: '#e67e22' }}>{photoMessage}</p>}
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

        <hr style={{ border: '0', borderTop: '1px solid #eee', margin: '0 0 30px 0' }} />

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

          <div style={{ textAlign: 'right', marginBottom: '30px' }}>
            <button type="submit" className="btn-save" disabled={isSaving}>
              {isSaving ? "Kaydediliyor..." : "Bilgileri Güncelle"}
            </button>
          </div>
        </form>

        <hr style={{ border: '0', borderTop: '1px solid #eee', margin: '0 0 30px 0' }} />

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
          style={{ backgroundColor: '#607d8b' }}
          onClick={handlePasswordSubmit}
        >
          Şifreyi Güncelle
        </button>
        {passMessage && <p style={{ marginTop: 10, color: '#e74c3c' }}>{passMessage}</p>}

      </div>
    </div>
  );
}