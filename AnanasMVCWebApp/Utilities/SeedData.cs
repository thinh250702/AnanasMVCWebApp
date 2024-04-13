using AnanasMVCWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnanasMVCWebApp.Utilities {
    public class SeedData {
        public static async Task SeedingDataAsync(DataContext _context, RoleManager<IdentityRole> _roleManager, UserManager<Customer> _userManager) {
            _context.Database.Migrate();

            Category shoes = new Category("Giày", "shoes");
            Category clothing = new Category("Quần áo", "clothing");
            Category accessories = new Category("Phụ kiện", "accessories");
            if (!_context.Categories.Any()) {
                _context.Categories.AddRange(new List<Category>() { shoes, clothing, accessories });
                _context.SaveChanges();
            }
            Collection basas = new Collection("BS", "Basas", "basas") { Category = shoes };
            Collection vintas = new Collection("VT", "Vintas", "vintas") { Category = shoes };
            Collection urbas = new Collection("UB", "Urbas", "urbas") { Category = shoes };
            Collection pattas = new Collection("PR", "Pattas", "pattas") { Category = shoes };
            Collection basicTee = new Collection("BT", "Basic Tee", "basic-tee") { Category = clothing };
            Collection graphicTee = new Collection("GT", "Graphic Tee", "graphic-tee") { Category = clothing };
            Collection sweatshirt = new Collection("SW", "Sweatshirt", "sweatshirt") { Category = clothing };
            Collection hoodie = new Collection("HD", "Hoodie", "hoodie") { Category = clothing };
            Collection truckerHat = new Collection("TH", "Trucker Hat", "trucker-hat") { Category = accessories };
            Collection shoelaces = new Collection("LB", "Dây giày", "shoelaces") { Category = accessories };
            Collection highCrewSock = new Collection("HC", "High Crew Sock", "high-crew-sock") { Category = accessories };
            Collection crewSock = new Collection("CS", "Crew Sock", "crew-sock") { Category = accessories };
            Collection toteBag = new Collection("TB", "Tote Bag", "tote-bag") { Category = accessories };
            if (!_context.Collections.Any()) {
                _context.Collections.AddRange(new List<Collection>() {
                    basas, vintas, urbas, pattas,
                    basicTee, graphicTee, sweatshirt, hoodie,
                    truckerHat, shoelaces, highCrewSock, crewSock, toteBag
                });
                _context.SaveChanges();
            }
            Size size35 = new Size("35");
            Size size36 = new Size("36");
            Size size37 = new Size("37");
            Size size38 = new Size("38");
            Size size39 = new Size("39");
            Size size40 = new Size("40");
            Size size41 = new Size("41");
            Size size42 = new Size("42");
            Size size43 = new Size("43");
            Size size44 = new Size("44");
            Size size45 = new Size("45");
            Size size46 = new Size("46");
            Size freesize = new Size("00");
            Size sizeS = new Size("01");
            Size sizeM = new Size("02");
            Size sizeL = new Size("03");
            Size sizeXL = new Size("04");
            Size sizeXXL = new Size("05");
            if (!_context.Sizes.Any()) {
                _context.Sizes.AddRange(new List<Size>() {
                    size35, size36, size37, size38, size39, size40,
                    size41, size42, size43, size44, size45, size46,
                    sizeS, sizeM, sizeL, sizeXL, sizeXXL, freesize,
                });
                _context.SaveChanges();
            }
            Style highTop = new Style("High Top", "high-top");
            Style lowTop = new Style("Low Top", "low-top");
            Style midTop = new Style("Mid Top", "mid-top");
            Style mule = new Style("Mule", "mule");
            if (!_context.Styles.Any()) {
                _context.Styles.AddRange(new List<Style>() {
                    highTop, lowTop, midTop, mule
                });
                _context.SaveChanges();
            }
            Color black = new Color("464646", "Black", "black");
            Color white = new Color("ffffff", "White", "white");
            Color grey = new Color("c3c3c3", "Grey", "grey");
            Color red = new Color("c10013", "Red", "red");
            Color orange = new Color("e9662c", "Orange", "orange");
            Color yellow = new Color("f5d255", "Yellow", "yellow");
            Color green = new Color("6d9951", "Green", "green");
            Color teal = new Color("006964", "Teal", "teal");
            Color pink = new Color("f1778a", "Pink", "pink");
            Color purple = new Color("8a5ca0", "Purple", "purple");
            Color navy = new Color("1c487c", "Navy", "navy");
            Color skyBlue = new Color("4cc9f0", "Sky Blue", "sky-blue");
            Color brown = new Color("865439", "Brown", "brown");
            Color beige = new Color("ebd0a2", "Beige", "beige");
            if (!_context.Colors.Any()) {
                _context.Colors.AddRange(new List<Color>() {
                    black, white, grey, red, orange, yellow, green, teal,
                    pink, purple, skyBlue, brown, navy, beige
                });
                _context.SaveChanges();
            }
            Product product1 = new Product() {
                Name = "Basas Workaday",
                Description = "Lorem ipsum",
                Price = 650000,
                Collection = basas,
                Style = highTop
            };
            Product product2 = new Product() {
                Name = "Basas Workaday",
                Description = "Lorem ipsum",
                Price = 580000,
                Collection = basas,
                Style = lowTop
            };
            Product product3 = new Product() {
                Name = "Vintas Public 2000s",
                Description = "Lorem ipsum",
                Price = 620000,
                Collection = vintas,
                Style = lowTop
            };
            Product product4 = new Product() {
                Name = "Logos Packed",
                Description = "Lorem ipsum",
                Price = 350000,
                Collection = graphicTee,
            };
            Product product5 = new Product() {
                Name = "Go Skate",
                Description = "Lorem ipsum",
                Price = 220000,
                Collection = toteBag,
            };
            if (!_context.Products.Any()) {
                _context.Products.AddRange(new List<Product>() {
                    product1, product2, product3, product4, product5
                });
                _context.SaveChanges();
            }
            ProductVariant variant1 = new ProductVariant {
                Code = $"A{product2.Collection.Code}001",
                ColorName = "Black",
                HexCode = "2c2f32",
                Color = black,
                Product = product2
            };
            ProductVariant variant2 = new ProductVariant {
                Code = $"A{product2.Collection.Code}002",
                ColorName = "Real Teal",
                HexCode = "405e74",
                Color = teal,
                Product = product2
            };
            ProductVariant variant3 = new ProductVariant {
                Code = $"A{product4.Collection.Code}001",
                ColorName = "Snow White",
                HexCode = "f0f0ec",
                Color = white,
                Product = product4
            };
            ProductVariant variant4 = new ProductVariant {
                Code = $"A{product5.Collection.Code}001",
                ColorName = "Orion Blue",
                HexCode = "2a4656",
                Color = navy,
                Product = product5
            };
            if (!_context.ProductVariants.Any()) {
                _context.ProductVariants.AddRange(new List<ProductVariant>() {
                    variant1, variant2, variant3, variant4
                });
                _context.SaveChanges();
            }
            Random random = new Random();
            List<ProductSKU> variant1SKUs = new List<ProductSKU>();
            for (int i = 35; i <= 46; i++) {
                var size = _context.Sizes.Where(x => x.Code == i.ToString()).FirstOrDefault();
                variant1SKUs.Add(new ProductSKU {
                    Code = $"{variant1.Code}-{size.Code}",
                    InStock = random.Next(8,25),
                    Size = size,
                    ProductVariant = variant1
                });
            }
            List<ProductSKU> variant2SKUs = new List<ProductSKU>();
            for (int i = 35; i <= 46; i++) {
                var size = _context.Sizes.Where(x => x.Code == i.ToString()).FirstOrDefault();
                variant2SKUs.Add(new ProductSKU {
                    Code = $"{variant2.Code}-{size.Code}",
                    InStock = random.Next(8, 25),
                    Size = size,
                    ProductVariant = variant2
                });
            }
            if (!_context.ProductSKUs.Any()) {
                _context.ProductSKUs.AddRange(variant1SKUs);
                _context.ProductSKUs.AddRange(variant2SKUs);
                _context.SaveChanges();
            }

            if (!await _roleManager.RoleExistsAsync(ApplicationRole.Admin)) {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationRole.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(ApplicationRole.Customer)) {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationRole.Customer));
            }

            var result = await _userManager.FindByEmailAsync("admin@ananas.vn");
            if (result == null) {
                var admin = new Customer {
                    UserName = "admin@ananas.vn",
                    Email = "admin@ananas.vn",
                    FullName = "Nguyễn Trường Thịnh",
                    Dob = new DateTime(2002, 7, 25),
                };
                string adminPassword = "Ananas123456";
                var createAdmin = await _userManager.CreateAsync(admin, adminPassword);
                if (createAdmin.Succeeded) {
                    await _userManager.AddToRoleAsync(admin, ApplicationRole.Admin);
                }
            }

            ShippingMethod ghnMethod = new ShippingMethod("Giao hàng nhanh", "Thời gian giao hàng dự kiến từ 3 - 5 ngày", "ghn");
            ShippingMethod ghtkMethod = new ShippingMethod("Giao hàng tiết kiệm", "Thời gian giao hàng dự kiến từ 5 - 7 ngày", "ghtk");
            if (!_context.ShippingMethods.Any()) {
                _context.ShippingMethods.AddRange(new List<ShippingMethod>() { ghnMethod, ghtkMethod });
                _context.SaveChanges();
            }

            PaymentMethod cod = new PaymentMethod("Thanh toán trực tiếp khi giao hàng", "Là phương thức thanh toán bằng tiền mặt trực tiếp khi nhận hàng", "cod");
            PaymentMethod card = new PaymentMethod("Thanh toán bằng Thẻ quốc tế / Thẻ nội địa / QR Code", "Phương thức thanh toán sử dụng các loại thẻ quốc tế như Visa, Master, JCB,… hoặc các loại thẻ thanh toán nội địa (ATM) hoặc thanh toán bằng QR ngân hàng hoặc ví điện tử.", "card");
            if (!_context.PaymentMethods.Any()) {
                _context.PaymentMethods.AddRange(new List<PaymentMethod>() { cod, card });
                _context.SaveChanges();
            }

            OrderStatus placed = new OrderStatus("Đặt hàng thành công", "placed");
            OrderStatus logistic = new OrderStatus("Chuyển qua giao nhận", "logistic");
            OrderStatus delivering = new OrderStatus("Đang giao hàng", "delivering");
            OrderStatus success = new OrderStatus("Giao hàng thành công", "success");
            if (!_context.OrderStatus.Any()) {
                _context.OrderStatus.AddRange(new List<OrderStatus>() { placed, logistic, delivering, success });
                _context.SaveChanges();
            }
        }
    }
}
