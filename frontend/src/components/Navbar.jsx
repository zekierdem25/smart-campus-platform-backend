import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Navbar = () => {
    const { user, logout } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    return (
        <nav className="bg-white shadow-sm">
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="flex justify-between h-16">
                    <div className="flex">
                        <div className="flex-shrink-0 flex items-center">
                            <Link to="/dashboard" className="text-xl font-bold text-campus-green">Smart Campus</Link>
                        </div>
                    </div>
                    {user && (
                        <div className="flex items-center space-x-4">
                            <Link to="/profile" className="text-gray-700 hover:text-campus-blue font-medium">
                                {user.fullName || user.firstName}
                            </Link>
                            <button
                                onClick={handleLogout}
                                className="text-gray-500 hover:text-red-600 font-medium"
                            >
                                Çıkış Yap
                            </button>
                        </div>
                    )}
                </div>
            </div>
        </nav>
    );
};

export default Navbar;
