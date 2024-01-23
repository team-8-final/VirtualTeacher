namespace VirtualTeacher.Models;

public class Assignment
{
    public int Id { get; set; }

    public IList<User> Teachers { get; set; } = null!;
    public User Student { get; set; } = null!;
    public Lecture Lecture { get; set; } = null!;
    public IList<Grade> Grades { get; set; } = null!;
}