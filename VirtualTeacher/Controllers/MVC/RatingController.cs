using Microsoft.AspNetCore.Mvc;
using System.Threading;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels.Lectures;

namespace VirtualTeacher.Controllers.MVC
{
    public class RatingController : Controller
    {
        private readonly ICourseService courseService;

        public RatingController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        public IActionResult Create([FromForm] int courseId, [FromForm] RatingCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Course", new { id = courseId });
            }
            try
            {
                var newRating = courseService.RateCourse(courseId, dto);

                return RedirectToAction("Details", "Course", new { id = courseId });
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

        [HttpPost]
        public IActionResult Delete([FromForm] int courseId)
        {
            try
            {
                courseService.RemoveRating(courseId);

                return RedirectToAction("Details", "Course", new { id = courseId });
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

    }
}
