using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class OrderDetail {
        public string ProductSKUId { get; set; }
        public virtual ProductSKU ProductSKU { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }

        public OrderDetail(ProductSKU productSKU, Order order, int quantity, int subTotal) {
            ProductSKU = productSKU;
            Order = order;
            Quantity = quantity;
            SubTotal = subTotal;
        }
    }
}
