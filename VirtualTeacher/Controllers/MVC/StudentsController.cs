using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtualTeacher.Helpers;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;
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
        public ActionResult Index(StudentsQueryParameters queryParameters)
        {
            queryParameters.PageSize = 30;
            var userId = int.Parse(User.FindFirstValue("UserId"));

            StudentsViewModel studentsVM = new StudentsViewModel();

            studentsVM.FilteredCourses = courseService.FilterByTeacherId(userId).ToList();

                List<User> allUsersObj = studentsVM.FilteredCourses
                .SelectMany(course => course.EnrolledStudents)
                .Distinct()
                .ToList();

            studentsVM.AllStudents = mapper.MapStudentsToDto(allUsersObj); 

            return View(studentsVM);
        }

        [HttpGet]
        public ActionResult StudentsByName(string searchWord)  //needs a new view
        {
            var students = userService.GetUsersByKeyWord(searchWord);

            return View(students);
        }

        public ActionResult StudentDetails(int studentId)  
        {
            throw new NotImplementedException();

        }


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
