// frontend/src/pages/Dashboard.jsx
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import AdminDashboard from "./AdminDashboard";
import FacultyDashboard from "./FacultyDashboard";
import StudentDashboard from "./StudentDashboard";

export default function Dashboard() {
  const { user } = useContext(AuthContext);

  if (!user) {
    return <p>Loading dashboard...</p>;
  }

  const role = (user.role || "").toLowerCase();
  console.log("Dashboard user role:", role, user);

  switch (role) {
    case "admin":
      return <AdminDashboard user={user} />;

    case "faculty":
      return <FacultyDashboard user={user} />;

    case "student":
      return <StudentDashboard user={user} />;

    default:
      return <p>Unknown role: {role}</p>;
  }
}
