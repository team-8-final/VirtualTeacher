using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VirtualTeacher.Controllers.MVC
{
    public class CourseController : Controller
    {

        private readonly ICourseService courseService;
        private readonly ModelMapper mapper;

        public CourseController(ICourseService courseService, ModelMapper mapper)
        {
            this.courseService = courseService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("/Courses")]
        public IActionResult Index(CourseQueryParameters queryParameters)
        {
            var courses = courseService.FilterCoursesBy(queryParameters);
            var allCourses = courseService.GetAllCourses();
            CoursesListViewModel coursesVM = new CoursesListViewModel();
            coursesVM.Courses = courses;
            coursesVM.AllCourses = allCourses;

            return View(coursesVM);
        }

        [IsTeacherOrAdmin]
        [HttpGet]
        public IActionResult Create()
        {
            var courseVM = new CourseCreateViewModel();

            return View(courseVM);
        }

        [IsTeacherOrAdmin]
        [HttpPost]
        public IActionResult Create(CourseCreateViewModel courseVM)
        {
            if (!ModelState.IsValid)
            {
                return View(courseVM);
            }
            try
            {
                var newCourse = courseService.CreateCourse(courseVM);

                return RedirectToAction("Details", "Course", new { id = newCourse.Id });
            }
            catch (UnauthorizedOperationException e)
            {
                TempData["StatusCode"] = StatusCodes.Status401Unauthorized;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }

        [IsTeacherOrAdmin]
        [HttpGet]
        public IActionResult Update([FromRoute] int id)
        {
            try
            {
                var course = courseService.GetCourseById(id);
                var courseVM = new CourseUpdateViewModel
                {
                    Title = course.Title,
                    Description = course.Description,
                    StartingDate = course.StartingDate,
                    CourseTopic = course.CourseTopic,
                    Published = course.Published
                };

                return View(courseVM);
            }
            catch (EntityNotFoundException e)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

        [IsTeacherOrAdmin]
        [HttpPost]
        public IActionResult Update([FromRoute] int id, CourseUpdateViewModel courseVM)
        {
            if (!ModelState.IsValid)
            {
                return View(courseVM);
            }
            try
            {
                var updatedCourse = courseService.UpdateCourse(id, courseVM);

                return RedirectToAction("Details", "Course", new { id = updatedCourse.Id });
            }
            catch (InvalidUserInputException e)
            {
                ModelState.AddModelError("Content", e.Message);

                return View("Index", "Home");
            }
            catch (UnauthorizedAccessException e)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                ViewData["ErrorMessage"] = e.Message;

                return View("Error");
            }
        }

        public IActionResult Details([FromRoute] int id)
        {
            var course = courseService.GetCourseById(id);

            return View(course);
        }

    }
}
