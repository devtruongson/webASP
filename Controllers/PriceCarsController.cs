using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using webASP.DTO;

namespace webASP.Controllers
{
    public class PriceCarsController : Controller
    {
        private readonly ILogger<RentedBikeController> _logger;
        private readonly SqlConnection? connection;

        public PriceCarsController(ILogger<RentedBikeController> logger)
        {
            _logger = logger;
            ConnectionService cnService = new ConnectionService();
            this.connection = cnService.cn;
        }

        public IActionResult Index()
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

                this.connection.Close();
            }
            ViewBag.Data = data;
            ViewBag.Text = "aaa";
            return View("~/Views/System/PriceCars.cshtml");
        }


        [HttpPost]

        public IActionResult HandleUpdatePrice(string price, string model)
        {

            if (this.connection != null)
            {
                this.connection.Open();

                string query = "UPDATE Products SET price = " + Double.Parse(price) + " WHERE model = '" + model + "'";
                SqlCommand command = new SqlCommand(query, this.connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("read");
                    return RedirectToAction("PriceCars", "System", new { changePrice = "false" });
                }
            }


            ViewBag.ChangePrice = "true";
            return RedirectToAction("Dashboard", "System", new { changePrice = "true" });
        }

    }
}
