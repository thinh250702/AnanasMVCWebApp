namespace AnanasMVCWebApp.Models.ViewModels {
    public class OrderViewModel {
        public List<OrderItemViewModel> OrderItems { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public int Discount { get; set; }
        public int ShippingFee { get; set; }
        public int GrandTotal { get; set; }
        public int OrderTotal { get; set; }
        public string StatusName { get; set; }
        public string StatusSlug { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public OrderViewModel(List<OrderItemViewModel> itemList, Order order) {
            OrderItems = itemList;
            OrderCode = order.Code;
            OrderDate = order.OrderDate;
            Discount = order.Discount;
            ShippingFee = order.ShippingFee;
            GrandTotal = order.GrandTotal;
            OrderTotal = order.OrderTotal;
            StatusName = order.OrderStatus.Name;
            StatusSlug = order.OrderStatus.Slug;
            ShippingMethod = order.ShippingMethod.Name;
            PaymentMethod = order.PaymentMethod.Name;
        }
        public OrderViewModel(List<OrderItemViewModel> itemList, Order order, ShippingInfo info) {
            OrderItems = itemList;
            OrderCode = order.Code;
            OrderDate = order.OrderDate;
            Discount = order.Discount;
            ShippingFee = order.ShippingFee;
            GrandTotal = order.GrandTotal;
            OrderTotal = order.OrderTotal;
            StatusName = order.OrderStatus.Name;
            StatusSlug = order.OrderStatus.Slug;
            ShippingMethod = order.ShippingMethod.Name;
            PaymentMethod = order.PaymentMethod.Name;
            FullName = info.Name;
            Phone = info.Phone;
            Email = info.Email;
            Address = info.Address;
            City = info.City;
            District = info.District;
            Ward = info.Ward;
        }
    }
}
