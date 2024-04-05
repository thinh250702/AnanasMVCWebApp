using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repositories;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AnanasMVCWebApp.Services {
    public class ProductService : IProductService {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICollectionRepository _collectionRepo;
        private readonly IStyleRepository _styleRepo;
        private readonly IColorRepository _colorRepo;
        private readonly IWebHostEnvironment _environment;

        public ProductService(IProductRepository productRepo, ICategoryRepository categoryRepo, ICollectionRepository collectionRepo, IStyleRepository styleRepo, IColorRepository colorRepo, IWebHostEnvironment environment) {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _collectionRepo = collectionRepo;
            _styleRepo = styleRepo;
            _colorRepo = colorRepo;
            _environment = environment;
        }

        public ProductListViewModel GetAllProductByFilters(string category, string collection, string style, string color, string price) {
            ProductQueryable queryable = new(_productRepo.GetAll().AsQueryable());
            ProductListViewModel products = new ProductListViewModel();
            Category? queryCategory = _categoryRepo.GetCategoryBySlug(category);
            if (queryCategory != null) {
                queryable.setFilterStrategy(new FilterByCategory());
                queryable.performFilter(queryCategory.Slug);
            }
            if (collection != null) {
                queryable.setFilterStrategy(new FilterByCollection());
                queryable.performFilter(collection);
            }
            if (style != null) {
                queryable.setFilterStrategy(new FilterByStyle());
                queryable.performFilter(style);
            }
            if (color != null) {
                queryable.setFilterStrategy(new FilterByColor());
                queryable.performFilter(color);
            }
            products.Category = queryCategory;
            products.CollectionFilter = _collectionRepo.GetCollectionsByCategory(queryCategory);
            products.StyleFilter = _styleRepo.GetStylesByCategory(queryCategory);
            products.ColorFilter = _colorRepo.GetAll().ToList();
            queryable.GetItems().ToList().ForEach(item => {
                products.ProductList.Add(ToProductViewModel(item));
            });
            return products;
        }

        public ProductViewModel? GetProductByCode(string code) {
            var result = _productRepo.GetProductVariantByCode(code);
            return (result != null) ? ToProductViewModel(result) : null;
        }

        public int GetProductMaxOrderQuantity(string code, int cartQuantity) {
            var product = _productRepo.GetProductSKUByCode(code);
            int productStock = (product != null) ? product.StockQuantity : 0;
            return (productStock - cartQuantity >= 12) ? 12 : productStock - cartQuantity;
        }

        public ProductViewModel ToProductViewModel(ProductVariant productVariant) {
            return new ProductViewModel() {
                ProductId = productVariant.Code,
                ProductName = productVariant.Product.Name,
                Description = productVariant.Product.Description,
                Price = productVariant.Product.Price,
                ColorName = productVariant.ColorName,
                HexCode = productVariant.HexCode,
                Category = productVariant.Product.Collection.Category,
                Collection = productVariant.Product.Collection,
                Style = productVariant.Product.Style,
                ImageList = GetAllProductImages(productVariant.Code),
                SiblingProducts = _productRepo.GetAllSiblingProducts(productVariant),
                SKUList = _productRepo.GetAllProductSKUs(productVariant),
            };
        }
        private List<string> GetAllProductImages(string code) {
            var imageList = new List<string>();
            string[] filePaths = Directory.GetFiles(Path.Combine(this._environment.WebRootPath, "uploads/"));
            foreach (string filePath in filePaths) {
                string name = Path.GetFileName(filePath);
                if (name.Contains(code)) {
                    imageList.Add(name);
                }
            }
            return imageList;
        }
    }
}
