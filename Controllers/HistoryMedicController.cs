using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;//agregado
using Patitas_Felices.Models;
using Patitas_Felices.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace appejemplo2.Controllers
{
    public class HistoryMedicController: Controller
    {
        private readonly ILogger<HistoryMedicController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HistoryMedicController(ApplicationDbContext context,ILogger<HistoryMedicController> logger,UserManager<IdentityUser> userManager){
            _context = context;
            _logger = logger;
            _userManager= userManager;
        } 


        public async Task<IActionResult> Index()
        {
            var historiasMedicas = _context.H_MEDICO.Include(h => h.MASCOTAS).ToList();

            historiasMedicas = historiasMedicas.OrderByDescending(h => ConvertToDate(h.Fecha_reg)).ToList();

            return View(historiasMedicas);
        }
        private DateTime ConvertToDate(string fecha)
        {
            
            if (fecha.Length >= 8)
            {
                string formattedDate = $"20{fecha.Substring(6, 2)}-{fecha.Substring(3, 2)}-{fecha.Substring(0, 2)}";
                if (DateTime.TryParse(formattedDate, out DateTime date))
                {
                    return date;
                }
            }
          
            return DateTime.MinValue;
        }


             public async Task<IActionResult> Details(int? id)
        {
           
                if (id == null)
                {
                    return NotFound();
                }
               
                H_MEDICO hMedico = await _context.H_MEDICO
                    .Include(h => h.MASCOTAS) 
                    .FirstOrDefaultAsync(h => h.id == id);

                if (hMedico == null)
                {
                    return NotFound();
                }

                   return View(hMedico);
        
        }

       





        // GET: HistoryMedic/Create
         public IActionResult Create()
        {
            // Obtén la lista de mascotas desde la base de datos
            var mascotas = _context.MASCOTAS.ToList();

            // Construye una lista de elementos SelectListItem para usar en el formulario
            ViewBag.Mascotas = new SelectList(mascotas, "id", "nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(H_MEDICO hMedico, IFormFile ImagenUp, IFormFile fileMedicinas, IFormFile fileTratamientos)
        {
            if (ImagenUp != null && ImagenUp.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    ImagenUp.CopyTo(ms);
                    hMedico.Adj_img = ms.ToArray();
                }

                if (fileMedicinas != null && fileMedicinas.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        fileMedicinas.CopyTo(ms);
                        hMedico.D_medicinas = ms.ToArray();
                    }
                }

                if (fileTratamientos != null && fileTratamientos.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        fileTratamientos.CopyTo(ms);
                        hMedico.D_tratamientos = ms.ToArray();
                    }
                }

                // Asegúrate de asignar una mascota válida a la historia médica.
                MASCOTAS mascotaSeleccionada = _context.MASCOTAS.FirstOrDefault(m => m.id == hMedico.MASCOTAS.id);

                if (mascotaSeleccionada != null)
                {
                    hMedico.MASCOTAS = mascotaSeleccionada;

                    // Aquí puedes agregar la lógica para guardar la historia médica en la base de datos
                    _context.H_MEDICO.Add(hMedico);
                    _context.SaveChanges();

                    return RedirectToAction("Index"); // Redirige a donde quieras después de guardar
                }
            }

            // Si el modelo no es válido o la mascota no se encontró, vuelve a cargar el formulario con los errores
            var mascotas = _context.MASCOTAS.ToList();
            ViewBag.Mascotas = new SelectList(mascotas, "id", "nombre");

            return View(hMedico);
        }
                    













        public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        H_MEDICO hMedico = await _context.H_MEDICO.FindAsync(id);

        if (hMedico == null)
        {
            return NotFound();
        }

        // Obtén la lista de mascotas desde la base de datos
        var mascotas = await _context.MASCOTAS.ToListAsync();

        // Construye una lista de elementos SelectListItem para usar en el formulario
        ViewBag.Mascotas = new SelectList(mascotas, "id", "nombre");

        return View(hMedico);
    }

    // POST: HMedico/Edit/5
// POST: HMedico/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, H_MEDICO hMedico, IFormFile ImagenUp, IFormFile fileMedicinas, IFormFile fileTratamientos)
{
    if (id != hMedico.id)
    {
        return NotFound();
    }

    try
    {
        var existingHMedico = await _context.H_MEDICO
            .AsNoTracking()
            .Include(h => h.MASCOTAS)
            .FirstOrDefaultAsync(h => h.id == id);

        if (existingHMedico != null)
        {
            // Actualiza los campos modificables
            existingHMedico.Tip_Sangre = hMedico.Tip_Sangre;
            existingHMedico.Fecha_reg = hMedico.Fecha_reg;
            existingHMedico.observaciones = hMedico.observaciones;

            // Actualiza la imagen si se proporciona una nueva
            if (ImagenUp != null && ImagenUp.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    ImagenUp.CopyTo(ms);
                    existingHMedico.Adj_img = ms.ToArray();
                }
            }
            else
            {
                // Evita la modificación si no se proporciona una nueva imagen
                _context.Entry(existingHMedico).Property("Adj_img").IsModified = false;
            }

            // Actualiza los archivos de medicinas si se proporciona uno nuevo
            if (fileMedicinas != null && fileMedicinas.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileMedicinas.CopyTo(ms);
                    existingHMedico.D_medicinas = ms.ToArray();
                }
            }
            else
            {
                // Evita la modificación si no se proporciona un nuevo archivo de medicinas
                _context.Entry(existingHMedico).Property("D_medicinas").IsModified = false;
            }

            // Actualiza los archivos de tratamientos si se proporciona uno nuevo
            if (fileTratamientos != null && fileTratamientos.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileTratamientos.CopyTo(ms);
                    existingHMedico.D_tratamientos = ms.ToArray();
                }
            }
            else
            {
                // Evita la modificación si no se proporciona un nuevo archivo de tratamientos
                _context.Entry(existingHMedico).Property("D_tratamientos").IsModified = false;
            }

           

            _context.Update(existingHMedico);
            await _context.SaveChangesAsync();
        }
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!H_MEDICOExists(hMedico.id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }
    return RedirectToAction("Index"); // Redirige a donde quieras después de guardar
}

    private bool H_MEDICOExists(int id)
    {
        return _context.H_MEDICO.Any(e => e.id == id);
    }






        











        
        // GET: HistoryMedic/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.H_MEDICO == null)
            {
                return NotFound();
            }

            var historyMedic = await _context.H_MEDICO
                .FirstOrDefaultAsync(m => m.id == id);
            if (historyMedic == null)
            {
                return NotFound();
            }

            return View(historyMedic);
        }

        // POST: HistoryMedic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.H_MEDICO == null)
            {
                return Problem("Entity set 'ApplicationDbContext.H_MEDICO'  is null.");
            }

            var historyMedic = await _context.H_MEDICO.FindAsync(id);
            if (historyMedic != null)
            {
                _context.H_MEDICO.Remove(historyMedic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool historyMedicExists(int id)
        {
          return (_context.H_MEDICO?.Any(e => e.id == id)).GetValueOrDefault();
        }







    


        // private List<MASCOTAS> ObtenerListaDeMascotas()
        // {
        //     // Utiliza Entity Framework para obtener la lista de mascotas desde la base de datos
        //     return _context.MASCOTAS.ToList();
        // }






    }
   

}