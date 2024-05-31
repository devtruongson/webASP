using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using webASP.Models;
using System.Diagnostics;

using webASP.dto;


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

    [HttpPost]
    public IActionResult HandleCreateBlog(string title, string content_MarkDown, string content_HTML, string cate_id, string description)
    {
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "INSERT INTO Blogs (title, description, is_active, content_MarkDown, content_HTML, cate_id) VALUES (@title, @description,@is_active, @content_MarkDown, @content_HTML, @cate_id)";
            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@content_MarkDown", content_MarkDown);
            command.Parameters.AddWithValue("@content_HTML", content_HTML);
            command.Parameters.AddWithValue("@cate_id", cate_id);
            command.Parameters.AddWithValue("@is_active", true);

            int result = command.ExecuteNonQuery();
            if (result > 0)
            {
                // Insert succeeded
                return RedirectToAction("Index", "System");
            }
        }
        return RedirectToAction("Index", "System");
    }

    [HttpPost]
    public IActionResult DeleteBlog(string id)
    {
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "delete from Blogs where id = @id";
            SqlCommand command = new SqlCommand(query, this.connection);
            command.Parameters.AddWithValue("@id", id);
            int result = command.ExecuteNonQuery();
            if (result > 0)
            {
                return RedirectToAction("Index", "System");
            }
        }
        return RedirectToAction("Index", "System");
    }

    [HttpGet]
    public IActionResult GetAllBlog()
    {
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "SELECT * FROM Blogs";
            SqlCommand command = new SqlCommand(query, this.connection);

            SqlDataReader reader = command.ExecuteReader();
            var blogs = new List<BlogDTOGet>();

            while (reader.Read())
            {
                string title = reader["title"].ToString();
                string description = reader["description"].ToString();
                string ContentMarkdown = reader["content_MarkDown"].ToString();
                string ContentHtml = reader["content_HTML"].ToString();
                int CateId = reader.GetInt32(reader.GetOrdinal("cate_id"));
                int id = reader.GetInt32(reader.GetOrdinal("id"));

                var blog = new BlogDTOGet
                {
                    Id = id,
                    Title = title,
                    Description = description,
                    ContentMarkdown = ContentMarkdown,
                    ContentHtml = ContentHtml,
                    CateId = CateId,
                };

                blogs.Add(blog);
            }

            reader.Close();
            this.connection.Close();

            return Json(blogs);
        }

        return StatusCode(500, "Database connection is null.");
    }

    public IActionResult Index()
    {
        return View("~/Views/System/Index.cshtml");
    }

    public IActionResult Dashboard()
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
        return View("~/Views/System/Dashboard.cshtml");
    }

    [HttpGet]
    public IActionResult InfoAdmin()
    {

        string queryString = Request.Query["query"].ToString();
        return PartialView(queryString);
    }

    [HttpGet]
    public IActionResult GetDataPrice()
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
        return View("~/Views/System/PriceCars.cshtml");

    }

    public IActionResult PriceCars()
    {

        return View("~/Views/System/PriceCars.cshtml");
    }


    [HttpPost]

    public IActionResult HandleChangeInfoAdmin(string email, string password)
    {

        if (this.connection != null)
        {
            this.connection.Open();
            string query = "UPDATE Admin SET password = '" + password + "' WHERE email = '" + email + "'";
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return RedirectToAction("Dashboard", "System", new { isChanged = false });
            }
        }

        // return View("~/Views/System/Dashboard.cshtml");
        return RedirectToAction("Dashboard", "System", new { isChanged = true, password = password });
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
                return RedirectToAction("Dashboard", "System", new { changePrice = "false" });
            }
        }

        // return View("~/Views/System/Dashboard.cshtml");
        // return RedirectToAction("Dashboard", "System", new { isChanged = true });
        ViewBag.ChangePrice = "true";
        return RedirectToAction("Dashboard", "System", new { changePrice = "true" });

    }

}
