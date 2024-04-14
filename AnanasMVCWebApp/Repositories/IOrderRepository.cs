using AnanasMVCWebApp.Models;

namespace AnanasMVCWebApp.Repositories {
    public interface IOrderRepository : IRepository<Order> {
        public List<Order> GetAllOrdersByCustomer(string id);
        public Order? GetOrderByCode(string code);
    }
    public interface IOrderDetailRepository : IRepository<OrderDetail> {
        public List<OrderDetail> GetAllItemsInOrder(int orderId);
    }
    public interface IOrderStatusRepository : IRepository<OrderStatus> {
        public OrderStatus GetStatusBySlug(string slug);
    }
    public interface IShippingRepository : IRepository<ShippingMethod> { }
    public interface IPaymentRepository : IRepository<PaymentMethod> { }
    public interface IShippingInfoRepository : IRepository<ShippingInfo> {
        public ShippingInfo GetShippingInfoByOrder(int orderId);
    }
    public interface ICouponRepository : IRepository<Coupon> {
        public Coupon? GetCouponByCode(string code);
    }
}
