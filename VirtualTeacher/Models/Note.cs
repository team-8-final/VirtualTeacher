using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models;

public class Note
{
    public int Id { get; set; }

    [MaxLength(1024)]
    public string FilePath { get; set; } = null!;

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}