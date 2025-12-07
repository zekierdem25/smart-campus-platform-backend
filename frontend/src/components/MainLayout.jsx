import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function MainLayout({ children }) {
  const { user, logout } = useContext(AuthContext);

  return (
    <div className="app-layout">
      <header className="app-topbar">
        <div className="app-topbar-left">
          <div className="app-logo-dot" />
          <span>Smart Campus Platform</span>
        </div>

        <div className="app-topbar-right">
          {user && (
            <>
              <span>
                {user.firstName} {user.lastName}
              </span>
              <span className="app-user-role">{user.role}</span>
            </>
          )}

          <button className="btn btn-success" onClick={logout}>
            Logout
          </button>
        </div>
      </header>

      <main className="app-content">{children}</main>
    </div>
  );
}
