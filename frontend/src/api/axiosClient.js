import axios from 'axios';
import {API_CONFIG} from '../common/constants/apiConfig.js'

const axiosClient = axios.create({
    // Lấy URL từ file .env 
    baseURL: API_CONFIG.BASE_URL, 
    headers: {
        'Content-Type': 'application/json',
    },
    // Timeout sau 10s nếu mạng lag
    timeout: 10000, 
});

// 1. Interceptor gửi đi: Tự động gắn Token
axiosClient.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('access_token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);

// 2. Interceptor nhận về: Làm gọn data
axiosClient.interceptors.response.use(
    (response) => {
        // Trả về thẳng data, không cần gọi response.data ở component nữa
        return response.data;
    },
    (error) => {
        // Xử lý lỗi chung (VD: 401 thì đá về trang login)
        if (error.response?.status === 401) {
            localStorage.removeItem('access_token');
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);

export default axiosClient;