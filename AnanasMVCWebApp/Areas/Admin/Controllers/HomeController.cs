using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Areas.Admin.Controllers {
    [Area("Admin")]
    //[Authorize(Roles = ApplicationRole.Admin)]
    /*[AdminAuthorization]*/
    /*[CustomAuthorization(role: ApplicationRole.Admin)]*/
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
