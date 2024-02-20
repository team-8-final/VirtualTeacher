using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels.Lectures;

namespace VirtualTeacher.Controllers.MVC;

[Route("Course/{courseId}/Lecture/")]
[ApiExplorerSettings(IgnoreApi = true)]
public class LectureController : Controller
{
    private readonly ICourseService courseService;
    private readonly IAccountService accountService;
    private readonly ModelMapper mapper;

    public LectureController(ICourseService courseService, IAccountService accountService, ModelMapper mapper)
    {
        this.courseService = courseService;
        this.accountService = accountService;
        this.mapper = mapper;
    }

    [IsTeacherOrAdmin]
    [HttpGet("Create")]
    public IActionResult Create(int courseId)
    {
        var lectureVM = new LectureCreateViewModel();

        return View();
    }

    [IsTeacherOrAdmin]
    [HttpPost("Create")]
    public IActionResult Create(int courseId, LectureCreateViewModel lectureVM)
    {
        if (!ModelState.IsValid)
        {
            return View(lectureVM);
        }

        try
        {
            var newLecture = courseService.CreateLecture(lectureVM, courseId);

            return RedirectToAction("Details", "Lecture", new { courseId, id = newLecture.Id });
        }
        catch (UnauthorizedOperationException e)
        {
            TempData["StatusCode"] = StatusCodes.Status401Unauthorized;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (EntityNotFoundException e)
        {
            TempData["StatusCode"] = StatusCodes.Status404NotFound;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpGet("{id}")]
    public IActionResult Details(int courseId, int id)
    {
        try
        {
            var lecture = courseService.GetLectureById(courseId, id);
            var course = courseService.GetCourseById(courseId);
            var user = accountService.GetLoggedUser();


            ViewBag.Course = course;
            ViewBag.User = user;
            ViewBag.Note = courseService.GetNote(courseId, id);
            ViewBag.Teacher = course.ActiveTeachers.FirstOrDefault();

            return View(lecture);
        }
        catch (EntityNotFoundException e)
        {
            TempData["StatusCode"] = StatusCodes.Status404NotFound;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (UnauthorizedOperationException e)
        {
            TempData["StatusCode"] = StatusCodes.Status401Unauthorized;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
    }

    [IsTeacherOrAdmin]
    [HttpGet]
    [Route("{lectureId}/Update")]
    public IActionResult Update([FromRoute] int courseId, [FromRoute] int lectureId)
    {
        try
        {
            var lecture = courseService.GetLectureById(courseId, lectureId);
            LectureUpdateViewModel lectureVM = mapper.MapUpdateVM(lecture);

            return View(lectureVM);
        }
        catch (EntityNotFoundException e)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            ViewData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
    }

    [IsTeacherOrAdmin]
    [HttpPost]
    [Route("{lectureId}/Update")]
    public IActionResult Update([FromRoute] int courseId, [FromRoute] int lectureId, LectureUpdateViewModel lectureVM)
    {
        if (!ModelState.IsValid)
        {
            return View(lectureVM);
        }

        try
        {
            var updatedLecture = courseService.UpdateLecture(lectureVM, courseId, lectureId);

            return RedirectToAction("Details", "Lecture", new { courseId, id = updatedLecture.Id });
        }
        catch (UnauthorizedOperationException e)
        {
            TempData["StatusCode"] = StatusCodes.Status401Unauthorized;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
    }

    //todo add more catch blocks if needed
    [IsTeacherOrAdmin]
    [HttpGet]
    [Route("{lectureId}/Delete")]
    public IActionResult Delete([FromRoute] int courseId, [FromRoute] int lectureId)
    {
        try
        {
            courseService.DeleteLecture(courseId, lectureId);

            return Json(new { success = true });
        }
        catch (Exception e)
        {
            return Json(new { success = false, errorMessage = e.Message });
        }
    }

    // Assignments

    [HttpGet("/{lectureId}/get-assignment")]
    public IActionResult GetAssignment(int courseId, int lectureId)
    {
        try
        {
            var filePath = courseService.GetAssignmentFilePath(courseId, lectureId);

            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound("Assignment file not found.");
            }

            var fileName = Path.GetFileName(filePath);
            var mimeType = "application/octet-stream";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType, fileName);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("An error occurred while downloading the file.");
        }
    }

    [HttpPost("/{lectureId}/create-assignment")]
    public IActionResult CreateAssignment(int courseId, int lectureId, IFormFile file)
    {
        if (file is { Length: 0 })
        {
            return BadRequest("File is not selected or empty.");
        }

        try
        {
            var result = courseService.CreateAssignment(courseId, lectureId, file);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Assignment could not be created. Please try again.");
        }
    }

    [HttpPost("/{lectureId}/delete-assignment")]
    public IActionResult DeleteAssignment(int courseId, int lectureId, string requestLocation)
    {
        try
        {
            var message = courseService.DeleteAssignment(courseId, lectureId);

            if (requestLocation == "teacherPanel")
            {
                return Redirect($"/Assignments");
            }

            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Assignment could not be deleted. Please try again.");
        }
    }

    // Submissions

    [HttpGet("/{lectureId}/get-submission")]
    public IActionResult GetSubmission(int courseId, int lectureId)
    {
        try
        {
            var filePath = courseService.GetSubmissionFilePath(courseId, lectureId);

            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound("Submission file not found.");
            }

            var fileName = Path.GetFileName(filePath);
            var mimeType = "application/octet-stream";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType, fileName);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("An error occurred while downloading the file.");
        }
    }

    public IActionResult GetSubmission(int courseId, int lectureId, int studentId)
    {
        try
        {
            var filePath = courseService.GetSubmissionFilePath(courseId, lectureId, studentId);

            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound("Submission file not found.");
            }

            var fileName = Path.GetFileName(filePath);
            var mimeType = "application/octet-stream";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType, fileName);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("An error occurred while downloading the file.");
        }
    }

    [IsTeacherOrAdmin]
    [HttpPost("/{lectureId}/assess-submission")]
    public IActionResult AssessSubmission(int userId, int lectureId, int grade)
    {
        try
        {
            if (grade > 100)
            {
                return BadRequest("The value of grade cannot be more than 100");
            }

            byte grade2 = (byte)grade;
            courseService.AssessSubmission(lectureId, userId, grade2);

            return RedirectToAction("Index", "Assignments", new { openPanel = lectureId });
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("/{lectureId}/create-submission")]
    public IActionResult CreateSubmission(int courseId, int lectureId, IFormFile file)
    {
        try
        {
            if (file is { Length: 0 })
            {
                return BadRequest("File is not selected or empty.");
            }

            _ = courseService.CreateSubmission(courseId, lectureId, file);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Submission could not be created. Please try again.");
        }
    }

    [HttpPost("/{lectureId}/delete-submission")]
    public IActionResult DeleteSubmission(int courseId, int lectureId)
    {
        try
        {
            _ = courseService.DeleteSubmission(courseId, lectureId);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Submission could not be deleted. Please try again.");
        }
    }

    // Comments

    [HttpPost("/{lectureId}/add-comment")]
    public IActionResult AddComment(int courseId, int lectureId, string content)
    {
        try
        {
            var dto = new CommentCreateDto
            {
                Content = content
            };

            _ = courseService.CreateComment(courseId, lectureId, dto);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/{lectureId}/edit-comment")]
    public IActionResult EditComment(int courseId, int lectureId, int commentId, string content)
    {
        try
        {
            var dto = new CommentCreateDto
            {
                Content = content
            };

            _ = courseService.UpdateComment(courseId, lectureId, commentId, dto);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/{lectureId}/delete-comment")]
    public IActionResult DeleteComment(int courseId, int lectureId, int commentId)
    {
        try
        {
            _ = courseService.DeleteComment(courseId, lectureId, commentId);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // notes

    [HttpGet("/{lectureId}/get-note")]
    public IActionResult GetNote(int courseId, int lectureId)
    {
        try
        {
            var note = courseService.GetNote(courseId, lectureId);
            return Json(note);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/{lectureId}/save-note")]
    public IActionResult SaveNote(int courseId, int lectureId, string content)
    {
        try
        {
            var result = courseService.UpdateNoteContent(courseId, lectureId, content);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}