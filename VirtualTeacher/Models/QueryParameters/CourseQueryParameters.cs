using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.QueryParameters;

public class CourseQueryParameters
{
    public string? Title { get; set; }
    public CourseTopic? Topic { get; set; }
    public string? TeacherUsername { get; set; }

    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
    public byte? Rating { get; set; }

    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
}