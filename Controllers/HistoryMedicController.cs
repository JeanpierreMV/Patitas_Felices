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
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                IQueryable<H_MEDICO> historyMedicQuery = _context.H_MEDICO;

                if (!String.IsNullOrEmpty(searchString))
                {
                    historyMedicQuery = historyMedicQuery.Where(s => s.observaciones.Contains(searchString));
                }

                var historyMedic = await historyMedicQuery.ToListAsync();
                return View(historyMedic);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción aquí, como registrarla para su posterior revisión.
                // Log.Error(ex, "Ocurrió un error al buscar historias médicas.");
                return RedirectToAction("Error", "Home"); // Redirige a una página de error genérica.
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var objHistoryMedic = await _context.H_MEDICO.FindAsync(id);
                if (objHistoryMedic == null)
                {
                    return NotFound();
                }

                return View(objHistoryMedic);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción aquí, como registrarla para su posterior revisión.
                // Log.Error(ex, "Ocurrió un error al obtener los detalles de la historia médica.");
                return RedirectToAction("Error", "Home"); // Redirige a una página de error genérica.
            }
        }


        private List<MASCOTAS> ObtenerListaDeMascotas()
        {
            // Utiliza Entity Framework para obtener la lista de mascotas desde la base de datos
            return _context.MASCOTAS.ToList();
        }

        // GET: HistoryMedic/Create
        public IActionResult Create()
        {
            var viewModel = new MascotaMedicoViewModel();
            viewModel.Medico = new H_MEDICO();
            viewModel.Mascotas = ObtenerListaDeMascotas();
            return View(viewModel);
        }

        // POST: HistoryMedic/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MascotaMedicoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var historyMedic = viewModel.Medico;
                historyMedic.MASCOTAS = await _context.MASCOTAS.FindAsync(viewModel.Medico.MASCOTAS.id);
                
                if (historyMedic.MASCOTAS == null)
                {
                    ModelState.AddModelError(nameof(viewModel.Medico.MASCOTAS.id), "Mascota no válida.");
                    viewModel.Mascotas = ObtenerListaDeMascotas();
                    return View(viewModel);
                }

                _context.Add(historyMedic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Mascotas = ObtenerListaDeMascotas();
            return View(viewModel);
        }

        
        // GET: HistoryMedic/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.H_MEDICO == null)
            {
                return NotFound();
            }

            var historyMedic = await _context.H_MEDICO.FindAsync(id);
            if (historyMedic == null)
            {
                return NotFound();
            }
            return View(historyMedic);
        }

        // POST: HistoryMedic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Tip_Sangre,Fecha_reg,Adj_img,D_medicinas,D_tratamientos,observaciones")] H_MEDICO historyMedic)
        {
            if (id != historyMedic.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historyMedic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!historyMedicExists(historyMedic.id))
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
            return View(historyMedic);
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

    }
}