import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import MainLayout from '../../components/MainLayout';
import { AuthContext } from '../../context/AuthContext';

describe('MainLayout', () => {
    const mockLogout = jest.fn();

    it('renders title and children', () => {
        render(
            <AuthContext.Provider value={{ user: null, logout: mockLogout }}>
                <MemoryRouter>
                    <MainLayout>
                        <div data-testid="child-content">Child Content</div>
                    </MainLayout>
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByText('Akıllı Kampüs Platformu')).toBeInTheDocument();
        expect(screen.getByTestId('child-content')).toBeInTheDocument();
    });

    it('renders user info when logged in', () => {
        const user = { firstName: 'Ahmet', lastName: 'Yilmaz', role: 'Student' };
        render(
            <AuthContext.Provider value={{ user, logout: mockLogout }}>
                <MemoryRouter>
                    <MainLayout>
                        <div>Child</div>
                    </MainLayout>
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByText('Ahmet Yilmaz')).toBeInTheDocument();
        expect(screen.getByText('Öğrenci')).toBeInTheDocument();
        expect(screen.getByText('Profilim')).toBeInTheDocument();
    });

    it('renders correct role label for Admin', () => {
        const user = { firstName: 'Admin', lastName: 'User', role: 'Admin' };
        render(
            <AuthContext.Provider value={{ user, logout: mockLogout }}>
                <MemoryRouter>
                    <MainLayout><div>Child</div></MainLayout>
                </MemoryRouter>
            </AuthContext.Provider>
        );
        expect(screen.getByText('Yönetici')).toBeInTheDocument();
    });

    it('calls logout when logout button is clicked', () => {
        const user = { firstName: 'User', lastName: 'Test', role: 'Student' };
        render(
            <AuthContext.Provider value={{ user, logout: mockLogout }}>
                <MemoryRouter>
                    <MainLayout><div>Child</div></MainLayout>
                </MemoryRouter>
            </AuthContext.Provider>
        );

        fireEvent.click(screen.getByText('Çıkış yap'));
        expect(mockLogout).toHaveBeenCalled();
    });
});
