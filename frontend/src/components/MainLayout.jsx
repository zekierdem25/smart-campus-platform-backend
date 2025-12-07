// frontend/src/components/MainLayout.jsx
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { Link } from "react-router-dom";

export default function MainLayout({ children }) {
  const { user, logout } = useContext(AuthContext);

  return (
    <div className="app-layout">
      {/* ÜST BAR */}
      <header className="app-topbar">
        <div className="app-topbar-left">
          <div className="app-logo-dot" />
          <span>Akıllı Kampüs Platformu</span>
        </div>

        <div className="app-topbar-right">
          {user && (
            <>
              <span>
                {user.firstName} {user.lastName}
              </span>
              <span className="app-user-role">
                {user.role === "Admin"
                  ? "Yönetici"
                  : user.role === "Faculty"
                  ? "Öğretim Üyesi"
                  : "Öğrenci"}
              </span>

              <Link to="/profile" className="btn btn-secondary app-profile-link">
                Profilim
              </Link>
            </>
          )}

          <button className="btn btn-success" onClick={logout}>
            Çıkış yap
          </button>
        </div>
      </header>

      {/* SAYFA İÇERİĞİ */}
      <main className="app-content">{children}</main>
    </div>
  );
}