using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtualTeacher.Helpers;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels.Students;

namespace VirtualTeacher.Controllers.MVC
{
    [IsTeacherOrAdmin]
    public class StudentsController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly ModelMapper mapper;

        public StudentsController(ICourseService courseService, IUserService userService, ModelMapper mapper)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("/Students")]
        public ActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));

            List<Course> courses = courseService.GetAllCourses()
                .Where(course => course.ActiveTeachers.Any(teacher => teacher.Id == userId))
                .ToList();
            
            StudentsViewModel studentsVM = new StudentsViewModel();
            studentsVM.Courses = courses;

            return View(studentsVM);
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Grade/5
        public ActionResult GradeAssignment(int assignmentId)
        {
            return View();
        }

        public ActionResult ChangeGradeAssignment(int assignmentId)
        {
            return View();
        }


    }
}
