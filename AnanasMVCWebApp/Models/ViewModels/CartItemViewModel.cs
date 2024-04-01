namespace AnanasMVCWebApp.Models.ViewModels
{
    public class CartItemViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public string ImageName { get; set; }
        public List<Size> SizeList { get; set; }
        public int Stock { get; set; }
        public int SubTotal
        {
            get { return Quantity * Price; }
        }
        public CartItemViewModel() { }
        public CartItemViewModel(ProductSKU productSKU, int quantity)
        {
            ProductId = productSKU.Id;
            ProductName = GetProductName(productSKU);
            Quantity = quantity;
            Price = productSKU.ProductVariant.Product.Price;
            Size = productSKU.Size.Name;
            Stock = productSKU.StockQuantity;
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
