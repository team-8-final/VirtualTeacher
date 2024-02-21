using VirtualTeacher.Models;

namespace VirtualTeacher.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Course>? CoursesByDate { get; set; }
        public IEnumerable<Course>? CoursesByPopularity { get; set; }
        public IEnumerable<Course>? CoursesByRating { get; set; }
        public IEnumerable<User>? Teachers { get; set; }
        public int UsersCount { get; set; }
        public int CoursesCount { get; set; }
        public int LecturesCount { get; set; }
    }
}
