import axiosClient from './axiosClient';
export default class BaseService {
  
  /**
   * @param {string} baseUrl - Đường dẫn gốc của API (VD: /api/Product)
   */
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
  }

  /**
   * Lấy danh sách (có phân trang, lọc, sắp xếp)
   * @param {object} params - Query params (VD: { page: 1, keyword: 'abc' })
   * @param {object} config - Cấu hình Axios bổ sung
   */
  getAll(params = {}, config = {}) {
    return axiosClient.get(this.baseUrl, { 
      params, 
      ...config 
    });
  }

  /**
   * Lấy chi tiết theo ID
   * @param {string|number} id - ID của đối tượng
   */
  getById(id, config = {}) {
    return axiosClient.get(`${this.baseUrl}/${id}`, config);
  }

  /**
   * Tạo mới
   * @param {object} data - Dữ liệu body gửi lên
   */
  create(data, config = {}) {
    return axiosClient.post(this.baseUrl, data, config);
  }

  /**
   * Cập nhật thông tin
   * @param {string|number} id - ID đối tượng cần sửa
   * @param {object} data - Dữ liệu mới
   */
  update(id, data, config = {}) {
    return axiosClient.put(`${this.baseUrl}/${id}`, data, config);
  }

  /**
   * Xóa đối tượng
   * @param {string|number} id - ID đối tượng cần xóa
   */
  delete(id, config = {}) {
    return axiosClient.delete(`${this.baseUrl}/${id}`, config);
  }
}