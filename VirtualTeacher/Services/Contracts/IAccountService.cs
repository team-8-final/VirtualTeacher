using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Services.Contracts
{
    public interface IAccountService
    {
        string GenerateToken(CredentialsDto credentialsDto);
        int GetLoggedUserId();
        bool ValidateCredentials(string email, string password);
        UserRole CheckUserRole(User user);
        void ValidateAdminRole();
        void ValidateAuthorOrAdmin(int userId);
        string EncodePassword(string password);
        User GetLoggedUser();
        bool UserIsLoggedIn();
        User AccountUpdate(AccountUpdateDto dto);
        string Sha512(string input);
        string SaveAccountAvatar(IFormFile file);
        string GetUserAvatar(string username);
        bool DeleteUserAvatar(string username);
        List<Course> GetRatedCourses();
        List<Course> GetCompletedCourses();
    }
}