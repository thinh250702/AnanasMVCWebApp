using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Utilities;

namespace AnanasMVCWebApp.Repositories {
    public class ProductRepository : GenericRepository<ProductVariant>, IProductRepository {
        public ProductRepository(DataContext context) : base(context) {}
        public List<ProductSKU> GetAllProductSKUs(ProductVariant variant) {
            return _context.ProductSKUs.Where(i => i.ProductVariant.Id == variant.Id).ToList();
        }
        public List<ProductVariant> GetAllSiblingProducts(ProductVariant variant) {
            return _context.ProductVariants.Where(x => x.ProductId == variant.ProductId && x.Id != variant.Id).ToList();
        }
        public ProductSKU? GetProductSKUByCode(string code) {
            return _context.ProductSKUs.Where(c => c.Code == code).FirstOrDefault();
        }
        public ProductVariant? GetProductVariantByCode(string code) {
            return _context.ProductVariants.Where(p => p.Code == code).FirstOrDefault();
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
        public List<Style> GetAllStyles() {
            return GetAll().ToList();
        }
    }
    public class ColorRepository : GenericRepository<Color>, IColorRepository {
        public ColorRepository(DataContext context) : base(context) {}
    }
    public class SizeRepository : GenericRepository<Size>, ISizeRepository {
        public SizeRepository(DataContext context) : base(context) {}
        public Size GetSizeByCode(string code) {
            return _context.Sizes.Where(s => s.Code == code).FirstOrDefault()!;
        }
    }
}
