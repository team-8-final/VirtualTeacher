using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories.Contracts
{
    public interface ICourseRepository
    {
        PaginatedList<Course> FilterBy(CourseQueryParameters parameters);

        Course? GetCourseById(int id);
        Course? CreateCourse(CourseCreateDto dto, User teacher);
        Course? UpdateCourse(int id, CourseUpdateDto dto);
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

        public string GetNoteContent(int userId, int lectureId);

        public string UpdateNoteContent(int userId, int lectureId, string updatedContent);
    }
}