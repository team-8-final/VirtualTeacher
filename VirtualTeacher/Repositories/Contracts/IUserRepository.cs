using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IUserRepository
    {
        PaginatedList<User> FilterBy(UserQueryParameters parameters);
        IQueryable<User> GetUsers();
        List<User> GetHomeTeachers();
        List<User> GetAvailableTeachers(int courseId);
        public List<User> GetUsersByKeyWord(string keyWord);
        User? GetById(int id);
        User? GetByUsername(string username);
        User? Create(UserCreateDto dto);
        User? UpdateAccount(int id, AccountUpdateDto updateData);
        User? UpdateUser(int id, UserUpdateDto updateData);
        bool? Delete(int id);
        int GetUserCount();
        bool CheckDuplicateEmail(string email);
        bool CheckDuplicateUsername(string username);
        User? ChangeRole(int id, int roleId);
        User? GetByEmail(string email);
    }
}