using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API
{
    public class AuthApiController : Controller
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        private readonly ModelMapper mapper;

        public AuthApiController(IAuthService authService, IUserService userService, ModelMapper mapper)
        {
            this.authService = authService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginCredentials)
        {
            if (string.IsNullOrEmpty(loginCredentials.Username) || string.IsNullOrEmpty(loginCredentials.Password))
            {
                return Unauthorized("Fields cannot be empty");
            }

            IList<User> users = userService.GetUsers();

            if (!users.Any(u => u.Username == loginCredentials.Username && u.Password == loginCredentials.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            try
            {
                var token = authService.GenerateToken(loginCredentials);

                return Ok(token);
            }
            catch (InvalidCredentialsException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EntityNotFoundException)
            {
                return Unauthorized("Wrong credentials!");
            }
            catch (InvalidUserInputException)
            {
                return Unauthorized("Wrong credentials!");
            }
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] UserCreateDto userDto)

        {
            try
            {
                if (userDto.UserRole != UserRole.Teacher && userDto.UserRole != UserRole.Student)
                {
                    throw new InvalidOperationException("You cannot create an account with this role");
                }

                User createdUser = userService.Create(mapper.MapCreate(userDto));

                UserResponseDto createdUserDto = new UserResponseDto();
                createdUserDto = mapper.MapResponse(createdUser);

                return StatusCode(StatusCodes.Status201Created, createdUserDto);
            }
            catch (DuplicateEntityException e)
            {
                return Conflict(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
        }
    }
}