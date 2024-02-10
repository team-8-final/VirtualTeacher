namespace VirtualTeacher.Models;

public class Rating
{
    public int Id { get; set; }

    public double Value { get; set; }
    public string? Review { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int StudentId { get; set; }
    public User Student { get; set; } = null!;
}