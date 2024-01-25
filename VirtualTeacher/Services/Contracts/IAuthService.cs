using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Services.Contracts
{
    public interface IAuthService
    {
        string GenerateToken(LoginRequest loginRequest);

        int GetLoggedUserId();
        User ValidateCredentials(LoginRequest loginCredentials);
        UserRole CheckRoleAuthorization(User user);

        string EncodePassword(string password);
    }
}