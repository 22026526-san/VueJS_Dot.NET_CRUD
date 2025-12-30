import { createRouter, createWebHistory } from 'vue-router'

import HelloWord from '../components/HelloWorld.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HelloWord,
      meta:{
        layout:null
      }
    }
  ]
})

export default router