using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Patitas_Felices.Models;

namespace Patitas_Felices.Controllers;

public class PagoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public PagoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
