using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts
{
    public interface IUserService
    {
        IList<User> FilterBy(UserQueryParameters parameters);
        User GetById(int id);
        User Create(User user);
        User Update(int idToUpdate, User user);
        bool Delete(int id);
        int GetUserCount();
    }
}
