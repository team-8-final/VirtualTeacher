using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.ViewModels.Account;

namespace VirtualTeacher.Controllers.MVC;

public class AccountController : Controller
{
    private readonly IUserService userService;
    private readonly IAccountService accountService;

    public AccountController(IUserService userService, IAccountService accountService)
    {
        this.userService = userService;
        this.accountService = accountService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();

            var model = new AccountInfoModel
            {
                Username = loggedUser.Username,
                FirstName = loggedUser.FirstName,
                LastName = loggedUser.LastName,
                Email = loggedUser.Email,
                EnrolledCourses = loggedUser.EnrolledCourses,
                CreatedCourses = loggedUser.CreatedCourses
            };

            return View("Index", model);
        }
        catch (Exception e)
        {
            TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Error", "Shared");
        }
    }

    [HttpPost]
    public IActionResult Index(AccountInfoModel model)
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();
            model.Username = loggedUser.Username;

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var dto = new UserUpdateDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };

            accountService.AccountUpdate(dto);

            return RedirectToAction("Index");
        }
        catch (UnauthorizedOperationException)
        {
            return RedirectToAction("login");
        }
        catch (DuplicateEntityException)
        {
            ModelState.AddModelError("Email", "The email is already in use.");

            return View("Index", model);
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

            var model = new AccountViewModel
            {
                Username = loggedUser.Username,
                FirstName = loggedUser.FirstName,
                LastName = loggedUser.LastName,
                Email = loggedUser.Email,
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
    public IActionResult Update(AccountViewModel model)
    {
        try
        {
            var loggedUser = accountService.GetLoggedUser();
            model.Username = loggedUser.Username;

            if (!ModelState.IsValid)
            {
                return View("Update", model);
            }

            var dto = new UserUpdateDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };

            accountService.AccountUpdate(dto);

            return RedirectToAction("Update");
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
            };

            var user = userService.Create(dto);

            var loginViewModel = new LoginViewModel
            {
                Email = user.Email,
                Password = user.Password,
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

            if (!ModelState.IsValid)
            {
                return View("Password", model);
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("NewPassword", "The passwords do not match.");
                ModelState.AddModelError("ConfirmNewPassword", "The passwords do not match.");
                return View("Password", model);
            }

            var validPassword = accountService.ValidateCredentials(loggedUser. Email, model.CurrentPassword);

            if (!validPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Incorrect password.");
                return View("Password", model);
            }
            var dto = new UserUpdateDto()
            {
                Password = model.NewPassword
            };

            accountService.AccountUpdate(dto);

            ViewData["SuccessMessage"] = "Password updated successfully.";
            return View("Success");
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
        // catch (Exception e)
        // {
        //     TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
        //     TempData["ErrorMessage"] = e.Message;
        //     return RedirectToAction("Error", "Shared");
        // }
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

            var test = HttpContext.User.FindFirstValue("UserId");

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
            //
            // TempData["StatusCode"] = StatusCodes.Status202Accepted;
            // TempData["SuccessMessage"] = "User logged out successfully.";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception e)
        {
            // TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
            // TempData["ErrorMessage"] = e.Message;
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

            return RedirectToAction("Index", "Home");
        }
        catch (InvalidOperationException e)
        {
            TempData["StatusCode"] = StatusCodes.Status400BadRequest;
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
}