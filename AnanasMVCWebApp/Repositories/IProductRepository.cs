using AnanasMVCWebApp.Models;
using System.Linq.Expressions;

namespace AnanasMVCWebApp.Repositories {
    public interface IProductRepository : IRepository<Product> {}
    public interface IProductVariantRepository : IRepository<ProductVariant> {
        public ProductVariant? GetProductVariantByCode(string code);
        public List<ProductVariant> GetAllSiblingProducts(ProductVariant variant);
        public List<ProductVariant> GetAllVariantsOfProduct(Product product);
        public int GetInStockOfVariant(string code);
        public int GetSoldOfVariant(string code);
        public ProductVariant? GetLastProductVariantByCode(string code);
    }
    public interface IProductSKURepository : IRepository<ProductSKU> {
        public ProductSKU? GetProductSKUByCode(string code);
        public List<ProductSKU> GetAllProductSKUs(ProductVariant variant);
    }
    public interface ICategoryRepository : IRepository<Category> {
        public Category? GetCategoryBySlug(string slug);
    }
    public interface ICollectionRepository : IRepository<Collection> {
        public List<Collection> GetCollectionsByCategory(Category category);
    }
    public interface IStyleRepository : IRepository<Style> {}
    public interface IColorRepository : IRepository<Color> {
        public Color GetNearestColor(string hexCode);
    }
    public interface ISizeRepository : IRepository<Size> {
        public Size GetSizeByCode(string code);
    }
}
