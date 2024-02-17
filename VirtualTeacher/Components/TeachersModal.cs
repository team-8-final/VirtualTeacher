using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Components
{
    public class TeachersModal : ViewComponent
    {
        private readonly IUserService userService;
        private readonly ICourseService courseService;

        public TeachersModal(IUserService userService, ICourseService courseService)
        {
            this.userService = userService;
            this.courseService = courseService;
        }

        public IViewComponentResult Invoke(int courseId)
        {
            TempData["CourseId"] = courseId; 
            var teachers = userService.GetAvailableTeachers(courseId);

            return View("Index", teachers);
        }
    }
}
