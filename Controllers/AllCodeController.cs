using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace webASP.Controllers;

public class ALLCodeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SqlConnection? connection;

    public ALLCodeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        ConnectionService cnService = new ConnectionService();
        this.connection = cnService.cn;
    }
    [HttpGet]
    public List<AllCodeDTO> HandleGetData()
    {
        List<AllCodeDTO> allcode = new List<AllCodeDTO>();
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "select * from [ALL-code] ";
            SqlCommand command = new SqlCommand(query, this.connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int idAllcode = Convert.ToInt32(reader["Id"]);
                string? type = reader["type"].ToString();
                string? content_title = reader["content_title"].ToString();
                string? content_detail = reader["content_detail"].ToString();
                string? code = reader["code"].ToString();

                allcode.Add(new AllCodeDTO(idAllcode, type, content_title, content_detail, code));
            }
            this.connection.Close();
        }

        return allcode;
    }
    [HttpPost]
    public IActionResult HandleData(string type, string content_title, string content_detail, string code)
    {
        this.connection.Open();
        string query = "INSERT INTO [All-code] (type, content_title, content_detail, code) VALUES (@Type, @ContentTitle, @ContentDetail, @Code)";
        SqlCommand command = new SqlCommand(query, this.connection);
        command.Parameters.Add(new SqlParameter("@Type", type));
        command.Parameters.Add(new SqlParameter("@ContentTitle", content_title));
        command.Parameters.Add(new SqlParameter("@ContentDetail", content_detail));
        command.Parameters.Add(new SqlParameter("@Code", code));

        command.ExecuteNonQuery();
        return RedirectToAction("Index", "AllCode");
    }
    [HttpPost]
    public IActionResult UpdateData(int id, string type, string content_title, string content_detail, string code)
    {
        this.connection.Open();
        string query = "UPDATE [All-code] SET type = @Type, content_title = @ContentTitle, content_detail = @ContentDetail, code = @Code WHERE id = @Id";
        SqlCommand command = new SqlCommand(query, this.connection);
        command.Parameters.Add(new SqlParameter("@Type", type));
        command.Parameters.Add(new SqlParameter("@ContentTitle", content_title));
        command.Parameters.Add(new SqlParameter("@ContentDetail", content_detail));
        command.Parameters.Add(new SqlParameter("@Code", code));
        command.Parameters.Add(new SqlParameter("@Id", id));
        Console.WriteLine(query);

        command.ExecuteNonQuery();
        return RedirectToAction("Index", "AllCode");
    }
    [HttpPost]
    public IActionResult DeleteData(int id)
    {
        this.connection.Open();
        string query = "delete [All-code] where id = " + id;
        SqlCommand command = new SqlCommand(query, this.connection);

        command.ExecuteNonQuery();
        return RedirectToAction("Index", "AllCode");
    }
    public IActionResult Index()
    {
        List<AllCodeDTO> allcode = new List<AllCodeDTO>();
        if (this.connection != null)
        {
            this.connection.Open();
            string query = "select * from [ALL-code] ";
            SqlCommand command = new SqlCommand(query, this.connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int idAllcode = Convert.ToInt32(reader["Id"]);
                string? type = reader["type"].ToString();
                string? content_title = reader["content_title"].ToString();
                string? content_detail = reader["content_detail"].ToString();
                string? code = reader["code"].ToString();

                allcode.Add(new AllCodeDTO(idAllcode, type, content_title, content_detail, code));
            }
            this.connection.Close();
        }
        ViewBag.Data = allcode;
        return View("~/Views/System/ControlAllCode.cshtml");
    }
}
