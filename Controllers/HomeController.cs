using System.Data;
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



    [HttpPost]
    public IActionResult HandleSearch(string textSearch)
    {
        if (this.connection != null)
        {
            this.connection.Open();
            if (!string.IsNullOrEmpty(textSearch))
            {
                string query = "SELECT * FROM [Products] WHERE model LIKE '%' + @textSearch + '%' OR brand LIKE '%' OR ";
                SqlCommand command = new SqlCommand(query, this.connection);
                SqlDataReader reader = command.ExecuteReader();
                List<string> listModal = new List<string>();
                List<string> listBrand = new List<string>();
                List<string> listCapacity = new List<string>();
                List<string> listThumbnail = new List<string>();

                while (reader.Read())
                {
                    string modal = reader["model"].ToString();
                    string brand = reader["brand"].ToString();
                    string capacity = reader["capacity"].ToString();
                    string thumbnail = reader["thumbnail"].ToString();

                    listModal.Add(modal);
                    listBrand.Add(brand);
                    listCapacity.Add(capacity);
                    listThumbnail.Add(thumbnail);
                }

                ViewBag.DataModal = listModal;
                ViewBag.DataBrand = listBrand;
                ViewBag.DataCapacity = listCapacity;
                ViewBag.DataThumbnail = listThumbnail;
            }

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
