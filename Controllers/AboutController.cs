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
            string query = "select * from Products left join [All-code] on [All-code].id = Products.location_id";
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["Id"]);
                string? model = reader["model"].ToString();
                string? brand = reader["brand"].ToString();
                string? capacity = reader["capacity"].ToString();
                string? thumbnail = reader["thumbnail"].ToString();
                string? price = reader["price"].ToString();
                string? location = reader["content_title"].ToString();
                string? percentNew = reader["precent_new"].ToString();

                ProductDTO product = new ProductDTO(id, model, brand, capacity, thumbnail, price, location, percentNew);
                data.Add(product);
            }
            ViewBag.Data = data;
        }
        return View("~/Views/About/PriceList.cshtml");
    }

    public IActionResult Contact()
    {
        return View("~/Views/About/Contact.cshtml");
    }

    [HttpPost]
    public IActionResult HandleDataFormContact(string Name, string Email, string Title, string Description)
    {
        if (this.connection != null)
        {
            try
            {
                this.connection.Open();
                string query = "insert into Contact (Name, Email, Description, Title) values ('" + Name + "', '" + Email + "', '" + Description + "', '" + Title + "')";

                SqlCommand command = new SqlCommand(query, this.connection);
                command.ExecuteNonQuery();
                this.connection.Close();
            }
            catch (System.Exception err)
            {
                Console.WriteLine("Error " + err);
            }
        }
        return RedirectToAction("ContactForm", "About", new { isContactDone = true });
    }

    public IActionResult ContactForm()
    {
        return View("~/Views/About/Contact.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
