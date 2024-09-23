using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize(Roles = ApplicationRole.Admin)]
    //[Authorize(Roles = ApplicationRole.Admin)]
    /*[AdminAuthorization]*/
    /*[CustomAuthorization(role: ApplicationRole.Admin)]*/
    public class HomeController : Controller {
        public IActionResult Index() {
            ViewBag.Menu = "dashboard";
            return View();
        }
        [HttpGet]
        public IActionResult GetSaleStatistic() {
            return Json("");
        }
        [HttpGet]
        public IActionResult GetOrderStatistic() {
            return Json("");
        }
        [HttpGet]
        public IActionResult GetSaleByCategory() {
            return Json("");
        }
    }
}
