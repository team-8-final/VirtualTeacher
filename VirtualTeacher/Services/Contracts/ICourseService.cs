using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts;

public interface ICourseService
{
    IList<Course> FilterCoursesBy(CourseQueryParameters parameters);
    Course GetCourseById(int id);
    Course CreateCourse(CourseCreateDto dto);
    Course UpdateCourse(int id, CourseUpdateDto dto);
    string DeleteCourse(int id);

    List<Rating> GetRatings(int courseId);
    Rating RateCourse(int courseId, RatingCreateDto dto);
    string RemoveRating(int courseId);

    List<Lecture> GetLectures(int courseId);
    Lecture GetLectureById(int courseId, int lectureId);
}