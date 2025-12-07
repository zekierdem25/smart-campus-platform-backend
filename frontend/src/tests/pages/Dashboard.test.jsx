import React from 'react';
import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import Dashboard from '../../pages/Dashboard';
import { AuthContext } from '../../context/AuthContext';

// Mock child dashboards to verify switching logic
jest.mock('../../pages/AdminDashboard', () => () => <div data-testid="admin-dashboard">Admin Dashboard</div>);
jest.mock('../../pages/FacultyDashboard', () => () => <div data-testid="faculty-dashboard">Faculty Dashboard</div>);
jest.mock('../../pages/StudentDashboard', () => () => <div data-testid="student-dashboard">Student Dashboard</div>);
jest.mock('../../components/MainLayout', () => ({ children }) => <div data-testid="main-layout">{children}</div>);

describe('Dashboard Page', () => {
    it('renders loading state when user is null', () => {
        render(
            <AuthContext.Provider value={{ user: null }}>
                <MemoryRouter>
                    <Dashboard />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByText('Panel yükleniyor...')).toBeInTheDocument();
    });

    it('renders AdminDashboard for admin role', () => {
        const user = { role: 'Admin', email: 'admin@example.com' };
        render(
            <AuthContext.Provider value={{ user }}>
                <MemoryRouter>
                    <Dashboard />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByTestId('admin-dashboard')).toBeInTheDocument();
    });

    it('renders FacultyDashboard for faculty role', () => {
        const user = { role: 'Faculty', email: 'prof@example.com' };
        render(
            <AuthContext.Provider value={{ user }}>
                <MemoryRouter>
                    <Dashboard />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByTestId('faculty-dashboard')).toBeInTheDocument();
    });

    it('renders StudentDashboard for student role', () => {
        const user = { role: 'Student', email: 'student@example.com' };
        render(
            <AuthContext.Provider value={{ user }}>
                <MemoryRouter>
                    <Dashboard />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByTestId('student-dashboard')).toBeInTheDocument();
    });

    it('renders error for unknown role', () => {
        const user = { role: 'Unknown', email: 'unknown@example.com' };
        render(
            <AuthContext.Provider value={{ user }}>
                <MemoryRouter>
                    <Dashboard />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByText(/Bilinmeyen rol/i)).toBeInTheDocument();
    });
});
