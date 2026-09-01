<script setup>
  import api from '@/Axios/api';
  import { ref, reactive} from 'vue'
  import { useRouter } from 'vue-router';

  const router = useRouter()
  const account = reactive({
    name: null,
    email: null,
    password: null,
    role: null
  })

  async function createAccount(){
    try{
      const response = await api.post('/authentication/sign-up', account)
      router.push('/')
    }
    catch(err){
      alert(err.response.data.email)
      console.log(err.response.data.password)
      console.log(err.response.data.role)
      console.log(err.response.data.name)
      console.log(err.response.data.email)
    }
  } 
</script>

<template>
    <div class="auth-shell">
      <section class="auth-brand">
        <div class="auth-brand-mark">The <span>Ledger</span> Market</div>
        <div class="auth-stamp">
          <div class="auth-stamp-text">
            Est.<br />Trading<br />Post<br />No. 12
          </div>
        </div>
        <div class="auth-brand-copy">
          <h2>Two sides of the same counter.</h2>
          <p>
            Buyers browse the stalls. Sellers keep the ledger. Tell us which
            side of the counter you're standing on.
          </p>
        </div>
      </section>

      <section class="auth-panel">
        <div class="auth-card">
          <p class="eyebrow">Get started</p>
          <h1>Create your account</h1>
          <p class="lede">Choose a role to set up your counter correctly.</p>

          <div>
            <div class="role-select">
              <label class="role-card">
                <input v-model="account.role" type="radio" :value=1 />
                <span class="role-icon">B</span>
                <span class="role-name">Buyer: </span>
                <span class="role-desc">Browse & purchase</span>
                <span class="role-stamp">Chosen</span>
              </label>
              <label class="role-card">
                <input v-model="account.role" type="radio" :value=0 />
                <span class="role-icon">S</span>
                <span class="role-name">Seller: </span>
                <span class="role-desc">List & manage stock</span>
                <span class="role-stamp">Chosen</span>
              </label>
            </div>

            <div class="form-field">
              <label>Full name</label>
              <input v-model="account.name" type="text" placeholder="Jordan Reyes" />
            </div>
            <div class="form-field">
              <label>Email address</label>
              <input v-model="account.email" type="email" placeholder="you@example.com" />
            </div>
            <div class="form-field">
              <label>Password</label>
              <input v-model="account.password" type="password" placeholder="Create a password" />
            </div>

            <button @click="createAccount" class="btn btn-primary btn-block">
              Open my counter
            </button>
          </div>

          <p class="auth-switch">
            Already trading with us? <RouterLink to="/">Sign in</RouterLink>
          </p>
        </div>
      </section>
    </div>
</template>