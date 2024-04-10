using AnanasMVCWebApp.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Areas.Admin.Models {
    public class ProductVariantEM {
        public string ProductCode { get; set; } = "";
        public string HexCode { get; set; } = "#cccccc";
        public string ColorName { get; set; } = "";
        public List<string> Images { get; set; } = new List<string>();
        public List<IFormFile> FilesUpload { get; set; } = new List<IFormFile>();
        public List<ProductSkuEM> SKUs { get; set; } = new List<ProductSkuEM>();
    }
}
