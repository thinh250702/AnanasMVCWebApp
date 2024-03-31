using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class Color {
        [Key]
        public int Id { get; set; }
        public string HexCode { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public Color(string hexCode, string name, string slug) {
            HexCode = hexCode;
            Name = name;
            Slug = slug;
        }
    }
}
