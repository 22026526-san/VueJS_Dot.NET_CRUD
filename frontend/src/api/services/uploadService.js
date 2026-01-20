import BaseService from '../baseService.js';
import { API_ENDPOINTS } from '../../common/constants/apiConfig';
import axiosClient from '../axiosClient.js';

class UploadService extends BaseService {
  constructor() {
    super(API_ENDPOINTS.UPLOAD.INDEX); 
  }

  /**
   * Upload file lên Server
   * @param {File} file - File lấy từ input
   */
  uploadImage(file) {
    const formData = new FormData();
    formData.append('file', file); 

    // Gọi POST với header multipart
    return axiosClient.post(this.baseUrl, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  }

  /**
   * Xóa file trên Server
   * @param {string} publicId - ID của ảnh cần xóa
   */
  deleteImage(publicId) {
    return this.delete(publicId);
  }
}

export default new UploadService();