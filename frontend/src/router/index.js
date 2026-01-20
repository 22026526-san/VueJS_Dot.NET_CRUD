import { createRouter, createWebHistory } from 'vue-router'
import ProductList from '../views/ProductList.vue'
import InsuranceOrder from '../views/InsuranceOrder.vue'
import InsuranceOrderDetail from '../views/InsuranceOrderDetail.vue'
import OnlyHeader from '../layout/OnlyHeader.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: ProductList
    },
    {
      path: '/dang_ki_bao_hiem',
      name: 'insurance_order',
      component: InsuranceOrder,
      meta:{
        layout: null
      }
    },
    {
      path: '/chi_tiet_bao_hiem/:orderId',
      name: 'insurance_order_detail',
      component: InsuranceOrderDetail,
      meta:{
        layout: OnlyHeader
      }
    },
  ]
})

export default router