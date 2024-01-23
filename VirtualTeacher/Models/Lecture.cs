namespace VirtualTeacher.Models;

public class Lecture
{
    public int Id { get; set; }

    public Course Course { get; set; } = null!;
    public IList<User> Student { get; set; } = null!;
    public IList<User> Teacher { get; set; } = null!;

    public IList<Assignment> Assignments { get; set; } = null!;

}