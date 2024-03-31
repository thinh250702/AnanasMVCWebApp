using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class Style {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Style(string name, string slug) {
            Name = name;
            Slug = slug;
        }
    }
}
