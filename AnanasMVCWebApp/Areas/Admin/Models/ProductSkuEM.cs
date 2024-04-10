namespace AnanasMVCWebApp.Areas.Admin.Models {
    public class ProductSkuEM {
        public string Code { get; set; } //Format: A1BS001-35
        public int InStock { get; set; } = 0;
        public string SizeName { get; set; } = "";
    }
}
