using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webASP.Models;

namespace webASP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SqlConnection? connection;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        ConnectionService cnService = new ConnectionService();
        this.connection = cnService.cn;
    }

    public IActionResult Index()
    {
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "select * from [All-code]";
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string> dataList = new List<string>();

            while (reader.Read())
            {
                string dataItem = reader["content_title"].ToString();
                dataList.Add(dataItem);
            }
            ViewBag.Data = dataList;
            this.connection.Close();
        }
        return View();
    }



    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
