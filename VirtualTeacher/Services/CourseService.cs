using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Course;
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
        var createdCourse = courseRepository.CreateCourse(dto);

        return createdCourse ?? throw new Exception($"The course could not be created.");
    }

    public Course UpdateCourse(int id, CourseUpdateDto dto)
    {
        var updatedCourse = courseRepository.UpdateCourse(id, dto);

        return updatedCourse ?? throw new Exception($"The course could not be updated.");

    }

    public string DeleteCourse(int id)
    {
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
}