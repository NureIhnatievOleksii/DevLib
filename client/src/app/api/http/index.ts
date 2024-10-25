import axios from "axios";
/* import { AuthResponse } from "../../models/AuthResponse"; */

export const API_URL = /* process.env.REACT_APP_API_URL */ '../../../api/';

const $api = axios.create({
    withCredentials: true,
    baseURL: API_URL
});

$api.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${localStorage.getItem('token')}`;
    return config;
}, (error) => {
    return Promise.reject(error);
});
/* 
$api.interceptors.response.use((response) => {
    return response;
}, async (error) => {
    const originalRequest = error.config;

    if (error.response.status === 401 && error.config && !error.config._isRetry) {
        originalRequest._isRetry = true;
        try {
            const response = await axios.get<AuthResponse>(`${API_URL}/token/refresh`, {
                withCredentials: true, // отправляет куки
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            localStorage.setItem('token', response.data.accessToken);
            originalRequest.headers.Authorization = `Bearer ${response.data.accessToken}`;

            return $api.request(originalRequest);
        } catch (e) {
            console.log(e);
            // Здесь можно обрабатывать случай, если обновление токена не удалось
            // Например, редиректить на страницу логина
        }
    }

    return Promise.reject(error);
});
 */
export default $api;
