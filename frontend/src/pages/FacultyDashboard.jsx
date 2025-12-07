export default function FacultyDashboard({ user }) {
  return (
    <div>
      <h2>Faculty Dashboard</h2>
      <p>Welcome, {user.firstName}! Here is your teaching summary.</p>

      <div className="dashboard-grid">
        <div className="dashboard-card">
          <div className="dashboard-card-title">Courses Taught</div>
          <div className="dashboard-card-value">4</div>
          <div className="dashboard-card-sub">Current semester</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Pending Approvals</div>
          <div className="dashboard-card-value">7</div>
          <div className="dashboard-card-sub">Student requests</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Meetings Today</div>
          <div className="dashboard-card-value">3</div>
          <div className="dashboard-card-sub">Scheduled in your calendar</div>
        </div>
      </div>
    </div>
  );
}
