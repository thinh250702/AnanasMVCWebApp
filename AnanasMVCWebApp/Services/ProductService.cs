using AnanasMVCWebApp.Areas.Admin.Models;
using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repositories;
using AnanasMVCWebApp.Utilities;
using System.Xml.Linq;

namespace AnanasMVCWebApp.Services
{
    public class ProductService : IProductService {
        private readonly IProductRepository _productRepo;
        private readonly IProductVariantRepository _productVariantRepo;
        private readonly IProductSKURepository _productSKURepository;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICollectionRepository _collectionRepo;
        private readonly IStyleRepository _styleRepo;
        private readonly IColorRepository _colorRepo;
        private readonly IWebHostEnvironment _environment;

        public ProductService(IProductRepository productRepo, IProductVariantRepository productVariantRepo, IProductSKURepository productSKURepository, ICategoryRepository categoryRepo, ICollectionRepository collectionRepo, IStyleRepository styleRepo, IColorRepository colorRepo, IWebHostEnvironment environment) {
            _productRepo = productRepo;
            _productVariantRepo = productVariantRepo;
            _productSKURepository = productSKURepository;
            _categoryRepo = categoryRepo;
            _collectionRepo = collectionRepo;
            _styleRepo = styleRepo;
            _colorRepo = colorRepo;
            _environment = environment;
        }

        public ProductListViewModel GetAllProductByFilters(string category, string collection, string style, string color, string price) {
            ProductQueryable queryable = new(_productVariantRepo.GetAll().AsQueryable());
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
            products.CollectionFilter = (category != null) ? _collectionRepo.GetCollectionsByCategory(queryCategory!) : _collectionRepo.GetAll().ToList();
            products.StyleFilter = (category == "shoes" || category == null) ? _styleRepo.GetAll().ToList() : null;
            products.ColorFilter = _colorRepo.GetAll().ToList();
            queryable.GetItems().ToList().ForEach(item => {
                products.ProductList.Add(ToProductViewModel(item));
            });
            return products;
        }

        public ProductViewModel? GetProductByCode(string code) {
            var result = _productVariantRepo.GetProductVariantByCode(code);
            return (result != null) ? ToProductViewModel(result) : null;
        }

        public ProductBaseEM GetProductForEdit(string code) {
            Product product = _productVariantRepo.GetProductVariantByCode(code)!.Product;
            var result = new ProductBaseEM() {
                ProductId = product.Id,
                Name = product.Name,
                CategoryId = product.Collection.CategoryId,
                Categories = _categoryRepo.GetAll().ToList(),
                StyleId = (product.Style != null) ? product.Style.Id : -1,
                Styles = (product.Style != null) ? _styleRepo.GetAll().ToList() : new List<Style>(),
                CollectionId = product.CollectionId,
                Collections = _collectionRepo.GetCollectionsByCategory(product.Collection.Category),
                Price = product.Price,
                Description = product.Description,
                Variants = new List<ProductVariantEM>()
            };
            _productVariantRepo.GetAllVariantsOfProduct(product).ForEach(item => {
                result.Variants.Add(new ProductVariantEM() {
                    ProductCode = item.Code,
                    HexCode = item.HexCode,
                    ColorName = item.ColorName,
                    Images = GetAllProductImages(item.Code),
                    SKUs = _productSKURepository.GetAllProductSKUs(item),
                });
            });
            return result;
        }

        public List<ProductViewModel> GetProductList() {
            var result = new List<ProductViewModel>();
            _productVariantRepo.GetAll().ToList().ForEach(item => {
                result.Add(ToProductViewModel(item));
            });
            return result;
        }

        public int GetProductMaxOrderQuantity(string code, int cartQuantity) {
            var product = _productSKURepository.GetProductSKUByCode(code);
            int productStock = (product != null) ? product.InStock : 0;
            return (productStock - cartQuantity >= 12) ? 12 : productStock - cartQuantity;
        }

        public ProductViewModel ToProductViewModel(ProductVariant productVariant) {
            return new ProductViewModel() {
                ProductId = productVariant.Code,
                ProductName = productVariant.Product.Name,
                Description = productVariant.Product.Description,
                Price = productVariant.Product.Price,
                InStock = _productVariantRepo.GetInStockOfVariant(productVariant.Code),
                Sold = _productVariantRepo.GetSoldOfVariant(productVariant.Code),
                ColorName = productVariant.ColorName,
                HexCode = productVariant.HexCode,
                Category = productVariant.Product.Collection.Category,
                Collection = productVariant.Product.Collection,
                Style = productVariant.Product.Style,
                ImageList = GetAllProductImages(productVariant.Code),
                SiblingProducts = _productVariantRepo.GetAllSiblingProducts(productVariant),
                SKUList = _productSKURepository.GetAllProductSKUs(productVariant),
            };
        }

        public void UpdateProduct(ProductBaseEM model) {
            Product product = _productRepo.GetById(model.ProductId);
            if (product != null) {
                // Update Base Product
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Collection = _collectionRepo.GetById(model.CollectionId);
                if (model.StyleId != -1) {
                    product.Style = _styleRepo.GetById(model.StyleId);
                }
                model.Variants.ForEach(item => {
                    ProductVariant? variant = _productVariantRepo.GetProductVariantByCode(item.ProductCode);
                    if (variant != null) {
                        variant.ColorName = item.ColorName;
                        variant.HexCode = item.HexCode.Replace("#", "");
                        variant.Color = _colorRepo.GetNearestColor(item.HexCode.Replace("#", ""));
                        if (item.SKUs != null) {
                            item.SKUs.ForEach(skuItem => {
                                ProductSKU? sku = _productSKURepository.GetProductSKUByCode(skuItem.Code);
                                if (sku != null) {
                                    sku.InStock = skuItem.InStock;
                                    _productSKURepository.Update(sku);
                                }
                            });
                            _productSKURepository.Save();
                        }
                        _productVariantRepo.Update(variant);
                    }
                });
                _productVariantRepo.Save();
                _productRepo.Update(product);
                _productRepo.Save();
            }
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
