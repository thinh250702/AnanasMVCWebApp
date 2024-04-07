using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models.ViewModels {
    public class RegisterViewModel {
        [Required(ErrorMessage = "Please enter your name"), DisplayName("Họ tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter your phone number"), DisplayName("Số điện thoại")]
        public string Phone {  get; set; }
        [Required(ErrorMessage = "Please enter your email address"), EmailAddress(ErrorMessage = "Please provide an valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please provide your date of birth"), DataType(DataType.Date), DisplayName("Ngày sinh")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Please choose your gender"), DisplayName("Giới tính")]
        public bool Gender { get; set; }
        [DataType(DataType.Password), Required(ErrorMessage = "Please enter your password"), DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter confirm password"), Compare(nameof(Password), ErrorMessage = "Passwords don't match."), DisplayName("Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }

    }
}
