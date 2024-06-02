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


    [HttpPost]
    public IActionResult SubmitBookingForm(string LocationId, DateTime FirstReceived, DateTime DateReturnCar, int CustomerId, string PhoneNumber)
    {
        if (this.connection != null)
        {
            this.connection.Open();
            string queryCheckUser = "SELECT * FROM Customers WHERE id = @CustomerId";
            bool isValid = true;
            SqlCommand commandCheck = new SqlCommand(queryCheckUser, this.connection);
            commandCheck.Parameters.AddWithValue("@CustomerId", CustomerId);
            SqlDataReader readerCheck = commandCheck.ExecuteReader();
            if (!readerCheck.Read())
            {
                isValid = false;
                readerCheck.Close();
                string queryAddUser = "INSERT INTO Customers (id, phonenumber) VALUES (@id, @phonenumber)";
                SqlCommand commandInsert = new SqlCommand(queryAddUser, this.connection);
                commandInsert.Parameters.AddWithValue("@id", CustomerId);
                commandInsert.Parameters.AddWithValue("@phonenumber", PhoneNumber);
                commandInsert.ExecuteNonQuery();
            }
            else
            {
                readerCheck.Close();
            }

            string query = "INSERT INTO booking_form (location_id, first_received, date_return_car, customer_id) VALUES (@LocationId, @FirstReceived, @DateReturnCar, @CustomerId)";
            SqlCommand command = new SqlCommand(query, this.connection);

            command.Parameters.AddWithValue("@LocationId", LocationId ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@FirstReceived", FirstReceived);
            command.Parameters.AddWithValue("@DateReturnCar", DateReturnCar);
            command.Parameters.AddWithValue("@CustomerId", CustomerId);

            command.ExecuteNonQuery();
            this.connection.Close();
        }

        return Json(new { success = true, message = "Đã nhận thông tin!" });
    }




    public IActionResult Index()
    {
        List<AllCodeDTO> data = new List<AllCodeDTO>();

        if (this.connection != null)
        {
            this.connection.Open();
            string query = "select * from [All-code] where type = 'LOCATION';";
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                string? type = reader["type"].ToString();
                string? contentTitle = reader["content_title"].ToString();
                string? contentDetail = reader["content_detail"].ToString();
                string? code = reader["code"].ToString();


                AllCodeDTO Allcode = new AllCodeDTO(id, type, contentTitle, contentDetail, code);
                data.Add(Allcode);
            }
            this.connection.Close();
        }
        ViewBag.location = data;


        return View();
    }

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        ConnectionService cnService = new ConnectionService();
        this.connection = cnService.cn;
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

    public List<ProductDTO> GetDataOnDataBase(string viewInput, string? startCapacity = "0", string? endCapacity = "1000000", string? startPercent = "0", string? endPercent = "1000000", string? startPrice = "0", string? endPrice = "1000000")
    {

        List<ProductDTO> data = new List<ProductDTO>();
        if (this.connection != null)
        {
            this.connection.Open();
            string filter = "";
            if (string.IsNullOrEmpty(startCapacity) || string.IsNullOrEmpty(endCapacity))
            {
                startCapacity = "0";
                endCapacity = "1000000";
            }
            if (string.IsNullOrEmpty(startPercent) || string.IsNullOrEmpty(endPercent))
            {
                startPercent = "0";
                endPercent = "100000";
            }
            if (string.IsNullOrEmpty(startPrice) || string.IsNullOrEmpty(endPrice))
            {
                startPrice = "0";
                endPrice = "10000000";
            }

            if (!string.IsNullOrEmpty(startCapacity) && !string.IsNullOrEmpty(endCapacity))
            {
                filter += "capacity between " + startCapacity + " and " + endCapacity + " ";
            }
            if (!string.IsNullOrEmpty(startPrice) && !string.IsNullOrEmpty(endPrice))
            {
                filter += "and price between " + startCapacity + " and " + endPrice + " ";
            }
            if (!string.IsNullOrEmpty(startPercent) && !string.IsNullOrEmpty(endPercent))
            {
                filter += "and precent_new between " + startPercent + " and " + endPercent;
            }
            string query = "select * from Products left join [All-code] on [All-code].id = Products.location_id where model like '%" + viewInput + "%' and  (" + filter + ")";
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
        return data;
    }

    public IActionResult Search()
    {
        string queryString = Request.Query["query"].ToString();
        string capacityString = Request.Query["capacity-filter"].ToString();
        string priceStrings = Request.Query["price"].ToString();
        string percentStrings = Request.Query["percent"].ToString();
        string startPrice = "";
        string endPrice = "";
        string startPercent = "";
        string endPercent = "";
        string startCapacity = "";
        string endCapacity = "";

        if (!string.IsNullOrEmpty(capacityString))
        {
            string[] capacity = capacityString.Split(new string[] { "and" }, StringSplitOptions.None);
            startCapacity = capacity[0];
            endCapacity = capacity[1];
        }
        if (!string.IsNullOrEmpty(priceStrings))
        {
            string[] prices = priceStrings.Split(new string[] { "and" }, StringSplitOptions.None);
            startPrice = prices[0];
            endPrice = prices[1];
        }

        if (!string.IsNullOrEmpty(percentStrings))
        {
            string[] percents = percentStrings.Split(new string[] { "and" }, StringSplitOptions.None);
            startPercent = percents[0];
            endPercent = percents[1];
        }

        List<ProductDTO> data = GetDataOnDataBase(queryString, startCapacity, endCapacity, startPercent, endPercent, startPrice, endPrice);
        ViewBag.Data = data;
        ViewBag.Query = queryString;
        ViewBag.capacity = capacityString;
        ViewBag.price = priceStrings;
        ViewBag.startPrice = startPrice;
        ViewBag.endPrice = endPrice;

        ViewBag.startPercent = startPercent == "" ? "1" : startPrice;
        ViewBag.endPercent = endPercent == "" ? "100" : endPercent;
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
