using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Helpers;
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

        public IActionResult Details([FromRoute] int id)
        {
            var course = courseService.GetCourseById(id);

            return View(course);
        }

    }
}
