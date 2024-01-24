using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts
{
    public interface IUserService
    {
        IList<User> FilterBy(UserQueryParameters parameters);
        User GetById(int id);
        User Create(User user);
        User Update(int idToUpdate, User updateData);
        bool Delete(int id);
        User PromoteToTeacher(int id);
        User DemoteToStudent(int id);
        int GetUserCount();
    }
}
