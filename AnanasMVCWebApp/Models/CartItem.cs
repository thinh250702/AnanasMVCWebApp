namespace AnanasMVCWebApp.Models {
    public class CartItem {
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int SubTotal {  
            get { return Quantity * Price; } 
        }
        public CartItem(ProductSKU productSKU) {
            ProductSKU = productSKU.Id;
            ProductName = productSKU.ProductVariant.Product.Name;
            Quantity = 1;
            Price = productSKU.ProductVariant.Product.Price;
        }
    }
}
