import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import ResetPassword from "../../pages/ResetPassword";
import api from "../../api/axios";
import { BrowserRouter, Route, Routes } from "react-router-dom"; // Use real router for params
import React from "react";

// Mock API
jest.mock("../../api/axios");

const renderReset = (token = "valid-token") => {
    // To test useParams, we need to wrap in Routes and navigate to URL with param
    window.history.pushState({}, "Test page", `/reset-password/${token}`);

    return render(
        <BrowserRouter>
            <Routes>
                <Route path="/reset-password/:token" element={<ResetPassword />} />
            </Routes>
        </BrowserRouter>
    );
};

describe("ResetPassword Page", () => {
    beforeEach(() => {
        jest.clearAllMocks();
    });

    test("renders correctly", () => {
        renderReset();
        expect(screen.getByText("Yeni Şifre Belirle")).toBeInTheDocument();
    });

    test("validates matching passwords", async () => {
        renderReset();

        fireEvent.change(screen.getByPlaceholderText("Yeni şifre"), { target: { value: "12345678" } });
        fireEvent.change(screen.getByPlaceholderText("Yeni şifre (tekrar)"), { target: { value: "87654321" } });

        fireEvent.click(screen.getByText("Şifreyi güncelle"));
        expect(await screen.findByText("Şifre ve şifre tekrarı eşleşmiyor.")).toBeInTheDocument();
    });

    test("submits new password successfully", async () => {
        api.post.mockResolvedValueOnce({ data: { success: true } });

        renderReset("mytoken123");

        fireEvent.change(screen.getByPlaceholderText("Yeni şifre"), { target: { value: "NewPass123" } });
        fireEvent.change(screen.getByPlaceholderText("Yeni şifre (tekrar)"), { target: { value: "NewPass123" } });

        fireEvent.click(screen.getByText("Şifreyi güncelle"));

        await waitFor(() => {
            expect(api.post).toHaveBeenCalledWith("/auth/reset-password", {
                token: "mytoken123",
                newPassword: "NewPass123",
                confirmPassword: "NewPass123"
            });
            expect(screen.getByText(/Şifreniz başarıyla güncellendi/i)).toBeInTheDocument();
        });
    });
});
