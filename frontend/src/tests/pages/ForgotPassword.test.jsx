import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import ForgotPassword from "../../pages/ForgotPassword";
import api from "../../api/axios";
import { BrowserRouter } from "react-router-dom";
import React from "react";

// Mock API
jest.mock("../../api/axios");

const renderForgot = () => {
    return render(
        <BrowserRouter>
            <ForgotPassword />
        </BrowserRouter>
    );
};

describe("ForgotPassword Page", () => {
    beforeEach(() => {
        jest.clearAllMocks();
    });

    test("renders correctly", () => {
        renderForgot();
        expect(screen.getByText("Şifremi Unuttum")).toBeInTheDocument();
        expect(screen.getByPlaceholderText("E-posta adresi")).toBeInTheDocument();
    });

    test("validates empty email", async () => {
        renderForgot();
        fireEvent.click(screen.getByText("Sıfırlama bağlantısı gönder"));
        expect(await screen.findByText("Lütfen e-posta adresinizi girin.")).toBeInTheDocument();
    });

    test("submits email successfully", async () => {
        api.post.mockResolvedValueOnce({ data: { success: true } });
        renderForgot();

        const emailInput = screen.getByPlaceholderText("E-posta adresi");
        fireEvent.change(emailInput, { target: { value: "test@example.com" } });
        fireEvent.click(screen.getByText("Sıfırlama bağlantısı gönder"));

        await waitFor(() => {
            expect(api.post).toHaveBeenCalledWith("/auth/forgot-password", { email: "test@example.com" });
            expect(screen.getByText(/Şifre sıfırlama bağlantısı e-posta adresinize gönderildi/i)).toBeInTheDocument();
        });
    });

    test("handles API error", async () => {
        api.post.mockRejectedValueOnce({ response: { data: { message: "User not found" } } });
        renderForgot();

        const emailInput = screen.getByPlaceholderText("E-posta adresi");
        fireEvent.change(emailInput, { target: { value: "wrong@example.com" } });
        fireEvent.click(screen.getByText("Sıfırlama bağlantısı gönder"));

        await waitFor(() => {
            expect(screen.getByText("User not found")).toBeInTheDocument();
        });
    });
});
