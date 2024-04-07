using AnanasMVCWebApp.Areas.Admin.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Areas.Admin.Controllers {
    [Area("Admin")]
    public class ProductController : Controller {
        private readonly DataContext _context;
        public ProductController(DataContext context) {
            _context = context;
        }
        public IActionResult Index() {
            return View();
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductBaseVM model) {
            return View();
        }
        public IActionResult Edit(string id) {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(ProductBaseVM model) {
            return View();
        }
    }
}
