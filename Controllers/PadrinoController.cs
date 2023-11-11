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
    
    public class PadrinoController : Controller
    {
        private readonly ILogger<PadrinoController> _logger;
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PadrinoController(ILogger<PadrinoController> logger,ApplicationDbContext context,UserManager<IdentityUser> userManager)
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
                var mascotasApadrinadas = await _context.PADRINAJE
                    .Where(p => p.CLIENTE.id == cliente.id)
                    .Select(p => p.MASCOTAS)
                    .Distinct()
                    .ToListAsync();

                return View(mascotasApadrinadas);
            }

            // Manejar el caso en el que el cliente no se encuentra
            return View("Error");
        }
        public async Task<IActionResult> Lista()
    {
        // Obtén la lista de PADRINAJE que deseas mostrar en la vista
        var listaPadrinos = _context.PADRINAJE.Include(p => p.CLIENTE).Include(p => p.MASCOTAS).ToList();
        
        // Pasa la lista como modelo a la vista
        return View(listaPadrinos);
    }


    }
        

}