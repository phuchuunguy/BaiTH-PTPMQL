using MailKit.Net.Smtp;
using MailKit.Security;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace DemoMVC.Models.Process
{
    public class SendMailService : IEmailSender
    {
        private readonly MailSettings mailSettings;
        private readonly ILogger<SendMailService> logger;
        public SendMailService(IOptions<MailSettings> _mailSettings, ILogger<SendMailService> _logger)
        {
            mailSettings = _mailSettings.Value;
            logger = _logger;
            logger.LogInformation("Create SendMailService");
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(message);
            }
            catch (Exception ex) {
                System.IO.Directory.CreateDirectory("mailsSave");
                var emailSaveFile = string.Format(@"mailsSave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailSaveFile);

                logger.LogInformation("Lỗi gửi email, lưu tại - " + emailSaveFile);
                logger.LogError(ex.Message);
            }
            smtp.Disconnect(true);
            logger.LogInformation("Send email to: " + email);
        }
    }
}