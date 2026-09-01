<script setup>
import api from '@/Axios/api';
import {ref, onMounted} from 'vue'
import { useRouter } from 'vue-router';

const router = useRouter()

const name = localStorage.getItem("name")
const products = ref([])
const cartProducts = ref([])
const cartPrice = ref(0)

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
  cartProducts.value = getItemsFromCart()
})

function AddToCart(product){
  const products = JSON.parse(localStorage.getItem('cart-products') || '[]')
  const exists = products.some(e => e.id == product.id)
  if(exists) return
  products.push(product)
  cartPrice.value = products.reduce((sum, product) => sum + product.price, 0)
  localStorage.setItem('cart-products', JSON.stringify(products))
  router.push("/products")
}

function getItemsFromCart(){
  const products = JSON.parse(localStorage.getItem('cart-products',) || '[]')
  cartPrice.value = products.reduce((sum, product) => sum + product.price, 0)
  return products
}

function removeItem(productID){
  const products = JSON.parse(localStorage.getItem('cart-products') || '[]')
  const updatedProducts = products.filter(p => p.id !== productID)

  localStorage.setItem('cart-products', JSON.stringify(updatedProducts))
  router.push("/products")
}
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

async function checkout() {
  const products = JSON.parse(localStorage.getItem('cart-products') || '[]')
  if(!products || products.length === 0){
    console.log('Empty Cart')
    return;
  }
 try{
   const response = await api.post('/Stripe',products)

   window.location.href = response.data.url
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

        <header class="topbar">
        

        <div class="topbar-actions">
          <div class="cart-wrap" tabindex="0">
            <button type="button" class="cart-trigger" aria-label="View cart">
              🛒
              <span class="cart-badge">{{cartProducts.length}}</span>
            </button>

            <div class="cart-panel">
              <div class="cart-panel-head">
                <h3>Your cart</h3>
                <span>{{cartProducts.length}} items</span>
              </div>

              <div class="cart-items">

                <div class="cart-item" v-for="item in cartProducts">
                  <div class="cart-item-thumb"></div>
                  <div class="cart-item-info">
                    <div class="cart-item-name">{{item.title}}</div>
                    <div class="cart-item-meta">Qty 1 · ${{item.price}}</div>
                  </div>
                  <button
                    type="button"
                    @click="removeItem(item.id)"
                    class="cart-remove"
                    aria-label="Remove item"
                  >
                    ✕
                  </button>
                </div>
              </div>

              <div class="cart-footer">
                <div class="cart-subtotal-row">
                  <span>Subtotal</span>
                  <strong>${{cartPrice}}</strong>
                </div>
                <button
                  @click="checkout"
                  class="btn btn-primary btn-block btn-small"
                  >Proceed to checkout</button
                >
              </div>
            </div>
          </div>
        </div>
      </header>
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
                <div class="price-tag">{{product.price}}$<span>{{product.quantity}}</span></div>
                <button @click="AddToCart(product)" class="btn btn-primary btn-small">Add to cart</button>
              </div>
            </article>

          </div>
        </div>
      </div>
    </div>
</template>