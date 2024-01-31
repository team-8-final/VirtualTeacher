using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IAccountService accountService;

        public UserService(IUserRepository userRepository, IAccountService accountService)
        {
            this.userRepository = userRepository;
            this.accountService = accountService;
        }

        public User Create(UserCreateDto dto)
        {
            if (userRepository.CheckDuplicateUsername(dto.Username))
            {
                throw new DuplicateEntityException($"Username {dto.Username} is already in use!");
            }

            if (userRepository.CheckDuplicateEmail(dto.Email))
            {
                throw new DuplicateEntityException($"Email {dto.Email} is already in use!");
            }

            dto.Password = accountService.Sha512(dto.Password);

            var createdUser = userRepository.Create(dto);

            return createdUser ?? throw new Exception($"The registration could not be completed.");
        }

        public IList<User> GetUsers()
        {
           return userRepository.GetUsers().ToList();
        }

        public IList<User> FilterBy(UserQueryParameters parameters)
        {
            if (userRepository.GetUserCount() == 0)
                throw new EntityNotFoundException("No users found!");

            return userRepository.FilterBy(parameters);
        }

        public User GetById(int id)
        {
            var foundUser = userRepository.GetById(id);

            return foundUser ?? throw new EntityNotFoundException($"User with id '{id}' was not found.");
        }

        public User GetByEmail(string email)
        {
            var foundUser = userRepository.GetByEmail(email);

            return foundUser ?? throw new EntityNotFoundException($"User with email '{email}' was not found.");
        }

        public User Update(int idToUpdate, UserUpdateDto dto)
        {
            if (userRepository.CheckDuplicateEmail(dto.Email))
                throw new DuplicateEntityException($"Email {dto.Email} is already in use!");

            if (dto.Password != null)
            {
                dto.Password = accountService.Sha512(dto.Password);
            }

            var updatedUser = userRepository.UpdateUser(idToUpdate, dto);

            return updatedUser ?? throw new Exception("Your profile could not be updated.");
        }

        public string Delete(int id)
        {
            var foundUser = GetById(id);
            var loggedUser = accountService.GetLoggedUser();

            if (foundUser.Username == "admin")
            {
                throw new InvalidOperationException("The admin user cannot be deleted.");
            }

            if (loggedUser.UserRole != UserRole.Admin && foundUser.Id != loggedUser.Id)
            {
                throw new UnauthorizedOperationException("A user can be deleted only by themselves or an admin.");
            }

            bool? userDeleted = userRepository.Delete(id);

            if (userDeleted == true)
                return $"User with id '{id}' was deleted.";

            throw new Exception($"User with id '{id}' could not be deleted.");
        }

        public User ChangeRole(int id, int roleId)
        {

            if (roleId < 0 || roleId > 2)
                throw new InvalidUserInputException("Invalid role id!");

            var foundUser = GetById(id);

            if (Convert.ToInt32(foundUser.UserRole) == roleId)
                throw new InvalidUserInputException($"User is already a {foundUser.UserRole}!");

            var updatedUser = userRepository.ChangeRole(id, roleId);

            return updatedUser ?? throw new Exception($"User with id '{id}' could not be updated.");
        }

        public int GetUserCount()
        {
            return userRepository.GetUserCount();
        }
    }
}