import { createRouter, createWebHistory } from 'vue-router'
import Login from '@/views/Login.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: Login
    },
    {
      path: '/sign-up',
      name: 'sign-up',
      component: ()=> import('../views/sign-up.vue')
    },
    {
      path: '/products',
      name: 'products',
      component: ()=> import('../views/product-listings.vue')
    },
    {
      path: '/admin',
      name: 'admin',
      component: ()=> import('../views/admin-layout.vue'),
      children: [
        {
        path: 'add-product',
        name: 'product',
        component: ()=> import('../views/admin-addProduct.vue')
      },
      {
        path: 'view-products',
        name: 'view-products',
        component: ()=> import('../views/admin-products.vue')
      }
      ]
    }
  ],
})

export default router
