using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductRequest
    {
        [Required]
        public string title { get; set; } = string.Empty;

        [Required]
        public string category { get; set; } = string.Empty;

        [Required]
        public decimal price { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public string sku { get; set; } = string.Empty;

        [Required]
        public string description { get; set; } = string.Empty;

        public Product convertToProduct()
        {
            return new Product() { title = title, category = category, price = price, quantity = quantity, sku = sku, description = description};
        }
    }
}
