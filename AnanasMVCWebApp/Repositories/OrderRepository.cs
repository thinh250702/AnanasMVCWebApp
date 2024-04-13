using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Utilities;

namespace AnanasMVCWebApp.Repositories {
    public class OrderRepository : GenericRepository<Order>, IOrderRepository {
        public OrderRepository(DataContext context) : base(context) {}

        public List<Order> GetAllOrdersByCustomer(string id) {
            return _context.Orders.Where(c => c.CustomerId == id).ToList();
        }
        public Order? GetOrderByCode(string code) {
            return _context.Orders.Where(c => c.Code == code).FirstOrDefault();
        }
    }
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository {
        public OrderDetailRepository(DataContext context) : base(context) {}
        public List<OrderDetail> GetAllItemsInOrder(int orderId) {
            return _context.OrderDetails.Where(c => c.OrderId == orderId).ToList();
        }
    }
    public class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository {
        public OrderStatusRepository(DataContext context) : base(context) { }

        public OrderStatus GetStatusBySlug(string slug) {
            return _context.OrderStatus.Where(s => s.Slug == slug).FirstOrDefault();
        }
    }
    public class ShippingRepository : GenericRepository<ShippingMethod>, IShippingRepository {
        public ShippingRepository(DataContext context) : base(context) { }
    }
    public class PaymentRepository : GenericRepository<PaymentMethod>, IPaymentRepository {
        public PaymentRepository(DataContext context) : base(context) { }
    }
    public class ShippingInfoRepository : GenericRepository<ShippingInfo>, IShippingInfoRepository {
        public ShippingInfoRepository(DataContext context) : base(context) { }

        public ShippingInfo GetShippingInfoByOrder(int orderId) {
            return _context.ShippingInfos.Where(c => c.OrderId == orderId).FirstOrDefault()!;
        }
    }
}
