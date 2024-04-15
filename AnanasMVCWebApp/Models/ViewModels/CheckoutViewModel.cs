using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Models.ViewModels {
    public class CheckoutViewModel {
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        [Required(ErrorMessage = "Vui lòng nhập họ tên"), DisplayName("Họ tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại"), DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ"), DisplayName("Địa chỉ")]
        public string Adress { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn Tỉnh/Thành phố"), 
        DisplayName("Tỉnh/Thành phố")]
        public int Province {  get; set; }
        public string ProvinceName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn Quận/Huyện"),
        DisplayName("Quận/Huyện")]
        public int District { get; set; }
        public string DistrictName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn Phường/Xã"),
        DisplayName("Phường/Xã")]
        public int Ward { get; set; }
        public string WardName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn phương thức giao hàng")]
        public int ShippingMethod { get; set; }
        public List<ShippingMethod> ShippingMethods { get; set; } = new List<ShippingMethod>();
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public int PaymentMethod { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public int ShippingFee { get; set; }
        /*public int DiscountAmount { get; set; }*/
        public List<string> Coupons { get; set; } = new List<string>(); // Store the coupon code that has been used
        public int GrandTotal {
            get {
                int grandTotal = 0;
                foreach (var item in CartItems) {
                    grandTotal += item.SubTotal;
                }
                return grandTotal;
            }
        }
    }
}
