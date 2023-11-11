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
    public class TareaController : Controller
    {
        private readonly ILogger<TareaController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

         public TareaController(ILogger<TareaController> logger,ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

         public async Task<IActionResult> Index()
        {
 var userName = User.Identity.Name;
    var cliente = await _context.CLIENTE.FirstOrDefaultAsync(c => c.User.UserName == userName);

    if (cliente == null)
    {
        return NotFound(); // Manejar el caso en que el cliente no exista
    }

    var voluntario = await _context.VOLUNTARIO
        .Include(v => v.TareasRealizadas)
        .FirstOrDefaultAsync(v => v.CLIENTE.id == cliente.id);

    if (voluntario == null)
    {
        return NotFound(); // Manejar el caso en que el voluntario no exista
    }

    var tareasSeleccionadas = voluntario.TareasRealizadas.ToList();

    return View(tareasSeleccionadas);
        }


public async Task<IActionResult> Create()
{
    var userName = User.Identity.Name;
    var cliente = await _context.CLIENTE.FirstOrDefaultAsync(c => c.User.UserName == userName);

    if (cliente == null)
    {
        return NotFound(); // Manejar el caso en que el cliente no exista
    }

    var voluntario = await _context.VOLUNTARIO
        .Include(v => v.TareasRealizadas)
        .FirstOrDefaultAsync(v => v.CLIENTE.id == cliente.id);

    if (voluntario == null)
    {
        return NotFound(); // Manejar el caso en que el voluntario no exista
    }

    var tareasDisponibles = await _context.TAREA.ToListAsync();

    ViewBag.Voluntario = voluntario;
    return View(tareasDisponibles);
}

[HttpPost]
public async Task<IActionResult> Create(int[] tareasSeleccionadas)
{
    var userName = User.Identity.Name;
    var cliente = await _context.CLIENTE.FirstOrDefaultAsync(c => c.User.UserName == userName);

    if (cliente == null)
    {
        return NotFound(); // Manejar el caso en que el cliente no exista
    }

    var voluntario = await _context.VOLUNTARIO
        .Include(v => v.TareasRealizadas)
        .FirstOrDefaultAsync(v => v.CLIENTE.id == cliente.id);

    if (voluntario == null)
    {
        return NotFound(); // Manejar el caso en que el voluntario no exista
    }

    var tareas = await _context.TAREA.Where(t => tareasSeleccionadas.Contains(t.Id)).ToListAsync();

    voluntario.TareasRealizadas = tareas;

    await _context.SaveChangesAsync();

    return RedirectToAction("Index", "Home");
}


        public async Task<IActionResult> Edit(int? id)
        {
            var tarea = await _context.TAREA.FindAsync(id);

            if (tarea == null)
            {
                return NotFound(); // Manejar el caso en que la tarea no exista
            }

            // Cambiar el estado de completado de la tarea
            tarea.Completada = !tarea.Completada;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redi
        }

      

        // Acción para mostrar detalles de una tarea
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.TAREA
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // Acción para eliminar una tarea
        public async Task<IActionResult> Delete(int? id)
        {
            var tarea = await _context.TAREA.FindAsync(id);

                if (tarea == null)
                {
                    return NotFound(); // Manejar el caso en que la tarea no exista
                }

                return View(tarea);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
                var tarea = await _context.TAREA.FindAsync(id);

                

                    _context.TAREA.Remove(tarea);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Tarea");
        }

        // Función para verificar si una tarea existe
        private bool TareaExists(int id)
        {
            return _context.TAREA.Any(e => e.Id == id);
        }


    }
}