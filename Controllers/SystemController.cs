using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace webASP.Controllers;
public class SystemController : Controller
{

    private readonly ILogger<SystemController> _logger;
    private readonly SqlConnection? connection;

    public SystemController(ILogger<SystemController> logger)
    {
        // _logger = logger;
        // ConnectionService cnService = new ConnectionService();
        // this.connection = cnService.cn;

    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public String HandleLogin(string email, string password)
    {
        Console.WriteLine(email);
        Console.WriteLine(password);
        return email;
    }


}