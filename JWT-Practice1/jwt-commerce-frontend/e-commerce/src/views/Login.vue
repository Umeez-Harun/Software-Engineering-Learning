<script setup>
import {ref, reactive} from 'vue'

const user = reactive({
  email: null,
  password: null,
})

async function login(){
  return 0;
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
          <h2>Every listing, weighed and stamped by hand.</h2>
          <p>
            Sign back in to pick up where your counter left off — new orders,
            fresh stock, and the ledger keeps running.
          </p>
        </div>
      </section>

      <section class="auth-panel">
        <div class="auth-card">
          <p class="eyebrow">Welcome back</p>
          <h1>Sign in</h1>
          <p class="lede">Enter your details to reach your counter.</p>

          <div>
            <div class="form-field">
              <label>Email address</label>
              <input v-model="user.email" type="email" placeholder="you@example.com" />
            </div>
            <div class="form-field">
              <label>Password</label>
              <input v-model="user.password" type="password" placeholder="••••••••••" />
            </div>

            <div class="form-row">
              <label class="form-check">
                <input type="checkbox" /> Keep me signed in
              </label>
              <a href="#">Forgot password?</a>
            </div>

            <button @click="login" class="btn btn-primary btn-block">
              Sign in
            </button>
          </div>

          <p class="auth-switch">
            New to the market? <RouterLink to="sign-up">Create Account</RouterLink>
          </p>
        </div>
      </section>
    </div>
</template>
