using VirtualTeacher.Models;

namespace VirtualTeacher.Services.Contracts
{
    public interface IApplicationService
    {
        public List<TeacherApplication> GetAllApplications();
        TeacherApplication GetById(int id);
        public TeacherApplication CreateApplication();
        public string ResolveApplication(int applicationId, bool resolution);
        public int GetActiveApplicationsCount();
    }
}
