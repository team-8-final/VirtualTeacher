using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels.Lectures;

namespace VirtualTeacher.Controllers.MVC
{
    [Route("Course/{courseId}/Lecture/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LectureController : Controller
    {
        private readonly ICourseService courseService;

        public LectureController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [IsTeacherOrAdmin]
        [HttpGet("Create")]
        public IActionResult Create(int courseId)
        {
            var lectureVM = new LectureCreateViewModel();
            //TempData["CourseId"] = courseId;

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
                //TempData["CourseId"] = courseId;

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

        [HttpGet]
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
            catch (Exception ex)
            {
                return BadRequest("An error occurred while downloading the file.");
            }
        }

        [HttpPost("/lecture/{courseId}/{lectureId}/create-assignment")]
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
            catch (Exception e)
            {
                return NotFound("Assignment could not be created. Please try again.");
            }
        }

        [HttpPost("/lecture/{courseId}/{lectureId}/delete-assignment")]
        public IActionResult DeleteAssignment(int courseId, int lectureId)
        {
            try
            {
                var message = courseService.DeleteAssignment(courseId, lectureId);
                // Redirect or return success message, e.g.:
                return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
            }
            catch (Exception e)
            {
                return NotFound("Assignment could not be deleted. Please try again.");
            }
        }
    }
}