using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts;

public interface ICourseService
{
    PaginatedList<Course> FilterCoursesBy(CourseQueryParameters parameters);
    Course GetCourseById(int id);
    Course CreateCourse(CourseCreateDto dto);
    Course UpdateCourse(int id, CourseUpdateDto dto);
    string DeleteCourse(int id);

    string Enroll(int courseId);
    string AddTeacher(int courseId, int teacherId);

    List<Rating> GetRatings(int courseId);
    Rating RateCourse(int courseId, RatingCreateDto dto);
    string RemoveRating(int courseId);

    List<Lecture> GetLectures(int courseId);
    Lecture GetLectureById(int courseId, int lectureId);

    Lecture CreateLecture(LectureCreateDto dto, int courseId);

    Lecture UpdateLecture(LectureUpdateDto dto, int courseId, int lectureId);

    public string DeleteLecture(int courseId, int lectureId);
    List<Comment> GetComments(int courseId, int lectureId);
    Comment GetCommentById(int courseId, int lectureId, int commentId);
    Comment CreateComment(int courseId, int lectureId, CommentCreateDto dto);
    Comment UpdateComment(int courseId, int lectureId, int commentId, CommentCreateDto dto);
    string DeleteComment(int courseId, int lectureId, int commentId);

    public string GetNoteContent(int courseId, int lectureId);

    public string UpdateNoteContent(int courseId, int lectureId, string updatedContent);

    string CreateSubmission(int courseId, int lectureId, IFormFile file);
    string DeleteSubmission(int courseId, int lectureId);
    string GetSubmissionFilePath(int courseId, int lectureId, string username);
}