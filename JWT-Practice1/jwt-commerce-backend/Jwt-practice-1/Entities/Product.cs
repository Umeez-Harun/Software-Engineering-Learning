using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        [Key]
        public Guid id {  get; set; }

        [Required]
        [StringLength(25)]
        public string title { get; set; } = string.Empty;

        [Required]
        [StringLength(25)]
        public string category { get; set; } = string.Empty;

        [Required]
        [Precision(18,2)]
        public decimal price { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        [StringLength(25)]
        public string sku { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string description { get; set; } = string.Empty;
        
        [Required]
        [ForeignKey(nameof(seller))]
        public Guid seller_id { get; set; }

        public DateTime createdAt { get; set; }
        public bool isDeleted { get; set; }
        public ApplicationUser seller { get; set; } = null!;
    }
}
