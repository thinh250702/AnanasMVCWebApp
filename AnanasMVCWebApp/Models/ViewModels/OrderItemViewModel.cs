using Microsoft.CodeAnalysis;

namespace AnanasMVCWebApp.Models.ViewModels {
    public class OrderItemViewModel {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int SubTotal {
            get { return Quantity * Price; }
        }
        public OrderItemViewModel(ProductSKU productSKU, int quantity) {
            ProductCode = productSKU.Code;
            ProductName = GetProductName(productSKU);
            Quantity = quantity;
            Price = productSKU.ProductVariant.Product.Price;
            Size = productSKU.Size.Name;
        }

        private string GetProductName(ProductSKU sku) {
            string name = "";
            if (sku.ProductVariant.Product.Style != null) {
                name = $"{sku.ProductVariant.Product.Name} - {sku.ProductVariant.Product.Style.Name} - {sku.ProductVariant.ColorName}";
            } else {
                name = $"{sku.ProductVariant.Product.Collection.Name} - {sku.ProductVariant.Product.Name} - {sku.ProductVariant.ColorName}";
            }
            return name;
        }
    }
}
