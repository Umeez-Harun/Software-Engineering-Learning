<script setup>

import api from '@/Axios/api';
import {useRouter } from 'vue-router';

const router = useRouter()
const name = localStorage.getItem("name")
async function logout(){
  localStorage.removeItem('token')
  localStorage.removeItem('refreshToken')

  router.push('/')
}
</script>

<template>
    <div class="app-shell">
      <aside class="sidebar">
        <div>
          <div class="sidebar-brand">The <span>Ledger</span> Market</div>
          <nav class="sidebar-nav">
            <RouterLink to="add-product" class="nav-item nav-item--active">
              <span class="nav-icon">＋</span> Add product
            </RouterLink>
            <RouterLink to="view-products" class="nav-item">
              <span class="nav-icon">▤</span> View products
            </RouterLink>
            <button @click="logout" class="nav-item">
              <span class="nav-icon">↩</span> Log out
            </button>
          </nav>
        </div>
        <div class="sidebar-foot">{{name}}</div>
      </aside>

      <RouterView />
    </div>
</template>