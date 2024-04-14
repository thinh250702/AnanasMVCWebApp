using Microsoft.CodeAnalysis;

namespace AnanasMVCWebApp.Models.ViewModels {
    public class OrderItemViewModel {
        public string ProductName { get; set; }
        public string ImageName { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int SubTotal {
            get { return Quantity * Price; }
        }
        public OrderItemViewModel(OrderDetail item) {
            ProductName = item.ProductName;
            Price = item.Price;
            ImageName = item.ImageName;
            Size = item.Size;
            Quantity = item.Quantity;
        }
    }
}
