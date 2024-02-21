using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Services.Contracts;

public interface ICourseService
{
    public List<Course> GetAllCourses();
    PaginatedList<Course> FilterCoursesBy(CourseQueryParameters parameters);
    List<Course> FilterByTeacherId(int? teacherId);
    List<Course> GetNewestCourses();
    List<Course> GetTopRatedCourses();
    List<Course> GetPopularCourses();
    Course GetCourseById(int id);
    Course CreateCourse(CourseCreateDto dto);
    Course UpdateCourse(int id, CourseUpdateDto dto);
    string DeleteCourse(int id);
    int GetCoursesCount();

    string Enroll(int courseId);
    string AddTeacher(int courseId, int teacherId);
    string InviteFriend(int courseId, string email, string name);

    List<Rating> GetRatings(int courseId);
    Rating RateCourse(int courseId, RatingCreateDto dto);
    string RemoveRating(int courseId);

    List<Lecture> GetLectures(int courseId);
    Lecture GetLectureById(int courseId, int lectureId);
    Lecture CreateLecture(LectureCreateDto dto, int courseId);
    Lecture UpdateLecture(LectureUpdateDto dto, int courseId, int lectureId);
    public string DeleteLecture(int courseId, int lectureId);
    int GetLecturesCount();

    List<Comment> GetComments(int courseId, int lectureId);
    Comment GetCommentById(int courseId, int lectureId, int commentId);
    Comment CreateComment(int courseId, int lectureId, CommentCreateDto dto);
    Comment UpdateComment(int courseId, int lectureId, int commentId, CommentCreateDto dto);
    string DeleteComment(int courseId, int lectureId, int commentId);

    public string GetNote(int courseId, int lectureId);

    public string UpdateNoteContent(int courseId, int lectureId, string updatedContent);

    string CreateSubmission(int courseId, int lectureId, IFormFile file);
    string DeleteSubmission(int courseId, int lectureId);
    string AssessSubmission(int lectureId, int userId, byte grade);
    string GetSubmissionFilePath(int courseId, int lectureId); // get logged user submission
    string GetSubmissionFilePath(int courseId, int lectureId, int studentId); // get student submission by teacher
    Submission? GetSubmission(int courseId, int lectureId, int userId);

    string GetAssignmentFilePath(int courseId, int lectureId);
    string CreateAssignment(int courseId, int lectureId, IFormFile file);
    string DeleteAssignment(int courseId, int lectureId);
    List<Comment> GetCommentsByUser();
}