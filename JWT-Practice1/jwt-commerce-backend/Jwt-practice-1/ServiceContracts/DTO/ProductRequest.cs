using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductRequest
    {
        public string title { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int quantity { get; set; }
        public string sku { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;

        public Product convertToProduct()
        {
            return new Product() { title = title, category = category, price = price, quantity = quantity, sku = sku, description = description};
        }
    }
}
