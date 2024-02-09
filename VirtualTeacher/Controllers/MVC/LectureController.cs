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
    }
}
