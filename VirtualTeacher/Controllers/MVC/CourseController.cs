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
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index(CourseQueryParameters queryParameters)
        {
            var courses = courseService.FilterCoursesBy(queryParameters);

            List<string> allTeachers = courseService.GetAllCourses()
            .SelectMany(course => course.ActiveTeachers.Select(teacher => teacher.Username)
                .Distinct())
                .Distinct()
                .ToList();

            List<string>  allTopics = courseService.GetAllCourses().Select(course => course.CourseTopic.ToString())
                .Distinct()
                .ToList();

            var allCourses = courseService.GetAllCourses();
            CoursesListViewModel coursesVM = mapper.MapCourseList(courses, allTeachers, allTopics, queryParameters);

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
            catch (Exception e)
            {
                TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
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

                return RedirectToAction("Error", "Shared");
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

        [HttpPost]
        public IActionResult Enroll([FromRoute] int id)
        {
            try
            {
                var course = courseService.GetCourseById(id);
                courseService.Enroll(id);

                return RedirectToAction("Details", "Course", new { id = course.Id });
            }
            catch (DuplicateEntityException e)
            {
                TempData["StatusCode"] = StatusCodes.Status409Conflict;
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

        public IActionResult Details([FromRoute] int id)
        {
            var course = courseService.GetCourseById(id);

            return View(course);
        }

    }
}
