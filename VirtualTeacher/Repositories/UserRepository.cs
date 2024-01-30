using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Models.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace VirtualTeacher.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        // todo add inclusions
        public IQueryable<User> GetUsers()
        {
            return context.Users
                .Include(u => u.EnrolledCourses)
                .Include(u => u.CreatedCourses)
                .Where(u => !u.IsDeleted);
        }

        public User? Create(UserCreateDto dto)
        {
            var newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserRole = dto.UserRole
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            return newUser;
        }

        public User? GetById(int id)
        {
            User? user = GetUsers().FirstOrDefault(u => u.Id == id);

            return user;
        }

        public User? GetByName(string username)
        {
            User? user = GetUsers().FirstOrDefault(u => u.Username == username);

            return user;
        }

        public User? GetByEmail(string email)
        {
            var test = GetUsers().ToList();
            User? user = GetUsers().FirstOrDefault(u => u.Email == email);

            return user;
        }

        public User? UpdateUser(int id, UserUpdateDto updateData)
        {
            var updatedUser = GetById(id);

            if (updatedUser == null)
                return null;

            updatedUser.FirstName = updateData.FirstName ?? updatedUser.FirstName;
            updatedUser.LastName = updateData.LastName ?? updatedUser.LastName;

            updatedUser.Email = updateData.Email ?? updatedUser.Email;
            updatedUser.Password = updateData.Password ?? updatedUser.Password;

            context.Update(updatedUser);
            context.SaveChanges();

            return updatedUser;
        }

        public User? ChangeRole(int id, int roleId)
        {
            var user = GetById(id);

            if (user == null)
                return null;

            user.UserRole = (UserRole)roleId;
            context.SaveChanges();

            return user;
        }

        public bool? Delete(int id)
        {
            User? user = GetById(id);

            if (user == null)
                return null;

            user.IsDeleted = true;
            context.SaveChanges();

            return true;
        }

        public bool CheckDuplicateEmail(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        public bool CheckDuplicateUsername(string username)
        {
            return context.Users.Any(u => u.Username == username);
        }

        public int GetUserCount()
        {
            return context.Users.Count();
        }

        public IList<User> FilterBy(UserQueryParameters parameters)
        {
            IQueryable<User> result = GetUsers();

            result = FilterByUsername(result, parameters.Username);
            result = FilterByEmail(result, parameters.Email);
            result = FilterByFirstName(result, parameters.FirstName);
            result = FilterByLastName(result, parameters.LastName);
            result = FilterByRole(result, parameters.Role);
            result = SortBy(result, parameters.SortBy);
            result = OrderBy(result, parameters.SortOrder);

            return new List<User>(result.ToList());
        }

        //Query methods
        private static IQueryable<User> FilterByUsername(IQueryable<User> users, string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                username = username.ToLower();
                return users.Where(u => u.Username.ToLower().Contains(username));
            }
            else return users;
        }

        private static IQueryable<User> FilterByEmail(IQueryable<User> users, string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                email = email.ToLower();
                return users.Where(u => u.Email.ToLower().Contains(email));
            }
            else return users;
        }

        private static IQueryable<User> FilterByFirstName(IQueryable<User> users, string firstName)
        {
            if (!string.IsNullOrEmpty(firstName))
            {
                firstName = firstName.ToLower();
                return users.Where(u => u.FirstName.ToLower().Contains(firstName.ToLower()));
            }
            else return users;
        }

        private static IQueryable<User> FilterByLastName(IQueryable<User> users, string lastName)
        {
            if (!string.IsNullOrEmpty(lastName))
            {
                lastName = lastName.ToLower();
                return users.Where(u => u.LastName.ToLower().Contains(lastName));
            }
            else return users;
        }

        private static IQueryable<User> FilterByRole(IQueryable<User> users, string role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                role = role.ToLower();

                switch (role)
                {
                    case "admin":
                        return users.Where(u => u.UserRole == UserRole.Admin);
                    case "teacher":
                        return users.Where(u => u.UserRole == UserRole.Teacher);
                    case "student":
                        return users.Where(u => u.UserRole == UserRole.Student);
                    default:
                        return users;
                }
            }
            else return users;
        }

        private static IQueryable<User> OrderBy(IQueryable<User> users, string sortOrder)
        {
            return (sortOrder == "desc") ? users.Reverse() : users;
        }

        public IQueryable<User> SortBy(IQueryable<User> users, string sortByCriteria)
        {
            {
                switch (sortByCriteria)
                {
                    case "id":
                        return users.OrderBy(u => u.Id);
                    case "username":
                        return users.OrderByDescending(u => u.Username);
                    case "email":
                        return users.OrderByDescending(u => u.Email);
                    case "firstname":
                        return users.OrderByDescending(u => u.FirstName);
                    case "lastname":
                        return users.OrderByDescending(u => u.LastName);
                    default:
                        return users;
                }
            }
        }
    }
}