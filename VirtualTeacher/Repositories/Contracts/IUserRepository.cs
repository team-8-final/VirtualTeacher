using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IUserRepository
    {
        //Promote to teacher, get count, get by email?
        IList<User> FilterBy(UserQueryParameters parameters);
        User GetById(int id);

        User GetByEmail(string email);
        User Create(User user);
        User Update(int id, User updateData);
        bool Delete(int id);
        int GetUserCount();
        bool CheckDuplicateEmail(string email);
        User PromoteToTeacher(int id);
        User DemoteToStudent(int id);


    }
}