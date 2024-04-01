namespace AnanasMVCWebApp.Models.ViewModels
{
    public class CartViewModel {
        public List<CartItemViewModel> CartItems { get; set; }
        public int GrandTotal { 
            get {
                int grandTotal = 0;
                foreach (var item in CartItems)
                {
                    grandTotal += item.SubTotal;
                }
                return grandTotal;
            } 
        }
        public CartViewModel() { }
    }
}
