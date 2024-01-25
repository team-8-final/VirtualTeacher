using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts;

public interface ICourseService
{
    IList<Course> FilterBy(CourseQueryParameters parameters);

    Course GetById(int id);
    Course Create(CourseCreateDto dto);
    Course Update(int id, CourseUpdateDto dto);
    string Delete(int id);
}