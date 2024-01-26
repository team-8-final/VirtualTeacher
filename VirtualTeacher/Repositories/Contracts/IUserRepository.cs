using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IUserRepository
    {
        IList<User> FilterBy(UserQueryParameters parameters);
        User GetById(int id);
        User GetByName(string username);
        User GetByEmail(string email);
        User Create(User user);
        User Update(int id, User updateData);
        bool Delete(int id);
        int GetUserCount();
        bool CheckDuplicateEmail(string email);
        bool CheckDuplicateUsername(string username);
        User PromoteToTeacher(int id);
        User DemoteToStudent(int id);
    }
}