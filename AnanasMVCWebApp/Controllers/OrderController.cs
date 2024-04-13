using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Controllers {
    public class OrderController : Controller {
        private readonly IOrderService _orderService;
        private readonly UserManager<Customer> _userManager;

        public OrderController(IOrderService orderService, UserManager<Customer> userManager) {
            _orderService = orderService;
            _userManager = userManager;
        }

        [Authorize(Roles = ApplicationRole.Customer)]
        public async Task<IActionResult> Index() {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = _orderService.GetAllOrdersByCustomer(user.Id);
            return View(result);
        }

        [Authorize(Roles = ApplicationRole.Customer)]
        public IActionResult Detail(string id) {
            if (id == "") return RedirectToAction("Index");
            OrderViewModel? order = _orderService.GetOrderByCode(id);
            if (order == null) {
                return NotFound();
            } else {
                return View(order);
            }
        }
    }
}
