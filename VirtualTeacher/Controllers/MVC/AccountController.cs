using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.ViewModels.Account;
using VirtualTeacher.Helpers;
using MailKit;
using VirtualTeacher.Models.DTOs;
using VirtualTeacher.Models.DTOs.Account;

namespace VirtualTeacher.Controllers.MVC;

public class AccountController : Controller
{
    private readonly IUserService userService;
    private readonly IAccountService accountService;
    private readonly ICourseService courseService;

    public AccountController(IUserService userService, IAccountService accountService, ICourseService courseService)
    {
        this.userService = userService;
        this.accountService = accountService;
        this.courseService = courseService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();

            var model = new AccountInfoViewModel
            {
                Id = loggedUser.Id,
                Username = loggedUser.Username,
                FirstName = loggedUser.FirstName,
                LastName = loggedUser.LastName,
                Email = loggedUser.Email,
                EnrolledCourses = loggedUser.EnrolledCourses,
                CreatedCourses = loggedUser.CreatedCourses,
                AvatarUrl = accountService.GetUserAvatar(loggedUser.Username),
                UserRole = loggedUser.UserRole,
                CompletedCourses = accountService.GetCompletedCourses(),
                RatedCourses = accountService.GetRatedCourses(),
                CourseComments = courseService.GetCommentsByUser(),
                TeacherApplication = loggedUser.TeacherApplication
            };

            return View("Index", model);
        }
        catch (UnauthorizedOperationException e)
        {
            return RedirectToAction("Login", "Account");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpGet]
    public IActionResult Update()
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();

            var model = new AccountUpdateViewModel
            {
                Username = loggedUser.Username,
                FirstName = loggedUser.FirstName,
                LastName = loggedUser.LastName,
                Email = loggedUser.Email,
                AvatarUrl = accountService.GetUserAvatar(loggedUser.Username),
            };

            return View("Update", model);
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpPost]
    public IActionResult Update(AccountUpdateViewModel model)
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();
            model.Username = loggedUser.Username;

            if (!ModelState.IsValid)
            {
                return View("Update", model);
            }

            var dto = new AccountUpdateDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                AvatarUrl = accountService.GetUserAvatar(loggedUser.Username),
            };

            accountService.AccountUpdate(dto);

            return RedirectToAction("Update", model);
        }
        catch (UnauthorizedOperationException)
        {
            return RedirectToAction("login");
        }
        catch (DuplicateEntityException)
        {
            ModelState.AddModelError("Email", "The email is already in use.");
            return View("Update", model);
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpPost]
    public IActionResult ChangeAvatar(IFormFile? avatarFile)
    {
        if (avatarFile == null)
        {
            return Json(new { success = false, errorMessage = "No file received." });
        }

        try
        {
            var filePath = accountService.SaveAccountAvatar(avatarFile);
            return Json(new { success = true, fileName = Path.GetFileName(filePath) });
        }
        catch (ArgumentException e)
        {
            return Json(new { success = false, errorMessage = e.Message });
        }
        catch (Exception)
        {
            return Json(new { success = false, errorMessage = "An error occurred while uploading the file." });
        }
    }


    [HttpGet]
    public IActionResult Register()
    {
        try
        {
            var loggedIn = accountService.UserIsLoggedIn();

            if (loggedIn)
            {
                return RedirectToAction("Index", "Account");
            }

            var model = new RegisterViewModel();

            return View("Register", model);
        }
        catch (UnauthorizedOperationException e)
        {
            return RedirectToAction("Login", "Account");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            if (model.Password != model.PasswordConfirmation)
            {
                ModelState.AddModelError("Password", "The passwords do not match.");
                ModelState.AddModelError("PasswordConfirmation", "The passwords do not match.");
                return View("Register", model);
            }

            var dto = new UserCreateDto
            {
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                UserRole = model.UserRole,
            };

            var user = userService.Create(dto);

            var loginViewModel = new LoginViewModel
            {
                Email = user.Email,
                Password = model.Password,
                RememberLogin = false,
            };

            Login(loginViewModel);
            return RedirectToAction("Index", "Account");
        }
        catch (UnauthorizedOperationException e)
        {
            return RedirectToAction("Login", "Account");

        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }


    [HttpGet]
    public IActionResult Password()
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();

            var model = new PasswordViewModel
            {
                Username = loggedUser.Username,
                AvatarUrl = accountService.GetUserAvatar(loggedUser.Username),
            };

            ViewData["Username"] = loggedUser.Username;
            return View("Password", model);
        }
        catch (UnauthorizedOperationException e)
        {
            return RedirectToAction("Login", "Account");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpPost]
    public IActionResult Password(PasswordViewModel model)
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();
            model.Username = loggedUser.Username;
            model.AvatarUrl = accountService.GetUserAvatar(loggedUser.Username);

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("NewPassword", "The passwords do not match.");
                ModelState.AddModelError("ConfirmNewPassword", "The passwords do not match.");
                return View("Password", model);
            }

            if (!ModelState.IsValid)
            {
                return View("Password", model);
            }

            var validPassword = accountService.ValidateCredentials(loggedUser. Email, model.CurrentPassword);

            if (!validPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Incorrect password.");
                return View("Password", model);
            }
            var dto = new AccountUpdateDto
            {
                Password = model.NewPassword
            };

            accountService.AccountUpdate(dto);

            return RedirectToAction("Index");
        }
        catch (UnauthorizedOperationException)
        {
            return RedirectToAction("login");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpGet]
    public IActionResult Login()
    {
        try
        {
            var userIsLoggedIn = accountService.UserIsLoggedIn();

            if (userIsLoggedIn)
            {
                return RedirectToAction("Index", "Account");
            }

            return View("Login");
        }
        catch (UnauthorizedOperationException e)
        {
            ModelState.AddModelError("Password", "Invalid credentials.");
            return View("Index");
        }
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        try
        {
            var userIsLoggedIn = accountService.UserIsLoggedIn();

            if (userIsLoggedIn)
            {
                return RedirectToAction("Index", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            var validCredentials = accountService.ValidateCredentials(model.Email, model.Password);
            if (!validCredentials)
            {
                throw new UnauthorizedOperationException("Invalid credentials.");
            }

            var user = userService.GetByEmail(model.Email);

            List<Claim> claims = new List<Claim>
            {
                new("UserId", user.Id.ToString()),
                new("FirstName", user.FirstName),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.UserRole.ToString())
            };

            switch (user.UserRole)
            {
                case UserRole.Admin:
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    break;
                case UserRole.Teacher:
                    claims.Add(new Claim(ClaimTypes.Role, "Teacher"));
                    break;
                case UserRole.Student:
                    claims.Add(new Claim(ClaimTypes.Role, "Student"));
                    break;
                default:
                    claims.Add(new Claim(ClaimTypes.Role, "Anonymous"));
                    break;
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties { IsPersistent = model.RememberLogin });

            return RedirectToAction("Index");
        }
        catch (UnauthorizedOperationException e)
        {
            ModelState.AddModelError("Email", "Invalid credentials.");
            ModelState.AddModelError("Password", "Invalid credentials.");
            return View("Login");
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpGet]
    public IActionResult Logout()
    {
        try
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception e)
        {
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpGet]
    public IActionResult Delete()
    {
        try
        {
            var loggedInUser = accountService.GetLoggedUser();
            userService.Delete(loggedInUser.Id);

            Logout();

            return Json(new { success = true });

        }
        catch (InvalidOperationException e)
        {
            return Json(new { success = false, errorMessage = e.Message });
        }
        catch (Exception e)
        {
            return Json(new { success = false, errorMessage = e.Message });
        }
    }
}