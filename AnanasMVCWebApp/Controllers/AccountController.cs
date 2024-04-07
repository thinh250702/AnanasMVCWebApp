using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnanasMVCWebApp.Controllers {
    public class AccountController : Controller {
        private readonly DataContext _context;
        UserManager<Customer> _userManager;
        SignInManager<Customer> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(DataContext context, UserManager<Customer> userManager, SignInManager<Customer> signInManager, RoleManager<IdentityRole> roleManager) {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index() {
            return View();
        }
        public IActionResult Login() {
            return View();
        }
        public IActionResult Register() {
            return View();
        }
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null) {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, false, false);
                    if (result.Succeeded) {
                        if (await _userManager.IsInRoleAsync(user, ApplicationRole.Admin)) {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["error"] = "Mật khẩu không đúng. Vui lòng thử lại";
                }
                TempData["error"] = "Email không tồn tại. Vui lòng thử lại";
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model){
            if (ModelState.IsValid) {
                Customer customer = new Customer {
                    FullName = model.FullName,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    Dob = model.Dob,
                    Gender = model.Gender
                };
                IdentityResult result = await _userManager.CreateAsync(customer, model.Password);
                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(customer, ApplicationRole.Customer);
                    return RedirectToAction("Index", "Home");
                }
                /*foreach (IdentityError error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }*/
                TempData["error"] = "Tạo tài khoản không thành công. Vui lòng thử lại";
            }
            return View(model);
        }
    }
}
