using AnanasMVCWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Utilities.Components {
    public class CartViewComponent : ViewComponent {

        public IViewComponentResult Invoke() {
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            CartViewModel cart = new() {
                CartItems = cartItems,
            };
            return View(cart);
        }
    }
}
