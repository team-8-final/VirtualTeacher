using Microsoft.AspNetCore.Mvc;

namespace VirtualTeacher.Controllers.MVC;

public class SharedController : Controller
{
    public IActionResult Success()
    {
        return View("Success");
    }

    public IActionResult Error()
    {
        return View("Error");
    }
}