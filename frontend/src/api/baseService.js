import axiosClient from './axiosClient';

export default class BaseService {
    constructor(endpoint) {
        // endpoint ví dụ: '/users', '/products'
        this.endpoint = endpoint;
    }

    /**
     * Lấy tất cả (có thể truyền params phân trang, search)
     * VD: getAll({ page: 1, pageSize: 10, keyword: 'Admin' })
     */
    getAll(params = {}) {
        return axiosClient.get(this.endpoint, { params });
    }

    /**
     * Lấy chi tiết theo ID
     */
    getById(id) {
        return axiosClient.get(`${this.endpoint}/${id}`);
    }

    /**
     * Tạo mới
     */
    create(data) {
        return axiosClient.post(this.endpoint, data);
    }

    /**
     * Cập nhật
     */
    update(id, data) {
        return axiosClient.put(`${this.endpoint}/${id}`, data);
    }

    /**
     * Xóa
     */
    delete(id) {
        return axiosClient.delete(`${this.endpoint}/${id}`);
    }
}