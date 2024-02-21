using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API;

[ApiController]
[Route("api/Courses")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CourseApiController : ControllerBase
{
    private readonly ICourseService courseService;
    private readonly ModelMapper mapper;

    public CourseApiController(ICourseService courseService, ModelMapper mapper)
    {
        this.courseService = courseService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Retrieves all Courses
    /// </summary>
    /// <returns>
    /// A list of Courses
    /// </returns>
    ///<response code="200">The collection has been successfully retrieved</response>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(500)]
    [AllowAnonymous]
    [Tags("Course")]
    public IActionResult GetCourses([FromQuery] CourseQueryParameters parameters)
    {
        try
        {
            var courses = courseService.FilterCoursesBy(parameters);
            var dtosList = courses.Select(course => mapper.MapResponse(course)).ToList();

            return Ok(dtosList);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }

    /// <summary>
    /// Retrieves a Course by its id
    /// </summary>
    /// <remarks>
    /// The list of lectures in the response only has Id and Title per lecture
    /// </remarks>
    /// <returns>
    /// The found Course with the Id specified in the body
    /// </returns>
    /// <response code="404">A course with this Id was not found</response>
    /// <response code="200">The Course has been successfully retrieved</response>
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    [AllowAnonymous]
    [Tags("Course")]
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

    /// <summary>
    /// Creates a new Course.
    /// </summary>
    /// <returns>
    /// The created Course details
    /// </returns>
    /// <response code="201">The course was successfully created</response>
    [Authorize(Roles = "Teacher, Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("")]
    [Tags("Course")]
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

    /// <summary>
    /// Updates an already existing Course, by retrieving it by id
    /// </summary>
    /// <returns>
    /// </returns>
    /// <response code="200">The Course was updated successfully</response>
    /// <response code="404">A Course with that id was not found</response>
    /// <response code="401">Unauthorized, you need to be the author of the course or an admin to edit it</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [Tags("Course")]
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
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }

    /// <summary>
    /// Deletes an already existing Course, retrieving it by id
    /// </summary>
    /// <returns>
    /// Confirmation if the course was deleted or not
    /// </returns>
    /// <remarks>
    /// Example response if successfull:
    /// "Course with id '1' was deleted."
    /// </remarks>
    /// <response code="200">The Course was deleted successfully</response>
    /// <response code="404">A Course with that id was not found</response>
    /// <response code="401">Unauthorized, you need to be the author of the course or an admin to delete it</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [Tags("Course")]
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
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }

    /// <summary>
    /// Currently logged user enrolls to a Course by it's id.
    /// </summary>
    /// <returns>
    /// Confirmation if the enroll request was completed or not
    /// </returns>
    /// <response code="200">The enroll request was successful.</response>
    /// <response code="404">The Course with that id was not found</response>
    /// <response code="409">The current user is already an enrolled student or an active teacher in the course.</response>
    [HttpPost("{courseId}/Students/")]
    [Tags("Course")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public IActionResult Enroll(int courseId)
    {
        try
        {
            return Ok(courseService.Enroll(courseId));
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
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

    /// <summary>
    /// Enlists a user as an active teacher in a course
    /// </summary>
    /// <returns>
    /// The found Course in which the new teacher was enlisted.
    /// </returns>
    /// <response code="200">The user has been successfully enlisted as a teacher in the course</response>
    /// <response code="400">Only teachers can be assigned to the list of active course teachers.</response>
    /// <response code="401">Only active course teachers or admins can assign new teachers.</response>
    /// <response code="404">A course/teacher with this id/username was not found</response>
    /// <response code="409">User is already an active teacher in the course</response>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("{courseId}/Teachers/{teacherId}")]
    [Tags("Course")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
    public IActionResult AddTeacher(int courseId, int teacherId)
    {
        try
        {
            return Ok(courseService.AddTeacher(courseId, teacherId));
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (DuplicateEntityException e)
        {
            return Conflict(e.Message);
        }
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        //catch (Exception)
        //{
        //    return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        //}
    }

    // Ratings

    /// <summary>
    /// Retrieves all ratings pertaining to a Course, retrieves the Course by id
    /// </summary>
    /// <returns>
    /// A list of all the Ratings for that Course
    /// </returns>
    /// <response code="200">The list of Ratings was successfully retrieved</response>
    /// <response code="404">The Course with that id was not found or there are no Ratings for this Course</response>
    [HttpGet("{courseId}/Ratings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [Tags("Course > Rating")]
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

    /// <summary>
    /// Creates a new Rating for a Course with specified id
    /// </summary>
    /// <returns>
    /// The newly created Rating
    /// </returns>
    /// <response code="200">The Rating was successfully added</response>
    /// <response code="404">The Course with that id was not found</response>
    [HttpPut("{courseId}/Ratings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Rating")]
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

    /// <summary>
    /// Removes a Rating from a course with specified id
    /// </summary>
    /// <remarks>
    /// Only the User who placed the Rating can remove it, Admins cannot
    /// If the user have not placed a rating for the said course they will still get StatusOK 200, with this message:
    /// "Rating was not found."
    /// </remarks>
    /// <returns>
    /// A string response
    /// </returns>
    /// <response code="404">A Course with this id was not found</response>
    /// <response code="200">The Rating was successfully removed</response>
    [HttpDelete("{courseId}/Ratings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Rating")]
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



    // Lectures


    /// <summary>
    /// Retrieves all Lectures in a Course with specified id
    /// </summary>
    /// <returns>
    /// A list of all Lectures in the Course
    /// </returns>
    /// <response code="404">A Course with this id was not found or the Course has no Lectures yet</response>
    /// <response code="200">The list of Lectures was successfully retrieved</response>
    [HttpGet("{courseId}/Lectures")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Lecture")]
    public IActionResult GetLectures(int courseId)
    {
        try
        {
            var lectures = courseService.GetLectures(courseId);
            var lecturesDto = lectures.Select(lecture => mapper.MapResponse(lecture));

            return Ok(lecturesDto);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
        }
    }


    /// <summary>
    /// Retrieves a Lecture. Retrieves the lecture by Course id and Lecture id
    /// </summary>
    /// <returns>
    /// The Lecture with the specified id
    /// </returns>
    /// <response code="404">A Course with this id was not found or the Lecture was not found</response>
    /// <response code="200">The lecture was succesfully retrieved</response>
    [HttpGet("{courseId}/Lectures/{lectureId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Lecture")]
    public IActionResult GetLecture(int courseId, int lectureId)
    {
        try
        {
            var lecture = courseService.GetLectureById(courseId, lectureId);
            var lectureDto = mapper.MapResponse(lecture);

            return Ok(lectureDto);
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


    /// <summary>
    /// Updates Lecture details. Retrieves the lecture by Course id and Lecture id
    /// </summary>
    /// <returns>
    /// The updated Lecture details
    /// </returns>
    /// <remarks>Only the author or an Admin can edit the Lecture</remarks>
    /// <response code="404">A Course with this id was not found or the Lecture was not found</response>
    /// <response code="200">The lecture was succesfully updated</response>
    /// <response code="401">You are not authorized to change the Lecture, only the author or an Admin can edit it</response>
    [HttpPut("{courseId}/Lectures/{lectureId}/")]
    [Authorize(Roles = "Teacher, Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [Tags("Course > Lecture")]
    public IActionResult UpdateLecture(int courseId, int lectureId, [FromBody] LectureUpdateDto dto)
    {
        try
        {
            Lecture newLecture = courseService.UpdateLecture(dto, courseId, lectureId);
            var lectureResponseDto = mapper.MapResponse(newLecture);
            return Ok(lectureResponseDto);
        }

        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception)
        {
            return Conflict("Something went wrong");
        }

    }

    /// <summary>
    /// Retrieves the assignment file for a specific course and lecture.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="lectureId">The ID of the lecture.</param>
    /// <returns>
    /// The assignment file as a downloadable file if it exists.
    /// </returns>
    [HttpGet("{courseId}/Lectures/{lectureId}/assignment")]
    [Tags("Course > Lecture > Assignment")]
    public IActionResult GetAssignment(int courseId, int lectureId)
    {
        try
        {
            var contentType = "application/octet-stream"; // Default MIME type

            var filePath = courseService.GetAssignmentFilePath(courseId, lectureId);
            var fileName = Path.GetFileName(filePath);

            return PhysicalFile(filePath, contentType, fileName);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (FileNotFoundException e)
        {
            return NotFound("Assignment doesn't exist.");
        }
    }

    /// <summary>
    /// Uploads and creates an assignment for a given course and lecture.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="lectureId">The ID of the lecture.</param>
    /// <param name="file">The assignment file to upload.</param>
    /// <returns>
    /// An Ok result with a success message if the file is successfully uploaded.
    /// </returns>
    [HttpPut("{courseId}/Lectures/{lectureId}/assignment")]
    [Tags("Course > Lecture > Assignment")]
    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult CreateAssignment(int courseId, int lectureId, IFormFile file)
    {
        try
        {
            var result = courseService.CreateAssignment(courseId, lectureId, file);

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Deletes an existing assignment for a specified course and lecture.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="lectureId">The ID of the lecture.</param>
    /// <returns>
    /// An Ok result with a success message if the assignment is successfully deleted.
    /// </returns>
    [HttpDelete("{courseId}/Lectures/{lectureId}/assignment")]
    [Tags("Course > Lecture > Assignment")]
    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult DeleteAssignment(int courseId, int lectureId)
    {
        try
        {
            var result = courseService.DeleteAssignment(courseId, lectureId);

            return Ok(result);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }


    /// <summary>
    /// Retrieves a submission file for a given course and lecture, based on the logged-in user.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="lectureId">The ID of the lecture.</param>
    /// <returns>
    /// The submission file as a downloadable file.
    /// </returns>
    [HttpGet("{courseId}/lectures/{lectureId}/submission")]
    [Tags("Course > Lecture > Submission")]
    public IActionResult GetSubmission(int courseId, int lectureId)
    {
        try
        {
            var filePath = courseService.GetSubmissionFilePath(courseId, lectureId);

            var contentType = "application/octet-stream"; // Default MIME type

            var fileName = Path.GetFileName(filePath);
            return PhysicalFile(filePath, contentType, fileName);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return NotFound("Submission is not uploaded.");
        }
    }

    /// <summary>
    /// Uploads a submission file for a specific course and lecture.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="lectureId">The ID of the lecture.</param>
    /// <param name="file">The submission file to be uploaded.</param>
    /// <returns>A message indicating the outcome of the file upload.</returns>
    [HttpPut("{courseId}/Lectures/{lectureId}/submission")]
    [Tags("Course > Lecture > Submission")]
    [Authorize(Roles = "Teacher, Admin")]
    public IActionResult CreateSubmission(int courseId, int lectureId, IFormFile file)
    {
        try
        {
            var result = courseService.CreateSubmission(courseId, lectureId, file);

            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Deletes the submission file for a specific course and lecture.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <param name="lectureId">The ID of the lecture.</param>
    /// <returns>A message indicating the outcome of the operation.</returns>
    [Authorize(Roles = "Teacher, Admin")]
    [HttpDelete("{courseId}/Lectures/{lectureId}/submission")]
    [Tags("Course > Lecture > Submission")]
    public IActionResult DeleteSubmission(int courseId, int lectureId)
    {
        try
        {
            var result = courseService.DeleteSubmission(courseId, lectureId);

            return Ok(result);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            return StatusCode(404, "Submission is not uploaded.");
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Creates a new Lecture in a Course
    /// </summary>
    /// <returns>
    /// The newly created Lecture details
    /// </returns>
    /// <response code="404">A Course with this id was not found</response>
    /// <response code="201">The Lecture was succesfully created</response>
    /// <response code="401">A Lecrute can only be created by the Course Teachers or an Admin</response>
    [HttpPost("{courseId}")]
    [Authorize(Roles = "Teacher, Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [Tags("Course > Lecture")]
    public IActionResult CreateLecture([FromRoute] int courseId, LectureCreateDto dto)
    {
        try
        {
            Lecture newLecture = courseService.CreateLecture(dto, courseId);
            var lectureResponseDto = mapper.MapResponse (newLecture);
            return StatusCode(StatusCodes.Status201Created, lectureResponseDto);
        }
        catch(EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch(UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception)
        {
            return Conflict("Something went wrong");
        }
    }

    /// <summary>
    /// Deletes a Lecture from a Course
    /// </summary>
    /// <returns>
    /// A message with the result
    /// </returns>
    /// <response code="404">A Course or Lecture with this id was not found</response>
    /// <response code="200">The Lecture was succesfully deleted</response>
    /// <response code="401">A Lecrute can only be deleted by the Course Teachers or an Admin</response>
    [Tags("Course > Lecture")]
    [Authorize(Roles = "Teacher, Admin")]
    [HttpDelete("{courseId}/Lectures/{lectureId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public IActionResult DeleteLecture(int courseId, int lectureId)
    {
        try
        {
            return Ok(courseService.DeleteLecture(courseId, lectureId));
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    //Comments

    /// <summary>
    /// Retrieves a list of Comments for a specified Lecture
    /// </summary>
    /// <returns>
    /// A list of the Comments
    /// </returns>
    /// <remarks> To find the lecture is necessary to have the Course id and Lecture id</remarks>
    /// <response code="200">The list was successfully retrieved</response>
    /// <response code="404">The Course or Lecture was not found. Or there are no Comments under this Lecture. See message for details</response>
    [HttpGet("{courseId}/Lectures/{lectureId}/Comments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Lecture > Comment")]

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

    /// <summary>
    /// Adds a new Comment to a Lecture
    /// </summary>
    /// <remarks>
    /// The Lecture is found by Course id and Lecture id
    /// </remarks>
    /// <returns>
    /// The newly created Comment details
    /// </returns>
    /// <response code="201">The comment was successfully created</response>
    /// <response code="400">If the item is null</response>
    /// <response code="404">The item was not found</response>
    [HttpPost("{courseId}/Lectures/{lectureId}/Comments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Lecture > Comment")]
    public IActionResult CreateComment(int courseId, int lectureId, [FromBody] CommentCreateDto dto)
    {
        try
        {
            Comment createdComment = courseService.CreateComment(courseId, lectureId, dto);
            var commentDto = mapper.MapResponse(createdComment);

            return StatusCode(StatusCodes.Status201Created, commentDto);
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


    /// <summary>
    /// Updates a Comment
    /// </summary>
    /// <remarks>
    /// Only an Admin or the Author of the Comment can edit it
    /// </remarks>
    /// <returns>
    /// The updated Comment details
    /// </returns>
    /// <response code="401">The user is not Admin or the Author of the Comment</response>
    /// <response code="200">The Comment was successfully updated</response>
    /// <response code="404">Either the Course the Lecture or the Comment were not found. See message for details</response>
    [HttpPut("{courseId}/Lectures/{lectureId}/Comments/{commentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Lecture > Comment")]
    public IActionResult UpdateComment(int courseId, int lectureId, int commentId, [FromBody] CommentCreateDto dto)
    {
        try
        {
            var updatedcomment = courseService.UpdateComment(courseId, lectureId, commentId, dto);
            var commentDto = mapper.MapResponse(updatedcomment);

            return Ok(commentDto);
        }
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return Conflict("Something went wrong");
        }
    }

    /// <summary>
    /// Deletes a Comment
    /// </summary>
    /// <remarks>
    /// Only an Admin or the Author of the Comment can delete it
    /// </remarks>
    /// <returns>
    /// A string with the confirmation. Example:
    ///     "Comment with id '{commentId}' was deleted."
    /// </returns>
    /// <response code="401">The user is not Admin or the Author of the Comment</response>
    /// <response code="200">The Comment was successfully deleted</response>
    /// <response code="404">Either the Course the Lecture or the Comment were not found. See message for details</response>
    [HttpDelete("{courseId}/Lectures/{lectureId}/Comments/{commentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Lecture > Comment")]
    public IActionResult DeleteComment(int courseId, int lectureId, int commentId)
    {
        try
        {
            return Ok(courseService.DeleteComment(courseId, lectureId, commentId));
        }
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    // Notes



    /// <summary>
    /// Retrieves the Note.Content string of the logged user for a specified Lecture
    /// </summary>
    /// <remarks>
    /// Only the author can retrieve their Note
    /// </remarks>
    /// <returns>
    /// A string with confirmation:
    ///     ""
    /// </returns>
    /// <response code="401">The user is not the Author of the Note</response>
    /// <response code="200">The Note was successfully retrieved</response>
    /// <response code="404">Either the Course, the Lecture or the Note were not found. See message for details</response>
    [HttpGet("{courseId}/Lectures/{lectureId}/Note")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Lecture > Note")]

    public IActionResult GetNoteContent(int courseId, int lectureId)
    {
        try
        {
            return Ok(courseService.GetNote(courseId, lectureId));
        }
        catch(UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch(EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }



    /// <summary>
    /// Updates the Note.Content for a specified Lecture. If it doesn't exist, creates it.
    /// </summary>
    /// <remarks>
    /// Only the author can update their Note
    /// </remarks>
    /// <returns>
    /// A string with the content. Example:
    ///     ""Baka" is an important Japanese word"
    /// </returns>
    /// <response code="401">The user is not the Author of the Note</response>
    /// <response code="200">The Note was successfully updated</response>
    /// <response code="404">Either the Course, the Lecture or the Note were not found. See message for details</response>
    [HttpPut("{courseId}/Lectures/{lectureId}/Note")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [Tags("Course > Lecture > Note")]

    public IActionResult UpdateNoteContent(int courseId, int lectureId, [FromBody] string newContent)
    {
        try
        {
            return Ok(courseService.UpdateNoteContent(courseId, lectureId, newContent));
        }
        catch (UnauthorizedOperationException e)
        {
            return Unauthorized(e.Message);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}