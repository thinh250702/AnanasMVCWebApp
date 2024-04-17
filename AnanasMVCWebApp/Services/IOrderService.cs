using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Utilities.ObserverPattern;

namespace AnanasMVCWebApp.Services {
    public interface IOrderService : IOrderNotifier {
        public CheckoutViewModel GetCheckoutModel(List<CartItemViewModel> cartItems);
        public bool CreateOrder(CheckoutViewModel model, Customer customer);
        public OrderViewModel? GetOrderByCode(string orderCode);
        public List<OrderViewModel> GetAllOrdersByCustomer(string customerId);
        public List<OrderViewModel> GetAllOrders();
        public Coupon GetCouponByCode(string code);
        public bool UpdateOrderStatus(string orderCode, int statusId);
    }
}
