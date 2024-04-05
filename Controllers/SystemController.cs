using Microsoft.AspNetCore.Mvc;

namespace webASP.Controllers;
public class SystemController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}