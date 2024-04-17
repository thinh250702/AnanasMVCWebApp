using AnanasMVCWebApp.Models;
using AnanasMVCWebApp.Services;

namespace AnanasMVCWebApp.Utilities.ObserverPattern {
    public interface IOrderObserver {
        public Task UpdateAsync(Order order);
    }
    public class EmailObserver : IOrderObserver {
        private static EmailObserver _instance;
        private ISendMailService _sendMailService;

        private EmailObserver(ISendMailService sendMailService) {
            _sendMailService = sendMailService;
        }
        
        public static EmailObserver GetInstance(ISendMailService sendMailService) {
            if (_instance == null) {
                _instance = new EmailObserver(sendMailService);
            }
            return _instance;
        }
        public async Task UpdateAsync(Order order) {
            MailContent content = new MailContent {
                To = "thinhkg1258@gmail.com",
                Subject = "Test Mail",
                Body = "<p><strong>Xin chào Thịnh Nguyễn</strong></p>"
            };
            await _sendMailService.SendMail(content);
        }
    }
}
