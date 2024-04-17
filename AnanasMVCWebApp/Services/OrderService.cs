using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Repositories;
using AnanasMVCWebApp.Utilities.ObserverPattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace AnanasMVCWebApp.Services {
    public class OrderService : IOrderService {
        private readonly IUnitOfWork _unitOfWork;
        public List<IOrderObserver> Observers = new List<IOrderObserver>();

        public OrderService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
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
            order.Discount = order.OrderTotal - order.GrandTotal - order.ShippingFee;
            _unitOfWork.OrderRepository.Insert(order);

            // Get shipping info
            var shippingInfo = new ShippingInfo() {
                Name = model.FullName,
                Phone = model.Phone,
                Email = model.Email,
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
                model.StatusList = _unitOfWork.OrderStatusRepository.GetAll().ToList();
                return model;
            }
            return null;
        }

        public Coupon GetCouponByCode(string code) {
            return _unitOfWork.CouponRepository.GetCouponByCode(code);
        }

        public List<OrderViewModel> GetAllOrders() {
            var orderList = new List<OrderViewModel>();
            _unitOfWork.OrderRepository.GetAll().ToList().ForEach(order => {
                var itemList = new List<OrderItemViewModel>();
                var info = _unitOfWork.ShippingInfoRepository.GetShippingInfoByOrder(order.Id);
                orderList.Add(new OrderViewModel(itemList, order, info));
            });
            return orderList;
        }

        public bool UpdateOrderStatus(string orderCode, int statusId) {
            var order = _unitOfWork.OrderRepository.GetOrderByCode(orderCode);
            if (order != null) {
                var status = _unitOfWork.OrderStatusRepository.GetById(statusId);
                order.OrderStatus = status;
                if (status.Slug == "success") {
                    Notify(order);
                }
                _unitOfWork.OrderRepository.Update(order);
                _unitOfWork.Complete();
                
                return true;
            }
            return false;
        }

        public void Attach(IOrderObserver observer) {
            Observers.Add(observer);
        }

        public void Detach(IOrderObserver observer) {
            Observers.Remove(observer);
        }

        public void Notify(Order order) {
            foreach (var observer in Observers) {
                observer.UpdateAsync(order);
            }
        }
    }
}
