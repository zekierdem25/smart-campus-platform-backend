// frontend/src/pages/ForgotPassword.jsx
import { useState } from "react";
import { Link } from "react-router-dom";
import api from "../api/axios";

export default function ForgotPassword() {
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");
    setSuccess("");

    if (!email) {
      setError("Lütfen e-posta adresinizi girin.");
      return;
    }

    setIsSubmitting(true);

    try {
      const res = await api.post("/auth/forgot-password", { email });

      if (res.data?.success) {
        setSuccess(
          "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi. Lütfen e-posta kutunuzu kontrol edin."
        );
        setEmail("");
      } else {
        setError(
          res.data?.message ||
            "Şifre sıfırlama isteği oluşturulamadı. Lütfen daha sonra tekrar deneyin."
        );
      }
    } catch (err) {
      console.error("Forgot password error:", err);
      const msg =
        err.response?.data?.message ||
        "Şifre sıfırlama isteği sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
      setError(msg);
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <div className="card">
      <h2 className="title">Şifremi Unuttum</h2>

      <p style={{ fontSize: 14, marginBottom: 10 }}>
        Şifre sıfırlama bağlantısı almak için kayıtlı e-posta adresinizi girin.
      </p>

      <form onSubmit={handleSubmit}>
        <input
          className="input"
          type="email"
          placeholder="E-posta adresi"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
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
          {isSubmitting ? "Gönderiliyor..." : "Sıfırlama bağlantısı gönder"}
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