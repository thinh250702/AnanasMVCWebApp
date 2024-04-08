using AnanasMVCWebApp.Models;
using System.Linq.Expressions;

namespace AnanasMVCWebApp.Repositories {
    public interface IProductRepository : IRepository<ProductVariant> {
        public List<ProductVariant> GetAllSiblingProducts(ProductVariant variant);
        public List<ProductSKU> GetAllProductSKUs(ProductVariant variant);
        public ProductSKU? GetProductSKUByCode(string code);
        public ProductVariant? GetProductVariantByCode(string code);
        public int GetInStockOfVariant(string code);
        public int GetSoldOfVariant(string code);
    }
    public interface ICategoryRepository : IRepository<Category> {
        public Category? GetCategoryBySlug(string slug);
    }
    public interface ICollectionRepository : IRepository<Collection> {
        public List<Collection> GetCollectionsByCategory(Category category);
    }
    public interface IStyleRepository : IRepository<Style> { 
        public List<Style> GetAllStyles();
    }
    public interface IColorRepository : IRepository<Color> { }
    public interface ISizeRepository : IRepository<Size> {
        public Size GetSizeByCode(string code);
    }
}
