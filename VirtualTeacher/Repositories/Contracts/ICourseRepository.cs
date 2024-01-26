using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ICourseRepository
    {
        IList<Course> FilterBy(CourseQueryParameters parameters);

        Course? GetCourseById(int id);
        Course? CreateCourse(CourseCreateDto dto, User teacher);
        Course? UpdateCourse(int id, CourseUpdateDto dto);
        bool? DeleteCourse(int id);
        List<Rating> GetRatings(Course course);
        Rating? CreateRating(Course course, User user, RatingCreateDto dto);
        Rating? UpdateRating(Rating rating, RatingCreateDto dto);
        bool RemoveRating(Rating rating);
    }
}