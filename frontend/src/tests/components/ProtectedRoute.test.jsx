import React from 'react';
import { render, screen } from '@testing-library/react';
import { MemoryRouter, Routes, Route } from 'react-router-dom';
import ProtectedRoute from '../../components/ProtectedRoute';
import { AuthContext } from '../../context/AuthContext';

describe('ProtectedRoute', () => {
    it('redirects to login if no token', () => {
        const contextValue = { accessToken: null };

        render(
            <AuthContext.Provider value={contextValue}>
                <MemoryRouter initialEntries={['/protected']}>
                    <Routes>
                        <Route path="/login" element={<div>Login Page</div>} />
                        <Route path="/protected" element={
                            <ProtectedRoute>
                                <div>Protected Content</div>
                            </ProtectedRoute>
                        } />
                    </Routes>
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByText('Login Page')).toBeInTheDocument();
        expect(screen.queryByText('Protected Content')).not.toBeInTheDocument();
    });

    it('renders children if token exists', () => {
        const contextValue = { accessToken: 'valid-token' };

        render(
            <AuthContext.Provider value={contextValue}>
                <MemoryRouter initialEntries={['/protected']}>
                    <ProtectedRoute>
                        <div>Protected Content</div>
                    </ProtectedRoute>
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByText('Protected Content')).toBeInTheDocument();
    });
});
