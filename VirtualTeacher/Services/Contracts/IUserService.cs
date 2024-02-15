using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts
{
    public interface IUserService
    {
        PaginatedList<User> FilterBy(UserQueryParameters parameters);
        IList<User> GetUsers();
        User GetById(int id);
        List<User> GetHomeTeachers();
        public List<User> GetUsersByKeyWord(string keyWord);
        User Create(UserCreateDto dto);
        User Update(int idToUpdate, UserUpdateDto dto);
        string Delete(int id);
        User ChangeRole(int id, int roleId);
        int GetUserCount();
        User GetByEmail(string email);
    }
}