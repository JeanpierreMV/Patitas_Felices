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
    public class MascotasController: Controller
    {
        private readonly ILogger<MascotasController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MascotasController(ApplicationDbContext context,ILogger<MascotasController> logger,UserManager<IdentityUser> userManager){
            _context = context;
            _logger = logger;
            _userManager= userManager;
        } 
        public async Task<IActionResult> Index(string? searchString){

            var producto = from o in _context.MASCOTAS select o;

            if(!String.IsNullOrEmpty(searchString)){

                producto =producto.Where( s => s.nombre.Contains(searchString));
            }

            return View(await producto.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id){
            MASCOTAS objMascot = await _context.MASCOTAS.FindAsync(id);
            if(objMascot == null){
                return NotFound();
            }
            return View(objMascot);
        }

    }
}