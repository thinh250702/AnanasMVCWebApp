using AnanasMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnanasMVCWebApp.Repository {
    public class SeedData {
        public static void SeedingData(DataContext _context) {
            _context.Database.Migrate();

            Category shoes = new Category() { Id = 1, Name = "Giày" };
            Category clothing = new Category() { Id = 2, Name = "Quần áo" };
            Category accessories = new Category() { Id = 3, Name = "Phụ kiện" };

            if (!_context.Categories.Any()) {
                _context.Categories.AddRange(new List<Category>() {
                    shoes, clothing, accessories
                });
                _context.SaveChanges();
            }
            if (!_context.Collections.Any()) {
                _context.Collections.AddRange(new List<Collection>() {
                    new Collection() { Id = "BS", Name = "Basas", Category = shoes },
                    new Collection() { Id = "VT", Name = "Vintas", Category = shoes },
                    new Collection() { Id = "UB", Name = "Urbas", Category = shoes },
                    new Collection() { Id = "PR", Name = "Pattas", Category = shoes },
                    new Collection() { Id = "BT", Name = "Basic Tee", Category = clothing },
                    new Collection() { Id = "GY", Name = "Graphic Tee", Category = clothing },
                    new Collection() { Id = "SW", Name = "Sweatshirt", Category = clothing },
                    new Collection() { Id = "HD", Name = "Hoodie", Category = clothing },
                    new Collection() { Id = "TH", Name = "Trucker Hat", Category = accessories },
                    new Collection() { Id = "LB", Name = "Dây giày", Category = accessories },
                    new Collection() { Id = "HC", Name = "High Crew Sock", Category = accessories },
                    new Collection() { Id = "CS", Name = "Crew Sock", Category = accessories },
                    new Collection() { Id = "TB", Name = "Tote Bage", Category = accessories },
                });
                _context.SaveChanges();
            }
        }
    }
}
