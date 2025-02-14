﻿using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Utilities;

namespace AnanasMVCWebApp.Repositories {
    public class ProductRepository : GenericRepository<Product>, IProductRepository {
        public ProductRepository(DataContext context) : base(context) {}
    }
    public class ProductVariantRepository : GenericRepository<ProductVariant>, IProductVariantRepository {
        public ProductVariantRepository(DataContext context) : base(context) {}

        public List<ProductVariant> GetAllSiblingProducts(ProductVariant variant) {
            return _context.ProductVariants.Where(x => x.ProductId == variant.ProductId && x.Id != variant.Id).ToList();
        }
        public int GetInStockOfVariant(string code) {
            var result = _context.ProductSKUs.Where(c => c.ProductVariant.Code == code);
            return (result != null) ? result.Sum(c => c.InStock) : -1;
        }
        public ProductVariant? GetProductVariantByCode(string code) {
            return _context.ProductVariants.Where(p => p.Code == code).FirstOrDefault();
        }
        public int GetSoldOfVariant(string code) {
            var result = _context.ProductSKUs.Where(c => c.ProductVariant.Code == code);
            return (result != null) ? result.Sum(c => c.Sold) : -1;
        }
        public List<ProductVariant> GetAllVariantsOfProduct(Product product) {
            return _context.ProductVariants.Where(p => p.ProductId == product.Id).ToList();
        }

        public ProductVariant? GetLastProductVariantByCode(string code) {
            return _context.ProductVariants.Where(p => p.Code.Contains(code)).OrderByDescending(p => p.Code).FirstOrDefault();
        }
    }
    public class ProductSKURepository : GenericRepository<ProductSKU>, IProductSKURepository {
        public ProductSKURepository(DataContext context) : base(context) {}
        public List<ProductSKU> GetAllProductSKUs(ProductVariant variant) {
            var result = _context.ProductSKUs.Where(i => i.ProductVariant.Id == variant.Id);
            return (result != null) ? result.ToList() : new List<ProductSKU>();
        }
        public ProductSKU? GetProductSKUByCode(string code) {
            return _context.ProductSKUs.Where(c => c.Code == code).FirstOrDefault();
        }
    }
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository {
        public CategoryRepository(DataContext context) : base(context) {}
        public Category? GetCategoryBySlug(string slug) {
            return _context.Categories.Where(x => x.Slug == slug).FirstOrDefault();
        }
    }
    public class CollectionRepository : GenericRepository<Collection>, ICollectionRepository {
        public CollectionRepository(DataContext context) : base(context) {}
        public List<Collection> GetCollectionsByCategory(Category category) {
            return _context.Collections.Where(c => c.CategoryId == category.Id).ToList();
        }
    }
    public class StyleRepository : GenericRepository<Style>, IStyleRepository {
        public StyleRepository(DataContext context) : base(context) {}
    }
    public class ColorRepository : GenericRepository<Color>, IColorRepository {
        public ColorRepository(DataContext context) : base(context) {}
        private Dictionary<string, int> ConvertHextoRGB(string hexCode) {
            hexCode = hexCode.Replace("#", "");
            var result = new Dictionary<string, int>();
            result["R"] = Convert.ToInt32(hexCode.Substring(0, 2), 16);
            result["G"] = Convert.ToInt32(hexCode.Substring(2, 2), 16);
            result["B"] = Convert.ToInt32(hexCode.Substring(4, 2), 16);
            return result;
        }
        private double CalculateColorDistance(Color source, string target) {
            var targetColor = ConvertHextoRGB(target);
            var sourceColor = ConvertHextoRGB(source.HexCode);
            return Math.Sqrt(Math.Pow(targetColor["R"] - sourceColor["R"], 2) + Math.Pow(targetColor["G"] - sourceColor["G"], 2) + Math.Pow(targetColor["B"] - sourceColor["B"], 2));
        }
        public Color GetNearestColor(string hexCode) {
            List<Color> colorList = GetAll().ToList();

            double closestDistance = CalculateColorDistance(colorList[0], hexCode);
            Color? closestColor = colorList[0];
            /*foreach (var color in colorList)
            {
                var distance = CalculateColorDistance(color, hexCode);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestColor = color;
                }
            }*/
            colorList.ForEach(color => {
                var distance = CalculateColorDistance(color, hexCode);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestColor = color;
                }
            });
            return closestColor;
        }
    }
    public class SizeRepository : GenericRepository<Size>, ISizeRepository {
        public SizeRepository(DataContext context) : base(context) {}
        public Size GetSizeByCode(string code) {
            return _context.Sizes.Where(s => s.Code == code).FirstOrDefault()!;
        }
    }
}
