using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace webASP.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SqlConnection? connection;

        public ProductController(ILogger<HomeController> logger)
        {
            _logger = logger;
            ConnectionService cnService = new ConnectionService();
            this.connection = cnService.cn;
        }

        public IActionResult ProductDetail()
        {
            string queryIdProduct = Request.Query["id"].ToString();
            string productName = Request.Query["name"].ToString();
            ViewBag.productName = productName;

            if (!string.IsNullOrEmpty(queryIdProduct))
            {
                ProductDTO product = HandleGetData(queryIdProduct);
                ViewBag.Product = product;

                if (product != null)
                {
                    AllCodeDTO location = GetLocationById(product.location_id);
                    ViewBag.location = location;
                }
            }

            return View("~/Views/Product/DetailProduct.cshtml");
        }

        public ProductDTO HandleGetData(string id)
        {
            ProductDTO product = new ProductDTO();
            if (this.connection != null)
            {
                this.connection.Open();
                string query = "select * from Products where id = " + id;
                SqlCommand command = new SqlCommand(query, this.connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int idProduct = Convert.ToInt32(reader["Id"]);
                    string? model = reader["model"].ToString();
                    string? brand = reader["brand"].ToString();
                    string? capacity = reader["capacity"].ToString();
                    string? thumbnail = reader["thumbnail"].ToString();
                    string? required_age = reader["required_age"].ToString();
                    int count_rented = Convert.ToInt32(reader["count_rented"]);
                    int count_remaining = Convert.ToInt32(reader["count_remaining"]);
                    string? content_markdow = reader["content_markdow"].ToString();
                    string? content_HTML = reader["content_HTML"].ToString();
                    string? location_id = reader["location_id"].ToString();
                    string? precent_new = reader["precent_new"].ToString();
                    string? price = reader["price"].ToString();

                    product = new ProductDTO(idProduct, model, brand, capacity, thumbnail, required_age, count_rented, count_remaining, content_markdow, content_HTML, location_id, precent_new, price);
                }
                this.connection.Close();
            }

            return product;
        }

        public AllCodeDTO GetLocationById(string locationId)
        {
            AllCodeDTO location = new AllCodeDTO();
            if (this.connection != null)
            {
                this.connection.Open();
                string query = "select * from [All-code] where id = " + locationId;
                SqlCommand command = new SqlCommand(query, this.connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string? type = reader["type"].ToString();
                    string? content_title = reader["content_title"].ToString();
                    string? content_detail = reader["content_detail"].ToString();
                    string? code = reader["code"].ToString();

                    location = new AllCodeDTO(id, type, content_title, content_detail, code);
                }
                this.connection.Close();
            }

            return location;
        }
    }
}
