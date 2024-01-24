using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Exceptions;

namespace VirtualTeacher.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        private IQueryable<User> GetUsers()
        {
            return context.Users;
        }

        public User Create(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public User GetById(int id)
        {
            User user = GetUsers().FirstOrDefault(u => u.Id == id);

            return user ?? throw new EntityNotFoundException($"User not found!");
        }

        public User Update(int id, User updateData)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            User user = GetById(id);
            user.IsDeleted = true;
            context.SaveChanges();

            return true; //temp
        }

        public bool CheckDuplicateEmail(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        public int GetUserCount()
        {
            return context.Users.Count();
        }

        public IList<User> FilterBy(UserQueryParameters parameters)
        {
            IQueryable<User> result = GetUsers();

            result = FilterByEmail(result, parameters.Email);
            result = FilterByFirstName(result, parameters.FirstName);
            result = FilterByLastName(result, parameters.LastName);
            result = SortBy(result, parameters.SortBy);
            result = OrderBy(result, parameters.SortOrder);


            return new List<User>(result.ToList());
        }

        //Query methods

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

        private static IQueryable<User> FilterByEmail(IQueryable<User> users, string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                email = email.ToLower();
                return users.Where(u => u.Email.ToLower().Contains(email));
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
