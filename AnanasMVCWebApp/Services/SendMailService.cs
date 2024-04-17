using AnanasMVCWebApp.Utilities;
using MailKit.Security;
using MimeKit;
using System.Configuration;

namespace AnanasMVCWebApp.Services {
    public class SendMailService : ISendMailService {
        private readonly IConfiguration Configuration;

        public SendMailService(IConfiguration configuration) {
            Configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
            await SendMail(new MailContent() {
                To = email,
                Subject = subject,
                Body = htmlMessage
            });
        }

        public async Task SendMail(MailContent mailContent) {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(Configuration["MailSettings:DisplayName"], Configuration["MailSettings:Mail"]);
            email.From.Add(new MailboxAddress(Configuration["MailSettings:DisplayName"], Configuration["MailSettings:Mail"]));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            // dùng SmtpClient của MailKit
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try {
                smtp.Connect(Configuration["MailSettings:Host"], Int32.Parse(Configuration["MailSettings:Port"]), SecureSocketOptions.StartTls);
                smtp.Authenticate(Configuration["MailSettings:Mail"], Configuration["MailSettings:Password"]);
                await smtp.SendAsync(email);
            } catch (Exception) {
                // Gửi mail thất bại
                System.Diagnostics.Debug.WriteLine("Gửi mail thất bại");
            }

            smtp.Disconnect(true);
        }
    }
}
