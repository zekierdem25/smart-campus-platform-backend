export default function AdminDashboard({ user }) {
  return (
    <div>
      <h2>Admin Dashboard</h2>
      <p>Welcome, {user.firstName}! Here is the campus overview.</p>

      <div className="dashboard-grid">
        <div className="dashboard-card">
          <div className="dashboard-card-title">Total Students</div>
          <div className="dashboard-card-value">1240</div>
          <div className="dashboard-card-sub">Active in the system</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Total Faculty</div>
          <div className="dashboard-card-value">85</div>
          <div className="dashboard-card-sub">Teaching staff</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Open Support Tickets</div>
          <div className="dashboard-card-value">12</div>
          <div className="dashboard-card-sub">Need attention</div>
        </div>
      </div>
    </div>
  );
}
