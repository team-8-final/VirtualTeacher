using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models;

public class Note
{
    public int Id { get; set; }

    [MaxLength(1024)]
    public string Content { get; set; } = "";

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    public int StudentId { get; set; }
    public User Student { get; set; } = null!;
}