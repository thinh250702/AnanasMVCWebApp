using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace AnanasMVCWebApp.Models {
    public class ProductSKU {
        [Key]
        public string Id { get; set; }
        public string SizeId { get; set; }
        public string ProductVariantId { get; set; }
        public int StockQuantity { get; set; }
        public virtual Size Size { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
        public ProductSKU() {
            Id = ProductVariantId + SizeId;
        }
    }
}
