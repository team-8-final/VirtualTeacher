using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels.Lectures;

namespace VirtualTeacher.Controllers.MVC
{
    [Route("Course/{courseId}/Lecture/")]
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

                return RedirectToAction("Details", "Course", new { id = courseId });
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
