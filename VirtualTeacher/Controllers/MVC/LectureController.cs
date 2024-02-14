﻿using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels.Lectures;

namespace VirtualTeacher.Controllers.MVC;

[Route("Course/{courseId}/Lecture/")]
[ApiExplorerSettings(IgnoreApi = true)]
public class LectureController : Controller
{
    private readonly ICourseService courseService;
    private readonly IAccountService accountService;

    public LectureController(ICourseService courseService, IAccountService accountService)
    {
        this.courseService = courseService;
        this.accountService = accountService;
    }

    [IsTeacherOrAdmin]
    [HttpGet("Create")]
    public IActionResult Create(int courseId)
    {
        var lectureVM = new LectureCreateViewModel();
        //TempData["CourseId"] = courseId;

        return View();
    }

    [IsTeacherOrAdmin]
    [HttpPost("Create")]
    public IActionResult Create(int courseId, LectureCreateViewModel lectureVM)
    {
        if (!ModelState.IsValid)
        {
            return View(lectureVM);
        }

        try
        {
            var newLecture = courseService.CreateLecture(lectureVM, courseId);
            //TempData["CourseId"] = courseId;

            return RedirectToAction("Details", "Lecture", new { courseId, id = newLecture.Id });
        }
        catch (UnauthorizedOperationException e)
        {
            TempData["StatusCode"] = StatusCodes.Status401Unauthorized;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (EntityNotFoundException e)
        {
            TempData["StatusCode"] = StatusCodes.Status404NotFound;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpGet("{id}")]
    public IActionResult Details(int courseId, int id)
    {
        try
        {
            var lecture = courseService.GetLectureById(courseId, id);
            var course = courseService.GetCourseById(courseId);
            var user = accountService.GetLoggedUser();

            ViewBag.Course = course;
            ViewBag.User = user;

            return View(lecture);
        }
        catch (EntityNotFoundException e)
        {
            TempData["StatusCode"] = StatusCodes.Status404NotFound;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (UnauthorizedOperationException e)
        {
            TempData["StatusCode"] = StatusCodes.Status401Unauthorized;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;

            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpPost("/{lectureId}/get-assignment")]
    public IActionResult GetAssignment(int courseId, int lectureId)
    {
        try
        {
            var filePath = courseService.GetAssignmentFilePath(courseId, lectureId);

            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound("Assignment file not found.");
            }

            var fileName = Path.GetFileName(filePath);
            var mimeType = "application/octet-stream";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType, fileName);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("An error occurred while downloading the file.");
        }
    }

    [HttpPost("/{lectureId}/create-assignment")]
    public IActionResult CreateAssignment(int courseId, int lectureId, IFormFile file)
    {
        if (file is { Length: 0 })
        {
            return BadRequest("File is not selected or empty.");
        }

        try
        {
            var result = courseService.CreateAssignment(courseId, lectureId, file);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Assignment could not be created. Please try again.");
        }
    }

    [HttpPost("/{lectureId}/delete-assignment")]
    public IActionResult DeleteAssignment(int courseId, int lectureId)
    {
        try
        {
            var message = courseService.DeleteAssignment(courseId, lectureId);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Assignment could not be deleted. Please try again.");
        }
    }

    [HttpPost("/{lectureId}/get-submission")]
    public IActionResult GetSubmission(int courseId, int lectureId)
    {
        try
        {
            var filePath = courseService.GetSubmissionFilePath(courseId, lectureId);

            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return NotFound("Submission file not found.");
            }

            var fileName = Path.GetFileName(filePath);
            var mimeType = "application/octet-stream";
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType, fileName);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("An error occurred while downloading the file.");
        }
    }

    [HttpPost("/{lectureId}/create-submission")]
    public IActionResult CreateSubmission(int courseId, int lectureId, IFormFile file)
    {
        try
        {
            if (file is { Length: 0 })
            {
                return BadRequest("File is not selected or empty.");
            }

            _ = courseService.CreateSubmission(courseId, lectureId, file);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }        
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Submission could not be created. Please try again.");
        }
    }

    [HttpPost("/{lectureId}/delete-submission")]
    public IActionResult DeleteSubmission(int courseId, int lectureId)
    {
        try
        {
            _ = courseService.DeleteSubmission(courseId, lectureId);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception)
        {
            return BadRequest("Submission could not be deleted. Please try again.");
        }
    }

    [HttpPost("/{lectureId}/add-comment")]
    public IActionResult AddComment(int courseId, int lectureId, string content)
    {
        try
        {
            var dto = new CommentCreateDto
            {
                Content = content
            };

            _ = courseService.CreateComment(courseId, lectureId, dto);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/{lectureId}/edit-comment")]
    public IActionResult EditComment(int courseId, int lectureId, int commentId, string content)
    {
        try
        {
            var dto = new CommentCreateDto
            {
                Content = content
            };

            _ = courseService.UpdateComment(courseId, lectureId, commentId, dto);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/{lectureId}/delete-comment")]
    public IActionResult DeleteComment(int courseId, int lectureId, int commentId)
    {
        try
        {
            _ = courseService.DeleteComment(courseId, lectureId, commentId);
            return Redirect($"/Course/{courseId}/Lecture/{lectureId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}