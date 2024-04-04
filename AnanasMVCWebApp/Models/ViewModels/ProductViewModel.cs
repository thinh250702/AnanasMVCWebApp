namespace AnanasMVCWebApp.Models.ViewModels {
    public class ProductViewModel {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
        public Category Category { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public Collection Collection { get; set; }
        public Style Style { get; set; }
        public List<string> ImageList { get; set; }
        public List<Dictionary<string, string>> SiblingProducts { get; set; }
        public List<ProductSKU> SKUList { get; set; }
    }
}
