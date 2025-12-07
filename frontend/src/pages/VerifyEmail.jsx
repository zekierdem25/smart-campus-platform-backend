// frontend/src/pages/VerifyEmail.jsx
import { useEffect, useState } from "react";
import { useParams, useNavigate, Link } from "react-router-dom";
import api from "../api/axios";

export default function VerifyEmail() {
  const { token } = useParams();
  const navigate = useNavigate();

  const [status, setStatus] = useState("loading"); // loading | success | error
  const [message, setMessage] = useState("E-posta doğrulanıyor...");

  useEffect(() => {
    async function verify() {
      try {
        if (!token) {
          setStatus("error");
          setMessage("Geçersiz doğrulama linki.");
          return;
        }

        const res = await api.post("/auth/verify-email", { token });

        if (res.data?.success) {
          setStatus("success");
          setMessage(
            "E-posta adresiniz başarıyla doğrulandı. 3 saniye içinde giriş sayfasına yönlendirileceksiniz."
          );

          // 3 sn sonra login sayfasına at
          setTimeout(() => {
            navigate("/login");
          }, 3000);
        } else {
          setStatus("error");
          setMessage(res.data?.message || "Doğrulama linki geçersiz veya süresi dolmuş.");
        }
      } catch (err) {
        console.error("Verify email error:", err);
        setStatus("error");
        setMessage(
          err.response?.data?.message ||
            "E-posta doğrulama sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin."
        );
      }
    }

    verify();
  }, [token, navigate]);

  // Basit kart tasarımı (Login/Register ile aynı stil)
  return (
    <div className="card">
      <h2 className="title">E-posta Doğrulama</h2>

      <p
        style={{
          marginTop: 10,
          fontSize: 14,
          color: status === "success" ? "green" : status === "error" ? "red" : "#333",
        }}
      >
        {message}
      </p>

      {status !== "loading" && (
        <p style={{ marginTop: 20 }}>
          Giriş sayfasına dönmek için{" "}
          <Link to="/login" style={{ color: "#1a73e8", fontWeight: 600 }}>
            buraya tıklayın
          </Link>
          .
        </p>
      )}
    </div>
  );
}