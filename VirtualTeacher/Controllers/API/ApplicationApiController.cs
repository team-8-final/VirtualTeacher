using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API
{
    [ApiController]
    [Route("api/")]
    [Tags("User > Teacher Application")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApplicationApiController : ControllerBase
    {
        private readonly IApplicationService applicationService;
        private readonly ModelMapper mapper;

        public ApplicationApiController(IApplicationService applicationService, ModelMapper mapper)
        {
            this.applicationService = applicationService;
            this.mapper = mapper;
        }


        /// <summary>
        /// Retrieves all Teacher Applications.
        /// </summary>
        /// <remarks>
        /// Only admin users are able to receive this list.
        /// </remarks>
        /// <returns>
        /// The list of Teacher Applications containing application id, user id, course id
        /// </returns>
        /// <response code="200">The list has been successfully retrieved</response>
        /// <response code="401">Only admins can view all active applications.</response>
        [HttpGet("applications/")]
        public IActionResult GetAllApplications()
        {
            try
            {
                var applications = applicationService
                    .GetAllApplications()
                    .Select(application => mapper
                    .MapResponse(application)).ToList();

                return Ok(applications);
            }
            catch (UnauthorizedOperationException e)
            {
                return Unauthorized(e.Message);
            }

        }

        /// <summary>
        /// Creates a Teacher Application, by the logged user.
        /// </summary>
        /// <returns>
        /// The created Teacher Application details.
        /// </returns>
        /// <response code="200">The application was successfully created</response>
        /// <response code="400">You already have a pending teacher application.</response>
        /// <response code="401">Only student users can apply to become teachers.</response>
        [HttpPost("course/application")]
        public IActionResult CreateApplication()
        {
            try
            {
                var teacherApplication = mapper.MapResponse(applicationService.CreateApplication());

                return Ok(teacherApplication);
            }
            catch (UnauthorizedOperationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Resolves the Teacher Application.
        /// </summary>
        /// <remarks>
        /// Marking 'true' approves the application, 'false' denies it.
        /// </remarks>
        /// <returns>
        /// Confirmation of the Teacher Application resolution.
        /// </returns>
        /// <response code="200">The application was resolved successfully.</response>
        /// <response code="400">You are already a teacher.</response>
        /// <response code="401">Only admins can resolve applications.</response>
        /// <response code="404">Application with this id was not found.</response>
        /// <response code="409">This application is already marked as complete.</response>
        [HttpPut("{applicationId}")]
        public IActionResult ResolveApplication(int applicationId, [Required] bool resolution)
        {
            try
            {
                var result = applicationService.ResolveApplication(applicationId, resolution);
                return Ok(result);
            }
            catch (UnauthorizedOperationException e)
            {
                return Unauthorized(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidUserInputException e)
            {
                return Conflict(e.Message);
            }
        }

        // todo add GetMyApplications();
    }
}
