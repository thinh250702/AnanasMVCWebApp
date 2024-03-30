using AnanasMVCWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AnanasMVCWebApp.Repository {
    public class SeedData {
        public static void SeedingData(DataContext _context) {
            _context.Database.Migrate();

            Category shoes = new Category() { Name = "Giày", Slug = "shoes" };
            Category clothing = new Category() { Name = "Quần áo", Slug = "clothing" };
            Category accessories = new Category() { Name = "Phụ kiện", Slug = "accessories" };
            if (!_context.Categories.Any()) {
                _context.Categories.AddRange(new List<Category>() { shoes, clothing, accessories });
                _context.SaveChanges();
            }
            Collection basas = new Collection() { Id = "BS", Name = "Basas", Slug = "basas" };
            Collection vintas = new Collection() { Id = "VT", Name = "Vintas", Slug = "vintas" };
            Collection urbas = new Collection() { Id = "UB", Name = "Urbas", Slug = "urbas" };
            Collection pattas = new Collection() { Id = "PR", Name = "Pattas", Slug = "pattas" };
            Collection basicTee = new Collection() { Id = "BT", Name = "Basic Tee", Slug = "basic-tee" };
            Collection graphicTee = new Collection() { Id = "GY", Name = "Graphic Tee", Slug = "graphic-tee" };
            Collection sweatshirt = new Collection() { Id = "SW", Name = "Sweatshirt", Slug = "sweatshirt" };
            Collection hoodie = new Collection() { Id = "HD", Name = "Hoodie", Slug = "hoodie" };
            Collection truckerHat = new Collection() { Id = "TH", Name = "Trucker Hat", Slug = "trucker-hat" };
            Collection shoelaces = new Collection() { Id = "LB", Name = "Dây giày", Slug = "shoelaces" };
            Collection highCrewSock = new Collection() { Id = "HC", Name = "High Crew Sock", Slug = "high-crew-sock" };
            Collection crewSock = new Collection() { Id = "CS", Name = "Crew Sock", Slug = "crew-sock" };
            Collection toteBag = new Collection() { Id = "TB", Name = "Tote Bag", Slug = "tote-bag" };
            if (!_context.Collections.Any()) {
                _context.Collections.AddRange(new List<Collection>() {
                    basas, vintas, urbas, pattas, 
                    basicTee, graphicTee, sweatshirt, hoodie, 
                    truckerHat, shoelaces, highCrewSock, crewSock, toteBag
                });
                _context.SaveChanges();
            }
            Size size35 = new Size() { Id = "35", Name = "35" };
            Size size36 = new Size() { Id = "36", Name = "36" };
            Size size37 = new Size() { Id = "37", Name = "37" };
            Size size38 = new Size() { Id = "38", Name = "38" };
            Size size39 = new Size() { Id = "39", Name = "39" };
            Size size40 = new Size() { Id = "40", Name = "40" };
            Size size41 = new Size() { Id = "41", Name = "41" };
            Size size42 = new Size() { Id = "42", Name = "42" };
            Size size43 = new Size() { Id = "43", Name = "43" };
            Size size44 = new Size() { Id = "44", Name = "44" };
            Size size45 = new Size() { Id = "45", Name = "45" };
            Size size46 = new Size() { Id = "46", Name = "46" };
            Size freesize = new Size() { Id = "00", Name = "Freesize" };
            Size sizeS = new Size() { Id = "01", Name = "S" };
            Size sizeM = new Size() { Id = "02", Name = "M" };
            Size sizeL = new Size() { Id = "03", Name = "L" };
            Size sizeXL = new Size() { Id = "04", Name = "XL" };
            Size sizeXXL = new Size() { Id = "05", Name = "XXL" };
            if (!_context.Sizes.Any()) {
                _context.Sizes.AddRange(new List<Size>() {
                    size35, size36, size37, size38, size39, size40, 
                    size41, size42, size43, size44, size45, size46,
                    sizeS, sizeM, sizeL, sizeXL, sizeXXL, freesize,
                });
                _context.SaveChanges();
            }
            Style highTop = new Style() { Name = "High Top", Slug = "high-top" };
            Style lowTop = new Style() { Name = "Low Top", Slug = "low-top" };
            Style midTop = new Style() { Name = "Mid Top", Slug = "mid-top" };
            Style mule = new Style() { Name = "Mule", Slug = "mule" };
            if (!_context.Styles.Any()) {
                _context.Styles.AddRange(new List<Style>() {
                    highTop, lowTop, midTop, mule
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
            Product product1 = new Product() {
                Name = "Basas Workaday",
                Description = "Lorem ipsum",
                Price = 500000,
                Style = highTop,
                Category = shoes,
                Collection = basas
            };
            Product product2 = new Product() {
                Name = "Basas Workaday",
                Description = "Lorem ipsum",
                Price = 500000,
                Style = lowTop,
                Category = shoes,
                Collection = basas
            };
            Product product3 = new Product() {
                Name = "Vintas Public 2000s",
                Description = "Lorem ipsum",
                Price = 500000,
                Style = lowTop,
                Category = shoes,
                Collection = vintas
            };
            if (!_context.Products.Any()) {
                _context.Products.AddRange(new List<Product>() {
                    product1, product2, product3
                });
                _context.SaveChanges();
            }
        }
    }
}
