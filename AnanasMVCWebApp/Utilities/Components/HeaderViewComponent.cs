using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnanasMVCWebApp.Utilities.Components {
    public class HeaderViewComponent : ViewComponent {
        private readonly UserManager<Customer> _userManager;
        public HeaderViewComponent(UserManager<Customer> userManager) {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync() {
            if (User.Identity!.IsAuthenticated) {
                Customer user = await _userManager.FindByEmailAsync(User.Identity.Name);
                ViewBag.Name = user != null ? user.FullName.Split(" ").Last() : "";
                ViewBag.FullName = user != null ? user.FullName : "";
                ViewBag.Email = user != null ? user.Email : "";
            }
            return View();
        }
    }
}
