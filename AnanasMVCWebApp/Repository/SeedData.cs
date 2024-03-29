using AnanasMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnanasMVCWebApp.Repository {
    public class SeedData {
        public static void SeedingData(DataContext _context) {
            _context.Database.Migrate();
            if (!_context.Categories.Any()) {
                _context.Categories.AddRange(new List<Category>() {
                    new Category() { Name = "Giày", Slug = "shoes" },
                    new Category() { Name = "Quần áo", Slug = "clothing" },
                    new Category() { Name = "Phụ kiện", Slug = "accessories" }
                });
                _context.SaveChanges();
            }
            if (!_context.Collections.Any()) {
                _context.Collections.AddRange(new List<Collection>() {
                    new Collection() { Id = "BS", Name = "Basas", Slug = "basas"},
                    new Collection() { Id = "VT", Name = "Vintas",  Slug = "vintas"},
                    new Collection() { Id = "UB", Name = "Urbas", Slug = "urbas" },
                    new Collection() { Id = "PR", Name = "Pattas", Slug = "pattas" },
                    new Collection() { Id = "BT", Name = "Basic Tee", Slug = "basic-tee" },
                    new Collection() { Id = "GY", Name = "Graphic Tee", Slug = "graphic-tee" },
                    new Collection() { Id = "SW", Name = "Sweatshirt", Slug = "sweatshirt" },
                    new Collection() { Id = "HD", Name = "Hoodie", Slug = "hoodie" },
                    new Collection() { Id = "TH", Name = "Trucker Hat", Slug = "trucker-hat" },
                    new Collection() { Id = "LB", Name = "Dây giày", Slug = "shoelaces" },
                    new Collection() { Id = "HC", Name = "High Crew Sock", Slug = "high-crew-sock" },
                    new Collection() { Id = "CS", Name = "Crew Sock", Slug = "crew-sock" },
                    new Collection() { Id = "TB", Name = "Tote Bag", Slug = "tote-bag" },
                });
                _context.SaveChanges();
            }
            if (!_context.Sizes.Any()) {
                _context.Sizes.AddRange(new List<Size>() {
                    new Size(){ Name = "35"},
                    new Size(){ Name = "36" },
                    new Size(){ Name = "37" },
                    new Size(){ Name = "38" },
                    new Size(){ Name = "39" },
                    new Size(){ Name = "40" },
                    new Size(){ Name = "41" },
                    new Size(){ Name = "42" },
                    new Size(){ Name = "43" },
                    new Size(){ Name = "44" },
                    new Size(){ Name = "45" },
                    new Size(){ Name = "46" },
                    new Size(){ Name = "S" },
                    new Size(){ Name = "M" },
                    new Size(){ Name = "L" },
                    new Size(){ Name = "XL" },
                    new Size(){ Name = "XXL" },
                    new Size(){ Name = "Freesize" },
                });
                _context.SaveChanges();
            }
            if (!_context.Styles.Any()) {
                _context.Styles.AddRange(new List<Style>() {
                    new Style(){ Name = "High Top", Slug = "high-top"},
                    new Style(){ Name = "Low Top", Slug = "low-top"},
                    new Style(){ Name = "Mid Top", Slug = "mid-top"},
                    new Style(){ Name = "Mule", Slug = "mule"},
                });
                _context.SaveChanges();
            }
            if (!_context.Colors.Any()) {
                _context.Colors.AddRange(new List<Color>() {
                    new Color(){ Id = "464646", Name = "Black", Slug = "black"},
                    new Color(){ Id = "ffffff", Name = "White", Slug = "white"},
                    new Color(){ Id = "c3c3c3", Name = "Grey", Slug = "grey"},
                    new Color(){ Id = "c10013", Name = "Red", Slug = "red"},
                    new Color(){ Id = "e9662c", Name = "Orange", Slug = "orange"},
                    new Color(){ Id = "f5d255", Name = "Yellow", Slug = "yellow"},
                    new Color(){ Id = "6d9951", Name = "Green", Slug = "green"},
                    new Color(){ Id = "006964", Name = "Teal", Slug = "teal"},
                    new Color(){ Id = "f1778a", Name = "Pink", Slug = "pink"},
                    new Color(){ Id = "8a5ca0", Name = "Purple", Slug = "purple"},
                    new Color(){ Id = "1c487c", Name = "Navy", Slug = "navy"},
                    new Color(){ Id = "4cc9f0", Name = "Sky Blue", Slug = "sky-blue"},
                    new Color(){ Id = "865439", Name = "Brown", Slug = "brown"},
                    new Color(){ Id = "ebd0a2", Name = "Beige", Slug = "beige"},
                });
                _context.SaveChanges();
            }
        }
    }
}
