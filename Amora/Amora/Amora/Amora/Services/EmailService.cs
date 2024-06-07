using Amora.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Amora.Services
{
    public class EmailService
    {
        private readonly Smtp _smtpSettings;

        public EmailService(Smtp smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendEmailAsync(string recipient, string subject, string body)
        {
            var mailMessage = CreateMailMessage(recipient, subject, body);

            using var smtpClient = CreateSmtpClient();
            await smtpClient.SendMailAsync(mailMessage);
        }

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(_smtpSettings.Server)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.SEmail, _smtpSettings.Password),
                EnableSsl = true
            };
        }

        private MailMessage CreateMailMessage(string recipient, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.BigCompany),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(recipient);

            return mailMessage;
        }
    }
}