using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTeacher.Models
{
    public class TeacherApplication
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public User Teacher { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
