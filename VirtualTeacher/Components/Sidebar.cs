using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Components
{
    public class Sidebar : ViewComponent
    {
        private readonly IAccountService accountService;

        public Sidebar(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IViewComponentResult Invoke()
        {
            var user = accountService.GetLoggedUser();
            return View("Index", user);
        }
    }
}