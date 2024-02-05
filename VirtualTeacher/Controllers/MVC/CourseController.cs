using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels;

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

        public IActionResult Details([FromRoute] int id)
        {
            var course = courseService.GetCourseById(id);

            return View(course);
        }

    }
}
