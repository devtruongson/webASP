using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using webASP.DTO;

namespace webASP.Controllers
{
    public class InfoAdminController : Controller
    {
        private readonly ILogger<RentedBikeController> _logger;
        private readonly SqlConnection? connection;

        public InfoAdminController(ILogger<RentedBikeController> logger)
        {
            _logger = logger;
            ConnectionService cnService = new ConnectionService();
            this.connection = cnService.cn;
        }

        public IActionResult Index()
        {

            return View("~/Views/System/InfoAdmin.cshtml");
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

            // InfoAdmin
            // return View("~/Views/System/Dashboard.cshtml");
            return RedirectToAction("Dashboard", "System", new { isChanged = true, password = password });
        }




    }
}
