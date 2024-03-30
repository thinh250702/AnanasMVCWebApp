using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnanasMVCWebApp.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required"), 
        Range(1, int.MaxValue, ErrorMessage = "Price must be greater than or equal to 1")]
        public int Price { get; set; }
        public int? StyleId { get; set; }
        [Required(ErrorMessage = "Please select Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please select Collection")]
        public string CollectionId { get; set; }
        public virtual Style Style { get; set; }
        public virtual Category Category { get; set; }
        public virtual Collection Collection { get; set; }
        public Product() {
            Id = Guid.NewGuid().ToString("N").Substring(0,8);
        }
    }
}
