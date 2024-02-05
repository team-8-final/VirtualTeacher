using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Data;
using VirtualTeacher.Models;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Exceptions;
using System.Threading;

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
                .ThenInclude(l => l.Submissions)
            .Include(course => course.Ratings)
                .ThenInclude(rating => rating.Student)
            .Include(course => course.ActiveTeachers)
                .Include(course => course.EnrolledStudents)
            .Where(course => course.IsDeleted == false);
    }

    public Course? GetCourseById(int id)
    {
        Course? course = GetCourses().FirstOrDefault(u => u.Id == id);

        return course;
    }

    public Course? CreateCourse(CourseCreateDto dto, User teacher)
    {
        var newCourse = new Course()
        {
            Title = dto.Title,
            Description = dto.Description ?? "",
            StartingDate = dto.StartingDate,
            CourseTopic = dto.CourseTopic,
            Published = dto.Published,
            EnrolledStudents = new List<User>(),
            Lectures = new List<Lecture>(),
            Ratings = new List<Rating>(),
            ActiveTeachers = new List<User> { teacher }
        };

        context.Courses.Add(newCourse);
        context.SaveChanges();

        return newCourse;
    }

    public Course? UpdateCourse(int id, CourseUpdateDto dto)
    {
        var updatedCourse = GetCourseById(id);

        if (updatedCourse == null)
        {
            return null;
        }

        updatedCourse.Title = dto.Title ?? updatedCourse.Title;
        updatedCourse.Description = dto.Description ?? updatedCourse.Description;
        updatedCourse.StartingDate = dto.StartingDate ?? updatedCourse.StartingDate;
        updatedCourse.CourseTopic = dto.CourseTopic;
        updatedCourse.Published = dto.Published;

        context.Update(updatedCourse);
        context.SaveChanges();

        return updatedCourse;

    }

    public bool? DeleteCourse(int id)
    {
        Course? course = GetCourseById(id);

        if (course == null)
        {
            return null;
        }

        course.IsDeleted = true;
        context.SaveChanges();

        return true;
    }

    // Ratings
    public List<Rating> GetRatings(Course course)
    {
        var ratingsList = context.Ratings
            .Include(rating => rating.Course)
            .Include(user => user.Student)
            .Where(rating => rating.Course == course)
            .ToList();

        return ratingsList;
    }

    public Rating? CreateRating(Course course, User user, RatingCreateDto dto)
    {
        var rating = new Rating()
        {
            Course = course,
            Student = user,
            Value = dto.Value
        };

        context.Ratings.Add(rating);
        context.SaveChanges();

        return rating;
    }

    public Rating? UpdateRating(Rating rating, RatingCreateDto dto)
    {
        rating.Value = dto.Value;
        context.SaveChanges();

        return rating;
    }

    public bool RemoveRating(Rating rating)
    {
        context.Ratings.Remove(rating);
        context.SaveChanges();

        return true;
    }


    //Lectures
    public List<Lecture> GetLectures(Course course)
    {
        var lecturesList = context.Lectures
            .Include(lecture => lecture.Course)
            .Include(lecture => lecture.Teacher)
            .Include(lecture => lecture.WatchedBy)
            .Include(lecture => lecture.Notes)
            .Include(lecture => lecture.Comments)
            .Include(lecture => lecture.Submissions)
            .Where(lecture => lecture.Course.Id == course.Id)
            .ToList();

        return lecturesList;
    }

    public Lecture? GetLecture(int courseId, int lectureId)
    {
        var lecture = context.Lectures
            .Include(lecture => lecture.Course)
            .Include(lecture => lecture.Teacher)
            .Include(lecture => lecture.WatchedBy)
            .Include(lecture => lecture.Notes)
            .Include(lecture => lecture.Comments)
            .Where(lecture => lecture.Course.Id == courseId)
            .FirstOrDefault(lecture => lecture.Id == lectureId);

        return lecture;
    }

    //delete
    //update

    public Lecture UpdateLecture(Lecture lecture, LectureUpdateDto dto)
    {

        lecture.Title = dto.Title;
        lecture.Description = dto.Description;
        lecture.VideoLink = dto.VideoLink;
        lecture.AssignmentLink = dto.AssignmentLink;

        context.SaveChanges();
        return lecture;
    }


    public Lecture? CreateLecture(LectureCreateDto dto, User teacher, int courseId)
    {
        var lecture = new Lecture()
        {
            Title = dto.Title,
            Description = dto.Description,
            VideoLink = dto.VideoLink,
            AssignmentLink = dto.AssignmentLink,
            TeacherId = teacher.Id,
            CourseId = courseId
        };

        context.Lectures.Add(lecture);
        context.SaveChanges();

        return lecture;
    }


    public PaginatedList<Course> FilterBy(CourseQueryParameters parameters)
    {
        IQueryable<Course> result = GetCourses();

        result = FilterByTitle(result, parameters.Title);
        result = FilterByTopic(result, parameters.Topic);
        result = FilterByTeacherUsername(result, parameters.TeacherUsername);
        result = FilterByRating(result, parameters.Rating);
        result = FilterByMinRating(result, parameters.MinRating);
        result = SortBy(result, parameters.SortBy);
        result = OrderBy(result, parameters.SortOrder);

        int totalPages = (int)Math.Ceiling(((double)result.Count()) / parameters.PageSize);

        return new PaginatedList<Course>(result.ToList(), totalPages, parameters.PageNumber);
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

        return courses.Where(course => course.ActiveTeachers
            .Any(teacher => teacher.Username.ToLower().Equals(username.ToLower())));
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
        if (rating.HasValue)
            return courses.Where(t => t.Ratings.Average(r => r.Value) == rating);
        else
            return courses;
    }


    private static IQueryable<Course> FilterByMinRating(IQueryable<Course> courses, byte? minRating)
    {
        if (minRating.HasValue)
            return courses.Where(t => t.Ratings.Average(r => r.Value) >= minRating);
        else
            return courses;
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
                    return courses.OrderBy(u => u.Title);
                case "rating":
                    return courses.OrderByDescending(u => u.Ratings.Average(r => r.Value));
                default:
                    return courses;
            }
        }
    }

    public bool DeleteLecture (Lecture lectureToDelete)
    {
        if (lectureToDelete == null)
        {
            return false;
        }

        context.Lectures.Remove(lectureToDelete);
        return context.SaveChanges() > 0;
    }

    public bool Enroll(int courseId, User user)
    {
        var course = GetCourseById(courseId);

        if (course == null)
            return false;

        course.EnrolledStudents.Add(user);

        return context.SaveChanges() > 0;
    }

    public bool AddTeacher(int courseId, User teacher)
    {
        var course = GetCourseById(courseId);

        if (course == null)
            return false;

        course.ActiveTeachers.Add(teacher);

        return context.SaveChanges() > 0;
    }


    //Comments

    public List<Comment> GetComments(Lecture lecture)
    {
        var commentList = context.Comments
            .Include(c => c.Lecture)
            .Include(c => c.Author)
            .Where(c => c.Lecture == lecture)
            .ToList();

        return commentList;
    }

    public Comment? GetComment(int lectureId, int commentId)
    {
        var comment = context.Comments
            .Include(c => c.Lecture)
            .Include(c => c.Author)
            .Where(c => c.LectureId == lectureId)
            .FirstOrDefault(c => c.Id == commentId);

        return comment;
    }

    public Comment? CreateComment(Lecture lecture, User user, CommentCreateDto dto)
    {
        var comment = new Comment()
        {
            Lecture = lecture,
            Author = user,
            Content = dto.Content
        };

        context.Comments.Add(comment);
        context.SaveChanges();

        return comment;
    }

    public Comment? UpdateComment(Comment comment, CommentCreateDto dto)
    {
        comment.Content = dto.Content;
        comment.IsEdited = true;
        comment.EditDate = DateTime.Now;

        context.SaveChanges();

        return comment;
    }

    public bool DeleteComment(Comment comment)
    {
        context.Comments.Remove(comment);
        context.SaveChanges();

        return true;
    }

    //Notes

    public string GetNoteContent(int userId, int lectureId)
    {
        if (context.Notes == null)
        {
            throw new EntityNotFoundException("Not found");
        }

        if (context.Notes.FirstOrDefault(note => note.LectureId == lectureId && note.StudentId == userId) == null)  // if the Note does not exist
        {
            throw new EntityNotFoundException("You have no notes for this Lecture");
        }

        return context.Notes.FirstOrDefault(note => note.LectureId == lectureId && note.StudentId == userId).Content
        ?? throw new EntityNotFoundException("Not found");                                                          // something doesn't match, this shouldn't be reached ever if everything works fine
    }

    public string UpdateNoteContent(int userId, int lectureId, string updatedContent)
    {


        if (context.Notes.FirstOrDefault(note => note.LectureId == lectureId && note.StudentId == userId) == null)  // if the Note does not exist yet...
        {
            if (!AddNote(new Note { LectureId = lectureId, StudentId = userId }))                                   // ...create it
            {
                throw new Exception("What the hell?");
            }
        }

        Note note = context.Notes.FirstOrDefault(note => note.LectureId == lectureId && note.StudentId == userId);
        note.Content = updatedContent;
        context.SaveChanges();

        return $"Note content updated to \"{updatedContent}\" ";
    }

    public bool AddNote(Note noteToAdd)
    {
        context.Notes.Add(noteToAdd);
        return context.SaveChanges() > 0;
    }

    public Submission? GetSubmission(int lectureId, int userId)
    {
        var foundSubmission = context.Submissions
            .FirstOrDefault(submission => submission.LectureId == lectureId && submission.StudentId == userId);

        return foundSubmission;
    }

    public bool CreateSubmission(int lectureId, int userId, byte? grade = null)
    {
        var submission = GetSubmission(lectureId, userId);

        if (submission != null)
        {
            DeleteSubmission(lectureId, userId);
        }

        var newSubmission = new Submission
        {
            LectureId = lectureId,
            StudentId = userId,
            Grade = grade,
        };

        context.Submissions.Add(newSubmission);
        context.SaveChanges();

        return true;
    }

    public bool GradeSubmission(int lectureId, int userId, byte grade)
    {
        var submission = GetSubmission(lectureId, userId);

        if (submission == null)
        {
            return false;
        }

        submission.Grade = grade;
        context.SaveChanges();

        return true;
    }

    public bool DeleteSubmission(int lectureId, int userId)
    {
        var submissionToDelete = context.Submissions
            .FirstOrDefault(submission => submission.LectureId == lectureId && submission.StudentId == userId);

        if (submissionToDelete == null)
        {
            return false;
        }

        context.Submissions.Remove(submissionToDelete);
        context.SaveChanges();
        return true;
    }

}