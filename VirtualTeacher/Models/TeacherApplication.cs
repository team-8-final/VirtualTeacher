using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualTeacher.Models
{
    public class TeacherApplication
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
