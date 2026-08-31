<script setup>
import api from '@/Axios/api';
import { onMounted, ref } from 'vue';

const products = ref([])
async function getProducts(){
  try{
    const response = await api.get('/product')
    products.value = response.data
  }
  catch(err){
    console.log(err)
  }
}

onMounted(() => {
  getProducts()
})

async function deleteProduct(productID){
  try{
    const response = await api.delete(`/product/${productID}`)
  }
  catch(err){
    console.log(err)
  }
}
</script>

<template>
    <div class="main">
        <div class="content">
          <div class="page-head">
            <div>
              <p class="eyebrow">Your stock</p>
              <h1>Your products</h1>
              <p>4 listings live on the market floor right now.</p>
            </div>
            <a href="seller-add-product.html" class="btn btn-primary"
              >＋ Add product</a
            >
          </div>

          <div class="product-grid">

            <article class="ticket" v-for="product in products">
              <div class="ticket-media">S</div>
              <div class="ticket-body">
                <span class="eyebrow">{{product.title}}</span>
                <h3>{{ product.category }}</h3>
                <p>{{product.description}}</p>
              </div>
              <div class="ticket-perf"></div>
              <div class="ticket-stub">
                <div class="price-tag">{{product.price}}<span>{{product.sku}}</span></div>
                <button @click="deleteProduct(product.id)" class="btn btn-danger btn-small">
                  Delete
                </button>
              </div>
            </article>

          </div>
        </div>
      </div>
</template>