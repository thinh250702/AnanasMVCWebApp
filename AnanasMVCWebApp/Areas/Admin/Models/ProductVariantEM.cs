using AnanasMVCWebApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Areas.Admin.Models {
    public class ProductVariantEM {
        public string ProductCode { get; set; } = "";
        public string HexCode { get; set; } = "";
        [Required(ErrorMessage = "Vui lòng nhập tên màu sắc")]
        public string ColorName { get; set; } = "";
        public List<string> Images { get; set; } = new List<string>();
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh sản phẩm")]
        public List<IFormFile> FilesUpload { get; set; } = new List<IFormFile>();
        public List<ProductSkuEM> SKUs { get; set; } = new List<ProductSkuEM>();
    }
}
