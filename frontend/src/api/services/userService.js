import BaseService from '../baseService';
import axiosClient from '../axiosClient';

class UserService extends BaseService {
    constructor() {
        // Gọi constructor cha và set endpoint là '/users'
        // API thực tế sẽ gọi: https://localhost:7000/api/users
        super('/users'); 
    }

    // Hàm riêng: Đổi mật khẩu (BaseService không có cái này)
    changePassword(id, payload) {
        // payload = { oldPass, newPass }
        return axiosClient.post(`${this.endpoint}/${id}/change-password`, payload);
    }

    // Hàm riêng: Khóa tài khoản
    blockUser(id) {
        return axiosClient.post(`${this.endpoint}/${id}/block`);
    }
}

// Xuất ra 1 instance duy nhất (Singleton) để dùng toàn app
export default new UserService();