import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import Profile from "../../pages/Profile";
import { AuthContext } from "../../context/AuthContext";
import api from "../../api/axios";
import React from "react";

// Mock API
jest.mock("../../api/axios");

// Mock URL.createObjectURL
global.URL.createObjectURL = jest.fn(() => "mock-blob-url");
global.URL.revokeObjectURL = jest.fn();

const mockUser = {
    id: "123",
    firstName: "Test",
    lastName: "User",
    email: "test@example.com",
    role: "student",
    phone: "1234567890",
    profilePictureUrl: null,
    studentInfo: {
        studentNumber: "ST123",
        departmentName: "Computer Engineering",
    },
};

const mockUpdateUser = jest.fn();

const renderProfile = (user = mockUser) => {
    return render(
        <AuthContext.Provider value={{ user, updateUser: mockUpdateUser }}>
            <Profile />
        </AuthContext.Provider>
    );
};

describe("Profile Page", () => {
    beforeEach(() => {
        jest.clearAllMocks();
    });

    test("renders user information correctly", () => {
        renderProfile();
        expect(screen.getByText("Test User")).toBeInTheDocument();
        expect(screen.getByText("test@example.com")).toBeInTheDocument();
        expect(screen.getByDisplayValue("Test")).toBeInTheDocument(); // firstName input
        expect(screen.getByDisplayValue("1234567890")).toBeInTheDocument(); // phone input
    });

    test("updates profile information successfully", async () => {
        api.put.mockResolvedValueOnce({ data: { user: { ...mockUser, firstName: "Updated" } } });
        renderProfile();

        const nameInput = screen.getByDisplayValue("Test");
        fireEvent.change(nameInput, { target: { value: "Updated" } });

        const saveButton = screen.getByText("Bilgileri Güncelle");
        fireEvent.click(saveButton);

        await waitFor(() => {
            expect(api.put).toHaveBeenCalledWith("/users/me", expect.objectContaining({ firstName: "Updated" }));
            expect(mockUpdateUser).toHaveBeenCalled();
        });
    });

    test("handles photo selection and upload", async () => {
        api.post.mockResolvedValueOnce({ data: { success: true } });
        api.get.mockResolvedValueOnce({ data: { data: { ...mockUser, profilePictureUrl: "/uploads/new.jpg" } } });

        const { container } = renderProfile();

        // Find file input
        const input = container.querySelector('input[type="file"]');
        const file = new File(["dummy"], "test.png", { type: "image/png" });

        // Select file
        fireEvent.change(input, { target: { files: [file] } });

        expect(global.URL.createObjectURL).toHaveBeenCalledWith(file);
        expect(screen.getByText("Fotoğraf seçildi. Yüklemek için butona basınız.")).toBeInTheDocument();

        // Click upload button ("Onayla ve Yükle")
        const uploadButton = screen.getByText("Onayla ve Yükle");
        fireEvent.click(uploadButton);

        await waitFor(() => {
            expect(api.post).toHaveBeenCalledWith("/users/me/profile-picture", expect.any(FormData));
            expect(api.get).toHaveBeenCalledWith("/users/me");
            expect(mockUpdateUser).toHaveBeenCalled();
            expect(screen.getByText("Profil fotoğrafı güncellendi!")).toBeInTheDocument();
        });
    });

    test("validates password mismatch", async () => {
        const { container } = renderProfile();

        fireEvent.change(container.querySelector('input[name="new"]'), { target: { value: "123456" } });
        fireEvent.change(container.querySelector('input[name="newAgain"]'), { target: { value: "654321" } });
        fireEvent.change(container.querySelector('input[name="current"]'), { target: { value: "current" } });

        fireEvent.click(screen.getByText("Şifreyi Güncelle"));

        expect(await screen.findByText("Yeni şifreler eşleşmiyor.")).toBeInTheDocument();
    });

    test("submits password change successfully", async () => {
        api.post.mockResolvedValueOnce({ data: { success: true } });
        // alert mock
        const alertMock = jest.spyOn(window, "alert").mockImplementation(() => { });

        const { container } = renderProfile();

        fireEvent.change(container.querySelector('input[name="current"]'), { target: { value: "currentPass" } });
        fireEvent.change(container.querySelector('input[name="new"]'), { target: { value: "NewPass123" } });
        fireEvent.change(container.querySelector('input[name="newAgain"]'), { target: { value: "NewPass123" } });

        fireEvent.click(screen.getByText("Şifreyi Güncelle"));

        await waitFor(() => {
            expect(api.post).toHaveBeenCalledWith("/users/me/change-password", {
                currentPassword: "currentPass",
                newPassword: "NewPass123",
                confirmPassword: "NewPass123"
            });
            expect(alertMock).toHaveBeenCalledWith("Şifreniz başarıyla güncellendi!");
        });

        alertMock.mockRestore();
    });
});
