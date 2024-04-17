using AnanasMVCWebApp.Utilities;

namespace AnanasMVCWebApp.Services {
    public interface ISendMailService {
        public Task SendMail(MailContent mailContent);

        public Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
