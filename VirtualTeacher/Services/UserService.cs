using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Create(User user)
        {
            if (userRepository.CheckDuplicateEmail(user.Email))
            {
                throw new DuplicateEntityException($"Email {user.Email} is already in use!");
            }

            return userRepository.Create(user);
        }

        public IList<User> FilterBy(UserQueryParameters parameters)
        {
            if (userRepository.GetUserCount() == 0)
                throw new EntityNotFoundException("No users!");

            return userRepository.FilterBy(parameters);
        }

        public User GetById(int id)
        {
            return userRepository.GetById(id);
        }

        //add loggedId = admin/idToUpdate validation
        public User Update(int idToUpdate, User updateData)
        {
            return userRepository.Update(idToUpdate, updateData);
        }

        //add loggedId = admin validation
        public bool Delete(int id)
        {
            return userRepository.Delete(id);
        }

        //add loggedId = admin validation
        public User PromoteToTeacher(int id)
        {
            return userRepository.PromoteToTeacher(id);
        }

        //add loggedId = admin validation
        public User DemoteToStudent(int id)
        {
            return userRepository.DemoteToStudent(id);
        }

        public int GetUserCount()
        {
            throw new NotImplementedException();
        }
    }
}
