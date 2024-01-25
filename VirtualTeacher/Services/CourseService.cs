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

    public CourseService(ICourseRepository courseRepository)
    {
        this.courseRepository = courseRepository;
    }

    public IList<Course> FilterBy(CourseQueryParameters parameters)
    {
        return courseRepository.FilterBy(parameters);
    }

    public Course GetById(int id)
    {
        var foundCourse = courseRepository.GetById(id);

        return foundCourse ?? throw new EntityNotFoundException($"Course with id '{id}' was not found.");
    }

    public Course Create(CourseCreateDto dto)
    {
        var createdCourse = courseRepository.Create(dto);

        return createdCourse ?? throw new EntityNotFoundException($"The course could not be created.");
    }

    public Course Update(int id, CourseUpdateDto dto)
    {
        var updatedCourse = courseRepository.Update(id, dto);

        return updatedCourse ?? throw new EntityNotFoundException($"The course could not be updated.");

    }

    public string Delete(int id)
    {
        bool? courseDeleted = courseRepository.Delete(id);

        if (courseDeleted == true)
        {
            return $"Course with id '{id}' was deleted.";
        }

        if (courseDeleted == null)
        {
            throw new EntityNotFoundException($"Course with id '{id}' was not found.");
        }

        throw new EntityNotFoundException($"Course with id '{id}' could not be deleted.");
    }
}