using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;
using MimeKit;
using VirtualTeacher.Models.DTOs;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Models;
using VirtualTeacher.Helpers;


namespace VirtualTeacher.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;
        private readonly MailMapper mapper;

        public EmailService(IConfiguration config, MailMapper mapper)
        {
            this.config = config;
            this.mapper = mapper;
        }

        public void EnrollConfirmation(User user, Course course)
        {
            EmailDto request = mapper.MapEnroll(user, course);
            SendEmail(request);
        }

        public void RegistrationConfirmation(User user)
        {
            EmailDto request = mapper.MapRegistration(user);
            SendEmail(request);
        }

        public void TeacherAddition(User user, Course course)
        {
            EmailDto request = mapper.MapTeacherAddition(user, course);
            SendEmail(request);
        }

        public void InviteFriend(string friendEmail, string friendName, User user, Course course)
        {
            EmailDto request = mapper.MapFriendInvite(friendEmail, friendName, user, course);
            SendEmail(request);
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
