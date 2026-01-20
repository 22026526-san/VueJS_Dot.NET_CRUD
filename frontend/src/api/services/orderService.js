import BaseService from '../baseService.js';
import { API_ENDPOINTS } from '../../common/constants/apiConfig';

class OrderService extends BaseService {
  constructor() {
    super(API_ENDPOINTS.ORDER.INDEX); 
  }
}

export default new OrderService();