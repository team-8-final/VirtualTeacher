using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.QueryParameters
{
    public class AssignmentsQueryParameters : CourseQueryParameters
    {
        //public int? PanelOpen {  get; set; }

        public string? CourseTitle { get; set; }

        public int? TeacherId { get; set; }

        public string? SearchWord { get; set; } //search word

    }
}
