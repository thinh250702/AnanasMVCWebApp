using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnanasMVCWebApp.Services {
    public class CartService : ICartService {
        private readonly IProductRepository _productRepo;
        private readonly ISizeRepository _sizeRepo;
        private readonly IWebHostEnvironment _environment;

        public CartService(IProductRepository productRepo, ISizeRepository sizeRepo, IWebHostEnvironment environment) {
            _productRepo = productRepo;
            _sizeRepo = sizeRepo;
            _environment = environment;
        }
        public List<CartItemViewModel> AddItemToCart(List<CartItemViewModel> cart, string code, int quantity) {
            CartItemViewModel? item = cart.Where(c => c.ProductId == code).FirstOrDefault();
            if (item == null) {
                cart.Add(this.CreateCartItem(code, quantity));
            } else {
                item.Quantity += quantity;
            }
            return cart;
        }
        public List<CartItemViewModel> UpdateItemSize(List<CartItemViewModel> cart, string skuCode, string sizeCode) {
            CartItemViewModel? item = cart.Where(c => c.ProductId == skuCode).FirstOrDefault();
            var size = _sizeRepo.GetSizeByCode(sizeCode);
            string newCode = skuCode.Split("-")[0] + "-" + sizeCode;
            if (item != null) {
                int currentItemQuantity = item.Quantity;
                int changeItemStock = _productRepo.GetProductSKUByCode(newCode).StockQuantity;
                // Find if the change size already exist in the cart
                CartItemViewModel? itemNewSize = cart.Where(c => c.ProductId == newCode).FirstOrDefault();
                if (itemNewSize != null) {
                    currentItemQuantity += itemNewSize.Quantity;
                    cart.RemoveAll(p => p.ProductId == itemNewSize.ProductId);
                }
                item.ProductId = newCode;
                item.Size = (size != null) ? size.Name : "";
                if (currentItemQuantity > changeItemStock) {
                    item.Quantity = (changeItemStock > 12) ? 12 : changeItemStock;
                } else {
                    item.Quantity = (currentItemQuantity > 12) ? 12 : currentItemQuantity;
                }
                item.Stock = changeItemStock;
            }
            return cart;
        }
        public List<CartItemViewModel> UpdateItemQuantity(List<CartItemViewModel> cart, string skuCode, int quantity) {
            CartItemViewModel? item = cart.Where(c => c.ProductId == skuCode).FirstOrDefault();
            if (item != null) {
                item.Quantity = quantity;
            }
            return cart;
        }
        private CartItemViewModel CreateCartItem(string code, int quantity) {
            ProductSKU? sku = _productRepo.GetProductSKUByCode(code);
            CartItemViewModel item = new CartItemViewModel(sku, quantity) {
                ImageName = GetProductImage(sku.ProductVariant.Code),
                SizeList = GetAvailableSize(sku.ProductVariant.Code),
            };
            return item;
        }
        private string GetProductImage(string code) {
            string[] filePaths = Directory.GetFiles(Path.Combine(this._environment.WebRootPath, "uploads/"));
            foreach (string filePath in filePaths) {
                string name = Path.GetFileName(filePath);
                if (name.Contains($"{code}_1")) {
                    return name;
                }
            }
            return "";
        }
        private List<Size> GetAvailableSize(string code) {
            var sizeList = new List<Size>();
            var variant = _productRepo.GetProductVariantByCode(code);
            var skuList = (variant != null) ? _productRepo.GetAllProductSKUs(variant) : new List<ProductSKU>();
            skuList.ForEach(x => {
                if (x.StockQuantity != 0) {
                    sizeList.Add(x.Size);
                }
            });
            return sizeList;
        }

        
    }
}
