using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MimeKit;
using VirtualTeacher.Models.DTOs;
using VirtualTeacher.Services.Contracts;


namespace VirtualTeacher.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config)
        {
            this.config = config;
        }

        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));

            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using (var client = new SmtpClient())
            {
                client.Connect(config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                client.Authenticate(config.GetSection("EmailUsername").Value, config.GetSection("EmailPassword").Value);
                client.Send(email);
                client.Disconnect(true);
            }
        }
    }
}
