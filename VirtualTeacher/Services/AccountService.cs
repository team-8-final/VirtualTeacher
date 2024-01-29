using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration config;

        public AccountService(IUserRepository userRepository, IConfiguration config,
        IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.config = config;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken(CredentialsDto credentialsDto)
        {
            var user = userRepository.GetByEmail(credentialsDto.Email);

            if (user == null)
            {
                throw new EntityNotFoundException("Invalid credentials.");
            }

            List<Claim> claims = new List<Claim>
            {
                new("UserId", user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.UserRole.ToString())
            };

            switch (user.UserRole)
            {
                case UserRole.Admin:
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    break;
                case UserRole.Teacher:
                    claims.Add(new Claim(ClaimTypes.Role, "Teacher"));
                    break;
                case UserRole.Student:
                    claims.Add(new Claim(ClaimTypes.Role, "Student"));
                    break;
                default:
                    claims.Add(new Claim(ClaimTypes.Role, "Anonymous"));
                    break;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                config.GetSection("Jwt:Key").Value!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        public UserRole CheckUserRole(User user)
        {
            switch (user.UserRole)
            {
                case UserRole.Admin:
                    return UserRole.Admin;
                case UserRole.Teacher:
                    return UserRole.Teacher;
                case UserRole.Student:
                    return UserRole.Student;
                default:
                    return UserRole.Anonymous;
            }
        }

        public string EncodePassword(string password)
        {
            var encodedPassword = Encoding.UTF8.GetBytes(password);

            return Convert.ToBase64String(encodedPassword);
        }

        public int GetLoggedUserId()
        {
            var result = -1;

            if (httpContextAccessor.HttpContext is null)
            {
                throw new UnauthorizedOperationException("You are not logged in!");
            }


            // TODO remove test
            var test = httpContextAccessor.HttpContext.User.FindFirstValue("UserId");

            if (httpContextAccessor.HttpContext.User.FindFirstValue("UserId") is not null
                && httpContextAccessor.HttpContext.User.FindFirstValue("UserId") != null)
            {
                result = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue("UserId"));
            }

            return result;
        }

        public User GetLoggedUser()
        {
            var loggedId = GetLoggedUserId();

            if (loggedId == -1)
            {
                throw new UnauthorizedOperationException("You are not logged in!");
            }

            var loggedUser = userRepository.GetById(loggedId);

            return loggedUser ?? throw new UnauthorizedOperationException("You are not logged in!");
        }

        public bool UserIsLoggedIn()
        {
            var loggedId = GetLoggedUserId();

            if (loggedId == -1)
            {
                return false;
            }

            return true;
        }

        public void ValidateAdminRole()
        {
            var currentRole = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (currentRole != UserRole.Admin.ToString())
            {
                throw new UnauthorizedOperationException("You are not an admin!");
            }
        }

        public void ValidateAuthorOrAdmin(int userId)
        {
            var currentRole = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            var currentId = GetLoggedUserId();

            if (currentRole != UserRole.Admin.ToString() && currentId != userId)
            {
                throw new UnauthorizedOperationException("You are not an author or admin!");
            }
        }

        public bool ValidateCredentials(string email, string password)
        {
            User? user = userRepository.GetByEmail(email);

            // TODO: var encodedPassword = EncodePassword(credentialsDto.Password);
            if (user == null || user.Password != password)
            {
                return false;
            }

            return true;
        }

        public User AccountUpdate(UserUpdateDto dto)
        {
            var user = GetLoggedUser();

            if (dto.Email != null
                && userRepository.CheckDuplicateEmail(dto.Email)
                && user.Email != dto.Email)
            {
                throw new DuplicateEntityException($"Email {dto.Email} is already in use!");
            }

            var updatedUser = userRepository.UpdateUser(user.Id, dto);

            return updatedUser ?? throw new Exception("Your profile could not be updated.");
        }
    }
}