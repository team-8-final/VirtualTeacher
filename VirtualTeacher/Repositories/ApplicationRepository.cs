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
                .Include(a => a.Teacher)
                .Include(a => a.Course)
                .ToList();

            return applications;
        }

        public List<TeacherApplication> GetCourseApplications(int courseId)
        {
            List<TeacherApplication> courseApplications = context.TeacherApplications
                .Where(a => !a.IsCompleted)
                .Where(a => a.CourseId == courseId)
                .Include(a => a.Teacher)
                .Include(a => a.Course)
                .ToList();

            return courseApplications;
        }

        public TeacherApplication? GetById(int id)
        {
            TeacherApplication? application = context.TeacherApplications
                .Include(a => a.Teacher)
                .Include(a => a.Course)
                .FirstOrDefault(a => a.Id == id);

            return application;
        }
    
        public TeacherApplication CreateApplication(Course course, User teacher)
        {
            var newApplication = new TeacherApplication
            {
                CourseId = course.Id,
                Course = course,
                TeacherId = teacher.Id,
                Teacher = teacher
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
        public bool CheckDuplicateApplication(int courseId, int teacherId)
        {
            return context.TeacherApplications
                .Any(a => a.CourseId == courseId && a.TeacherId == teacherId && !a.IsCompleted);
        }
    }
}
