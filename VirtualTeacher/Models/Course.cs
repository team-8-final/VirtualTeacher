namespace VirtualTeacher.Models;

public class Course
{
    public int Id { get; set; }

    public IList<User> Teachers { get; set; } = null!;
    public IList<User> Students { get; set; } = null!;
    public IList<Lecture> Lectures { get; set; } = null!;
    public IList<Rating> Ratings { get; set; } = null!;
}