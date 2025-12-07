// frontend/src/pages/ResetPassword.jsx
import { useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import api from "../api/axios";

export default function ResetPassword() {
  const { token } = useParams();
  const navigate = useNavigate();

  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");
    setSuccess("");

    if (!token) {
      setError("Geçersiz veya eksik şifre sıfırlama linki.");
      return;
    }

    if (!password || !confirmPassword) {
      setError("Lütfen yeni şifrenizi ve şifre tekrarını girin.");
      return;
    }

    if (password.length < 8) {
      setError("Şifre en az 8 karakter olmalıdır.");
      return;
    }

    if (password !== confirmPassword) {
      setError("Şifre ve şifre tekrarı eşleşmiyor.");
      return;
    }

    setIsSubmitting(true);

    try {
      // Backend DTO'suna göre bu alan adları değişebilir.
      // En yaygın olan: { token, newPassword, confirmPassword }
      const payload = {
        token,
        newPassword: password,
        confirmPassword,
      };

      console.log("Reset password payload:", payload);

      const res = await api.post("/auth/reset-password", payload);

      if (res.data?.success) {
        setSuccess(
          "Şifreniz başarıyla güncellendi. 3 saniye içinde giriş sayfasına yönlendirileceksiniz."
        );
        setPassword("");
        setConfirmPassword("");

        setTimeout(() => {
          navigate("/login");
        }, 3000);
      } else {
        setError(
          res.data?.message ||
            "Şifre sıfırlama işlemi gerçekleştirilemedi. Link geçersiz veya süresi dolmuş olabilir."
        );
      }
    } catch (err) {
      console.error("Reset password error:", err);
      const msg =
        err.response?.data?.message ||
        "Şifre sıfırlama sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
      setError(msg);
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <div className="card">
      <h2 className="title">Yeni Şifre Belirle</h2>

      <p style={{ fontSize: 14, marginBottom: 10 }}>
        Lütfen yeni şifrenizi belirleyin. Şifreniz en az 8 karakter olmalıdır.
      </p>

      <form onSubmit={handleSubmit}>
        <input
          className="input"
          type="password"
          placeholder="Yeni şifre"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <input
          className="input"
          type="password"
          placeholder="Yeni şifre (tekrar)"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          style={{ marginTop: 8 }}
        />

        {error && (
          <p style={{ color: "red", marginTop: 10, fontSize: 14 }}>{error}</p>
        )}
        {success && (
          <p style={{ color: "green", marginTop: 10, fontSize: 14 }}>
            {success}
          </p>
        )}

        <button
          className="btn btn-primary"
          type="submit"
          disabled={isSubmitting}
          style={{ marginTop: 12 }}
        >
          {isSubmitting ? "Şifre güncelleniyor..." : "Şifreyi güncelle"}
        </button>
      </form>

      <p style={{ marginTop: 15 }}>
        Giriş sayfasına dönmek için{" "}
        <Link to="/login" style={{ color: "#1a73e8", fontWeight: 600 }}>
          tıklayın
        </Link>
        .
      </p>
    </div>
  );
}