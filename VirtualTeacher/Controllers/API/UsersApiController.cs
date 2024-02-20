using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API
{
    [ApiController]
    [Route("api/users")]
    [Tags("Users")]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly ModelMapper mapper;

        public UsersApiController(IUserService userService, IAccountService accountService, ModelMapper mapper)
        {
            this.userService = userService;
            this.accountService = accountService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retrieves all registered Users.
        /// </summary>
        /// <returns>
        /// A list of all registered Users in the system.
        /// </returns>
        /// <response code="200">The list of Users was successfully retrieved</response>
        /// <response code="404">The system has no registered Users yet.</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromQuery] UserQueryParameters parameters)
        {
            try
            {
                accountService.ValidateAdminRole();
                //maybe move all mapping to the service
                var users = userService.FilterBy(parameters)
                    .Select(user => mapper.MapResponse(user))
                    .ToList();

                return Ok(users);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return Unauthorized(e.Message);
            }
        }

        /// <summary>
        /// Retrieves the User with the specified id
        /// </summary>
        /// <returns>
        /// The User with the specified id
        /// </returns>
        /// <response code="200">The User was succesfully retrieved</response>
        /// <response code="404">A User with this id was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = userService.GetById(id);
                var userDto = mapper.MapResponse(user);

                return Ok(userDto);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Deletes a User with the specified id
        /// </summary>
        /// <remarks>
        /// Only an Admin can delete other Users.
        /// </remarks>
        /// <returns>
        /// A string response
        /// </returns>
        /// <response code="200">The User was deleted</response>
        /// <response code="401">You are unauthorized to complete this request</response>
        /// <response code="404">A User with this id was not found</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                accountService.ValidateAdminRole();
                return Ok(userService.Delete(id));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return Unauthorized(e.Message);
            }
        }

        /// <summary>
        /// Updates the data of the User with the specified id.
        /// </summary>
        /// <remarks>
        /// Only an Admin or the User themselves can edit their data.
        /// </remarks>
        /// <returns>
        /// The newly updated User and their data.
        /// </returns>
        /// <response code="200">The User data was updated.</response>
        /// <response code="401">You are unauthorized to complete this request</response>
        /// <response code="404">A User with this id was not found</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] UserUpdateDto updateData)
        {
            try
            {
                accountService.ValidateAuthorOrAdmin(id);
                var updatedUser = userService.UpdateUser(id, updateData);
                var userDto = mapper.MapResponse(updatedUser);

                return Ok(userDto);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return Unauthorized(e.Message);
            }
        }

        /// <summary>
        /// Changes the role of the User with the specified id
        /// </summary>
        /// <remarks>
        /// Only an Admin can change User roles.
        /// </remarks>
        /// <returns>
        /// The newly updated User and their data.
        /// </returns>
        /// <response code="200">The User role was updated.</response>
        /// <response code="401">You are unauthorized to complete this request</response>
        /// <response code="404">A User with this id was not found</response>
        /// <response code="409">A Role with this id was not found</response>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}/role/{roleId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public IActionResult ChangeRole(int id, int roleId)
        {
            try
            {
                //accountService.ValidateAdminRole();
                var updatedUser = userService.ChangeRole(id, roleId);
                var userDto = mapper.MapResponse(updatedUser);

                return Ok(userDto);
            }
            catch (InvalidUserInputException e)
            {
                return Conflict(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UnauthorizedOperationException e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}