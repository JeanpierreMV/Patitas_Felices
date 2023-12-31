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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Patitas_Felices.Controllers
{
    public class DonacionesController : Controller
    {
        private readonly ILogger<DonacionesController> _logger;
        private readonly ApplicationDbContext _context;
        
        public DonacionesController(ApplicationDbContext context,ILogger<DonacionesController> logger)
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

        public async Task<IActionResult> Donaciones(){
             var tareas = await _context.DONACIONES.ToListAsync();
            return View(tareas);
        }
        
        /* desde aqui */
        [HttpPost]
        public async Task<IActionResult> RealizarPago(DONACIONES donacion)
        {
            var userName = User.Identity.Name;
            var cliente = await _context.CLIENTE.FirstOrDefaultAsync(c => c.User.UserName == userName);

         
                //Guarda el cliente que hizo la donacion
                donacion.CLIENTE = cliente;

                // Configura la fecha y hora actual
                donacion.FechaHora = DateTime.UtcNow;

                // Guarda la donación en la base de datos
                _context.DONACIONES.Add(donacion);
                _context.SaveChanges();

                // Redirige a la acción 'Donaciones' con un mensaje de éxito
                TempData["MensajePago"] = "¡Gracias por tu donación!";
                return RedirectToAction("Donaciones", "Donaciones");
            

      
        
        }

        public async Task<IActionResult> listaDonaciones() {


          var tareas = await _context.DONACIONES.ToListAsync();
            return View(tareas);

        }

    }
}
