import React, { useContext } from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { AuthContext, AuthProvider } from '../../context/AuthContext';
import api from '../../api/axios';

// Mock axios
jest.mock('../../api/axios');

// Test component to consume context
const TestComponent = () => {
    const { user, login, logout, accessToken } = useContext(AuthContext);
    return (
        <div>
            <div data-testid="token">{accessToken}</div>
            <div data-testid="user">{user ? user.email : 'No User'}</div>
            <button onClick={() => login('test@example.com', 'password')}>Login</button>
            <button onClick={logout}>Logout</button>
        </div>
    );
};

describe('AuthContext', () => {
    beforeEach(() => {
        jest.clearAllMocks();
        localStorage.clear();
    });

    it('provides initial state', () => {
        render(
            <AuthProvider>
                <TestComponent />
            </AuthProvider>
        );

        expect(screen.getByTestId('token')).toBeEmptyDOMElement();
        expect(screen.getByTestId('user')).toHaveTextContent('No User');
    });

    it('login success updates state and localStorage', async () => {
        api.post.mockResolvedValue({
            data: {
                success: true,
                accessToken: 'fake-token',
                message: 'Login successful'
            }
        });

        // Mock fetchMe call that happens after login/token change
        api.get.mockResolvedValue({
            data: {
                data: { email: 'test@example.com', role: 'Student' } // structure based on code
            }
        });

        render(
            <AuthProvider>
                <TestComponent />
            </AuthProvider>
        );

        await userEvent.click(screen.getByText('Login'));

        await waitFor(() => {
            expect(screen.getByTestId('token')).toHaveTextContent('fake-token');
        });

        expect(localStorage.getItem('accessToken')).toBe('fake-token');

        // Check if fetchMe was called and user updated (via useEffect)
        // We need to wait for the useEffect to fire
        await waitFor(() => {
            expect(screen.getByTestId('user')).toHaveTextContent('test@example.com');
        });
    });

    it('login failure does not update state', async () => {
        api.post.mockResolvedValue({
            data: {
                success: false,
                message: 'Invalid credentials'
            }
        });

        render(
            <AuthProvider>
                <TestComponent />
            </AuthProvider>
        );

        await userEvent.click(screen.getByText('Login'));

        expect(screen.getByTestId('token')).toBeEmptyDOMElement();
        expect(localStorage.getItem('accessToken')).toBeNull();
    });

    it('logout clears state and localStorage', async () => {
        // Setup initial logged in state
        localStorage.setItem('accessToken', 'initial-token');

        // Mock user fetch for initial load
        api.get.mockResolvedValue({
            data: {
                data: { email: 'test@example.com' }
            }
        });

        render(
            <AuthProvider>
                <TestComponent />
            </AuthProvider>
        );

        // Verify initial load
        await waitFor(() => {
            expect(screen.getByTestId('user')).toHaveTextContent('test@example.com');
        });

        // Logout
        await userEvent.click(screen.getByText('Logout'));

        expect(screen.getByTestId('token')).toBeEmptyDOMElement();
        expect(screen.getByTestId('user')).toHaveTextContent('No User');
        expect(localStorage.getItem('accessToken')).toBeNull();
    });
});
