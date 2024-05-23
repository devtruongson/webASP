using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace webASP.Controllers;
public class BlogController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SqlConnection? connection;

    public BlogController(ILogger<HomeController> logger)
    {
        _logger = logger;
        ConnectionService cnService = new ConnectionService();
        this.connection = cnService.cn;
    }
    public BlogDTO HandleGetData(string id)
    {
        BlogDTO allcode = new BlogDTO();
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "select * from [ALL-code] where  id = " + id;
            SqlCommand command = new SqlCommand(query, this.connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idBlog = Convert.ToInt32(reader["Id"]);
                string? title = reader["title"].ToString();
                string? description = reader["description"].ToString();
                bool is_active = Convert.ToBoolean(reader["is_active"]);
                string? content_MarkDown = reader["content_MarkDown"].ToString();
                string? content_HTML = reader["content_HTML"].ToString();
                int cate_id = Convert.ToInt32(reader["cate_id"]);

                allcode = new BlogDTO(idBlog, title, description, is_active, content_MarkDown, content_HTML, cate_id);
            }
            this.connection.Close();
        }

        return allcode;
    }
    public IActionResult thuexemay()
    {
        return View("~/Views/Blog/Blog.cshtml");
    }
}