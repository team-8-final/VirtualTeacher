using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API;

public class AccountApiController : Controller
{
    private readonly IAccountService accountService;
    private readonly IUserService userService;
    private readonly ModelMapper mapper;

    public AccountApiController(IAccountService accountService, IUserService userService, ModelMapper mapper)
    {
        this.accountService = accountService;
        this.userService = userService;
        this.mapper = mapper;
    }

    [HttpPost("token")]
    public IActionResult Token([FromBody] CredentialsDto dto)
    {
        try
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
            {
                return Unauthorized("Credentials cannot be empty.");
            }

            var credentialsValid = accountService.ValidateCredentials(dto.Email, dto.Password);

            if (!credentialsValid)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = accountService.GenerateToken(dto);

            return Ok(token);
        }
        catch (InvalidCredentialsException e)
        {
            return Unauthorized(e.Message);
        }
        catch (EntityNotFoundException)
        {
            return Unauthorized("Invalid credentials.");
        }
        catch (InvalidUserInputException)
        {
            return Unauthorized("Invalid credentials.");
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserCreateDto userDto)
    {
        try
        {
            if (userDto.UserRole != UserRole.Teacher && userDto.UserRole != UserRole.Student)
            {
                throw new InvalidOperationException("You cannot create an account with this role.");
            }

            if (ModelState.TryGetValue("Password", out var entry) && entry.Errors.Count > 0)
            {
                return BadRequest(new { PasswordErrors = entry.Errors.Select(e => e.ErrorMessage) });
            }

            var createdUser = userService.Create(userDto);
            var createdUserDto = mapper.MapResponse(createdUser);

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