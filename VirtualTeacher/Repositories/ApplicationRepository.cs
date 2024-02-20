using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Repositories.Contracts;

namespace VirtualTeacher.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext context;

        public ApplicationRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<TeacherApplication> GetAllApplications()
        {
            List<TeacherApplication> applications = context.TeacherApplications.
                Where(a => !a.IsCompleted)
                .Include(a => a.Student)
                .ToList();

            return applications;
        }

        public TeacherApplication? GetById(int id)
        {
            TeacherApplication? application = context.TeacherApplications
                .Include(a => a.Student)
                .FirstOrDefault(a => a.Id == id);

            return application;
        }
    
        public TeacherApplication CreateApplication(User student)
        {
            var newApplication = new TeacherApplication
            {
                StudentId = student.Id,
                Student = student
            };

            context.TeacherApplications.Add(newApplication);
            context.SaveChanges();

            return newApplication;
        }

        public void MarkComplete(int applicationId)
        {
            TeacherApplication? application = GetById(applicationId);

            application!.IsCompleted = true;
            context.SaveChanges();
        }

        //Validations
        public bool CheckDuplicateApplication(int studentId)
        {
            return context.TeacherApplications
                .Any(a => a.StudentId == studentId && !a.IsCompleted);
        }

        public int GetActiveApplicationsCount()
        {
            return context.TeacherApplications.Where(a => !a.IsCompleted).Count();
        }
    }
}
