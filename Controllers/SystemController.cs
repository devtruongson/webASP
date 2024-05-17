using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace webASP.Controllers;
public class SystemController : Controller
{

    private readonly ILogger<SystemController> _logger;
    private readonly SqlConnection? connection;

    public SystemController(ILogger<SystemController> logger)
    {
        _logger = logger;
        ConnectionService cnService = new ConnectionService();
        this.connection = cnService.cn;
    }


    [HttpPost]
    public IActionResult HandleLogin(string email, string password)
    {
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "select * from Admin where email = '" + email + "' and password = '" + password + "'";
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return RedirectToAction("Index", "System", new { isValid = "wrong password or email" });
            }
        }
        return RedirectToAction("Index", "System", new { email = email, password = password });
    }

    public IActionResult Index()
    {
        return View("~/Views/System/Index.cshtml");
    }

    public IActionResult Dashboard()
    {
        return View("~/Views/System/Dashboard.cshtml");
    }
}
