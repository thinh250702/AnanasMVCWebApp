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

    }
}
