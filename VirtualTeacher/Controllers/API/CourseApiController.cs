using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API
{
    [ApiController]
    [Route("api/Courses")]
    public class CourseApiController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly ModelMapper mapper;

        public CourseApiController(ICourseService courseService, ModelMapper mapper)
        {
            this.courseService = courseService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetAll([FromQuery] CourseQueryParameters parameters)
        {
            try
            {
                var courses = courseService.FilterBy(parameters)
                    .Select(course => mapper.MapResponse(course))
                    .ToList();

                return Ok(courses);
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
                var course = courseService.GetById(id);
                var courseDto = mapper.MapResponse(course);

                return Ok(courseDto);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CourseCreateDto dto)
        {
            try
            {
                Course createdCourse = courseService.Create(dto);

                var responseDto = mapper.MapResponse(createdCourse);
                return StatusCode(StatusCodes.Status201Created, responseDto);
            }
            catch (DuplicateEntityException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CourseUpdateDto dto)
        {
            try
            {
                var updatedCourse = courseService.Update(id, dto);

                return Ok(mapper.MapResponse(updatedCourse));
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
                return Ok(courseService.Delete(id));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}