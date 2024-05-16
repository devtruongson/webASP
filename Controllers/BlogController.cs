using Microsoft.AspNetCore.Mvc;

namespace webASP.Controllers;
public class BlogController : Controller
{
    public IActionResult thuexemay()
    {
        return View("~/Views/Blog/Blog.cshtml");
    }
}