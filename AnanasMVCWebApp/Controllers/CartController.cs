﻿using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Controllers {
    public class CartController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
