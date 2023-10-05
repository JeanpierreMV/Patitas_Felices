using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Patitas_Felices.Data;
using Patitas_Felices.Models;

namespace Patitas_Felices.Controllers
{
    public class MascotaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MascotaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
              return _context.MASCOTAS != null ? 
                          View(await _context.MASCOTAS.OrderBy(i =>i.id).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.MASCOTAS'  is null.");
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MASCOTAS == null)
            {
                return NotFound();
            }

            var mascotas = await _context.MASCOTAS
                .FirstOrDefaultAsync(m => m.id == id);
            if (mascotas == null)
            {
                return NotFound();
            }

            return View(mascotas);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,raza,genero,edad,tamaño,caracter,Imagen,tipo,Descripcion")] MASCOTAS mascotas)
        {
          
            if (ModelState.IsValid)
            {
                
                _context.Add(mascotas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mascotas);
        }


        // GET: Producto/Edit/5
        
        public async Task<IActionResult>Edit(int? id)
        {
            if (id == null || _context.MASCOTAS == null)
            {
                return NotFound();
            }

            var productos = await _context.MASCOTAS.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int id, [Bind("id,nombre,raza,genero,edad,tamaño,caracter,Imagen,tipo,Descripcion")] MASCOTAS mascotas)
        {
            if (id != mascotas.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mascotas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(mascotas.id))
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
            return View(mascotas);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MASCOTAS == null)
            {
                return NotFound();
            }

            var productos = await _context.MASCOTAS
                .FirstOrDefaultAsync(m => m.id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MASCOTAS == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MASCOTAS'  is null.");
            }
            var  mascotas = await _context.MASCOTAS.FindAsync(id);
            if (mascotas != null)
            {
                _context.MASCOTAS.Remove(mascotas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
          return (_context.MASCOTAS?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
