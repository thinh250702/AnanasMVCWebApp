using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Controllers {
    public class CustomerViewModel {
        [DisplayName("Họ tên")]
        public string FullName { get; set; }
        [DisplayName("Số điện thoại")]
        public string Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Ngày sinh"), DataType(DataType.Date)]
        public DateTime Dob {  get; set; }
        [DisplayName("Giới tính")]
        public bool Gender { get; set; }
    }
}
