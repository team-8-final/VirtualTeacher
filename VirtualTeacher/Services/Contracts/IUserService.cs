using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts
{
    public interface IUserService
    {
        IList<User> FilterBy(UserQueryParameters parameters);
        IList<User> GetUsers();
        User GetById(int id);
        User Create(User user);
        User Update(int idToUpdate, User updateData);
        bool Delete(int id);
        User ChangeRole(int id, int roleId);
        int GetUserCount();
    }
}
