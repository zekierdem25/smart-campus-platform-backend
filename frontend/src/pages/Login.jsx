import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate, Link } from "react-router-dom";

export default function Login() {
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [rememberMe, setRememberMe] = useState(false);
  const [isSubmitting, setIsSubmitting] = useState(false);

  async function handleSubmit(e) {
    e.preventDefault();
    console.log("Login form submitted");

    if (!login) {
      console.error("login fonksiyonu context içinde bulunamadı!");
      setError("İç hata: Giriş fonksiyonu bulunamadı.");
      return;
    }

    if (!email || !password) {
      setError("Lütfen e-posta ve şifreyi doldurun.");
      return;
    }

    setError("");
    setIsSubmitting(true);

    const result = await login(email, password);

    console.log("Login result in component:", result);

    setIsSubmitting(false);

    if (result.ok) {
      // İleride rememberMe true ise farklı bir davranış ekleyebiliriz.
      console.log("Remember me:", rememberMe);
      navigate("/dashboard");
    } else {
      setError(result.message || "Giriş başarısız. Lütfen tekrar deneyin.");
    }
  }

  return (
    <div className="card">
      <h2 className="title">Smart Campus Giriş</h2>

      <form onSubmit={handleSubmit}>
        <input
          className="input"
          type="email"
          placeholder="E-posta adresi"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />

        <input
          className="input"
          type="password"
          placeholder="Şifre"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />

        <div
          style={{
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
            marginTop: 8,
            marginBottom: 8,
            fontSize: 14,
          }}
        >
          <label style={{ display: "flex", alignItems: "center", gap: 6 }}>
            <input
              type="checkbox"
              checked={rememberMe}
              onChange={(e) => setRememberMe(e.target.checked)}
            />
            Beni hatırla
          </label>

          <Link
            to="/forgot-password"
            style={{ color: "#1a73e8", fontWeight: 500, textDecoration: "none" }}
          >
            Şifremi unuttum?
          </Link>
        </div>

        {error && (
          <p style={{ color: "red", marginBottom: 10 }}>
            {error}
          </p>
        )}

        <button className="btn btn-primary" type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Giriş yapılıyor..." : "Giriş yap"}
        </button>
      </form>

      <p style={{ marginTop: 15 }}>
        Hesabın yok mu?{" "}
        <Link to="/register" style={{ color: "#1a73e8", fontWeight: 600 }}>
          Kayıt ol
        </Link>
      </p>
    </div>
  );
}
