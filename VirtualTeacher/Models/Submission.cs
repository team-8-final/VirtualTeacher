namespace VirtualTeacher.Models;

public class Submission
{
    public int Id { get; set; }

    public byte? Grade { get; set; }

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    public int StudentId { get; set; }
    public User Student { get; set; } = null!;
}