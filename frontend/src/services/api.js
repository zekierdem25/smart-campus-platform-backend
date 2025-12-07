import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5000/api/v1',
    headers: {
        'Content-Type': 'application/json',
    },
});

// Request interceptor to add auth token
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('accessToken');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Response interceptor to handle errors
let isRefreshing = false;
let failedQueue = [];

const processQueue = (error, token = null) => {
    failedQueue.forEach(prom => {
        if (error) {
            prom.reject(error);
        } else {
            prom.resolve(token);
        }
    });

    failedQueue = [];
};

api.interceptors.response.use(
    (response) => {
        return response;
    },
    async (error) => {
        const originalRequest = error.config;

        if (error.response && error.response.status === 401 && !originalRequest._retry) {
            if (originalRequest.url.includes('/auth/login') || originalRequest.url.includes('/auth/refresh')) {
                return Promise.reject(error);
            }

            if (isRefreshing) {
                return new Promise(function (resolve, reject) {
                    failedQueue.push({ resolve, reject });
                }).then(token => {
                    originalRequest.headers['Authorization'] = 'Bearer ' + token;
                    return api(originalRequest);
                }).catch(err => {
                    return Promise.reject(err);
                });
            }

            originalRequest._retry = true;
            isRefreshing = true;

            const refreshToken = localStorage.getItem('refreshToken');

            if (!refreshToken) {
                isRefreshing = false;
                localStorage.removeItem('accessToken');
                localStorage.removeItem('user');
                return Promise.reject(error);
            }

            try {
                const response = await api.post('/auth/refresh', { refreshToken });

                if (response.data.success) {
                    const { accessToken } = response.data;
                    localStorage.setItem('accessToken', accessToken);
                    // Update refresh token if returned (optional, depends on backend)
                    if (response.data.refreshToken) {
                        localStorage.setItem('refreshToken', response.data.refreshToken);
                    }

                    api.defaults.headers.common['Authorization'] = 'Bearer ' + accessToken;
                    processQueue(null, accessToken);

                    originalRequest.headers['Authorization'] = 'Bearer ' + accessToken;
                    return api(originalRequest);
                }
            } catch (err) {
                processQueue(err, null);
                localStorage.removeItem('accessToken');
                localStorage.removeItem('refreshToken');
                localStorage.removeItem('user');
                // Optional: window.location.href = '/login';
            } finally {
                isRefreshing = false;
            }
        }

        return Promise.reject(error);
    }
);

export default api;
