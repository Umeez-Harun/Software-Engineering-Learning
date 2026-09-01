<template>
    <div class="status-shell">
      <div class="status-card">
        <div class="status-seal status-seal--fail">
          <span class="status-seal-icon">✕</span>
        </div>

        <h1>Payment didn't go through</h1>
        <p>
          Your card was declined and no charge was made. Your cart is still
          saved — try again with the same or a different card.
        </p>

        <div class="status-summary">
          <div class="status-row">
            <span>Order number</span><span>LDG-58231</span>
          </div>
          <div class="status-row">
            <span>Attempted total</span><span>$85.00</span>
          </div>
          <div class="status-row">
            <span>Reason</span><span>Card declined</span>
          </div>
        </div>

        <div class="status-actions">
          <a href="buyer-products.html" class="btn btn-primary btn-block"
            >Try again</a
          >
          <a href="#" class="btn btn-ghost btn-block">Contact support</a>
        </div>
      </div>
    </div>
</template>