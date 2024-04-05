using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol;

namespace AnanasMVCWebApp.Controllers {
    public class ProductController : Controller {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
            _productService = productService;
        }

        public IActionResult Index(string category, string collection, string style, string color, string price, string page) {
            var list = _productService.GetAllProductByFilters(category, collection, style, color, price);
            return View(list);
        }
        public async Task<IActionResult> Detail(string id = "") {
            if (id == "") return RedirectToAction("Index");
            var product = _productService.GetProductByCode(id);
            if (product == null) {
                return RedirectToAction("Index");
            } else {
                return Content(product.ToJson());
            }
        }
        [HttpGet]
        public IActionResult GetStock(string id) {
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");
            if (cartItems != null) {
                CartItemViewModel? item = cartItems.Where(c => c.ProductId == id).FirstOrDefault();
                if (item != null) {
                    return Json(new { stock = _productService.GetProductMaxOrderQuantity(id, item.Quantity) });
                }
            }
            return Json(new { stock = _productService.GetProductMaxOrderQuantity(id, 0) });
        }
    }
}
