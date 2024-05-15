using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webASP.Models;
using System.Data.SqlClient;


namespace webASP.Controllers;

public class AboutController : Controller
{
    private readonly ILogger<AboutController> _logger;
    private readonly SqlConnection? connection;

    public AboutController(ILogger<AboutController> logger)
    {
        _logger = logger;
        ConnectionService cnService = new ConnectionService();
        this.connection = cnService.cn;


    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Info()
    {
        Console.WriteLine("Check: " + Request.Query["id"].ToString());
        return View();
    }

    public IActionResult Introduce()
    {
        return View();
    }

    public IActionResult PolicyBooking()
    {
        return View();
    }

    public IActionResult PriceList()
    {
        List<ProductDTO> data = new List<ProductDTO>();

        if (this.connection != null)
        {
            this.connection.Open();
            string query = "select * from Products";
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["Id"]);
                string model = reader["model"].ToString();
                string brand = reader["brand"].ToString();
                string capacity = reader["capacity"].ToString();
                string thumbnail = reader["thumbnail"].ToString();
                string price = reader["price"].ToString();


                ProductDTO product = new ProductDTO(id, model, brand, capacity, thumbnail, price);
                data.Add(product);
            }
            ViewBag.Data = data;
        }
        return View("~/Views/About/PriceList.cshtml");
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
