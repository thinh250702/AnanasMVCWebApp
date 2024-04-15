using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnanasMVCWebApp.Services {
    public class OrderService : IOrderService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public OrderService(IUnitOfWork unitOfWork, IWebHostEnvironment environment) {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        public bool CreateOrder(CheckoutViewModel model, Customer customer) {
            // Create order
            Order order = new Order() {
                OrderDate = DateTime.UtcNow,
                GrandTotal = model.GrandTotal,
                Discount = 0, // default is 0 when create order
                ShippingFee = model.ShippingFee,
                OrderTotal = 0, // default is 0 when create order
                OrderStatus = _unitOfWork.OrderStatusRepository.GetStatusBySlug("placed"),
                ShippingMethod = _unitOfWork.ShippingRepository.GetById(model.ShippingMethod),
                PaymentMethod = _unitOfWork.PaymentRepository.GetById(model.PaymentMethod),
                Customer = customer,
            };
            // Apply coupon - Decorator Pattern
            ConcreteCoupon decorator = new ConcreteCoupon(order);
            if (model.Coupons.Count > 0) {
                model.Coupons.ForEach(code => {
                    Coupon coupon = _unitOfWork.CouponRepository.GetCouponByCode(code)!;
                    decorator = new ConcreteCoupon(decorator);
                    decorator.Coupon = coupon;
                });
            }
            order.OrderTotal = decorator.calculatePrice() + order.ShippingFee;
            order.Discount = order.OrderTotal - order.GrandTotal;
            _unitOfWork.OrderRepository.Insert(order);

            // Get shipping info
            var shippingInfo = new ShippingInfo() {
                Name = customer.FullName,
                Phone = customer.PhoneNumber,
                Email = customer.Email,
                Address = model.Adress,
                City = model.ProvinceName,
                District = model.DistrictName,
                Ward = model.WardName,
                Order = order,
            };
            _unitOfWork.ShippingInfoRepository.Insert(shippingInfo);

            model.CartItems.ForEach(item => {
                var orderDetail = new OrderDetail() {
                    ProductName = item.ProductName,
                    Price = item.Price,
                    ImageName = item.ImageName,
                    Size = item.Size,
                    Quantity = item.Quantity,
                    Order = order,
                };
                _unitOfWork.OrderDetailRepository.Insert(orderDetail);

                var sku = _unitOfWork.ProductSKURepository.GetProductSKUByCode(item.ProductId);
                sku.InStock = sku.InStock - 1;
                _unitOfWork.ProductSKURepository.Update(sku);
            });
            return _unitOfWork.Complete() > 0 ? true : false;
        }

        public CheckoutViewModel GetCheckoutModel(List<CartItemViewModel> cartItems) {
            CheckoutViewModel model = new() {
                CartItems = cartItems,
                ShippingMethods = _unitOfWork.ShippingRepository.GetAll().ToList(),
                PaymentMethods = _unitOfWork.PaymentRepository.GetAll().ToList()
            };
            return model;
        }

        public List<OrderViewModel> GetAllOrdersByCustomer(string customerId) {
            var orderList = new List<OrderViewModel>();
            _unitOfWork.OrderRepository.GetAllOrdersByCustomer(customerId).ForEach(order => {
                var itemList = new List<OrderItemViewModel>();
                _unitOfWork.OrderDetailRepository.GetAllItemsInOrder(order.Id).ForEach(item => {
                    itemList.Add(new OrderItemViewModel(item));
                });
                orderList.Add(new OrderViewModel(itemList, order));
            });
            return orderList;
        }

        public OrderViewModel? GetOrderByCode(string orderCode) {
            var itemList = new List<OrderItemViewModel>();
            var order = _unitOfWork.OrderRepository.GetOrderByCode(orderCode);
            if (order != null) {
                _unitOfWork.OrderDetailRepository.GetAllItemsInOrder(order.Id).ForEach(item => {
                    itemList.Add(new OrderItemViewModel(item));
                });
                var info = _unitOfWork.ShippingInfoRepository.GetShippingInfoByOrder(order.Id);
                var model = new OrderViewModel(itemList, order, info);
                return model;
            }
            return null;
        }

        public Coupon GetCouponByCode(string code) {
            return _unitOfWork.CouponRepository.GetCouponByCode(code);
        }
    }
}
