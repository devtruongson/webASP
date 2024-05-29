using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using webASP.DTO;

namespace webASP.Controllers
{
    public class RentedBikeController : Controller
    {
        private readonly ILogger<RentedBikeController> _logger;
        private readonly SqlConnection? connection;

        public RentedBikeController(ILogger<RentedBikeController> logger)
        {
            _logger = logger;
            ConnectionService cnService = new ConnectionService();
            this.connection = cnService.cn;
        }

        public IActionResult Index()
        {
            List<RentedBikeDTO> rentedBikes = new List<RentedBikeDTO>();
            if (this.connection != null)
            {
                this.connection.Open();
                string query = @"
                    SELECT o.id, o.time_order, o.expried_car, o.price_total, c.email, c.phonenumber, p.model, p.thumbnail
                    FROM Orders o
                    JOIN Customers c ON o.customer_id = c.id
                    JOIN Products p ON o.product_id = p.id
                    WHERE o.is_received = 1"; // Assuming only received orders should be displayed
                SqlCommand command = new SqlCommand(query, this.connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string timeOrder = reader["time_order"].ToString();
                    string expriedCar = reader["expried_car"].ToString();
                    string priceTotal = reader["price_total"].ToString();
                    string customerEmail = reader["email"].ToString();
                    string customerPhoneNumber = reader["phonenumber"].ToString();
                    string model = reader["model"].ToString();
                    string thumbnail = reader["thumbnail"].ToString();

                    rentedBikes.Add(new RentedBikeDTO(id, timeOrder, expriedCar, priceTotal, customerEmail, customerPhoneNumber, model, thumbnail));
                }
                this.connection.Close();
            }
            ViewBag.RentedBikes = rentedBikes;
            return View("~/Views/System/Rented.cshtml");
        }
    }
}
