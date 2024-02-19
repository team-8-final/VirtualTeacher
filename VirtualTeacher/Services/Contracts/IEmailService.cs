using VirtualTeacher.Models.DTOs;

namespace VirtualTeacher.Services.Contracts
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);

    }
}
