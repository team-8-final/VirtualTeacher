using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.ViewModels.Students
{
    public class AssignmentsViewModel
    {
        public int OpenPanel {  get; set; }

        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100.")]
        public byte Grade { get; set; }
        public List<Course>? FilteredCourses { get; set; }
        public List<UserResponseDto>? AllStudents { get; set; }
        public CourseQueryParameters? Parameters { get; set; }

    }
}
