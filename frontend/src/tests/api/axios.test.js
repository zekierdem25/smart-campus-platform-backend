import api from "../../api/axios";
import axios from "axios";

jest.mock("axios", () => {
    const mockCreate = jest.fn(() => {
        return {
            interceptors: {
                request: {
                    use: jest.fn(),
                    eject: jest.fn(),
                },
                response: {
                    use: jest.fn(),
                    eject: jest.fn(),
                }
            },
        };
    });
    return {
        create: mockCreate,
    };
});

describe("Axios Instance", () => {
    let interceptorCallback;

    beforeEach(() => {
        jest.clearAllMocks();
        localStorage.clear();

        // Re-import to trigger create and interceptor registration logic if needed,
        // but typically module is cached. Only needed if we want to capture the callback passed to 'use'.
        // Since we can't easily reset module cache in Jest without isolation, we rely on the fact that
        // the mock setup above captures the interceptor when the file is first imported.
        // However, since the file is already imported potentially, we might need a way to access the specific instance created.

        // Actually, we are testing the *logic* inside the interceptor. 
        // A common pattern is to test if the header is added.
        // But since we mock axios.create, the 'api' export is the mock object returned by axios.create.
        // We can inspect the arguments passed to 'use'.
    });

    // Strategy: We want to get the function passed to api.interceptors.request.use
    // and call it with different configs.

    test("interceptor adds Authorization header if token exists", () => {
        // Access the mock instance returned by create. 
        // Since 'api' is the exported instance, and we mocked 'create' to return an object with interceptors...
        // 'api' IS that object.

        const requestUseMock = api.interceptors.request.use;
        // The file executes 'use' immediately on import.
        expect(requestUseMock).toHaveBeenCalled();

        // Get the success callback (first argument)
        const requestInterceptor = requestUseMock.mock.calls[0][0];

        localStorage.setItem("accessToken", "fake-token");
        const config = { headers: {} };

        const result = requestInterceptor(config);

        expect(result.headers.Authorization).toBe("Bearer fake-token");
    });

    test("interceptor does not add Authorization header if no token", () => {
        const requestUseMock = api.interceptors.request.use;
        const requestInterceptor = requestUseMock.mock.calls[0][0];

        localStorage.removeItem("accessToken");
        const config = { headers: {} };

        const result = requestInterceptor(config);

        expect(result.headers.Authorization).toBeUndefined();
    });
});
