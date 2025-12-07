import { render, screen } from "@testing-library/react";
import App from "../App";
import { AuthContext } from "../context/AuthContext";
import React from "react";

// Since App has its own Router, we don't wrap it in one.
// But we need to mock AuthProvider if we want to bypass context errors, 
// or simply wrap App in a provider. App.jsx does NOT contain AuthProvider (index.jsx does).

const renderApp = (user = null) => {
    return render(
        <AuthContext.Provider value={{ user, accessToken: user ? "valid-token" : null, updateUser: jest.fn() }}>
            <App />
        </AuthContext.Provider>
    );
};

describe("App Routing", () => {
    test("redirects unauthenticated user to login from root", () => {
        // By default, App renders default route logic.
        // We can't easily set initial route for Browser Router inside App without specialized mocks or just testing behaviour.
        // App uses BrowserRouter, which uses window.location.

        window.history.pushState({}, "Test page", "/");
        renderApp(null);

        // Should redirect to login
        expect(screen.getByText(/Giriş Yap/i)).toBeInTheDocument();
    });

    test("accessing protected route without token redirects to login", () => {
        window.history.pushState({}, "Test page", "/dashboard");
        renderApp(null);

        expect(screen.getByText(/Giriş Yap/i)).toBeInTheDocument();
    });

    // Note: Testing successful protected route access is tricky because BrowserRouter ignores history pushState updates 
    // unless configured properly or using MemoryRouter. 
    // Since App.jsx hardcodes BrowserRouter, integration testing of specific routes is harder without generic Router dependency injection.
    // However, for basic smoke testing, the above is good.
});
