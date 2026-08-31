<script setup>
import api from '@/Axios/api';
import {ref, onMounted} from 'vue'
import { useRouter } from 'vue-router';

const router = useRouter()

const name = localStorage.getItem("name")
const products = ref([])
async function getProducts(){
  try{
    const response = await api.get('/product')
    products.value = response.data;
  }
  catch(err){
    console.log(err)
  }
}
onMounted(() => {
  getProducts()
})

async function logout(){
  try{
    localStorage.removeItem("name")
    localStorage.removeItem("token")
    localStorage.removeItem("refreshToken")

    const response = await api.get('/authentication/logout')
    router.push("/")
  }
  catch(err){
    console.log(err)
  }
}
</script>

<template>
  <div class="app-shell" style="flex-direction: column">
      <header class="topbar">
        <div class="topbar-brand">The <span>Ledger</span> Market</div>
        <div style="margin-left: auto; margin-right: 15px;">{{ name }}</div>
        <button @click="logout" class="btn btn-ghost btn-small">↩ Log out</button>
      </header>

      <div class="main">
        <div class="content">
          <div class="page-head">
            <div>
              <p class="eyebrow">Market floor</p>
              <h1>Today's stalls</h1>
              <p>Fresh listings from sellers across the market.</p>
            </div>
          </div>

          <div class="product-grid">

            <article class="ticket" v-for="product in products">
              <div class="ticket-media">C</div>
              <div class="ticket-body">
                <span class="eyebrow">{{product.title}}</span>
                <h3>{{product.category}}</h3>
                <p>{{product.description}}.</p>
              </div>
              <div class="ticket-perf"></div>
              <div class="ticket-stub">
                <div class="price-tag">{{product.price}}<span>{{product.quantity}}</span></div>
                <button type="submit" class="btn btn-primary btn-small">
                  Buy
                </button>
              </div>
            </article>

          </div>
        </div>
      </div>
    </div>
</template>