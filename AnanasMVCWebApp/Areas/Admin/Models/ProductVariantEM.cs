using AnanasMVCWebApp.Models;

namespace AnanasMVCWebApp.Areas.Admin.Models {
    public class ProductVariantEM {
        public string ProductCode { get; set; }
        public string HexCode { get; set; }
        public string ColorName { get; set; }
        public List<string> Images { get; set; }
        public List<ProductSKU> SKUs { get; set; }
    }
}
