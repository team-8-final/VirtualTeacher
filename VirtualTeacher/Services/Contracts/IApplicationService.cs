using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface IApplicationService
    {
        public List<TeacherApplication> GetAllApplications();
        public List<TeacherApplication> GetCourseApplications(int courseId);
        TeacherApplication GetById(int id);
        public TeacherApplication CreateApplication(int courseId);
        public string ResolveApplication(int applicationId, bool resolution);
    }
}
