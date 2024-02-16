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
    public class AssignmentController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly ModelMapper mapper;

        public AssignmentController(ICourseService courseService, IUserService userService, ModelMapper mapper)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("/Assignments")]
        public ActionResult Index(AssignmentsQueryParameters queryParameters, int openPanel)
        {
            queryParameters.PageSize = 100;
            var userId = int.Parse(User.FindFirstValue("UserId"));

            AssignmentsViewModel studentsVM = new AssignmentsViewModel();

            studentsVM.FilteredCourses = courseService.FilterByTeacherId(userId).ToList();

                List<User> allUsersObj = studentsVM.FilteredCourses
                .SelectMany(course => course.EnrolledStudents)
                .Distinct()
                .ToList();

            studentsVM.AllStudents = mapper.MapStudentsToDto(allUsersObj);
            studentsVM.OpenPanel = openPanel;

            return View(studentsVM);
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
