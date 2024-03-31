namespace AnanasMVCWebApp.Models {
    public class ShippingInfo {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public ShippingInfo(string name, string phone, string email, string address, string city, string district, string ward, string orderId, Order order) {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            City = city;
            District = district;
            Ward = ward;
            OrderId = orderId;
            Order = order;
        }
    }
}
