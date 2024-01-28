using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository courseRepository;
    private readonly IAuthService authService;

    public CourseService(ICourseRepository courseRepository, IAuthService authService)
    {
        this.courseRepository = courseRepository;
        this.authService = authService;
    }

    public IList<Course> FilterCoursesBy(CourseQueryParameters parameters)
    {
        return courseRepository.FilterBy(parameters);
    }

    public Course GetCourseById(int id)
    {
        var foundCourse = courseRepository.GetCourseById(id);

        return foundCourse ?? throw new EntityNotFoundException($"Course with id '{id}' was not found.");
    }

    public Course CreateCourse(CourseCreateDto dto)
    {
        var loggedUser = authService.GetLoggedUser();
        var createdCourse = courseRepository.CreateCourse(dto, loggedUser);

        return createdCourse ?? throw new Exception($"The course could not be created.");
    }

    public Course UpdateCourse(int id, CourseUpdateDto dto)
    {
        var foundCourse = GetCourseById(id);
        var loggedUser = authService.GetLoggedUser();

        if (loggedUser.UserRole != UserRole.Admin && foundCourse.ActiveTeachers.All(t => t != loggedUser))
        {
            throw new UnauthorizedAccessException($"A course can be updated only by its authors or an admin.");
        }

        var updatedCourse = courseRepository.UpdateCourse(id, dto);

        return updatedCourse ?? throw new Exception($"The course could not be updated.");

    }

    public string DeleteCourse(int id)
    {
        var foundCourse = GetCourseById(id);
        var loggedUser = authService.GetLoggedUser();

        if (loggedUser.UserRole != UserRole.Admin && foundCourse.ActiveTeachers.All(t => t != loggedUser))
        {
            throw new UnauthorizedAccessException($"A course can be deleted only by its authors or an admin.");
        }

        bool? courseDeleted = courseRepository.DeleteCourse(id);

        if (courseDeleted == true)
        {
            return $"Course with id '{id}' was deleted.";
        }

        if (courseDeleted == null)
        {
            throw new EntityNotFoundException($"Course with id '{id}' was not found.");
        }

        throw new Exception($"Course with id '{id}' could not be deleted.");
    }

    public List<Rating> GetRatings(int courseId)
    {
        var course = GetCourseById(courseId);
        var ratingsList = courseRepository.GetRatings(course);

        if (ratingsList.Count == 0)
        {
            throw new EntityNotFoundException($"No ratings found for course with id '{courseId}'.");
        }

        return ratingsList;
    }

    public Rating RateCourse(int courseId, RatingCreateDto dto)
    {
        var course = GetCourseById(courseId);
        var loggedUser = authService.GetLoggedUser();
        var ratings = courseRepository.GetRatings(course);

        var foundRating = ratings.FirstOrDefault(rating => rating.Student.Id == loggedUser.Id
                                                           && rating.Course.Id == courseId);

        if (foundRating == null)
        {
            var createdRating = courseRepository.CreateRating(course, loggedUser, dto);
            return createdRating ?? throw new Exception($"The rating could not be created.");
        }

        var updatedRating = courseRepository.UpdateRating(foundRating, dto);
        return updatedRating ?? throw new Exception($"The rating could not be updated.");
    }

    public string RemoveRating(int courseId)
    {
        var course = GetCourseById(courseId);
        var loggedUser = authService.GetLoggedUser();
        var ratings = courseRepository.GetRatings(course);

        var foundRating = ratings.FirstOrDefault(rating => rating.Student.Id == loggedUser.Id
                                                           && rating.Course.Id == courseId);

        if (foundRating == null)
        {
            return "Rating was not found.";
        }

        var ratingRemoved = courseRepository.RemoveRating(foundRating);

        return ratingRemoved ? "Rating was removed." : "Rating could not be removed.";
    }

    //Lectures
    public List<Lecture> GetLectures(int courseId)
    {
        var course = GetCourseById(courseId);
        var lectures = courseRepository.GetLectures(course);

        if (lectures.Count == 0)
        {
            throw new EntityNotFoundException($"No lectures found for course with id '{courseId}'.");
        }

        return lectures;
    }

    public Lecture GetLectureById(int courseId, int lectureId)
    {
        var course = GetCourseById(courseId);
        var foundLecture = courseRepository.GetLecture(courseId, lectureId);

        return foundLecture ?? throw new EntityNotFoundException($"Lecture with id '{lectureId}' was not found.");
    }

    //delete
    //create
    //update


    public Lecture CreateLecture(LectureCreateDto dto, int courseId)
    {
        var loggedUser = authService.GetLoggedUser();

        var createdLecture = courseRepository.CreateLecture(dto, loggedUser, courseId);

        return createdLecture ?? throw new ArgumentNullException($"The lecture could not be created.");
    }


    //Comments
    public List<Comment> GetComments(int courseId, int lectureId)
    {
        var lecture = GetLectureById(courseId, lectureId);
        var commentList = courseRepository.GetComments(lecture);

        if (commentList.Count == 0)
            throw new EntityNotFoundException($"No comments found for lecture with id '{lectureId}'.");

        return commentList;
    }

    public Comment CreateComment(int courseId, int lectureId, CommentCreateDto dto)
    {
        var lecture = GetLectureById(courseId, lectureId);
        var loggedUser = authService.GetLoggedUser();

        var createdComment = courseRepository.CreateComment(lecture, loggedUser, dto);

        return createdComment ?? throw new Exception($"The comment could not be created.");
    }

    public Comment GetCommentById(int courseId, int lectureId, int commentId)
    {
        var comment = courseRepository.GetComment(lectureId, commentId);

        return comment ?? throw new Exception($"Comment with id '{commentId}' was not found.");
    }

    public Comment UpdateComment(int courseId, int lectureId, int commentId, CommentCreateDto dto)
    {
        var comment = courseRepository.GetComment(lectureId, commentId);
        var loggedUser = authService.GetLoggedUser();

        if (comment == null)
            throw new EntityNotFoundException($"Comment with id '{commentId}' was not found.");

        if (loggedUser.UserRole != UserRole.Admin && loggedUser.Id != comment.AuthorId)
            throw new UnauthorizedAccessException($"A comment can be updated only by its author or an admin.");

        var updatedComment = courseRepository.UpdateComment(comment, dto);

        return updatedComment ?? throw new Exception($"The comment could not be updated.");
    }

    public string DeleteComment(int courseId, int lectureId, int commentId)
    {
        var comment = courseRepository.GetComment(lectureId, commentId);
        var loggedUser = authService.GetLoggedUser();

        if (comment == null)
            throw new EntityNotFoundException($"Comment with id '{commentId}' was not found.");

        if (loggedUser.UserRole != UserRole.Admin && loggedUser.Id != comment.AuthorId)
            throw new UnauthorizedAccessException($"A comment can be deleted only by its author or an admin.");

        bool? commentDeleted = courseRepository.DeleteComment(comment);

        if (commentDeleted == true)
            return $"Comment with id '{commentId}' was deleted.";

        throw new Exception($"Comment with id '{commentId}' could not be deleted.");
    }
}