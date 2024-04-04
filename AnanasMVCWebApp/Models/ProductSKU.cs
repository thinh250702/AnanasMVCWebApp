using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace AnanasMVCWebApp.Models {
    public class ProductSKU {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } //Format: A1BS001-35
        public int SizeId { get; set; }
        public int ProductVariantId { get; set; }
        public int StockQuantity { get; set; }
        public virtual Size Size { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
