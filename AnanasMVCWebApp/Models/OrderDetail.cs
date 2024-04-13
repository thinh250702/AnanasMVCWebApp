using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class OrderDetail {
        public int ProductSKUSizeId { get; set; }
        public int ProductSKUProductVariantId { get; set; }
        public int OrderId { get; set; }
        public virtual ProductSKU ProductSKU { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }
    }
}
