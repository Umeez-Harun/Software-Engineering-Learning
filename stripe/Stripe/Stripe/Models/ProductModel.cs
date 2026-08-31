namespace Stripe.Models
{
    public class ProductModel
    {
        public Guid id { get; set; } 
        public string name { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string description { get; set; } = string.Empty;

        public List<ProductModel> getProducts()
        {
            return new List<ProductModel>()
            {
        new ProductModel
        {
            id = Guid.Parse("F395F28C-3BFC-4748-A5DA-2A72176E235D"),
            name = "Wireless Mouse",
            category = "Computer Accessories",
            quantity = 50,
            price = 25.99m,
            description = "Ergonomic wireless mouse with USB receiver"
        },
        new ProductModel
        {
            id = Guid.Parse("FC3757AB-D79A-434F-8235-908EE830E8D2"),
            name = "Mechanical Keyboard",
            category = "Computer Accessories",
            quantity = 30,
            price = 79.99m,
            description = "Mechanical keyboard with RGB backlighting"
        },
        new ProductModel
        {
            id = Guid.Parse("F2E77B94-DE17-458D-BC6D-D12FEC0DA769"),
            name = "USB-C Hub",
            category = "Computer Accessories",
            quantity = 40,
            price = 34.50m,
            description = "Multi-port USB-C hub with HDMI and USB ports"
        },
        new ProductModel
        {
            id = Guid.Parse("874CF94C-00E7-4313-A029-09CB35534A5B"),
            name = "Webcam",
            category = "Computer Accessories",
            quantity = 25,
            price = 59.99m,
            description = "1080p HD webcam with built-in microphone"
        },
        new ProductModel
        {
            id = Guid.Parse("7836C919-08DF-4D30-93BA-2C4E3C76D300"),
            name = "Laptop Stand",
            category = "Computer Accessories",
            quantity = 20,
            price = 42.00m,
            description = "Adjustable aluminum laptop stand"
        }
          };
        }
    }
}
