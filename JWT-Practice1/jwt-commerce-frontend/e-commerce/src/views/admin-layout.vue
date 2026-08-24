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
            <RouterLink to="/" class="nav-item">
              <span class="nav-icon">↩</span> Log out
            </RouterLink>
          </nav>
        </div>
        <div class="sidebar-foot">Counter No. 12 · Seller</div>
      </aside>

      <RouterView />
    </div>
</template>