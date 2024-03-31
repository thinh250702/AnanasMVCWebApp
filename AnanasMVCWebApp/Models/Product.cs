using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required"), 
        Range(1, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 1")]
        public int Price { get; set; }
        public int? StyleId { get; set; }
        [Required(ErrorMessage = "Please select Collection")]
        public int CollectionId { get; set; }
        public virtual Style Style { get; set; }
        public virtual Collection Collection { get; set; }
    }
}
