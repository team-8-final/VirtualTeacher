namespace VirtualTeacher.Models;

public class Grade
{
    public int Id { get; set; }
    
    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; } = null!;

    public int TeacherId { get; set; }
    public User Teacher { get; set; } = null!;
}