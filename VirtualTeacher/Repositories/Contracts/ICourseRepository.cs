using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ICourseRepository
    {
        IList<Course> FilterBy(CourseQueryParameters parameters);

        Course? GetById(int id);
        Course? Create(CourseCreateDto dto);
        Course? Update(int id, CourseUpdateDto dto);
        bool? Delete(int id);
    }
}