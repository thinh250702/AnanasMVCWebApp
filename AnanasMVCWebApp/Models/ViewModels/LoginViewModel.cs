using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models.ViewModels {
    public class LoginViewModel {
        [Required(ErrorMessage = "Please enter your email address"), EmailAddress(ErrorMessage = "Please provide an valid email")]
        public string Email { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}
