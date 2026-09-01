using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using Stripe;
using Stripe.Checkout;

namespace Jwt_practice_1.Controllers
{
    [Authorize(Roles ="Buyer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StripeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult checkout(List<ProductResponse> products)
        {
            List<SessionLineItemOptions> lineItems = new List<Stripe.Checkout.SessionLineItemOptions>();
            foreach(ProductResponse product in products)
            {
                lineItems.Add(new SessionLineItemOptions()
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "usd",
                        UnitAmount = (long)(product.price * 100),

                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = product.title,
                        }
                    }
                });
            }
            string domain = "http://localhost:5173";
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain + "/checkout/success",
                CancelUrl = domain + "/checkout/failed",

                LineItems = lineItems,
                Mode = "payment"
            };

            StripeClient client = new StripeClient(_configuration.GetSection("Stripe:SecretKey").Get<string>());
            var service = client.V1.Checkout.Sessions;

            Stripe.Checkout.Session session = service.Create(options);
            return Ok( new { Url = session.Url});
        }
    }
}
