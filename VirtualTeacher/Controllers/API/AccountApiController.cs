using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models.DTOs.Account;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/account")]
[Tags("Account")]
public class AccountApiController : ControllerBase
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

    /// <summary>
    /// Post credentials to get a token.
    /// </summary>
    /// <returns>
    /// Returns a token if credentials are valid.
    /// </returns>
    ///<response code="200">Token successfully issued.</response>
    [AllowAnonymous]
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
        catch (InvalidUserInputException)
        {
            return Unauthorized("Invalid credentials.");
        }
    }

    /// <summary>
    /// Gets information about the currently logged in user.
    /// </summary>
    /// <returns>
    /// Returns json with information about the currently logged in user.
    /// </returns>
    ///<response code="200">User json returned</response>
    [HttpGet]
    public IActionResult GetAccount()
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();
            var loggedUserDto = mapper.MapResponse(loggedUser);

            return Ok(loggedUserDto);
        }
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return Conflict(e.Message);
        }
    }

    /// <summary>
    /// Deletes the currently logged in user.
    /// </summary>
    /// <returns>
    /// Returns a success message string.
    /// </returns>
    ///<response code="200"></response>
    [HttpDelete]
    public IActionResult DeleteAccount()
    {
        try
        {
            var loggedUserId = accountService.GetLoggedUserId();
            var result  = userService.Delete(loggedUserId);

            return Ok(result);
        }
        catch (EntityNotFoundException)
        {
            return Unauthorized("You are not logged in.");
        }
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <returns>
    /// Returns json with information about the newly registered user.
    /// </returns>
    ///<response code="200">User json returned</response>
    [AllowAnonymous]
    [HttpPost]
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

    /// <summary>
    /// Updates the currently logged in user.
    /// </summary>
    /// <returns>
    /// Returns json with information about the updated user.
    /// </returns>
    ///<response code="200">User json returned</response>
    [HttpPut]
    public IActionResult Update([FromBody] UserUpdateDto userDto)
    {
        try
        {
            var loggedUserId = accountService.GetLoggedUserId();
            var updatedUser = userService.Update(loggedUserId, userDto);
            var updatedUserDto = mapper.MapResponse(updatedUser);

            return Ok(updatedUserDto);
        }
        catch (EntityNotFoundException e)
        {
            return Unauthorized(e.Message);
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

    /// <summary>
    /// Changes the password of the currently logged in user.
    /// </summary>
    /// <returns>
    /// Returns a success message string.
    /// </returns>
    ///<response code="200"></response>
    [HttpPut("password")]
    public IActionResult ChangePassword([FromBody] UserPasswordDto dto)
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();

            if (!accountService.ValidateCredentials(loggedUser.Email, dto.CurrentPassword))
            {
                return BadRequest("Invalid credentials.");
            }

            var userUpdateDto = new UserUpdateDto
            {
                Password = dto.NewPassword
            };

            _ = userService.Update(loggedUser.Id, userUpdateDto);

            return Ok("Password changed successfully.");
        }
        catch (InvalidOperationException e)
        {
            return Conflict(e.Message);
        }
    }
}