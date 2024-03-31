using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class ShippingMethod {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ShippingMethod(string name, string description) {
            Name = name;
            Description = description;
        }
    }
}
