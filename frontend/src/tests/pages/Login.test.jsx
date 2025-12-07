import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import Login from '../../pages/Login';
import { AuthContext } from '../../context/AuthContext';

// Mock useNavigate
const mockNavigate = jest.fn();
jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: () => mockNavigate,
}));

describe('Login Page', () => {
    const mockLogin = jest.fn();

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('renders login form', () => {
        render(
            <AuthContext.Provider value={{ login: mockLogin }}>
                <MemoryRouter>
                    <Login />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        expect(screen.getByPlaceholderText('E-posta adresi')).toBeInTheDocument();
        expect(screen.getByPlaceholderText('Şifre')).toBeInTheDocument();
        expect(screen.getByText('Giriş yap')).toBeInTheDocument();
    });

    it('validates empty inputs', async () => {
        render(
            <AuthContext.Provider value={{ login: mockLogin }}>
                <MemoryRouter>
                    <Login />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        const submitBtn = screen.getByText('Giriş yap');
        fireEvent.submit(submitBtn.closest('form'));

        expect(await screen.findByText('Lütfen e-posta ve şifreyi doldurun.')).toBeInTheDocument();
        expect(mockLogin).not.toHaveBeenCalled();
    });

    it('calls login function on valid submission', async () => {
        mockLogin.mockResolvedValue({ ok: true });

        render(
            <AuthContext.Provider value={{ login: mockLogin }}>
                <MemoryRouter>
                    <Login />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        fireEvent.change(screen.getByPlaceholderText('E-posta adresi'), { target: { value: 'test@example.com' } });
        fireEvent.change(screen.getByPlaceholderText('Şifre'), { target: { value: 'password123' } });
        fireEvent.click(screen.getByText('Giriş yap'));

        expect(mockLogin).toHaveBeenCalledWith('test@example.com', 'password123');

        await waitFor(() => {
            expect(mockNavigate).toHaveBeenCalledWith('/dashboard');
        });
    });

    it('displays error on failed login', async () => {
        mockLogin.mockResolvedValue({ ok: false, message: 'Invalid credentials' });

        render(
            <AuthContext.Provider value={{ login: mockLogin }}>
                <MemoryRouter>
                    <Login />
                </MemoryRouter>
            </AuthContext.Provider>
        );

        fireEvent.change(screen.getByPlaceholderText('E-posta adresi'), { target: { value: 'test@example.com' } });
        fireEvent.change(screen.getByPlaceholderText('Şifre'), { target: { value: 'wrongpass' } });
        fireEvent.click(screen.getByText('Giriş yap'));

        expect(await screen.findByText('Invalid credentials')).toBeInTheDocument();
        expect(mockNavigate).not.toHaveBeenCalled();
    });
});
