using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using AnanasMVCWebApp.Utilities.ObserverPattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Areas.Admin.Controllers {
    [Area("Admin")]
    public class OrderController : Controller {
        private readonly IOrderService _orderService;
        private readonly ISendMailService _sendMailService;

        public OrderController(IOrderService orderService, ISendMailService sendMailService) {
            _orderService = orderService;
            _sendMailService = sendMailService;

            var emailObserver = EmailObserver.GetInstance(_sendMailService);
            _orderService.Attach(emailObserver);
        }

        /*public OrderController(IOrderService orderService) {
            _orderService = orderService;
            // Singleton Pattern
            var emailObserver = EmailObserver.GetInstance();
            _orderService.Attach(emailObserver);
        }*/
        public IActionResult Index() {
            var result = _orderService.GetAllOrders();
            return View(result);
        }
        public IActionResult Detail(string id) {
            if (id == "") return RedirectToAction("Index");
            OrderViewModel? order = _orderService.GetOrderByCode(id);
            if (order == null) {
                return NotFound();
            } else {
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
