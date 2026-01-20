import BaseService from '../baseService.js';
import { API_ENDPOINTS } from '../../common/constants/apiConfig';

class ProductService extends BaseService {
  constructor() {
    super(API_ENDPOINTS.PRODUCT.INDEX); 
  }
}

export default new ProductService();