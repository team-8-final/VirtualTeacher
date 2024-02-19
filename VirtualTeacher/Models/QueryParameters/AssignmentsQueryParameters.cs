using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.QueryParameters
{
    public class AssignmentsQueryParameters : CourseQueryParameters
    {

        public string? SearchWord { get; set; }

        public int? OpenPanel { get; set; }

    }
}
