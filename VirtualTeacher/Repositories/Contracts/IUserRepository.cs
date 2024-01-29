using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IUserRepository
    {
        IList<User> FilterBy(UserQueryParameters parameters);

        IQueryable<User> GetUsers();
        User? GetById(int id);
        User? GetByName(string username);
        User? Create(UserCreateDto dto);
        User? UpdateUser(int id, UserUpdateDto updateData);
        bool? Delete(int id);
        int GetUserCount();
        bool CheckDuplicateEmail(string email);
        bool CheckDuplicateUsername(string username);
        User? ChangeRole(int id, int roleId);
        User? GetByEmail(string email);
    }
}