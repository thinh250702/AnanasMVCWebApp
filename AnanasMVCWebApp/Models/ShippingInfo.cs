using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class ShippingInfo {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
