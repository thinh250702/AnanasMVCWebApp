using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Policy;

namespace AnanasMVCWebApp.Controllers
{
    public class CartController : Controller {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ICartService _cartService;
        public CartController(DataContext context, IWebHostEnvironment environment, ICartService cartService) {
            _context = context;
            _environment = environment;
            _cartService = cartService;
        }
        public IActionResult Index() {
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            CartViewModel cart = new() {
                CartItems = cartItems,
            };
            return View(cart);
        }
        [HttpPost]
        public IActionResult Add(string quantity, string size) {
            List<CartItemViewModel> cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            var newCart = _cartService.AddItemToCart(cart, size, int.Parse(quantity));
            HttpContext.Session.SetJson("Cart", newCart);
            TempData["success"] = "Thêm vào giỏ hàng thành công";
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public IActionResult ModifyQuantity(string id, int quantity) {
            List<CartItemViewModel> cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");
            var newCart = _cartService.UpdateItemQuantity(cart, id, quantity);
            HttpContext.Session.SetJson("Cart", newCart);
            TempData["success"] = "Cập nhật số lượng thành công";
            return Json(new { redirectToUrl = Url.Action("Index", "Cart") });
        }
        [HttpGet]
        public IActionResult ModifySize(string id, string sizeCode) {
            List<CartItemViewModel> cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");
            var newCart = _cartService.UpdateItemSize(cart, id, sizeCode);
            HttpContext.Session.SetJson("Cart", newCart);
            TempData["success"] = "Cập nhật kích thước thành công";
            return Json(new { redirectToUrl = Url.Action("Index", "Cart") });
        }
        public IActionResult Remove(string id) {
            List<CartItemViewModel> cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");
            cart.RemoveAll(p => p.ProductId == id);
            if (cart.Count == 0) {
                HttpContext.Session.Remove("Cart");
            } else {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["success"] = "Xoá sản phẩm thành công";
            return Json(new { redirectToUrl = Url.Action("Index", "Cart") });
        }
    }
}
