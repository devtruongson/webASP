using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using webASP.dto;

namespace webASP.Controllers;

public class ExportController : Controller
{
    private readonly ILogger<BlogController> _logger;
    private readonly SqlConnection? _connection;

    public ExportController(ILogger<BlogController> logger)
    {
        _logger = logger;
        ConnectionService cnService = new ConnectionService();
        _connection = cnService.cn;
    }
    public IActionResult Index()
    {
        return View("~/Views/exports/Index.cshtml");
    }
}
