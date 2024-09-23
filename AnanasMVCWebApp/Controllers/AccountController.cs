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
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(DataContext context, UserManager<Customer> userManager, SignInManager<Customer> signInManager, RoleManager<IdentityRole> roleManager) {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = ApplicationRole.Customer)]
        public async Task<IActionResult> Index() {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new CustomerViewModel() {
                FullName = user.FullName,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Dob = user.Dob,
                Gender = user.Gender,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(CustomerViewModel model) {
            // Get user by email
            var user = await _userManager.FindByEmailAsync(model.Email);
            // Update the user model
            user.FullName = model.FullName;
            user.Dob = model.Dob;
            user.Gender = model.Gender;
            // Apply the changes if any to the db
            await _userManager.UpdateAsync(user);
            TempData["success"] = "Cập nhật thông tin thành công";
            return View(model);
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
                        /*if (await _userManager.IsInRoleAsync(user, ApplicationRole.Admin)) {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }*/
                        return RedirectToAction("Index", "Home");
                    } else {
                        TempData["error"] = "Mật khẩu không đúng. Vui lòng thử lại";
                    }
                } else {
                    TempData["error"] = "Email không tồn tại. Vui lòng thử lại";
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model){
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null) {
                    TempData["error"] = "Email đã tồn tại trong hệ thống. Vui lòng thử lại";
                } else {
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
                        TempData["success"] = "Tạo tài khoản thành công. Vui lòng đăng nhập.";
                        return RedirectToAction("Login");
                    }
                    TempData["error"] = "Tạo tài khoản không thành công. Vui lòng thử lại";
                }
            }
            return View(model);
        }
    }
}
