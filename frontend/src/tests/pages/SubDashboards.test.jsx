import { render, screen } from "@testing-library/react";
import StudentDashboard from "../../pages/StudentDashboard";
import FacultyDashboard from "../../pages/FacultyDashboard";
import AdminDashboard from "../../pages/AdminDashboard";
import { AuthContext } from "../../context/AuthContext";
import React from "react";

const mockUser = {
    firstName: "Test",
    lastName: "User",
    role: "student",
};

const renderWithAuth = (Component, user = mockUser) => {
    return render(
        <AuthContext.Provider value={{ user, updateUser: jest.fn() }}>
            <Component />
        </AuthContext.Provider>
    );
};

describe("Sub-Dashboards", () => {

    // --- Student Dashboard ---
    describe("StudentDashboard", () => {
        test("renders student specific content", () => {
            renderWithAuth(StudentDashboard, { ...mockUser, role: "student" });

            expect(screen.getByText("Öğrenci Paneli")).toBeInTheDocument();
            expect(screen.getByText(/Hoş geldin, ./)).toHaveTextContent("Test");
            expect(screen.getByText("Bu Dönem Ders Sayısı")).toBeInTheDocument();
            expect(screen.getByText("Devamsızlık Durumu")).toBeInTheDocument();
        });
    });

    // --- Faculty Dashboard ---
    describe("FacultyDashboard", () => {
        test("renders faculty specific content", () => {
            renderWithAuth(FacultyDashboard, { ...mockUser, role: "faculty" });

            expect(screen.getByText("Öğretim Üyesi Paneli")).toBeInTheDocument();
            expect(screen.getByText(/Hoş geldiniz, ./)).toHaveTextContent("Test User");
            expect(screen.getByText("Bu Dönem Verdiğiniz Dersler")).toBeInTheDocument();
            expect(screen.getByText("Yoklama Oturumu Aç")).toBeInTheDocument();
        });
    });

    // --- Admin Dashboard ---
    describe("AdminDashboard", () => {
        test("renders admin specific content", () => {
            renderWithAuth(AdminDashboard, { ...mockUser, role: "admin" });

            expect(screen.getByText("Yönetici Paneli")).toBeInTheDocument();
            expect(screen.getByText(/Hoş geldiniz, ./)).toHaveTextContent("Test User");
            expect(screen.getByText("Toplam Öğrenci")).toBeInTheDocument();
            expect(screen.getByText("Kullanıcı Yönetimi")).toBeInTheDocument();
        });
    });
});
