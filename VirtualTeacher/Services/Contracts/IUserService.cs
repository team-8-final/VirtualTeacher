using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts
{
    public interface IUserService
    {
        IList<User> FilterBy(UserQueryParameters parameters);
        IList<User> GetUsers();
        User GetById(int id);
        User Create(UserCreateDto user);
        User Update(int idToUpdate, UserUpdateDto updateData);
        string Delete(int id);
        User ChangeRole(int id, int roleId);
        int GetUserCount();
        User GetByEmail(string email);
    }
}