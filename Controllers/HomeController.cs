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

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult HandleData(string viewInput)
    {
        return RedirectToAction("Search", "Home", new { query = viewInput });
    }


    public List<ProductDTO> GetDataOnDataBase(string viewInput)
    {

        List<ProductDTO> data = new List<ProductDTO>();
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "select * from Products where model like '%" + viewInput + "%'";
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["Id"]);
                string model = reader["model"].ToString();
                string brand = reader["brand"].ToString();
                string capacity = reader["capacity"].ToString();
                string thumbnail = reader["thumbnail"].ToString();
                ProductDTO product = new ProductDTO(id, model, brand, capacity, thumbnail);
                data.Add(product);
            }
        }
        return data;
    }
    public IActionResult Search()
    {
        string queryString = Request.Query["query"].ToString();
        List<ProductDTO> data = GetDataOnDataBase(queryString);

        ViewBag.Query = queryString;
        ViewBag.Data = data;
        return View("~/Views/Search/Search.cshtml");
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