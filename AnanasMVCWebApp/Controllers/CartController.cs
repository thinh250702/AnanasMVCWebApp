using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Policy;

namespace AnanasMVCWebApp.Controllers
{
    public class CartController : Controller {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _environment;
        public CartController(DataContext context, IWebHostEnvironment environment) {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index() {
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            CartViewModel cart = new() {
                CartItems = cartItems,
            };
            //return Content(HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart").ToJson());
            return View(cart);
        }
        [HttpPost]
        public async Task<IActionResult> Add(string quantity, string size) {
            ProductSKU? sku = await _context.ProductSKUs.FindAsync(size);
            List<CartItemViewModel> cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            CartItemViewModel? item = cart.Where(c => c.ProductId == size).FirstOrDefault();
            if (item == null) {
                cart.Add(new CartItemViewModel(sku, int.Parse(quantity)) {
                    ImageName = GetImage(sku.ProductVariantId),
                    SizeList = GetSize(sku.ProductVariantId)
                });
            } else {
                item.Quantity += int.Parse(quantity);
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpGet]
        public async Task<IActionResult> ModifyQuantity(string id, int quantity) {
            ProductSKU? sku = await _context.ProductSKUs.FindAsync(id);
            List<CartItemViewModel> cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");
            CartItemViewModel? item = cart.Where(c => c.ProductId == id).FirstOrDefault();
            if (item != null) {
                item.Quantity = quantity;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Json(new { redirectToUrl = Url.Action("Index", "Cart") });
        }
        public async Task<IActionResult> ModifySize(string id, string sizeCode) {
            var size = _context.Sizes.Where(s => s.Code == sizeCode).FirstOrDefault();
            string newId = id.Split("-")[0] + "-" + sizeCode;
            ProductSKU? sku = await _context.ProductSKUs.FindAsync(id);
            List<CartItemViewModel> cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");
            CartItemViewModel? item = cart.Where(c => c.ProductId == id).FirstOrDefault();
            if (item != null) {
                // Find if the new size already exist in the cart
                CartItemViewModel? itemNewSize = cart.Where(c => c.ProductId == (newId)).FirstOrDefault();
                if (itemNewSize != null) {
                    item.Quantity += itemNewSize.Quantity;
                    cart.RemoveAll(p => p.ProductId == itemNewSize.ProductId);
                }
                item.ProductId = newId;
                item.Size = (size != null) ? size.Name : "";
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Json(new { redirectToUrl = Url.Action("Index", "Cart") });
        }
        [NonAction]
        public string GetImage(string id) {
            string[] filePaths = Directory.GetFiles(Path.Combine(this._environment.WebRootPath, "uploads/"));
            foreach (string filePath in filePaths) {
                string name = Path.GetFileName(filePath);
                if (name.Contains($"{id}_1")) {
                    return name;
                }
            }
            return "";
        }
        [NonAction]
        public List<Size> GetSize(string id) {
            var sizeList = new List<Size>();
            _context.ProductSKUs.Where(c => c.ProductVariantId == id).ToList().ForEach(x => {
                if (x.StockQuantity != 0) {
                    sizeList.Add(x.Size);
                }
            });
            return sizeList;
        }
    }
}
