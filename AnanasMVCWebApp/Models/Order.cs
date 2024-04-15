using AnanasMVCWebApp.Utilities.AdapterPattern;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models {
    public abstract class AbstractOrder
    {
        public abstract int calculatePrice();
    }
    
    public class Order : AbstractOrder {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime OrderDate { get; set; }
        public int GrandTotal { get; set; }
        public int Discount { get; set; }
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
            Code = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }
        public override int calculatePrice() {
            return GrandTotal;
        }
    }
    public class OrderStatus {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public OrderStatus(string name, string slug) {
            Name = name;
            Slug = slug;
        }
    }
    public class PaymentMethod {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public PaymentMethod(string name, string description, string slug) {
            Name = name;
            Description = description;
            Slug = slug;
        }
    }
    public class ShippingMethod {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public ShippingMethod(string name, string description, string slug) {
            Name = name;
            Description = description;
            Slug = slug;
        }
    }
}
