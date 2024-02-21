using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ICourseRepository
    {
        PaginatedList<Course> FilterBy(CourseQueryParameters parameters);

        List<Course> FilterByTeacherId(int? teacherId);
        Course? GetCourseById(int id);
        Course? CreateCourse(CourseCreateDto dto, User teacher);
        Course? UpdateCourse(int id, CourseUpdateDto dto);
        List<Course> GetNewestCourses();
        List<Course> GetTopRatedCourses();
        List<Course> GetPopularCourses();
        bool? DeleteCourse(int id);
        bool AddTeacher(int courseId, User teacher);
        bool Enroll(int courseId, User user);


        List<Rating> GetRatings(Course course);
        Rating? CreateRating(Course course, User user, RatingCreateDto dto);
        Rating? UpdateRating(Rating rating, RatingCreateDto dto);
        bool RemoveRating(Rating rating);

        List<Lecture> GetLectures(Course course);
        Lecture? GetLecture(int courseId, int lectureId);
        public Lecture? CreateLecture(LectureCreateDto dto, User teacher,int courseId);
        public bool DeleteLecture(Lecture lectureToDelete);

        Lecture UpdateLecture(Lecture lecture, LectureUpdateDto dto);
        List<Comment> GetComments(Lecture lecture);
        Comment? GetComment(int lectureId, int commentId);
        Comment? CreateComment(Lecture lecture, User user, CommentCreateDto dto);
        Comment? UpdateComment(Comment comment, CommentCreateDto dto);
        bool DeleteComment(Comment comment);

        string GetNoteContent(int userId, int lectureId);
        string UpdateNoteContent(int userId, int lectureId, string updatedContent);

        Submission? GetSubmission(int lectureId, int userId);
        bool DeleteSubmission(int lectureId, int userId);
        bool CreateSubmission(int lectureId, int userId, byte? grade = null);
        bool AssessSubmission(int lectureId, int userId, byte grade);
        bool CreateAssignment(int courseId, int lectureId, string fullPath);
        bool DeleteAssignment(int courseId, int lectureId);
        bool DeleteAllSubmissions(int lectureId);
        List<Comment> GetCommentsByUser(User user);

        int GetCoursesCount();
        int GetLecturesCount();
    }
}