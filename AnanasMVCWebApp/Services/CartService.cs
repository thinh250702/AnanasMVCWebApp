using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnanasMVCWebApp.Services {
    public class CartService : ICartService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        public CartService(IUnitOfWork unitOfWork, IWebHostEnvironment environment) {
            _unitOfWork = unitOfWork;
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
            var size = _unitOfWork.SizeRepository.GetSizeByCode(sizeCode);
            string newCode = skuCode.Split("-")[0] + "-" + sizeCode;
            if (item != null) {
                int currentItemQuantity = item.Quantity;
                int changeItemStock = _unitOfWork.ProductSKURepository.GetProductSKUByCode(newCode)!.InStock;
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
            var sku = _unitOfWork.ProductSKURepository.GetProductSKUByCode(code)!;
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
            var variant = _unitOfWork.ProductVariantRepository.GetProductVariantByCode(code);
            var skuList = (variant != null) ? _unitOfWork.ProductSKURepository.GetAllProductSKUs(variant) : new List<ProductSKU>();
            skuList.ForEach(x => {
                if (x.InStock != 0) {
                    sizeList.Add(x.Size);
                }
            });
            return sizeList;
        }

        
    }
}
