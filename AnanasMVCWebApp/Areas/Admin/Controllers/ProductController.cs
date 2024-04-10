using AnanasMVCWebApp.Areas.Admin.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace AnanasMVCWebApp.Areas.Admin.Controllers {
    [Area("Admin")]
    public class ProductController : Controller {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) {
            _productService = productService;
        }
        public IActionResult Index() {
            return View(_productService.GetProductList());
        }
        public IActionResult Create() {
            return View(_productService.GetProductForCreate());
        }
        [HttpPost]
        public IActionResult Create(ProductBaseEM model) {
        
            return View();
        }
        public IActionResult Edit(string id) {
            return View(_productService.GetProductForEdit(id));
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] string id, [FromForm] ProductBaseEM model) {
            var result = model;
            if (ModelState.IsValid) {
                _productService.UpdateProduct(model);
                TempData["success"] = "Cập nhật sản phẩm thành công";
            } else {
                TempData["error"] = "Cập nhật sản phẩm thất bại";
            }
            return View(_productService.GetProductForEdit(id));
        }
        [HttpGet]
        public IActionResult GetCollections(int id) {
            var data = _productService.GetCollectionsByCategory(id);
            return Json(data);
            //return Json(new { stock = _productService.GetProductMaxOrderQuantity(id, 0) });
        }
        [HttpGet]
        public IActionResult GetVariantTemplate(int categoryId, int collectionId, int count) {
            string code = _productService.GenerateProductCode(collectionId);
            var model = new ProductVariantEM() {
                ProductCode = code,
                SKUs = _productService.GenerateSKUList(categoryId, code)
            };
            ViewBag.Index = count;
            return PartialView("_ProductVarianPartial", model);
        }
    }
}
