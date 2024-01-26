﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;
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

        [HttpGet("")]
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

        [HttpGet("{id}")]
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                authService.ValidateAdminRole();
                bool userDeleted = userService.Delete(id);

                return Ok(userDeleted);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserUpdateDto updateData)
        {
            try
            {
                authService.ValidateAuthorOrAdmin(id);
                var updatedUser = userService.Update(id, mapper.MapUpdate(updateData));
                var dto = mapper.MapResponse(updatedUser);

                return Ok(dto);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}/role/{roleId}")]
        public IActionResult ChangeRole(int id, int roleId)
        {
            try
            {
                authService.ValidateAdminRole();
                var updatedUser = userService.ChangeRole(id, roleId);
                var dto = mapper.MapResponse(updatedUser);

                return Ok(dto);
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
