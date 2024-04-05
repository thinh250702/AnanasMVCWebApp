using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;

namespace AnanasMVCWebApp.Services {
    public interface ICartService {
        public List<CartItemViewModel> AddItemToCart(List<CartItemViewModel> cart, string code, int quantity);
        public List<CartItemViewModel> UpdateItemSize(List<CartItemViewModel> cart, string skuCode, string sizeCode);
        public List<CartItemViewModel> UpdateItemQuantity(List<CartItemViewModel> cart, string skuCode, int quantity);
    }
}
