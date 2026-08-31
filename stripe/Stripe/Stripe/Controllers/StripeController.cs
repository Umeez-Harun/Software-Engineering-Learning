using Microsoft.AspNetCore.Mvc;
using Stripe.Models;

namespace Stripe.Controllers
{
    [Route("{controller}/{action}")]
    public class StripeController : Controller
    {
        private readonly ProductModel _products;
        private readonly IConfiguration _configuration;
        public StripeController(ProductModel products, IConfiguration configuration)
        {
            _products = products;
            _configuration = configuration;
        }
        [Route("/")]
        public IActionResult Index()
        {
            List<ProductModel> products = _products.getProducts();
            return View(products);
        }

        [HttpGet]
        [Route("{productID}")]
        public IActionResult buyNow(Guid productID)
        {
            ProductModel? product = _products.getProducts().FirstOrDefault(temp => temp.id == productID);
            if (product == null)
            {
                throw new ArgumentNullException();
            }
            //use the product to checkout
            string domain = "https://localhost:7152";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain + "/Stripe/confirmation",
                CancelUrl = domain + "/Stripe/cancel",

                LineItems = new List<Checkout.SessionLineItemOptions>()
                {
                    new Checkout.SessionLineItemOptions()
                    {
                        PriceData = new Checkout.SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            UnitAmount = (long)(product.price * 100),

                            ProductData = new Checkout.SessionLineItemPriceDataProductDataOptions
                            {
                                Name = product.name
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment"
            };
            var client = new StripeClient(_configuration.GetSection("Stripe:SecretKey").Get<string>());
            var service = client.V1.Checkout.Sessions;

            Stripe.Checkout.Session session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult confirmation()
        {
            return View();
        }

        public IActionResult cancel()
        {
            return View();
        }
    }
}
