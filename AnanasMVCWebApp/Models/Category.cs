using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class Category {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Category(string name, string slug) {
            Name = name;
            Slug = slug;
        }
    }
}
