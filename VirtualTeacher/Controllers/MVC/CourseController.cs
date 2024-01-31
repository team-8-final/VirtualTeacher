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
            //ViewData["SortOrder"] = string.IsNullOrEmpty(queryParameters.SortOrder) ? "desc" : "";
            //ViewData["LanguageSearch"] = string.IsNullOrEmpty(queryParameters.Author) ? "" : queryParameters.Author;
            //ViewData["Popularity"] = string.IsNullOrEmpty(queryParameters.Title) ? "" : queryParameters.Title;  //based on ratings? 
            var threads = courseService.FilterCoursesBy(queryParameters);

            return View(threads);
        }
    }
}
