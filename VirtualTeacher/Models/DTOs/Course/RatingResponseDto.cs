using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.DTOs.Course;

public class RatingResponseDto
{
    public double Rating { get; set; }
    public string? Review { get; set; }
    public string Student { get; set; } = null!;
}