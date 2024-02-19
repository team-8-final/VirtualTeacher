using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Repositories;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace VirtualTeacher.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly ICourseService courseService;
        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public ApplicationService(IApplicationRepository applicationRepository, ICourseService courseService, IAccountService accountService, IUserService userService)
        {
            this.applicationRepository = applicationRepository;
            this.courseService = courseService;
            this.accountService = accountService;
            this.userService = userService;

        }

        public List<TeacherApplication> GetAllApplications()
        {
            var loggedUser = accountService.GetLoggedUser();

            if (loggedUser.UserRole != UserRole.Admin)
                throw new UnauthorizedOperationException("Only admins can view all active applications.");

            var applications = applicationRepository.GetAllApplications();

            return applications;
        }

        public List<TeacherApplication> GetCourseApplications(int courseId)
        {
            var loggedUser = accountService.GetLoggedUser();
            var course = courseService.GetCourseById(courseId);

            if (loggedUser.UserRole != UserRole.Admin
                && course.ActiveTeachers.All(t => t != loggedUser))
                throw new UnauthorizedOperationException("Only admins and active course teachers can view active course applications.");

            var courseApplications = applicationRepository.GetCourseApplications(courseId);

            return courseApplications;
        }

        //todo fix slow performance
        public TeacherApplication CreateApplication(int courseId)
        {
            var course = courseService.GetCourseById(courseId);
            var loggedUser = accountService.GetLoggedUser();

            if (loggedUser.UserRole != UserRole.Teacher)
                throw new UnauthorizedOperationException("Only teacher users can apply to be teachers in a course.");

            if (course.ActiveTeachers.Any(u => u.Id == loggedUser.Id))
                throw new DuplicateEntityException("You are already an active teacher in this course");

            if (applicationRepository.CheckDuplicateApplication(courseId, loggedUser.Id))
                throw new DuplicateEntityException("You already have a pending teacher application in this course.");

            var createdApplication = applicationRepository.CreateApplication(course, loggedUser);

            return createdApplication;
        }


        public string ResolveApplication(int applicationId, bool resolution)
        {
            var loggedUser = accountService.GetLoggedUser();
            var application = GetById(applicationId);

            var course = courseService.GetCourseById(application.CourseId);
            var teacher = userService.GetById(application.TeacherId);
            
            if (loggedUser.UserRole != UserRole.Admin 
                && course.ActiveTeachers.All(t => t != loggedUser))
                throw new UnauthorizedOperationException("Only admins and active course teachers can resolve applications.");

            string result;

            if (resolution == true)
            {
                result = courseService.AddTeacher(course.Id, teacher.Id);
            }
            else
            {
                result = $"Application successfully denied.";
            }

            applicationRepository.MarkComplete(application.Id);
            return result;
        }

        //todo maybe private
        public TeacherApplication GetById(int id)
        {
            TeacherApplication? foundApplication = applicationRepository.GetById(id);

            if (foundApplication == null)
                throw new EntityNotFoundException($"Application with id '{id}' was not found.");

            if (foundApplication.IsCompleted)
                throw new InvalidUserInputException("This application is already marked as complete.");

            return foundApplication;
        }
    }
}
