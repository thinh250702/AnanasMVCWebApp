using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public class Order {
        [Key]
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderTotal { get; set; }
        public int OrderStatusId { get; set; }
        public int ShippingMethodId { get; set; }
        public int PaymentMethodId { get; set; }
        public int ShippingFee { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public Order() {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8);
        }

    }
}
