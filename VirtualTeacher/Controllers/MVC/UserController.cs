using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels.Users;

namespace VirtualTeacher.Controllers.MVC
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IApplicationService applicationService;
        private readonly IAccountService accountService;
        private readonly ModelMapper mapper;

        public UserController(IUserService userService, IApplicationService applicationService, IAccountService accountService, ModelMapper mapper)
        {
            this.userService = userService;
            this.applicationService = applicationService;
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [IsAdmin]
        [HttpGet]
        [Route("/Users")]
        public IActionResult Index(UserQueryParameters parameters)
        {
            ViewData["SortOrder"] = string.IsNullOrEmpty(parameters.SortOrder) ? "desc" : "";
            ViewData["UserCount"] = userService.GetUserCount();
            var users = userService.FilterBy(parameters);
            var applications = applicationService.GetAllApplications();

            UserIndexViewModel vm = new UserIndexViewModel()
            {
                Users = users,
                Applications = applications
            };

            return View(vm);
        }

        [IsAdmin]
        [HttpGet]
        [Route("User/{id}/Update/")]
        public IActionResult Update([FromRoute] int id)
        {
            try
            {
                var user = userService.GetById(id);
                UserUpdateViewModel userVM = mapper.MapUpdateVM(user);

                return View(userVM);
            }
            catch (EntityNotFoundException e)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
            catch (Exception e)
            {
                TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }

        [IsAdmin]
        [HttpPost]
        [Route("User/{id}/Update/")]
        public IActionResult Update([FromRoute] int id, UserUpdateViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }
            try
            {
                var updatedUser = userService.UpdateUser(id, userVM);

                return RedirectToAction("Index", "User");
            }
            catch (Exception e)
            {
                TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }

        [IsAdmin]
        [HttpGet]
        [Route("User/{id}/Delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                userService.Delete(id);

                return Json(new { success = true });
            }
            catch (EntityNotFoundException e)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }

        [IsAdmin]
        [HttpGet]
        [Route("User/{id}/Avatar")]
        public IActionResult RemoveAvatar([FromRoute] int id)
        {
            try
            {
                var user = userService.GetById(id);
                accountService.DeleteUserAvatar(user.Username);

                return RedirectToAction("Update", "User", new { id = user.Id });
            }
            catch (EntityNotFoundException e)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                ViewData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }
    }
}