using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration config;

        public AuthService(IUserRepository userRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.config = config;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GenerateToken(LoginRequest loginRequest)
        {
            var user = userRepository.GetByName(loginRequest.Username);

            List<Claim> claims = new List<Claim>
            {
                new Claim("UserID", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
                //new Claim(ClaimTypes.Email, user.Email)
            };

            if (user.UserRole == UserRole.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else if (user.UserRole == UserRole.Teacher)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Teacher"));
            }
            else if (user.UserRole == UserRole.Student)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Student"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Anonymous"));  
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                config.GetSection("Jwt:Key").Value!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //HmacSha512Signature

            var Sectoken = new JwtSecurityToken(
                  claims: claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials
              );

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return token;
        }

        public UserRole CheckRoleAuthorization(User user)
        {
            if (user.UserRole == UserRole.Admin)
            {
                return UserRole.Admin;
            }
            else if (user.UserRole == UserRole.Teacher)
            {
                return UserRole.Teacher;
            }
            else if (user.UserRole == UserRole.Student)
            {
                return UserRole.Student;
            }

            return UserRole.Anonymous;
        }

        public string EncodePassword(string password)
        {
            var encodedPassword = Encoding.UTF8.GetBytes(password);

            return Convert.ToBase64String(encodedPassword);
        }

        public int GetLoggedUserId()
        {
            int result = -1;

            if (httpContextAccessor.HttpContext is not null)
            {
                result = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue("UserID"));
            }
            return result;
        }

        public User GetLoggedUser()
        {
            int loggedId = GetLoggedUserId();

            if (loggedId != -1)
            {
                var loggedUser = userRepository.GetById(loggedId);
                return loggedUser;
            }

            throw new UnauthorizedOperationException("You are not logged in!");
        }

        public void ValidateAdminRole()
        {
            var currentRole = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (currentRole != UserRole.Admin.ToString())
                throw new UnauthorizedOperationException("You are not an admin!");
        }

        public void ValidateAuthorOrAdmin(int id)
        {
            var currentRole = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var currentId = GetLoggedUserId();

            if (currentRole != UserRole.Admin.ToString() && currentId != id)
                throw new UnauthorizedOperationException("You are not an admin!");
        }

        public User ValidateCredentials(LoginRequest loginRequest)
        {
            try
            {
                User user = userRepository.GetByName(loginRequest.Username);

                string encodedPassword = EncodePassword(loginRequest.Password);

                if (user.Password != loginRequest.Password)
                {
                    throw new InvalidCredentialsException("Wrong credentials!");
                }

                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new InvalidCredentialsException("Wrong credentials!");
            }
        }
    }
}