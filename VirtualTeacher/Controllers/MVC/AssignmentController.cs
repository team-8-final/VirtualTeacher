using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtualTeacher.Exceptions;
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Index( int openPanel, string searchWord)
        {
            try 
            {
                var userId = int.Parse(User.FindFirstValue("UserId"));

                AssignmentsViewModel studentsVM = new AssignmentsViewModel();

                List<Course> courses = courseService.FilterByTeacherId(userId).ToList();
                if (searchWord != null)
                {
                    courses = courses.Where(course => course.Title.Contains(searchWord)).ToList();
                }
                studentsVM.FilteredCourses = courses;

                List<User> allUsersObj = studentsVM.FilteredCourses
                    .SelectMany(course => course.EnrolledStudents)
                    .Distinct()
                    .ToList();

                studentsVM.AllStudents = mapper.MapStudentsToDto(allUsersObj);
                studentsVM.OpenPanel = openPanel;

                return View(studentsVM);
            }
            catch (EntityNotFoundException e)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
            catch (UnauthorizedAccessException e)
            {
                TempData["StatusCode"] = StatusCodes.Status401Unauthorized;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }
    }
}
