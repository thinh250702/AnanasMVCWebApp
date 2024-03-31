using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models {
    public class Collection {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Collection(string code, string name, string slug) {
            Code = code;
            Name = name;
            Slug = slug;
        }
    }
}
