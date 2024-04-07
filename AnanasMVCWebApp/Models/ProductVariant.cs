using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class ProductVariant {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } // Format: A1BS001
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Sold { get; set; } = 0;
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
