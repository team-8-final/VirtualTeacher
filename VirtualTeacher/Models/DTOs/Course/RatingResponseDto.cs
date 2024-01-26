using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.DTOs.Course;

public class RatingResponseDto
{
    public byte Rating { get; set; }

    public string Course { get; set; } = null!;

    public string Student { get; set; } = null!;
}