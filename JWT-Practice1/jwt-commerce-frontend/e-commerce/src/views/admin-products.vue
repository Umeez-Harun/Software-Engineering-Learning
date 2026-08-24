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
            <article class="ticket">
              <div class="ticket-media">M</div>
              <div class="ticket-body">
                <span class="eyebrow">Home & Living</span>
                <h3>Hand-thrown ceramic mug</h3>
                <p>Matte glaze, holds 12oz, made in small batches.</p>
              </div>
              <div class="ticket-perf"></div>
              <div class="ticket-stub">
                <div class="price-tag">$24.00<span>SKU LDG-0142</span></div>
                <button type="submit" class="btn btn-danger btn-small">
                  Delete
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
                <div class="price-tag">$58.00<span>SKU LDG-0098</span></div>
                <button type="submit" class="btn btn-danger btn-small">
                  Delete
                </button>
              </div>
            </article>

            <article class="ticket">
              <div class="ticket-media">C</div>
              <div class="ticket-body">
                <span class="eyebrow">Pantry</span>
                <h3>Small-batch chili oil</h3>
                <p>Sichuan peppercorn, cold-pressed, 200ml jar.</p>
                <span
                  class="badge badge-low"
                  style="margin-top: 0.5rem; display: inline-block"
                  >Low stock · 3 left</span
                >
              </div>
              <div class="ticket-perf"></div>
              <div class="ticket-stub">
                <div class="price-tag">$14.50<span>SKU LDG-0211</span></div>
                <button type="submit" class="btn btn-danger btn-small">
                  Delete
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
                <div class="price-tag">$32.00<span>SKU LDG-0177</span></div>
                <button type="submit" class="btn btn-danger btn-small">
                  Delete
                </button>
              </div>
            </article>
          </div>
        </div>
      </div>
</template>