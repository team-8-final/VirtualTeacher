﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API
{
    [ApiController]
    [Route("api/users")]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthService authService;
        private readonly ModelMapper mapper;

        public UsersApiController(IUserService userService, IAuthService authService, ModelMapper mapper)
        {
            this.userService = userService;
            this.authService = authService;
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
        [HttpGet("")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAll([FromQuery] UserQueryParameters parameters)
        {
            try
            {
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
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
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int id)
        {
            int loggedUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value);

            try
            {
                authService.ValidateAdminRole();
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
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Update(int id, [FromBody] UserUpdateDto updateData)
        {
            try
            {
                authService.ValidateAuthorOrAdmin(id);
                var updatedUser = userService.Update(id, updateData);
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
        [Authorize]
        [HttpPut("{id}/role/{roleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult ChangeRole(int id, int roleId)
        {
            try
            {
                authService.ValidateAdminRole();
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
