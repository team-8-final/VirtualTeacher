using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.DTOs.User
{
    public class UserResponseDto
    {
        //Maybe add avatar url, courses, lectures, comments?
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public IList<string> EnrolledCourses { get; set; } = null!;
    }
}