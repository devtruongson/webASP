using Microsoft.AspNetCore.Mvc;

namespace webASP.Controllers;
public class TravelController : Controller
{
    public IActionResult blogdulich()
    {
        return View("~/Views/Travel/Travel.cshtml");
    }
}