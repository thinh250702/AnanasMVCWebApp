using AnanasMVCWebApp.Areas.Admin.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace AnanasMVCWebApp.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize(Roles = ApplicationRole.Admin)]
    public class ProductController : Controller {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) {
            _productService = productService;
        }
        public IActionResult Index() {
            ViewBag.Menu = "product";
            return View(_productService.GetProductList());
        }
        public IActionResult Create() {
            ViewBag.Menu = "product";
            return View(_productService.GetProductForCreate());
        }
        [HttpPost]
        public IActionResult Create(ProductBaseEM model) {
            if (ModelState.IsValid) {
                bool result = _productService.CreateProduct(model);
                if (result) {
                    TempData["success"] = "Thêm sản phẩm thành công";
                    return RedirectToAction("Index");
                } else {
                    TempData["error"] = "Thêm sản phẩm thất bại";
                    return View(model);
                }
            }
            TempData["error"] = "Vui lòng nhập đầy đủ thông tin sản phẩm";
            ViewBag.Menu = "product";
            return View(_productService.GetProductForCreate());
        }
        public IActionResult Edit(string id) {
            ViewBag.Menu = "product";
            return View(_productService.GetProductForEdit(id));
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] string id, [FromForm] ProductBaseEM model) {
            bool result = _productService.UpdateProduct(model);
            if (result) {
                TempData["success"] = "Cập nhật sản phẩm thành công";
            } else {
                TempData["error"] = "Cập nhật sản phẩm thất bại";
            }
            ViewBag.Menu = "product";
            return View(_productService.GetProductForEdit(id));
        }
        [HttpGet]
        public IActionResult GetCollections(int id) {
            var data = _productService.GetCollectionsByCategory(id);
            return Json(data);
        }
        [HttpGet]
        public IActionResult GetVariantTemplate(int categoryId, int collectionId, int count) {
            string code = _productService.GenerateProductCode(collectionId, count);
            var model = new ProductVariantEM() {
                ProductCode = code,
                SKUs = _productService.GenerateSKUList(categoryId, code)
            };
            ViewBag.Index = count;
            return PartialView("_ProductVarianPartial", model);
        }
    }
}
