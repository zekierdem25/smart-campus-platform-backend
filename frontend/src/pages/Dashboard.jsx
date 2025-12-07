// frontend/src/pages/Dashboard.jsx
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import AdminDashboard from "./AdminDashboard";
import FacultyDashboard from "./FacultyDashboard";
import StudentDashboard from "./StudentDashboard";
import MainLayout from "../components/MainLayout";

export default function Dashboard() {
  const { user } = useContext(AuthContext);

  if (!user) {
    return (
      <MainLayout>
        <p>Panel yükleniyor...</p>
      </MainLayout>
    );
  }

  const role = (user.role || "").toLowerCase();
  console.log("Dashboard user role:", role, user);

  switch (role) {
    case "admin":
      return (
        <MainLayout>
          <AdminDashboard user={user} />
        </MainLayout>
      );

    case "faculty":
      return (
        <MainLayout>
          <FacultyDashboard user={user} />
        </MainLayout>
      );

    case "student":
      return (
        <MainLayout>
          <StudentDashboard user={user} />
        </MainLayout>
      );

    default:
      return (
        <MainLayout>
          <p>Bilinmeyen rol: {role}</p>
        </MainLayout>
      );
  }
}
