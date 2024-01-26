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
            if (userRepository.CheckDuplicateUsername(user.Username))
            {
                throw new DuplicateEntityException($"Username {user.Username} is already in use!");
            }

            if (userRepository.CheckDuplicateEmail(user.Email))
            {
                throw new DuplicateEntityException($"Email {user.Email} is already in use!");
            }

            return userRepository.Create(user);
        }

        public IList<User> FilterBy(UserQueryParameters parameters)
        {
            if (userRepository.GetUserCount() == 0)
                throw new EntityNotFoundException("No users found!");

            return userRepository.FilterBy(parameters);
        }

        public User GetById(int id)
        {
            return userRepository.GetById(id);
        }

        public User Update(int idToUpdate, User updateData)
        {
            if (userRepository.CheckDuplicateEmail(updateData.Email))
            {
                throw new DuplicateEntityException($"Email {updateData.Email} is already in use!");
            }

            return userRepository.Update(idToUpdate, updateData);
        }

        public bool Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public User ChangeRole(int id, int roleId)
        {
            if (roleId < 0 || roleId > 2)
                throw new InvalidUserInputException("Invalid role id!");

            return userRepository.ChangeRole(id, roleId);
        }

        public int GetUserCount()
        {
            return userRepository.GetUserCount();
        }
    }
}