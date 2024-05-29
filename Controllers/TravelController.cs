using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webASP.dto;

namespace webASP.Controllers
{
    public class TravelController : Controller
    {
        private readonly ILogger<TravelController> _logger;
        private readonly SqlConnection? _connection;

        // Constructor sẽ được sử dụng để Inject ILogger
        public TravelController(ILogger<TravelController> logger)
        {
            _logger = logger;
            ConnectionService cnService = new ConnectionService();
            _connection = cnService.cn;
        }

        // Constructor này không sử dụng ILogger, nó có thể được loại bỏ
        //public TravelController(ILogger<TravelController> logger, SqlConnection connection)
        //{
        //    _logger = logger;
        //    _connection = connection;
        //}

        public List<BlogDTO> HandleGetData()
        {
            List<BlogDTO> blogs = new List<BlogDTO>();

            if (_connection != null)
            {
                _connection.Open();
                string query = "SELECT * FROM Blogs WHERE is_active = 1 and cate_id = 7"; // Assuming only active blogs should be displayed
                SqlCommand command = new SqlCommand(query, _connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : 0;
                    string title = reader["title"] != DBNull.Value ? reader["title"].ToString() : string.Empty;
                    string description = reader["description"] != DBNull.Value ? reader["description"].ToString() : string.Empty;
                    bool isActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"]);
                    string contentMarkdown = reader["content_MarkDown"] != DBNull.Value ? reader["content_MarkDown"].ToString() : string.Empty;
                    string contentHtml = reader["content_HTML"] != DBNull.Value ? reader["content_HTML"].ToString() : string.Empty;
                    int categoryId = reader["cate_id"] != DBNull.Value ? Convert.ToInt32(reader["cate_id"]) : 0;

                    var blog = new BlogDTO(id, title, description, isActive, contentMarkdown, contentHtml, categoryId);
                    blogs.Add(blog);
                }
                _connection.Close();
            }
            else
            {
                _logger.LogError("Database connection is null.");
            }

            return blogs;
        }

        public IActionResult BlogDuLich()
        {
            var blogs = HandleGetData();
            return View("~/Views/Travel/Travel.cshtml", blogs);
        }
    }
}
