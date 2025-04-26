using System.Net.Mail;
using System.Net;

namespace SmallUsedCars_WebApp.Service
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Read SMTP settings from configuration
            var smtpHost = _configuration["EmailSettings:Host"];
            var smtpPort = int.Parse(_configuration["EmailSettings:Port"]);
            var fromEmail = _configuration["EmailSettings:FromEmail"];
            var smtpUser = _configuration["EmailSettings:Username"];
            var smtpPass = _configuration["EmailSettings:Password"];
            var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSSL"] ?? "true");

            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);
                client.EnableSsl = enableSsl;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false // set to true if you want HTML emails
                };

                mailMessage.To.Add(toEmail);

                // Send the email
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
