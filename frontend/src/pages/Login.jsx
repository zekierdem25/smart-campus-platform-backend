import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate, Link } from "react-router-dom";

export default function Login() {
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();
    console.log("Login form submitted");

    if (!login) {
      console.error("login fonksiyonu context içinde bulunamadı!");
      setError("Internal error: login function missing");
      return;
    }

    const result = await login(email, password);

    console.log("Login result in component:", result);

    if (result.ok) {
      navigate("/dashboard");
    } else {
      setError(result.message || "Login failed");
    }
  }

  return (
    <div className="card">
      <h2 className="title">Smart Campus Login</h2>

      <form onSubmit={handleSubmit}>
        <input
          className="input"
          type="email"
          placeholder="Email Address"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />

        <input
          className="input"
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />

        {error && (
          <p style={{ color: "red", marginBottom: 10 }}>
            {error}
          </p>
        )}

        <button className="btn btn-primary" type="submit">
          Login
        </button>
      </form>

      <p style={{ marginTop: 15 }}>
        Don't have an account?{" "}
        <Link to="/register" style={{ color: "#1a73e8", fontWeight: 600 }}>
          Register
        </Link>
      </p>
    </div>
  );
}
