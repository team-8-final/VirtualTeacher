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
        private readonly IAccountService accountService;
        private readonly IUserService userService;

        public ApplicationService(IApplicationRepository applicationRepository, IAccountService accountService, IUserService userService)
        {
            this.applicationRepository = applicationRepository;
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

        public TeacherApplication CreateApplication()
        {
            var loggedUser = accountService.GetLoggedUser();

            if (loggedUser.UserRole != UserRole.Student)
                throw new UnauthorizedOperationException("Only students users can apply to be teachers.");

            if (applicationRepository.CheckDuplicateApplication(loggedUser.Id))
                throw new DuplicateEntityException("You already have a pending teacher application.");

            var createdApplication = applicationRepository.CreateApplication(loggedUser);

            return createdApplication;
        }

        public string ResolveApplication(int applicationId, bool resolution)
        {
            var loggedUser = accountService.GetLoggedUser();
            var application = GetById(applicationId);

            var student = userService.GetById(application.StudentId);
            
            if (loggedUser.UserRole != UserRole.Admin)
                throw new UnauthorizedOperationException("Only admins can resolve applications.");

            string result;

            if (resolution == true)
            {
                userService.ChangeRole(application.StudentId, 1);
                result = "Application approved.";
            }
            else
            {
                result = "Application denied.";
            }

            applicationRepository.MarkComplete(application.Id);
            return result;
        }

        public TeacherApplication GetById(int id)
        {
            TeacherApplication? foundApplication = applicationRepository.GetById(id);

            if (foundApplication == null)
                throw new EntityNotFoundException($"Application with id '{id}' was not found.");

            if (foundApplication.IsCompleted)
                throw new InvalidUserInputException("This application is already marked as complete.");

            return foundApplication;
        }

        public int GetActiveApplicationsCount()
        {
            return applicationRepository.GetActiveApplicationsCount();
        }
    }
}
