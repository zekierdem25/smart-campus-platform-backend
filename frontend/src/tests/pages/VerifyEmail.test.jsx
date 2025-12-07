import { render, screen, waitFor } from "@testing-library/react";
import VerifyEmail from "../../pages/VerifyEmail";
import api from "../../api/axios";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import React from "react";

// Mock API
jest.mock("../../api/axios");

const renderVerify = (token) => {
    const url = token ? `/verify-email/${token}` : '/verify-email';
    window.history.pushState({}, "Test page", url);
    return render(
        <BrowserRouter>
            <Routes>
                <Route path="/verify-email/:token?" element={<VerifyEmail />} />
            </Routes>
        </BrowserRouter>
    );
};

describe("VerifyEmail Page", () => {
    beforeEach(() => {
        jest.clearAllMocks();
    });

    test("shows success message on valid token", async () => {
        api.post.mockResolvedValueOnce({ data: { success: true } });

        renderVerify("valid-token");

        expect(screen.getByText("E-posta doğrulanıyor...")).toBeInTheDocument();

        await waitFor(() => {
            expect(api.post).toHaveBeenCalledWith("/auth/verify-email", { token: "valid-token" });
            expect(screen.getByText(/E-posta adresiniz başarıyla doğrulandı/i)).toBeInTheDocument();
        });
    });

    test("shows error message on invalid token", async () => {
        api.post.mockResolvedValueOnce({ data: { success: false, message: "Invalid token" } });

        renderVerify("invalid-token");

        await waitFor(() => {
            expect(screen.getByText("Invalid token")).toBeInTheDocument();
        });
    });
});
