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

        //add loggedId validation
        public User Update(int idToUpdate, User user)
        {
            throw new NotImplementedException();
        }

        //add loggedId validation
        public bool Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public int GetUserCount()
        {
            throw new NotImplementedException();
        }
    }
}
