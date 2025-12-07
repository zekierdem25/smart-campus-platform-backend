import React, { createContext, useState, useContext, useEffect } from 'react';
import authService from '../services/authService';

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const initAuth = () => {
            const currentUser = authService.getCurrentUser();
            if (currentUser) {
                setUser(currentUser);
            }
            setLoading(false);
        };

        initAuth();
    }, []);

    const login = async (email, password, rememberMe) => {
        const response = await authService.login(email, password, rememberMe);
        if (response.success) {
            setUser(response.user);
        }
        return response;
    };

    const logout = () => {
        authService.logout();
        setUser(null);
    };

    const updateUser = (userData) => {
        setUser(prev => ({ ...prev, ...userData }));
        // Also update local storage to keep them in sync
        const currentUser = JSON.parse(localStorage.getItem('user'));
        localStorage.setItem('user', JSON.stringify({ ...currentUser, ...userData }));
    }

    const value = {
        user,
        login,
        logout,
        updateUser,
        loading
    };

    return (
        <AuthContext.Provider value={value}>
            {!loading && children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    return useContext(AuthContext);
};
