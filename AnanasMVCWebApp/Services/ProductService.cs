using AnanasMVCWebApp.Areas.Admin.Models;
using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repositories;
using AnanasMVCWebApp.Utilities;
using System.Drawing;
using System;
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

        public string GenerateProductCode(int collectionId, int count) {
            string collectionCode = _unitOfWork.CollectionRepository.GetById(collectionId).Code;
            ProductVariant? result = _unitOfWork.ProductVariantRepository.GetLastProductVariantByCode($"A{collectionCode}");
            string code = "";
            if (result == null) {
                // Code = 0
                code = $"A{collectionCode}001";
            } else {
                // Code + 1
                int number = int.Parse(result.Code.Substring(3));
                number += (count + 1);
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
        public bool CreateProduct(ProductBaseEM model) {
            List<ProductVariant> variantList = new List<ProductVariant>();
            List<ProductSKU> skuList = new List<ProductSKU>();
            Product product = new Product() {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Collection = _unitOfWork.CollectionRepository.GetById(model.CollectionId),
                Style = (model.StyleId != -1) ? _unitOfWork.StyleRepository.GetById(model.StyleId) : null
            };

            string uploadDir = Path.Combine(_environment.WebRootPath, "uploads/");
            //Create folder if not exist
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            model.Variants.ForEach(item => {
                ProductVariant variant = new ProductVariant() {
                    Code = item.ProductCode,
                    ColorName = item.ColorName,
                    HexCode = item.HexCode.Replace("#", ""),
                    Color = _unitOfWork.ColorRepository.GetNearestColor(item.HexCode.Replace("#", "")),
                    Product = product
                };
                variantList.Add(variant);

                // File Upload
                UploadFileToFolder(uploadDir, item.ProductCode, item.FilesUpload);

                item.SKUs.ForEach(skuItem => {
                    ProductSKU sku = new ProductSKU() {
                        Code = skuItem.Code,
                        InStock = skuItem.InStock,
                        Size = _unitOfWork.SizeRepository.GetSizeByCode(skuItem.Code.Split("-")[1]),
                        ProductVariant = variant
                    };
                    skuList.Add(sku);
                });
            });
            // Add to database
            _unitOfWork.ProductRepository.Insert(product);
            _unitOfWork.ProductVariantRepository.InsertRange(variantList);
            _unitOfWork.ProductSKURepository.InsertRange(skuList);
            _unitOfWork.Complete();
            return true;
        }
        
        public bool UpdateProduct(ProductBaseEM model) {
            string uploadDir = Path.Combine(_environment.WebRootPath, "uploads/");
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

                        // Update Image
                        UploadFileToFolder(uploadDir, item.ProductCode, item.FilesUpload);

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
                return true;
            }
            return false;
        }
        public List<ProductSkuEM> GenerateSKUList(int categoryId, string code) {
            string categorySlug = _unitOfWork.CategoryRepository.GetById(categoryId).Slug;
            var result = new List<ProductSkuEM>();
            switch(categorySlug) {
                case "shoes":
                    for (int i = 35; i <= 46; i++) {
                        var shoeSize = _unitOfWork.SizeRepository.GetSizeByCode(i.ToString());
                        result.Add(new ProductSkuEM() {
                            Code = $"{code}-{shoeSize.Code}",
                            SizeName = shoeSize.Name
                        });
                    }
                    break;
                case "clothing":
                    for(int i = 1; i <= 5; i++) {
                        var clothingSize = _unitOfWork.SizeRepository.GetSizeByCode($"0{i}");
                        result.Add(new ProductSkuEM() {
                            Code = $"{code}-{clothingSize.Code}",
                            SizeName = clothingSize.Name
                        });
                    }
                    break;
                case "accessories":
                    var accessorySize = _unitOfWork.SizeRepository.GetSizeByCode("00");
                    result.Add(new ProductSkuEM() {
                        Code = $"{code}-{accessorySize.Code}",
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
        private void UploadFileToFolder(string dir, string code, List<IFormFile> list) {
            GetAllProductImages(code).ForEach(imageName => {
                string path = Path.Combine(dir, imageName);
                if (File.Exists(path)) {
                    File.Delete(path);
                }
            });
            for (int i = 0; i < list.Count; i++) {
                string fileExtension = list[i].FileName.Split('.')[1];
                string newFileName = $"{code}_{i}.{fileExtension}";
                string filePath = Path.Combine(dir, newFileName);
                // Check if file exists with its full path
                if (File.Exists(filePath)) {
                    File.Delete(filePath);
                }
                // Add file to folder
                FileStream stream = new FileStream(filePath, FileMode.Create);
                list[i].CopyTo(stream);
                stream.Close();
            }
        }
    }
}
