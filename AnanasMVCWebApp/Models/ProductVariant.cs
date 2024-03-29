using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class ProductVariant {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
