using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models;

public class Submission
{
    public int Id { get; set; }

    [MaxLength(8192)]
    public string SubmissionLink { get; set; } = null!;

    public byte Grade { get; set; }

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    public int StudentId { get; set; }
    public User Student { get; set; } = null!;

}