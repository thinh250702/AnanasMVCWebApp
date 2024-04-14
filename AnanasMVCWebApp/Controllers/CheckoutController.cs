using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using AnanasMVCWebApp.Utilities;
using AnanasMVCWebApp.Utilities.AdapterPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using NuGet.Protocol;
using System;
using System.Drawing.Drawing2D;
using System.Net.Http.Json;
using System.Text;

namespace AnanasMVCWebApp.Controllers {
    public class CheckoutController : Controller {
        private readonly IOrderService _orderService;
        private readonly UserManager<Customer> _userManager;

        public CheckoutController(IOrderService orderService, UserManager<Customer> userManager) {
            _orderService = orderService;
            _userManager = userManager;
        }

        [Authorize(Roles = ApplicationRole.Customer)]
        public IActionResult Index() {
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            /*if (cartItems.Count < 1) {
                return RedirectToAction("Index", "Cart");
            }*/
            var model = _orderService.GetCheckoutModel(cartItems);
            return View(model);
        }
        [Authorize(Roles = ApplicationRole.Customer)]
        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model) {
            List<CartItemViewModel> cartItems = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();
            if (ModelState.IsValid) {
                model.CartItems = cartItems;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                bool result = _orderService.CreateOrder(model, user);
                if (result) {
                    HttpContext.Session.Remove("Cart"); // Clear the cart when create order successfully
                    TempData["success"] = "Bạn đã đặt hàng thành công!";
                    return RedirectToAction("Index", "Order");
                }
            }
            var newModel = _orderService.GetCheckoutModel(cartItems);
            newModel.FullName = model.FullName;
            newModel.Phone = model.Phone;
            newModel.Email = model.Email;
            newModel.ShippingMethod = model.ShippingMethod;
            newModel.PaymentMethod = model.PaymentMethod;
            TempData["error"] = "Đặt hàng không thành công. Vui lòng thử lại.";
            return View(newModel);
        }
        [HttpGet]
        public async Task<IActionResult> GetGHNFee(int distrcitId, string wardCode) {
            int shippingFee = 0;
            using (var httpClient = new HttpClient()) {
                httpClient.DefaultRequestHeaders.Add("Token", "760d5095-f700-11ee-893f-b6ed573185af");
                httpClient.DefaultRequestHeaders.Add("ShopId", "935803");

                var data = new GHNRequestData() {
                    service_type_id = 2,
                    to_district_id = distrcitId,
                    to_ward_code = wardCode,
                    weight = 1000
                };
                string jsonContent = JsonConvert.SerializeObject(data);

                var request = new HttpRequestMessage { RequestUri = new Uri("https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee"), Method = HttpMethod.Post };
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);
                string apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GHNResponse>(apiResponse);
                shippingFee = result.data.total;
            }
            return Json(shippingFee);
        }
        [HttpGet]
        public async Task<IActionResult> GetGHTKFee(string address, string province, string district) {
            int shippingFee = 0;
            using (var httpClient = new HttpClient()) {
                httpClient.DefaultRequestHeaders.Add("Token", "28b4e343c4510cb58bcddf9ffce5680655a242f8");
                var query = new Dictionary<string, string>()
                {
                    //{ "address", address},
                    { "province", province},
                    { "district", district},
                    { "pick_province", "Quận 7"},
                    { "pick_district", "Hồ Chí Minh"},
                    { "weight", "1000"},
                    //{ "value", "3000000"},
                    { "deliver_option", "none"},
                };
                
                var request = new HttpRequestMessage { 
                    RequestUri = new Uri(QueryHelpers.AddQueryString("https://services.giaohangtietkiem.vn/services/shipment/fee", query)), 
                    Method = HttpMethod.Post };

                HttpResponseMessage response = await httpClient.SendAsync(request);
                string apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GHTKResponse>(apiResponse);
                shippingFee = result.fee.ship_fee_only;
            }
            return Json(shippingFee);
        }
        [HttpGet]
        public IActionResult GetCoupon(string code, int count) {
            var result = _orderService.GetCouponByCode(code);
            if (result != null) {
                ViewBag.Index = count;
                return PartialView("_CouponPartial", result);
            }
            return Json("");
        }
        [HttpGet]
        public IActionResult UpdateTotal(int tempPrice, int percent) {
            return Json("hello");
        }
    }
}
