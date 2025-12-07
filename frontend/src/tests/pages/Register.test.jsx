import React from 'react';
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import Register from '../../pages/Register';
import api from '../../api/axios';

jest.mock('../../api/axios');

const mockNavigate = jest.fn();
jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: () => mockNavigate,
}));

describe('Register Page', () => {
    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('renders registration form', () => {
        render(
            <MemoryRouter>
                <Register />
            </MemoryRouter>
        );

        expect(screen.getByPlaceholderText('Ad')).toBeInTheDocument();
        expect(screen.getByPlaceholderText('Soyad')).toBeInTheDocument();
        expect(screen.getByPlaceholderText('E-posta adresi')).toBeInTheDocument();
        expect(screen.getByText('Kayıt ol')).toBeInTheDocument();
    });

    it('validates password mismatch', async () => {
        render(
            <MemoryRouter>
                <Register />
            </MemoryRouter>
        );

        fireEvent.change(screen.getByPlaceholderText('Ad'), { target: { value: 'John' } });
        fireEvent.change(screen.getByPlaceholderText('Soyad'), { target: { value: 'Doe' } });
        fireEvent.change(screen.getByPlaceholderText('E-posta adresi'), { target: { value: 'john@example.com' } });
        fireEvent.change(screen.getByPlaceholderText('Şifre'), { target: { value: 'password123' } });
        fireEvent.change(screen.getByPlaceholderText('Şifre (tekrar)'), { target: { value: 'mismatch' } });
        fireEvent.change(screen.getByRole('combobox'), { target: { value: 'ceng' } }); // Department
        fireEvent.change(screen.getByPlaceholderText('Öğrenci Numarası'), { target: { value: '12345' } });

        // Check terms
        // The checkbox text is complex, find by label text part or use getByText with regex/function
        const termsCheckbox = screen.getByRole('checkbox', { name: /kabul ediyorum/i });
        fireEvent.click(termsCheckbox);

        fireEvent.click(screen.getByText('Kayıt ol'));

        expect(await screen.findByText('Şifre ve şifre tekrarı eşleşmiyor.')).toBeInTheDocument();
        expect(api.post).not.toHaveBeenCalled();
    });

    it('submits valid form successfully', async () => {
        api.post.mockResolvedValue({ data: { success: true } });

        render(
            <MemoryRouter>
                <Register />
            </MemoryRouter>
        );

        // Fill form
        fireEvent.change(screen.getByPlaceholderText('Ad'), { target: { value: 'John' } });
        fireEvent.change(screen.getByPlaceholderText('Soyad'), { target: { value: 'Doe' } });
        fireEvent.change(screen.getByPlaceholderText('E-posta adresi'), { target: { value: 'john@example.com' } });
        // Password > 8 chars
        fireEvent.change(screen.getByPlaceholderText('Şifre'), { target: { value: 'password123' } });
        fireEvent.change(screen.getByPlaceholderText('Şifre (tekrar)'), { target: { value: 'password123' } });

        fireEvent.change(screen.getByRole('combobox'), { target: { value: 'ceng' } }); // Department
        fireEvent.change(screen.getByPlaceholderText('Öğrenci Numarası'), { target: { value: '12345' } });

        const termsCheckbox = screen.getByRole('checkbox', { name: /kabul ediyorum/i });
        fireEvent.click(termsCheckbox);

        fireEvent.click(screen.getByText('Kayıt ol'));

        await waitFor(() => {
            expect(api.post).toHaveBeenCalled();
        });

        expect(await screen.findByText(/Kayıt başarılı/i)).toBeInTheDocument();
    });
});
