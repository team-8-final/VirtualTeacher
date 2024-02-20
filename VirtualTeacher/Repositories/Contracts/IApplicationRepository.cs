using VirtualTeacher.Models;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface IApplicationRepository
    {
        public TeacherApplication? GetById(int id);
        public TeacherApplication CreateApplication(User student);
        public List<TeacherApplication> GetAllApplications();
        public int GetActiveApplicationsCount();
        public void MarkComplete(int applicationId);
        bool CheckDuplicateApplication(int studentId);
    }
}
