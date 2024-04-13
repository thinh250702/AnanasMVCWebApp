using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace AnanasMVCWebApp.Controllers {
    public class CheckoutController : Controller {
        private readonly IOrderService _orderService;
        private readonly UserManager<Customer> _userManager;

        public CheckoutController(IOrderService orderService, UserManager<Customer> userManager) {
            _orderService = orderService;
            _userManager = userManager;
        }

        [Authorize(Roles = ApplicationRole.Customer)]
        public IActionResult Index() {
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            if (cartItems.Count < 1) {
                return RedirectToAction("Index", "Cart");
            }
            var model = _orderService.GetCheckoutModel(cartItems);
            return View(model);
        }
        [Authorize(Roles = ApplicationRole.Customer)]
        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model) {
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            if (ModelState.IsValid) {
                model.CartItems = cartItems;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                bool result = _orderService.CreateOrder(model, user);
                if (result) {
                    TempData["success"] = "Đặt hàng thành công.";
                    return RedirectToAction("Index", "Order");
                }
            }
            var newModel = _orderService.GetCheckoutModel(cartItems);
            newModel.FullName = model.FullName;
            newModel.Phone = model.Phone;
            newModel.Email = model.Email;
            newModel.ShippingMethod = model.ShippingMethod;
            newModel.PaymentMethod = model.PaymentMethod;
            TempData["error"] = "Đặt hàng không thành công. Vui lòng thử lại.";
            return View(newModel);
        }
    }
}
