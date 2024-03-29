using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class ProductVariant {
        [Key]
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
