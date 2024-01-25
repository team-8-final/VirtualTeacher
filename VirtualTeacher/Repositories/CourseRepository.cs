using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext context;

    public CourseRepository(AppDbContext context)
    {
        this.context = context;
    }

    private IQueryable<Course> GetCourses()
    {
        return context.Courses
            .Include(course => course.Lectures)
            .Include(course => course.Ratings)
            .Include(course => course.ActiveTeachers)
            .Include(course => course.EnrolledStudents)
            .Where(course => course.IsDeleted == false);
    }

    // todo: currently logged in user should be added as an active teacher
    public Course? GetById(int id)
    {
        Course? course = GetCourses().FirstOrDefault(u => u.Id == id);

        return course;
    }

    public Course? Create(CourseCreateDto dto)
    {
        var newCourse = new Course()
        {
            Title = dto.Title,
            Description = dto.Description ?? "",
            StartingDate = dto.StartingDate,
            CourseTopic = dto.CourseTopic,
            Published = dto.Published ?? false,
            EnrolledStudents = new List<User>(),
            Lectures = new List<Lecture>(),
            Ratings = new List<Rating>(),
            ActiveTeachers = new List<User>()
        };

        context.Courses.Add(newCourse);
        context.SaveChanges();

        return newCourse;
    }

    public Course? Update(int id, CourseUpdateDto dto)
    {
        var updatedCourse = GetById(id);

        if (updatedCourse == null)
        {
            return null;
        }

        updatedCourse.Title = dto.Title ?? updatedCourse.Title;
        updatedCourse.Description = dto.Description ?? updatedCourse.Description;
        updatedCourse.StartingDate = dto.StartingDate ?? updatedCourse.StartingDate;
        updatedCourse.CourseTopic = dto.CourseTopic;
        updatedCourse.Published = dto.Published ?? updatedCourse.Published;

        context.Update(updatedCourse);
        context.SaveChanges();

        return updatedCourse;

    }

    public bool? Delete(int id)
    {
        Course? course = GetById(id);

        if (course == null)
        {
            return null;
        }

        course.IsDeleted = true;
        context.SaveChanges();

        return true;
    }


    public IList<Course> FilterBy(CourseQueryParameters parameters)
    {
        IQueryable<Course> result = GetCourses();

        result = FilterByTitle(result, parameters.Title);
        result = FilterByTopic(result, parameters.Topic);
        result = FilterByTeacherUsername(result, parameters.TeacherUsername);
        result = FilterByRating(result, parameters.Rating);

        result = SortBy(result, parameters.SortBy);
        result = OrderBy(result, parameters.SortOrder);


        return new List<Course>(result.ToList());
    }

    private static IQueryable<Course> FilterByTitle(IQueryable<Course> courses, string? title)
    {
        if (string.IsNullOrEmpty(title))
        {
            return courses;
        }

        var lowerTitle = title.ToLower();
        return courses.Where(course => course.Title.ToLower().Contains(lowerTitle));
    }

    private static IQueryable<Course> FilterByTeacherUsername(IQueryable<Course> courses, string? username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return courses;
        }

        // todo: swap email and username property when implemented
        return courses.Where(course => course.ActiveTeachers
            .Any(teacher => teacher.Email.ToLower().Equals(username.ToLower())));
    }

    private static IQueryable<Course> FilterByTopic(IQueryable<Course> courses, CourseTopic? topic)
    {
        if (topic == null)
        {
            return courses;
        }

        return courses.Where(course => course.CourseTopic == topic);

    }

    private static IQueryable<Course> FilterByRating(IQueryable<Course> courses, byte? rating)
    {
        if (rating == null)
        {
            return courses;
        }

        return courses.Where(course => course.Ratings.Any(r => r.Value == rating));
    }

    private static IQueryable<Course> OrderBy(IQueryable<Course> courses, string? sortOrder)
    {
        return (sortOrder == "desc") ? courses.Reverse() : courses;
    }

    private IQueryable<Course> SortBy(IQueryable<Course> courses, string? sortByCriteria)
    {
        {
            switch (sortByCriteria)
            {
                case "id":
                    return courses.OrderBy(u => u.Id);
                case "title":
                    return courses.OrderByDescending(u => u.Title);
                case "rating":
                    return courses.OrderByDescending(u => u.Ratings);
                default:
                    return courses;
            }
        }
    }
}