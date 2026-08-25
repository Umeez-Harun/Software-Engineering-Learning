<script setup>
import {ref, reactive} from 'vue'
import general from '@/components/general.vue';
import error from '@/components/error.vue';
import confirmation from '@/components/confirmation.vue';

const product = reactive({
  title: null,
  category: null,
  price: null,
  quantity: null,
  sku: null,
  description: null,
})
const isclicked = ref(false)

async function addProduct(){
  isclicked.value = true
  return;
}
</script>

<template>
    <div class="main">
        <div class="content">
          <div class="page-head">
            <div>
              <p class="eyebrow">New listing</p>
              <h1>Add a product</h1>
            </div>
          </div>

          <div class="panel">
            <div class="upload-drop">
              <input type="file" />
              <strong>Click to upload</strong> or drag a photo here — JPG or
              PNG, up to 5MB
            </div>

            <div class="form-grid">
              <div class="form-field span-2">
                <label>Product title</label>
                <input v-model="product.title" type="text" placeholder="Hand-thrown ceramic mug" />
              </div>

              <div class="form-field">
                <label>Category</label>
                <select v-model="product.category">
                  <option>Home & Living</option>
                  <option>Apparel</option>
                  <option selected>Accessories</option>
                  <option>Pantry</option>
                </select>
              </div>

              <div class="form-field">
                <label>Price</label>
                <input v-model="product.price" type="text" placeholder="$ 24.00" />
              </div>

              <div class="form-field">
                <label>Stock quantity</label>
                <input v-model="product.quantity" type="text" placeholder="12" />
              </div>

              <div class="form-field">
                <label>SKU</label>
                <input v-model="product.sku" type="text" placeholder="LDG-0142" />
              </div>

              <div class="form-field span-2">
                <label>Description</label>
                <textarea
                  v-model="product.description"
                  class="form-textarea"
                  placeholder="A short, honest note about the piece — materials, size, what makes it worth keeping."
                ></textarea>
              </div>
            </div>

            <div class="form-actions">
              <button @click="addProduct" class="btn btn-primary">
                List product
              </button>
              <button type="button" class="btn btn-ghost">Save as draft</button>
            </div>
          </div>
        </div>
      </div>
</template>