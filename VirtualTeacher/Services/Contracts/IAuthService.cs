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
        void ValidateAdminRole();
        void ValidateAuthorOrAdmin(int id);
        string EncodePassword(string password);
        User GetLoggedUser();
    }
}