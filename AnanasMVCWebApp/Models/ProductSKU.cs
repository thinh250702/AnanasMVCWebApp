using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace AnanasMVCWebApp.Models {
    [Index(nameof(SKU), IsUnique = true)]
    public class ProductSKU {
        public string SKU { get; set; }
        public int SizeId { get; set; }
        public Guid ProductVariantId { get; set; }
        public int StockQuantity { get; set; }
        public virtual Size Size { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
