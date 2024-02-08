using VirtualTeacher.Models;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.ViewModels
{
    public class CoursesListViewModel
    {
        public PaginatedList<Course>? Courses { get; set; }
        public List<string>? AllTopics { get; set; }
        public List<string>? AllTeachers { get; set; } 
        public CourseQueryParameters? Parameters { get; set; }
    }
}
