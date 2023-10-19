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











        public IActionResult Create()
        {
            return View();
        }

      
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MASCOTAS mascota)
        {
            // if (ModelState.IsValid) {
                if (mascota.ImagenUpload != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await mascota.ImagenUpload.CopyToAsync(memoryStream);
                        mascota.Imagen = memoryStream.ToArray();
                    }
                }

                _context.Add(mascota);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            // }else{
            //      // Mostrar un mensaje de error
            //     ViewBag.ErrorMessage = "Ha ocurrido un error. Por favor, verifica los datos ingresados.";
            //     return View(mascota);

            // }

           
        }


        












        
        public async Task<IActionResult>Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mascotas = await _context.MASCOTAS.FindAsync(id);
            if (mascotas == null)
            {
                return NotFound();
            }
            return View(mascotas);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MASCOTAS mascota, IFormFile ImagenUpload)
        {
            if (id != mascota.id)
            {
                return NotFound();
            }


                try
                {
                    if (ImagenUpload != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await ImagenUpload.CopyToAsync(memoryStream);
                            mascota.Imagen = memoryStream.ToArray();
                        }
                    }  else   {

                        var mascotaOriginal = await _context.MASCOTAS.AsNoTracking().FirstOrDefaultAsync(m => m.id == id);
                        mascota.Imagen = mascotaOriginal.Imagen;
                    }
                    _context.Entry(mascota).Property("Imagen").IsModified = false;

                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MascotaExists(mascota.id))
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

        private bool MascotaExists(int id)
        {
            return _context.MASCOTAS.Any(e => e.id == id);
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
