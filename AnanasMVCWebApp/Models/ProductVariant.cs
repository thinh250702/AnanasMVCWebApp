using AnanasMVCWebApp.Repository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class ProductVariant {
        [Key]
        public string Id { get; set; } // Format: A1BS001
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
