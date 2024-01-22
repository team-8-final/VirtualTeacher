namespace VirtualTeacher.Models;

public class Assignment
{
    public int Id { get; set; }

    public User Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public IList<Grade> Grades { get; set; } = null!;
    public IList<User> Teachers { get; set; } = null!;
}