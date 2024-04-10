using AnanasMVCWebApp.Areas.Admin.Models;
using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repositories;
using AnanasMVCWebApp.Utilities;
using System.Xml.Linq;

namespace AnanasMVCWebApp.Services
{
    public class ProductService : IProductService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment environment) {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        public ProductListViewModel GetAllProductByFilters(string category, string collection, string style, string color, string price) {
            ProductQueryable queryable = new(_unitOfWork.ProductVariantRepository.GetAll().AsQueryable());
            ProductListViewModel products = new ProductListViewModel();
            Category? queryCategory = _unitOfWork.CategoryRepository.GetCategoryBySlug(category);
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
            products.CollectionFilter = (category != null) ? _unitOfWork.CollectionRepository.GetCollectionsByCategory(queryCategory!) : _unitOfWork.CollectionRepository.GetAll().ToList();
            products.StyleFilter = (category == "shoes" || category == null) ? _unitOfWork.StyleRepository.GetAll().ToList() : null;
            products.ColorFilter = _unitOfWork.ColorRepository.GetAll().ToList();
            queryable.GetItems().ToList().ForEach(item => {
                products.ProductList.Add(ToProductViewModel(item));
            });
            return products;
        }

        public List<Collection> GetCollectionsByCategory(int id) {
            Category cat = _unitOfWork.CategoryRepository.GetById(id);
            return _unitOfWork.CollectionRepository.GetCollectionsByCategory(cat);
        }

        public string GenerateProductCode(int collectionId) {
            string collectionCode = _unitOfWork.CollectionRepository.GetById(collectionId).Code;
            ProductVariant? result = _unitOfWork.ProductVariantRepository.GetLastProductVariantByCode($"A{collectionCode}");
            string code = "";
            if (result == null) {
                // Code = 0
                code = $"A{collectionCode}001";
            } else {
                // Code + 1
                int number = int.Parse(result.Code.Substring(3));
                number++;
                if (number < 10) {
                    code = $"A{collectionCode}00{number}";
                } else if (number < 100) {
                    code = $"A{collectionCode}0{number}";
                } else {
                    code = $"A{collectionCode}{number}";
                }
            }
            return code;
        }

        public ProductViewModel? GetProductByCode(string code) {
            var result = _unitOfWork.ProductVariantRepository.GetProductVariantByCode(code);
            return (result != null) ? ToProductViewModel(result) : null;
        }

        public ProductBaseEM GetProductForCreate() {
            var result = new ProductBaseEM() {
                Categories = _unitOfWork.CategoryRepository.GetAll().ToList(),
                Styles = _unitOfWork.StyleRepository.GetAll().ToList()
            };
            return result;
        }

        public ProductBaseEM GetProductForEdit(string code) {
            Product product = _unitOfWork.ProductVariantRepository.GetProductVariantByCode(code)!.Product;
            var result = new ProductBaseEM() {
                ProductId = product.Id,
                Name = product.Name,
                CategoryId = product.Collection.CategoryId,
                Categories = _unitOfWork.CategoryRepository.GetAll().ToList(),
                StyleId = (product.Style != null) ? product.Style.Id : -1,
                Styles = (product.Style != null) ? _unitOfWork.StyleRepository.GetAll().ToList() : new List<Style>(),
                CollectionId = product.CollectionId,
                Collections = _unitOfWork.CollectionRepository.GetCollectionsByCategory(product.Collection.Category),
                Price = product.Price,
                Description = product.Description,
                /*Variants = new List<ProductVariantEM>()*/
            };
            _unitOfWork.ProductVariantRepository.GetAllVariantsOfProduct(product).ForEach(variantItem => {
                var variant = new ProductVariantEM() {
                    ProductCode = variantItem.Code,
                    HexCode = variantItem.HexCode,
                    ColorName = variantItem.ColorName,
                    Images = GetAllProductImages(variantItem.Code),
                };
                _unitOfWork.ProductSKURepository.GetAllProductSKUs(variantItem).ForEach(skuItem => {
                    variant.SKUs.Add(new ProductSkuEM() {
                        Code = skuItem.Code,
                        InStock = skuItem.InStock,
                        SizeName = skuItem.Size.Name
                    });
                });
                result.Variants.Add(variant);
            });
            return result;
        }

        public List<ProductViewModel> GetProductList() {
            var result = new List<ProductViewModel>();
            _unitOfWork.ProductVariantRepository.GetAll().ToList().ForEach(item => {
                result.Add(ToProductViewModel(item));
            });
            return result;
        }

        public int GetProductMaxOrderQuantity(string code, int cartQuantity) {
            var product = _unitOfWork.ProductSKURepository.GetProductSKUByCode(code);
            int productStock = (product != null) ? product.InStock : 0;
            return (productStock - cartQuantity >= 12) ? 12 : productStock - cartQuantity;
        }

        public ProductViewModel ToProductViewModel(ProductVariant productVariant) {
            return new ProductViewModel() {
                ProductId = productVariant.Code,
                ProductName = productVariant.Product.Name,
                Description = productVariant.Product.Description,
                Price = productVariant.Product.Price,
                InStock = _unitOfWork.ProductVariantRepository.GetInStockOfVariant(productVariant.Code),
                Sold = _unitOfWork.ProductVariantRepository.GetSoldOfVariant(productVariant.Code),
                ColorName = productVariant.ColorName,
                HexCode = productVariant.HexCode,
                Category = productVariant.Product.Collection.Category,
                Collection = productVariant.Product.Collection,
                Style = productVariant.Product.Style,
                ImageList = GetAllProductImages(productVariant.Code),
                SiblingProducts = _unitOfWork.ProductVariantRepository.GetAllSiblingProducts(productVariant),
                SKUList = _unitOfWork.ProductSKURepository.GetAllProductSKUs(productVariant),
            };
        }

        public void UpdateProduct(ProductBaseEM model) {
            Product product = _unitOfWork.ProductRepository.GetById(model.ProductId);
            if (product != null) {
                // Update Base Product
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Collection = _unitOfWork.CollectionRepository.GetById(model.CollectionId);
                if (model.StyleId != -1) {
                    product.Style = _unitOfWork.StyleRepository.GetById(model.StyleId);
                }
                model.Variants.ForEach(item => {
                    ProductVariant? variant = _unitOfWork.ProductVariantRepository.GetProductVariantByCode(item.ProductCode);
                    if (variant != null) {
                        variant.ColorName = item.ColorName;
                        variant.HexCode = item.HexCode.Replace("#", "");
                        variant.Color = _unitOfWork.ColorRepository.GetNearestColor(item.HexCode.Replace("#", ""));
                        if (item.SKUs != null) {
                            item.SKUs.ForEach(skuItem => {
                                ProductSKU? sku = _unitOfWork.ProductSKURepository.GetProductSKUByCode(skuItem.Code);
                                if (sku != null) {
                                    sku.InStock = skuItem.InStock;
                                    _unitOfWork.ProductSKURepository.Update(sku);
                                }
                            });
                        }
                        _unitOfWork.ProductVariantRepository.Update(variant);
                    }
                });
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Complete();
            }
        }
        public List<ProductSkuEM> GenerateSKUList(int categoryId, string code) {
            string categorySlug = _unitOfWork.CategoryRepository.GetById(categoryId).Slug;
            var result = new List<ProductSkuEM>();
            switch(categorySlug) {
                case "shoes":
                    for (int i = 35; i <= 46; i++) {
                        var shoeSize = _unitOfWork.SizeRepository.GetSizeByCode(i.ToString());
                        result.Add(new ProductSkuEM() {
                            Code = $"{code}-{shoeSize.Name}",
                            SizeName = shoeSize.Name
                        });
                    }
                    break;
                case "clothing":
                    for(int i = 1; i <= 5; i++) {
                        var clothingSize = _unitOfWork.SizeRepository.GetSizeByCode($"0{i}");
                        result.Add(new ProductSkuEM() {
                            Code = $"{code}-{clothingSize.Name}",
                            SizeName = clothingSize.Name
                        });
                    }
                    break;
                case "accessories":
                    var accessorySize = _unitOfWork.SizeRepository.GetSizeByCode("00");
                    result.Add(new ProductSkuEM() {
                        Code = $"{code}-{accessorySize.Name}",
                        SizeName = accessorySize.Name
                    });
                    break;
            }
            return result;
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
