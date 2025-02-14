﻿namespace AnanasMVCWebApp.Models.ViewModels {
    public class ProductViewModel {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int InStock { get; set; }
        public int Sold { get; set; }
        public Category Category { get; set; }
        public string ColorName { get; set; }
        public string HexCode { get; set; }
        public Collection Collection { get; set; }
        public Style Style { get; set; }
        public List<string> ImageList { get; set; }
        public List<ProductVariant> SiblingProducts { get; set; }
        public List<ProductSKU> SKUList { get; set; }
    }
}
