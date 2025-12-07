import { createContext, useState, useEffect } from "react";
import api from "../api/axios";

export const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [accessToken, setAccessToken] = useState(
    localStorage.getItem("accessToken") || null
  );

  const [user, setUser] = useState(null);

  // Token değiştiğinde kullanıcı bilgilerini çek
  useEffect(() => {
    if (accessToken) {
      fetchMe();
    }
  }, [accessToken]);

  // --- KULLANICI BILGISI ÇEKME ---
  async function fetchMe() {
    try {
      console.log("→ GET /users/me");
      const res = await api.get("/users/me", {
        headers: { Authorization: `Bearer ${accessToken}` },
      });

      console.log("✓ Me response:", res.data);
      // Backend returns ApiResponseDto<UserResponseDto>, so the user object is in .data property
      setUser(res.data.data || res.data.user);
    } catch (err) {
      console.error("✗ Me fetch error:", err);
      logout();
    }
  }

  // --- LOGIN ---
  async function login(email, password) {
    try {
      const res = await api.post("/auth/login", { email, password });

      if (!res.data.success || !res.data.accessToken) {
        return { ok: false, message: res.data.message || "Login failed" };
      }

      const token = res.data.accessToken;

      // 1) Önce localStorage'a yaz
      localStorage.setItem("accessToken", token);

      // 2) Sonra state'i güncelle
      setAccessToken(token);

      // 3) FetchMe çağrısını state güncellendikten SONRA çalıştır
      // setAccessToken tetiklendiğinde useEffect devreye gireceği için
      // burada manuel fetchMe yapmaya gerek yok (hatta bu race condition yaratıyor).

      return { ok: true };
    } catch (err) {
      return {
        ok: false,
        message: err.response?.data?.message || "Login error",
      };
    }
  }


  function logout() {
    console.log("→ logout()");
    localStorage.removeItem("accessToken");
    setAccessToken(null);
    setUser(null);
  }

  return (
    <AuthContext.Provider
      value={{
        user,
        accessToken,
        login,
        logout,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}
