using VirtualTeacher.Models;

namespace VirtualTeacher.ViewModels
{
    public class CoursesListViewModel
    {
        public PaginatedList<Course>? Courses { get; set; }

        public List<Course>? AllCourses { get; set; }
    }
}
