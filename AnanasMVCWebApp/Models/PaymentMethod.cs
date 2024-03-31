using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class PaymentMethod {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public PaymentMethod(string name, string description) {
            Name = name;
            Description = description;
        }
    }
}
