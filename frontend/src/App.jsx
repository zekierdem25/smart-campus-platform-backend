import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Login';
import Register from './pages/Register';
import VerifyEmail from './pages/VerifyEmail';
import VerificationPending from './pages/VerificationPending';
import ForgotPassword from './pages/ForgotPassword';
import ResetPassword from './pages/ResetPassword';
import Dashboard from './pages/Dashboard';
import Profile from './pages/Profile';
import ProtectedRoute from './components/ProtectedRoute';
import './App.css';

import { AuthProvider } from './context/AuthContext';
import NotFound from './pages/NotFound';

function App() {
  return (
    <Router>
      <AuthProvider>
        <div className="App">
          <Routes>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/verify-email/:token" element={<VerifyEmail />} />
            <Route path="/verification-pending" element={<VerificationPending />} />
            <Route path="/forgot-password" element={<ForgotPassword />} />
            <Route path="/reset-password/:token" element={<ResetPassword />} />

            {/* Protected Routes */}
            <Route path="/dashboard" element={
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            } />
            <Route path="/profile" element={
              <ProtectedRoute>
                <Profile />
              </ProtectedRoute>
            } />

            <Route path="/" element={<Navigate to="/login" replace />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </div>
      </AuthProvider>
    </Router>
  );
}

export default App;

