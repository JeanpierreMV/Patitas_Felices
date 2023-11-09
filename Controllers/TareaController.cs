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
            var tareas = await _context.TAREA.ToListAsync();
            return View(tareas);
        }
         public IActionResult Create()
        {
            return View();
        }

        // Acción para guardar una nueva tarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TAREA tarea)
        {

                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            


        }

        // Acción para mostrar el formulario de edición de tarea
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.TAREA.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // Acción para actualizar una tarea
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TAREA tarea)
        {
            if (id != tarea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(tarea);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.TAREA.FindAsync(id);
            _context.TAREA.Remove(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Función para verificar si una tarea existe
        private bool TareaExists(int id)
        {
            return _context.TAREA.Any(e => e.Id == id);
        }



       
  






    }
}