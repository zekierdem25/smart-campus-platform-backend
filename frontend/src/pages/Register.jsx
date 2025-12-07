import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import api from "../api/axios";

export default function Register() {
  const navigate = useNavigate();

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [userType, setUserType] = useState("student"); // "student" | "faculty"
  const [studentNumber, setStudentNumber] = useState("");
  const [department, setDepartment] = useState("");
  const [termsAccepted, setTermsAccepted] = useState(false);

  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  // Basit departman listesi (UI için)
  const departments = [
    { value: "", label: "Bölüm seçiniz" },
    { value: "ceng", label: "Bilgisayar Mühendisliği" },
    { value: "ee", label: "Elektrik-Elektronik Mühendisliği" },
    { value: "me", label: "Makine Mühendisliği" },
  ];

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");
    setSuccess("");

    // ---- Form doğrulamaları ----
    if (!firstName || !lastName || !email || !password || !confirmPassword) {
      setError("Lütfen tüm zorunlu alanları doldurun.");
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

    if (!department) {
      setError("Lütfen bir bölüm seçiniz.");
      return;
    }

    if (userType === "student" && !studentNumber) {
      setError("Öğrenci türü seçildi, öğrenci numarası zorunludur.");
      return;
    }

    if (!termsAccepted) {
      setError("Kayıt olabilmek için şartları ve koşulları kabul etmelisiniz.");
      return;
    }

    setIsSubmitting(true);

    try {
      const DEFAULT_DEPARTMENT_ID = "11111111-1111-1111-1111-111111111111";

      const payload = {
        firstName,
        lastName,
        email,
        password,
        confirmPassword,
        userType: userType === "student" ? "Student" : "Faculty",
        studentNumber: userType === "student" ? studentNumber : null,
        departmentId: DEFAULT_DEPARTMENT_ID,
      };

      console.log("Register payload:", payload);

      const res = await api.post("/auth/register", payload);

      if (!res.data?.success) {
        setError(res.data?.message || "Kayıt sırasında bir hata oluştu.");
      } else {
        setSuccess(
          "Kayıt başarılı! Lütfen e-posta adresinize gönderilen doğrulama linkini onaylayın."
        );

        // Formu temizle
        setFirstName("");
        setLastName("");
        setEmail("");
        setPassword("");
        setConfirmPassword("");
        setStudentNumber("");
        setDepartment("");
        setTermsAccepted(false);

        // 3 saniye sonra login sayfasına yönlendir
        setTimeout(() => {
          navigate("/login");
        }, 3000);
      }
    } catch (err) {
      console.error("Register error:", err);
      const msg =
        err.response?.data?.message ||
        "Sunucu hatası. Lütfen daha sonra tekrar deneyin.";
      setError(msg);
    } finally {
      setIsSubmitting(false);
    }
  }

  return (
    <div className="card">
      <h2 className="title">Kayıt Ol</h2>

      <form onSubmit={handleSubmit}>
              {/* Ad */}
      <input
        className="input"
        type="text"
        placeholder="Ad"
        value={firstName}
        onChange={(e) => setFirstName(e.target.value)}
      />

      {/* Soyad */}
      <input
        className="input"
        type="text"
        placeholder="Soyad"
        value={lastName}
        onChange={(e) => setLastName(e.target.value)}
        style={{ marginTop: 8 }}
      />

        {/* E-posta */}
        <input
          className="input"
          type="email"
          placeholder="E-posta adresi"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          style={{ marginTop: 8 }}
        />

        {/* Şifre */}
        <input
          className="input"
          type="password"
          placeholder="Şifre"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          style={{ marginTop: 8 }}
        />

        {/* Şifre tekrar */}
        <input
          className="input"
          type="password"
          placeholder="Şifre (tekrar)"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          style={{ marginTop: 8 }}
        />

        {/* Kullanıcı türü */}
        <div
          style={{
            display: "flex",
            gap: 16,
            marginTop: 10,
            fontSize: 14,
            alignItems: "center",
          }}
        >
          <span>Tür:</span>
          <label>
            <input
              type="radio"
              name="userType"
              value="student"
              checked={userType === "student"}
              onChange={(e) => setUserType(e.target.value)}
            />{" "}
            Öğrenci
          </label>
          <label>
            <input
              type="radio"
              name="userType"
              value="faculty"
              checked={userType === "faculty"}
              onChange={(e) => setUserType(e.target.value)}
            />{" "}
            Öğretim Üyesi
          </label>
        </div>

        {/* Öğrenci numarası sadece öğrenci ise */}
        {userType === "student" && (
          <input
            className="input"
            type="text"
            placeholder="Öğrenci Numarası"
            value={studentNumber}
            onChange={(e) => setStudentNumber(e.target.value)}
            style={{ marginTop: 8 }}
          />
        )}

        {/* Bölüm seçimi */}
        <select
          className="input"
          value={department}
          onChange={(e) => setDepartment(e.target.value)}
          style={{ marginTop: 8 }}
        >
          {departments.map((d) => (
            <option key={d.value} value={d.value}>
              {d.label}
            </option>
          ))}
        </select>

        {/* Şartlar & koşullar */}
        <label
          style={{
            display: "flex",
            alignItems: "center",
            gap: 8,
            marginTop: 10,
            fontSize: 13,
            lineHeight: 1.4,
          }}
        >
          <input
            type="checkbox"
            checked={termsAccepted}
            onChange={(e) => setTermsAccepted(e.target.checked)}
          />
          <span>
            Kayıt olarak{" "}
            <strong>Kullanım Koşulları</strong>nı ve{" "}
            <strong>Gizlilik Politikasını</strong> kabul ediyorum.
          </span>
        </label>

        {/* Hata / başarı mesajları */}
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
          {isSubmitting ? "Kayıt yapılıyor..." : "Kayıt ol"}
        </button>
      </form>

      <p style={{ marginTop: 15 }}>
        Zaten hesabın var mı?{" "}
        <Link to="/login" style={{ color: "#1a73e8", fontWeight: 600 }}>
          Giriş yap
        </Link>
      </p>
    </div>
  );
}
