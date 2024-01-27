using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API;

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
            var courses = courseService.FilterCoursesBy(parameters);
            var dtosList = courses.Select(course => mapper.MapResponse(course)).ToList();

            return Ok(dtosList);
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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }

    [Authorize(Roles = "Teacher, Admin")]
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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
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
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }

    [Authorize]
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
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }

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
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }

    [Authorize]
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

    [Authorize]
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

    [HttpGet("{courseId}/Lectures")]
    public IActionResult GetLectures(int courseId)
    {
        try
        {
            var lectures = courseService.GetLectures(courseId);
            //var lecturesDto = lectures.Select(lecture => mapper.MapResponse(lecture));

            return Ok(lectures);
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

    [HttpGet("{courseId}/Lectures/{lectureId}")]
    public IActionResult GetLectures(int courseId, int lectureId)
    {
        try
        {
            var lecture = courseService.GetLectureById(courseId, lectureId);
            //var lectureDto = mapper.MapResponse(lecture);

            return Ok(lecture);
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

    //Comments
    [HttpGet("{courseId}/Lectures/{lectureId}/Comments")]
    public IActionResult GetComments(int courseId, int lectureId)
    {
        try
        {
            var comments = courseService.GetComments(courseId, lectureId);
            var commentsDto = comments.Select(comment => mapper.MapResponse(comment));

            return Ok(commentsDto);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize]
    [HttpPost("{courseId}/Lectures/{lectureId}/Comments")]
    public IActionResult CreateComment(int courseId, int lectureId, [FromBody] CommentCreateDto dto)
    {
        try
        {
            Comment createdComment = courseService.CreateComment(courseId, lectureId, dto);
            var commentDto = mapper.MapResponse(createdComment);

            return StatusCode(StatusCodes.Status201Created, commentDto);
        }
        catch (DuplicateEntityException e)
        {
            return Conflict(e.Message);
        }
    }

    [Authorize]
    [HttpPut("{courseId}/Lectures/{lectureId}/Comments/{commentId}")]
    public IActionResult UpdateComment(int courseId, int lectureId, int commentId, [FromBody] CommentCreateDto dto)
    {
        try
        {
            var updatedcomment = courseService.UpdateComment(courseId, lectureId, commentId, dto);
            var commentDto = mapper.MapResponse(updatedcomment);

            return Ok(commentDto);
        }
        catch (DuplicateEntityException e)
        {
            return Conflict(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{courseId}/Lectures/{lectureId}/Comments/{commentId}")]
    public IActionResult DeleteComment(int courseId, int lectureId, int commentId)
    {
        try
        {
            return Ok(courseService.DeleteComment(courseId, lectureId, commentId));
        }
        catch (DuplicateEntityException e)
        {
            return Conflict(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
    }

}