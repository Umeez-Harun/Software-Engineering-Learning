<template>
  <div class="app-shell" style="flex-direction: column">
      <header class="topbar">
        <div class="topbar-brand">The <span>Ledger</span> Market</div>
        <RouterLink to="/" class="btn btn-ghost btn-small">↩ Log out</RouterLink>
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
            <article class="ticket">
              <div class="ticket-media">M</div>
              <div class="ticket-body">
                <span class="eyebrow">Home & Living</span>
                <h3>Hand-thrown ceramic mug</h3>
                <p>Matte glaze, holds 12oz, made in small batches.</p>
              </div>
              <div class="ticket-perf"></div>
              <div class="ticket-stub">
                <div class="price-tag">$24.00<span>In stock</span></div>
                <button type="submit" class="btn btn-primary btn-small">
                  Buy
                </button>
              </div>
            </article>

            <article class="ticket">
              <div class="ticket-media">S</div>
              <div class="ticket-body">
                <span class="eyebrow">Apparel</span>
                <h3>Waxed canvas apron</h3>
                <p>Water resistant, adjustable strap, one size.</p>
              </div>
              <div class="ticket-perf"></div>
              <div class="ticket-stub">
                <div class="price-tag">$58.00<span>In stock</span></div>
                <button type="submit" class="btn btn-primary btn-small">
                  Buy
                </button>
              </div>
            </article>

            <article class="ticket">
              <div class="ticket-media">C</div>
              <div class="ticket-body">
                <span class="eyebrow">Pantry</span>
                <h3>Small-batch chili oil</h3>
                <p>Sichuan peppercorn, cold-pressed, 200ml jar.</p>
              </div>
              <div class="ticket-perf"></div>
              <div class="ticket-stub">
                <div class="price-tag">$14.50<span>Only 3 left</span></div>
                <button type="submit" class="btn btn-primary btn-small">
                  Buy
                </button>
              </div>
            </article>

            <article class="ticket">
              <div class="ticket-media">T</div>
              <div class="ticket-body">
                <span class="eyebrow">Accessories</span>
                <h3>Woven market tote</h3>
                <p>Undyed cotton, reinforced base, 40lb capacity.</p>
              </div>
              <div class="ticket-perf"></div>
              <div class="ticket-stub">
                <div class="price-tag">$32.00<span>In stock</span></div>
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