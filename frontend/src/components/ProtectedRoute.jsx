import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import LoadingSpinner from './LoadingSpinner';

const ProtectedRoute = ({ children }) => {
    const { user, loading } = useAuth();
    const location = useLocation();

    if (loading) {
        return (
            <div className="min-h-screen flex justify-center items-center">
                <LoadingSpinner size="lg" color="green" />
            </div>
        );
    }

    if (!user) {
        // Redirect to login page but save the current location they were trying to go to
        return <Navigate to="/login" state={{ from: location }} replace />;
    }

    return children;
};

export default ProtectedRoute;
