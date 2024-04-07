using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class OrderDetail {
        public int ProductSKUId { get; set; }
        public int OrderId { get; set; }
        public virtual ProductSKU ProductSKU { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }
    }
}
