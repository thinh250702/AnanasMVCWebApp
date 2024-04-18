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
        //private static EmailObserver _instance;
        private ISendMailService _sendMailService;
        private IWebHostEnvironment _environment;

        public EmailObserver(ISendMailService sendMailService, IWebHostEnvironment environment) {
            _sendMailService = sendMailService;
            _environment = environment;
        }

        /*public static EmailObserver GetInstance() {
            if (_instance == null) {
                _instance = new EmailObserver();
            }
            return _instance;
        }*/
        /*public void init(ISendMailService sendMailService, IWebHostEnvironment environment) {
            _sendMailService = sendMailService;
            _environment = environment;
        }*/
        private string ConvertImageToBase64(string path) {
            if (File.Exists(path)) {
                byte[] imageArray = File.ReadAllBytes(path);
                string base64String = Convert.ToBase64String(imageArray);
                return "data:image/png;base64," + base64String;
            }
            return "";
        }
        private string FormatCurrency(int number) {
            return number.ToString("#,##0.00 VNĐ").Replace(",00", "");
        }
        public async Task UpdateAsync(OrderViewModel order) {
            string body = "";
            string uploadDir = Path.Combine(_environment.WebRootPath, "uploads/");
            string templateDir = Path.Combine(_environment.WebRootPath, "templates/");
            string filePath = Path.Combine(templateDir, "OrderSuccess_Template.html");
            string productFilePath = Path.Combine(templateDir, "ProductRow_Template.html");

            if (File.Exists(filePath) && File.Exists(productFilePath)) {
                using (var streamReader = new StreamReader(filePath)) {
                    body = streamReader.ReadToEnd();
                }

                string productTemplate = "";
                StringBuilder orderItems = new StringBuilder();
                using (var streamReader = new StreamReader(productFilePath)) {
                    productTemplate = streamReader.ReadToEnd();
                }

                foreach (var item in order.OrderItems) {
                    var tmp = productTemplate;
                    string imagePath = Path.Combine(uploadDir, item.ImageName);
                    tmp = tmp.Replace("{product-image}", Path.Combine(uploadDir, item.ImageName));
                    tmp = tmp.Replace("{product-name}", item.ProductName);
                    tmp = tmp.Replace("{product-size}", item.Size);
                    tmp = tmp.Replace("{product-quantity}", item.Quantity.ToString());
                    tmp = tmp.Replace("{product-price}", FormatCurrency(item.Price));
                    tmp = tmp.Replace("{product-subtotal}", FormatCurrency(item.SubTotal));
                    orderItems.Append(tmp);
                };

                body = body.Replace("{orange-logo}", Path.Combine(templateDir, "orange-logo.png"));
                body = body.Replace("{grey-logo}", Path.Combine(templateDir, "grey-logo.png"));
                body = body.Replace("{order-icon}", Path.Combine(templateDir, "box-open-solid.png"));
                body = body.Replace("{address-icon}", Path.Combine(templateDir, "address-book-solid.png"));

                body = body.Replace("{order-list}", orderItems.ToString());
                body = body.Replace("{order-code}", order.OrderCode.ToUpper());
                body = body.Replace("{grand-total}", FormatCurrency(order.GrandTotal));
                body = body.Replace("{discount}", FormatCurrency(order.Discount));
                body = body.Replace("{shipping-fee}", FormatCurrency(order.ShippingFee));
                body = body.Replace("{order-total}", FormatCurrency(order.OrderTotal));
                body = body.Replace("{shipping-method}", order.ShippingMethod);
                body = body.Replace("{payment-method}", order.PaymentMethod);
                body = body.Replace("{fullname}", order.FullName);
                body = body.Replace("{phone}", order.Phone);
                body = body.Replace("{email}", order.Email);
                body = body.Replace("{address}", order.Address);
                body = body.Replace("{city}", order.City);
                body = body.Replace("{district}", order.District);
                body = body.Replace("{ward}", order.Ward);
            }

            MailContent content = new MailContent {
                To = order.Email,
                Subject = $"#{order.OrderCode.ToUpper()} - Đơn hàng đã được giao thành công",
                Body = body,
            };

            /*using (FileStream fs = new FileStream(Path.Combine(templateDir, "test.html"), FileMode.Create)) {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8)) {
                    w.WriteLine(body);
                }
            }*/

            await _sendMailService.SendMail(content);
        }
    }
}
