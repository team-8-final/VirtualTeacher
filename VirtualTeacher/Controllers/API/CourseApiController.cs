using Microsoft.AspNetCore.Authorization;
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
        public IActionResult GetCourses([FromQuery] CourseQueryParameters parameters)
        {
            try
            {
                var courses = courseService.FilterCoursesBy(parameters)
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
        public IActionResult GetCourseById(int id)
        {
            try
            {
                var course = courseService.GetCourseById(id);
                var courseDto = mapper.MapResponse(course);

                return Ok(courseDto);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult CreateCourse([FromBody] CourseCreateDto dto)
        {
            try
            {
                Course createdCourse = courseService.CreateCourse(dto);

                var responseDto = mapper.MapResponse(createdCourse);
                return StatusCode(StatusCodes.Status201Created, responseDto);
            }
            catch (DuplicateEntityException e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] CourseUpdateDto dto)
        {
            try
            {
                var updatedCourse = courseService.UpdateCourse(id, dto);

                return Ok(mapper.MapResponse(updatedCourse));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            try
            {
                return Ok(courseService.DeleteCourse(id));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [Authorize]
        [HttpGet("{courseId}/Ratings")]
        public IActionResult GetRatings(int courseId)
        {
            try
            {
                var ratings = courseService.GetRatings(courseId)
                    .Select(rating => mapper.MapResponse(rating))
                    .ToList();

                return Ok(ratings);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{courseId}/Ratings")]
        public IActionResult RateCourse(int courseId, [FromBody] RatingCreateDto dto)
        {
            try
            {
                var rating = courseService.RateCourse(courseId, dto);

                return Ok(mapper.MapResponse(rating));
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
            }
        }

        [HttpDelete("{courseId}/Ratings")]
        public IActionResult RemoveRating(int courseId)
        {
            try
            {
                var response = courseService.RemoveRating(courseId);
                return Ok(response);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
            }
        }
    }
}