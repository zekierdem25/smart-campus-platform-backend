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
// --- LOGIN ---
async function login(email, password) {
  try {
    const res = await api.post("/auth/login", { email, password });

    // Backend genel olarak ApiResponseDto döndürüyor:
    // { success: bool, message: string, data: ..., accessToken: "..." }
    if (!res.data.success || !res.data.accessToken) {
      return {
        ok: false,
        message:
          res.data.message ||
          "Giriş başarısız. Lütfen e-posta adresinizi ve şifrenizi kontrol edin.",
      };
    }

    const token = res.data.accessToken;

    // 1) Önce localStorage'a yaz
    localStorage.setItem("accessToken", token);

    // 2) Sonra state'i güncelle
    setAccessToken(token);

    // 3) fetchMe çağrısını burada manuel tetiklemiyoruz;
    // accessToken değiştiğinde useEffect içindeki fetchMe otomatik çalışıyor.

    return {
      ok: true,
      message: "Giriş başarılı. Yönlendiriliyorsunuz...",
    };
  } catch (err) {
    console.error("✗ Login error:", err);
    const message =
      err?.response?.data?.message ||
      "Giriş sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyin.";
    return {
      ok: false,
      message,
    };
  }
}


  function logout() {
    console.log("→ logout()");
    localStorage.removeItem("accessToken");
    setAccessToken(null);
    setUser(null);
  }

  // 🔹 Profil güncellemeden sonra global user bilgisini yenilemek için
  function updateUser(updatedUser) {
    setUser(updatedUser);
  }

    return (
    <AuthContext.Provider
      value={{
        user,
        accessToken,
        login,
        logout,
        updateUser, // ⬅️ bunu ekledik
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}
