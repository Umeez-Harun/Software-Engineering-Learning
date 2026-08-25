using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class ProductResponse
    {
        public Guid id { get; set; }
        public string title { get; set; } = string.Empty;
        public string category { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int quantity { get; set; }
        public string sku { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public Guid seller_id { get; set; }
        public DateTime createdAt { get; set; }
        public bool isDeleted { get; set; }
    }

    public static class ProductExtension
    {
        public static ProductResponse convertToProductResponse(this Product p)
        {
            return new ProductResponse() { id = p.id, title = p.title, category = p.category, price = p.price, quantity = p.quantity, description = p.description, sku = p.sku, createdAt = p.createdAt, seller_id = p.seller_id, isDeleted = p.isDeleted };
        }
    }
}
