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
            var mascota = from o in _context.MASCOTAS select o;

            if (!String.IsNullOrEmpty(searchString)){
                searchString = searchString.ToLower();
                mascota = mascota.Where(s => s.nombre.ToLower().Contains(searchString));
            }

            return View(await mascota.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id){
            MASCOTAS objMascot = await _context.MASCOTAS.FindAsync(id);
            if(objMascot == null){
                return NotFound();
            }
            return View(objMascot);
        }

        
        
        public async Task<IActionResult>Adopcion(int? id){
            
                if (id == null)
            {
                return NotFound();
            }

            // Crea una nueva instancia de ADOPCION y establece el ID de la mascota.
            ADOPCION adopcion = new ADOPCION
            {
                MASCOTAS = await _context.MASCOTAS.FindAsync(id)
            };

            if (adopcion.MASCOTAS == null)
            {
                return NotFound();
            }

            return View(adopcion);
        }


      
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Adopcion(ADOPCION adopcion, IFormFile ImagenDNI, IFormFile fileAgua, IFormFile fileLuz, IFormFile AntPenles, int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var mascota = await _context.MASCOTAS.FindAsync(id);
    var cliente = _context.CLIENTE.Include(c => c.User).FirstOrDefault(c => c.User.UserName == _userManager.GetUserName(User));

    if (mascota == null)
    {
        return NotFound();
    }

    adopcion.CLIENTE = cliente;
    adopcion.MASCOTAS = mascota;
    adopcion.Estado = "pendiente";
    
    if (ImagenDNI != null && ImagenDNI.Length > 0)
    {
        using (var memoryStream = new MemoryStream())
        {
            ImagenDNI.CopyTo(memoryStream);
            adopcion.Fot_DNI = memoryStream.ToArray();
        }
    }
    
    if (fileLuz != null && fileLuz.Length > 0)
    {
        using (var memoryStream = new MemoryStream())
        {
            fileLuz.CopyTo(memoryStream);
            adopcion.R_luz = memoryStream.ToArray();
        }
    }
    
    if (fileAgua != null && fileAgua.Length > 0)
    {
        using (var memoryStream = new MemoryStream())
        {
            fileAgua.CopyTo(memoryStream);
            adopcion.R_agua = memoryStream.ToArray();
        }
    }
    
    if (AntPenles != null && AntPenles.Length > 0)
    {
        using (var memoryStream = new MemoryStream())
        {
            AntPenles.CopyTo(memoryStream);
            adopcion.Ant_Penales = memoryStream.ToArray();
        }
    }
  

    _context.Add(adopcion);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}

                 

    }
}