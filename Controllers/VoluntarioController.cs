using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Patitas_Felices.Data;
using Patitas_Felices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Patitas_Felices.Controllers
{
    public class VoluntarioController : Controller
    {
        private readonly ILogger<VoluntarioController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

         public VoluntarioController(ILogger<VoluntarioController> logger,ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name;
            var cliente = await _context.CLIENTE.FirstOrDefaultAsync(c => c.User.UserName == userName);

            if (cliente != null)
            {
                var esVoluntario = await _context.VOLUNTARIO.AnyAsync(v => v.CLIENTE.id == cliente.id);

                if (!esVoluntario)
                {
                    return View(); // Muestra la vista de registro
                }
                else
                {
                    // El cliente ya es voluntario, mostrar un mensaje
                    TempData["Message"] = "Ya eres un voluntario registrado.";
                }
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(VOLUNTARIO voluntario)
        {
        
                var userName = User.Identity.Name;
                var cliente = await _context.CLIENTE.FirstOrDefaultAsync(c => c.User.UserName == userName);

                if (cliente != null)
                {
                    var esVoluntario = await _context.VOLUNTARIO.AnyAsync(v => v.CLIENTE.id == cliente.id);

                    if (!esVoluntario)
                    {
                        // Asegurarse de que la fecha sea UTC
                        voluntario.FechaYHora = DateTime.UtcNow;
                        voluntario.CLIENTE = cliente;
                        voluntario.TareasRealizadas = new List<TAREA>();

                        _context.VOLUNTARIO.Add(voluntario);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // El cliente ya es voluntario, mostrar un mensaje
                        TempData["Message"] = "Ya eres un voluntario registrado.";
                    }
                }
        

            return View(voluntario); // Volver a mostrar la vista de registro con errores de validación
        }

        public async Task<IActionResult> Tareas()
        {          
            

            return View();
        }




    }
}