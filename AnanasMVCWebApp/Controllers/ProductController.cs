using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Controllers {
    public class ProductController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Detail() {
            return View();
        }
    }
}
