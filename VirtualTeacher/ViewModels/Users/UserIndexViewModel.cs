using VirtualTeacher.Models;

namespace VirtualTeacher.ViewModels.Users
{
    public class UserIndexViewModel
    {
        public PaginatedList<User>? Users { get; set; }
        public List<TeacherApplication>? Applications { get; set; }

    }
}
