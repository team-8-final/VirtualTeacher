using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IApplicationRepository
    {
        public TeacherApplication? GetById(int id);
        public TeacherApplication CreateApplication(Course course, User teacher);
        public List<TeacherApplication> GetAllApplications();
        public List<TeacherApplication> GetCourseApplications(int courseId);
        public void MarkComplete(int applicationId);
        bool CheckDuplicateApplication(int courseId, int teacherId);
    }
}
