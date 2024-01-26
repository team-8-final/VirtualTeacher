using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IUserRepository
    {
        IList<User> FilterBy(UserQueryParameters parameters);

        IQueryable<User> GetUsers();
        User GetById(int id);
        User GetByName(string username);
        User Create(User user);
        User Update(int id, User updateData);
        bool Delete(int id);
        int GetUserCount();
        bool CheckDuplicateEmail(string email);
        bool CheckDuplicateUsername(string username);
        User ChangeRole(int id, int roleId);
    }
}