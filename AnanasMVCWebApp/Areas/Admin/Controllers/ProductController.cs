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
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductBaseEM model) {
            return View();
        }
        public IActionResult Edit(string id) {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(ProductBaseEM model) {
            return View();
        }
    }
}
