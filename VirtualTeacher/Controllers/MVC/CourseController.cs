using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Helpers;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;

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
        public IActionResult Index(CourseQueryParameters queryParameters)
        {
            ViewData["SortOrder"] = string.IsNullOrEmpty(queryParameters.SortOrder) ? "desc" : "";
            ViewData["Topic"] = string.IsNullOrEmpty(queryParameters.Topic.ToString()) ? "" : queryParameters.Topic; //todo to test this 
            ViewData["TeacherUsername"] = string.IsNullOrEmpty(queryParameters.TeacherUsername) ? "" : queryParameters.TeacherUsername;


            // ViewData["Rating"] = string.IsNullOrEmpty(queryParameters.TeacherUsername) ? "" : queryParameters.Rating; //based on ratings?

            var courses = courseService.FilterCoursesBy(queryParameters);

            return View(courses);
        }

        public IActionResult Details([FromRoute] int id)
        {
            var course = courseService.GetCourseById(id);

            return View(course);
        }

    }
}
