using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;

namespace AnanasMVCWebApp.Services {
    public interface IOrderService {
        public CheckoutViewModel GetCheckoutModel(List<CartItemViewModel> cartItems);
        public bool CreateOrder(CheckoutViewModel model, Customer customer);
        public OrderViewModel? GetOrderByCode(string orderCode);
        public List<OrderViewModel> GetAllOrdersByCustomer(string customerId);
    }
}
