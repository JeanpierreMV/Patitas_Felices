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
namespace Patitas_Felices.Controllers
{
  
    public class AdopcionController : Controller
    {
        private readonly ILogger<AdopcionController> _logger;
        private readonly ApplicationDbContext _context;

        public AdopcionController(ILogger<AdopcionController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context =context;
        }
 
        public IActionResult Index()
        {

                var adopciones = _context.ADOPCION
                    .Include(a => a.MASCOTAS)
                    .Include(a => a.CLIENTE)
                    .ToList();

            return View(adopciones);
        }






        public async Task<IActionResult> Details(int? id){
            if (id == null)
                {
                    return NotFound();
                }

                var adopcion = await _context.ADOPCION
                    .Include(h => h.MASCOTAS)
                    .Include(a => a.CLIENTE)
                    .FirstOrDefaultAsync(h => h.id == id);

                if (adopcion == null)
                {
                    return NotFound();
                }

                return View(adopcion);
        }


    }
}