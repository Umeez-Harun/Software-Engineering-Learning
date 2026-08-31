import axios from 'axios'

const api = axios.create({
    baseURL: 'https://localhost:7158/api'

})

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token')
    if(token){
        config.headers.Authorization = `Bearer ${token}`
    }
    return config;
})

// Handle 401
async (error) => {
    if (
        error.response?.status === 401 &&
        !error.config._retry &&
        !error.config.url.includes("generate-access-token")
    ) {
        error.config._retry = true;

        try {
            const refreshToken = localStorage.getItem("refreshToken");
            const token = localStorage.getItem("token");

            const response = await api.post(
                "authentication/generate-access-token",
                {
                    token: token,
                    refreshToken: refreshToken
                }
            );

            localStorage.setItem("token", response.data.token);

            error.config.headers.Authorization =
                `Bearer ${response.data.token}`;

            return api(error.config);
        }
        catch (refreshError) {
            localStorage.removeItem("token");
            localStorage.removeItem("refreshToken");

            return Promise.reject(refreshError);
        }
    }

    return Promise.reject(error);
}

export default api;