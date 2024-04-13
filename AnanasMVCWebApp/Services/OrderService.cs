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
            Order order = new Order() {
                OrderDate = DateTime.UtcNow,
                GrandTotal = model.GrandTotal,
                Discount = model.DiscountAmount,
                ShippingFee = model.ShippingFee,
                OrderTotal = model.OrderTotal,
                OrderStatus = _unitOfWork.OrderStatusRepository.GetStatusBySlug("placed-order"),
                ShippingMethod = _unitOfWork.ShippingRepository.GetById(model.ShippingMethod),
                PaymentMethod = _unitOfWork.PaymentRepository.GetById(model.PaymentMethod),
                Customer = customer,
            };
            _unitOfWork.OrderRepository.Insert(order);
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
                    ProductSKU = _unitOfWork.ProductSKURepository.GetProductSKUByCode(item.ProductId)!,
                    Order = order,
                    Quantity = item.Quantity,
                    SubTotal = item.SubTotal,
                };
                _unitOfWork.OrderDetailRepository.Insert(orderDetail);
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
                    itemList.Add(new OrderItemViewModel(item.ProductSKU, item.Quantity) {
                        ImageName = GetProductImage(item.ProductSKU.ProductVariant.Code),
                    });
                });
                orderList.Add(new OrderViewModel() {
                    OrderItems = itemList,
                    OrderCode = order.Code,
                    OrderDate = order.OrderDate,
                    Discount = order.Discount,
                    ShippingFee = order.ShippingFee,
                    GrandTotal = order.GrandTotal,
                    OrderTotal = order.OrderTotal,
                    StatusName = order.OrderStatus.Name
                });
            });
            return orderList;
        }

        public OrderViewModel? GetOrderByCode(string orderCode) {
            var itemList = new List<OrderItemViewModel>();
            var order = _unitOfWork.OrderRepository.GetOrderByCode(orderCode);
            if (order != null) {
                _unitOfWork.OrderDetailRepository.GetAllItemsInOrder(order.Id).ForEach(item => {
                    itemList.Add(new OrderItemViewModel(item.ProductSKU, item.Quantity) {
                        ImageName = GetProductImage(item.ProductSKU.ProductVariant.Code)
                    });
                });
                var info = _unitOfWork.ShippingInfoRepository.GetShippingInfoByOrder(order.Id);
                var model = new OrderViewModel() {
                    OrderItems = itemList,
                    OrderCode = order.Code,
                    OrderDate = order.OrderDate,
                    Discount = order.Discount,
                    ShippingFee = order.ShippingFee,
                    GrandTotal = order.GrandTotal,
                    OrderTotal = order.OrderTotal,
                    StatusName = order.OrderStatus.Name,
                    StatusSlug = order.OrderStatus.Slug,
                    ShippingMethod = order.ShippingMethod.Name,
                    PaymentMethod = order.PaymentMethod.Name,
                    FullName = info.Name,
                    Phone = info.Phone,
                    Email = info.Email,
                    Address = info.Address,
                    City = info.City,
                    District = info.District,
                    Ward = info.Ward,
                };
                return model;
            }
            return null;
        }

        private string GetProductImage(string code) {
            string[] filePaths = Directory.GetFiles(Path.Combine(_environment.WebRootPath, "uploads/"));
            foreach (string filePath in filePaths) {
                string name = Path.GetFileName(filePath);
                if (name.Contains($"{code}_1")) {
                    return name;
                }
            }
            return "";
        }
    }
}
