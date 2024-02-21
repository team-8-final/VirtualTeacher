using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs;

namespace VirtualTeacher.Services.Contracts
{
    public interface IEmailService
    {
        public void RegistrationConfirmation(User user);
        public void EnrollConfirmation(User user, Course course);
        public void TeacherAddition(User user, Course course);
        public void InviteFriend(string friendEmail, string friendName, User user, Course course);
        public void Contact(string email, string text);
        void SendEmail(EmailDto request);

    }
}
