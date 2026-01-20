// Cấu hình chung
export const API_CONFIG = Object.freeze({
  BASE_URL: import.meta.env.VITE_API_URL,
  TIMEOUT: 15000, 
});

// Danh sách Endpoints
export const API_ENDPOINTS = Object.freeze({
  
  // 1. Nhóm Order (Quản lý đơn hàng)
  ORDER: {
    INDEX: '/Order', 
  },

  // 2. Nhóm Product (Quản lý sản phẩm)
  PRODUCT: {
    INDEX: '/Product', 
  },

  // 3. Nhóm Upload (Upload file/ảnh)
  UPLOAD: {
    INDEX: '/Upload',
  },
});