namespace VirtualTeacher.Models;

public class Rating
{
    public int Id { get; set; }

    public Course Course { get; set; } = null!;
    public User User { get; set; } = null!;
}