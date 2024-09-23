using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using AnanasMVCWebApp.Utilities.ObserverPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Areas.Admin.Controllers {
    [Area("Admin")]
    [Authorize(Roles = ApplicationRole.Admin)]
    public class OrderController : Controller {
        private readonly IOrderService _orderService;
        private readonly ISendMailService _sendMailService;
        private readonly IWebHostEnvironment _environment;

        public OrderController(IOrderService orderService, ISendMailService sendMailService, IWebHostEnvironment environment) {
            _orderService = orderService;
            _sendMailService = sendMailService;
            _environment = environment;

            var emailObserver = EmailObserver.GetInstance();
            emailObserver.init(_sendMailService);

            _orderService.Attach(emailObserver);
            _environment = environment;
        }

        public IActionResult Index() {
            var result = _orderService.GetAllOrders();
            ViewBag.Menu = "order";
            return View(result);
        }

        public IActionResult Detail(string id) {
            if (id == "") return RedirectToAction("Index");
            OrderViewModel? order = _orderService.GetOrderByCode(id);
            if (order == null) {
                return NotFound();
            } else {
                ViewBag.Menu = "order";
                return View(order);
            }
        }
        [HttpGet]
        public IActionResult UpdateStatus(string code, int status) {
            if (_orderService.UpdateOrderStatus(code, status)) {
                return Json(new { redirectToUrl = Url.Action("Detail", "Order", new { Area = "Admin", id = code }) });
            }
            return Json("");
        }
    }
}
