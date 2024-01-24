using Microsoft.AspNetCore.Authorization;
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
        private readonly ModelMapper mapper;

        public UsersApiController(IUserService userService, ModelMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        //Move to auth controller
        [HttpPost("")]
        public IActionResult Create([FromBody] UserCreateDto userDto)
        {
            try
            {
                User createdUser = userService.Create(mapper.MapCreate(userDto));

                return StatusCode(StatusCodes.Status201Created, createdUser);
            }
            catch (DuplicateEntityException e)
            {
                return Conflict(e.Message);
            }
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

        //Add loggedId, , authorization exception
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool userDeleted = userService.Delete(id);

                return Ok(userDeleted);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        //Add loggedId, authorization exception
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserUpdateDto updateData)
        {
            try
            {
                var updatedUser = userService.Update(id, mapper.MapUpdate(updateData));

                return Ok(mapper.MapResponse(updatedUser));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        //Add loggedId, authorization exception
        [HttpPut("promote/{id}")]
        public IActionResult PromoteToTeacher(int id)
        {
            try
            {
                var promotedUser = userService.PromoteToTeacher(id);

                return Ok(mapper.MapResponse(promotedUser));
            }
            catch (InvalidUserInputException e)
            {
                return Conflict(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        //Add loggedId, authorization exception
        [HttpPut("demote/{id}")]
        public IActionResult DemoteToStudent(int id)
        {
            try
            {
                var demotedUser = userService.DemoteToStudent(id);

                return Ok(mapper.MapResponse(demotedUser));
            }
            catch (InvalidUserInputException e)
            {
                return Conflict(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
