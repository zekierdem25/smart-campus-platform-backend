import React from "react";
import ReactDOM from "react-dom/client";
import { AuthProvider } from "../context/AuthContext";
import App from "../App";

// Mock ReactDOM
jest.mock("react-dom/client", () => {
    const mRender = jest.fn();
    const mCreateRoot = jest.fn(() => ({ render: mRender }));
    return {
        __esModule: true,
        createRoot: mCreateRoot,
        default: {
            createRoot: mCreateRoot,
        },
    };
});

// Mock imported components to avoid deep rendering issues in a smoke test
jest.mock("../App", () => () => <div>App Component</div>);
jest.mock("../context/AuthContext", () => ({
    AuthProvider: ({ children }) => <div>{children}</div>,
}));

describe("Application Root", () => {
    beforeEach(() => {
        jest.resetModules();
        document.body.innerHTML = "";
    });

    test("renders without crashing", () => {
        const root = document.createElement("div");
        root.id = "root";
        document.body.appendChild(root);

        // Require index.jsx to trigger the render code
        require("../index.jsx");

        // Check call on the named export mock or capture from the spy if we saved it.
        // Since we defined the mock specifically, we can import it to assert.
        // However, require("react-dom/client") gives the module.

        const ReactDOMModule = require("react-dom/client");
        // ReactDOMModule.createRoot is the spy

        expect(ReactDOMModule.createRoot).toHaveBeenCalledWith(root);

        // Check render
        // failed: ReactDOMModule.createRoot.mock.results[0].value.render
        // But since we use the same mCreateRoot for default and named, we can check it.

        const rootInstance = ReactDOMModule.createRoot.mock.results[0].value;
        expect(rootInstance.render).toHaveBeenCalled();
    });
});
