export default function StudentDashboard({ user }) {
  return (
    <div>
      <h2>Student Dashboard</h2>
      <p>Welcome, {user.firstName}! Here is your student overview.</p>

      <div className="dashboard-grid">
        <div className="dashboard-card">
          <div className="dashboard-card-title">Active Courses</div>
          <div className="dashboard-card-value">5</div>
          <div className="dashboard-card-sub">Courses you are enrolled in</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Upcoming Exams</div>
          <div className="dashboard-card-value">2</div>
          <div className="dashboard-card-sub">This week</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Unread Announcements</div>
          <div className="dashboard-card-value">3</div>
          <div className="dashboard-card-sub">From your instructors</div>
        </div>
      </div>
    </div>
  );
}
