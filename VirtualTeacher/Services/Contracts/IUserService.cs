using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts
{
    public interface IUserService
    {
        PaginatedList<User> FilterBy(UserQueryParameters parameters);
        IList<User> GetUsers();
        User GetById(int id);
        User GetByUsername(string name);
        List<User> GetHomeTeachers();
        List<User> GetAvailableTeachers(int courseId);
        public List<User> GetUsersByKeyWord(string keyWord);
        User Create(UserCreateDto dto);
        User UpdateAccount(int idToUpdate, AccountUpdateDto dto);
        User UpdateUser(int idToUpdate, UserUpdateDto dto);
        string Delete(int id);
        User ChangeRole(int id, int roleId);
        int GetUserCount();
        User GetByEmail(string email);
    }
}