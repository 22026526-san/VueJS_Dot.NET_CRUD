// Cấu hình chung cho việc gọi API
export const API_CONFIG = {
  // Lấy Base URL từ biến môi trường (file .env)
  BASE_URL: import.meta.env.VITE_API_URL,
  
  // Thời gian chờ tối đa (ms)
  TIMEOUT: 15000, 
};

// Danh sách các Endpoints (Đường dẫn tương đối)
export const API_ENDPOINTS = {
  // 1. Nhóm Auth (Đăng nhập, đăng ký)
  AUTH: {
    LOGIN: '/auth/login',
    REGISTER: '/auth/register',
    REFRESH_TOKEN: '/auth/refresh-token',
    LOGOUT: '/auth/logout',
    ME: '/auth/me', // Lấy thông tin user hiện tại
  },

  // 2. Nhóm User
  USER: {
    BASE: '/users', // Dùng cho CRUD chuẩn: GET, POST, PUT, DELETE /users
    CHANGE_PASSWORD: '/users/change-password',
    FORGOT_PASSWORD: '/users/forgot-password',
    PROFILE: '/users/profile',
  },

  // 3. Nhóm Product (Ví dụ nghiệp vụ)
  PRODUCT: {
    BASE: '/products',
    GET_FEATURED: '/products/featured',
    UPLOAD_IMAGE: '/products/upload-image',
  },

  // 4. Nhóm Common/Master Data (Danh mục, Tỉnh thành...)
  COMMON: {
    CATEGORIES: '/categories',
    LOCATIONS: '/locations',
  }
};