using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Controllers {
    public class OrderController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Detail() {
            return View();
        }
    }
}
