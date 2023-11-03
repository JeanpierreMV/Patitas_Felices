using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Patitas_Felices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Patitas_Felices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Patitas_Felices.Controllers;

public class DonacionesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    
    public DonacionesController(ApplicationDbContext context,ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult VozAnimal()
    {
        return View();
    }
     public IActionResult Suministros()
    {
        return View();
    }
}

