using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Models.ViewModels;
using AnanasMVCWebApp.Services;
using Org.BouncyCastle.Utilities;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace AnanasMVCWebApp.Utilities.ObserverPattern {
    public interface IOrderObserver {
        public Task UpdateAsync(OrderViewModel order);
    }
    public class EmailObserver : IOrderObserver {
        private ISendMailService _sendMailService;
        private static EmailObserver _instance;
        private EmailObserver() { }
        public static EmailObserver GetInstance() {
            if (_instance == null) {
                _instance = new EmailObserver();
            }
            return _instance;
        }
        public void init(ISendMailService sendMailService) {
            _sendMailService = sendMailService;
        }
        public async Task UpdateAsync(OrderViewModel order) {
            string body = $"Xin chào <strong>{order.FullName}</strong>, Ananas đã giao cho bạn đầy đủ với các sản phẩm được liệt kê bên dưới theo đơn hàng #{order.OrderCode} của bạn, Ananas hi vọng bạn hài lòng với các sản phẩm này!";

            MailContent content = new MailContent {
                To = order.Email,
                Subject = $"#{order.OrderCode.ToUpper()} - Đơn hàng đã được giao thành công",
                Body = body,
            };

            await _sendMailService.SendMail(content);
        }
    }
}
