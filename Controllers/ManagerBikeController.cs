using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using webASP.DTO;
using webASP.Models;

namespace webASP.Controllers;

public class ManagerBikeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SqlConnection? connection;


    public ManagerBikeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        ConnectionService cnService = new ConnectionService();
        this.connection = cnService.cn;
    }

    public IActionResult HandleData(string viewInput)
    {
        return RedirectToAction("Index", "ManagerBike", new { query = viewInput });
    }

    public IActionResult Index()
    {
        string queryString = Request.Query["query"].ToString();
        List<RentedBikeDTO> rentedBikes = new List<RentedBikeDTO>();
        if (this.connection != null)
        {
            this.connection.Open();

            int idCustomer = 0;
            string queryInfoUser = "select id from Customers where email like '%" + queryString + "%' or phonenumber = '%" + queryString + "%'";
            using (SqlCommand commandInfo = new SqlCommand(queryInfoUser, connection))
            {
                using (SqlDataReader readerInfo = commandInfo.ExecuteReader())
                {
                    if (readerInfo.HasRows)
                    {
                        readerInfo.Read();
                        idCustomer = readerInfo.GetInt32(0);
                    }
                }

            }

            string query = @"
            SELECT o.id, o.time_order, o.expried_car, o.price_total, c.email, c.phonenumber, p.model, p.thumbnail
            FROM Orders o
            JOIN Customers c ON o.customer_id = c.id
            JOIN Products p ON o.product_id = p.id
            WHERE customer_id=" + idCustomer; // Giả sử chỉ hiển thị các đơn hàng đã nhận
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                string? timeOrder = reader["time_order"].ToString();
                string? expriedCar = reader["expried_car"].ToString();
                string? priceTotal = reader["price_total"].ToString();
                string? customerEmail = reader["email"].ToString();
                string? customerPhoneNumber = reader["phonenumber"].ToString();
                string? model = reader["model"].ToString();
                string? thumbnail = reader["thumbnail"].ToString();

                rentedBikes.Add(new RentedBikeDTO(id, timeOrder, expriedCar, priceTotal, customerEmail, customerPhoneNumber, model, thumbnail));
            }
            this.connection.Close();
        }
        ViewBag.RentedBikes = rentedBikes;
        return View("~/Views/ManagerBike/ManagerBike.cshtml");
    }
}
