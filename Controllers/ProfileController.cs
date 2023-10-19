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
    
    public class ProfileController : Controller
    {

        private readonly ILogger<ProfileController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

         public ProfileController(ILogger<ProfileController> logger,ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Obtiene el usuario actual
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirige al inicio de sesión si el usuario no está regitrado
            }
            // Busca el perfil del cliente asociado al usuario
             var userName = User.Identity?.Name;
            var cliente = _context.CLIENTE.FirstOrDefault(c => c.User.UserName == userName);

             if (cliente == null)
            {
                cliente = new CLIENTE(); // Crea un nuevo perfil si no existe
            }

       

            return View(cliente);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CLIENTE cliente)
        {
            // Obtiene el usuario actual
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirige al inicio de sesión si el usuario no está autenticado
            }

            // Asigna el usuario al perfil del cliente
            var userName = User.Identity?.Name;
            var cliente1 = _context.CLIENTE.FirstOrDefault(c => c.User.UserName == userName);

            // Si el cliente no existe, crea uno nuevo
            if (cliente1 == null)
            {
                cliente.User = user;
                _context.CLIENTE.Add(cliente);
            }
            else
            {
                // Actualiza los valores del cliente existente con los valores del cliente enviado
                cliente1.nombres = cliente.nombres;
                cliente1.apellidos = cliente.apellidos;
                cliente1.apodo = cliente.apodo;
                cliente1.domicilio = cliente.domicilio;
                cliente1.celular = cliente.celular;
                cliente1.dni = cliente.dni;
            }

            _context.SaveChanges();

            // Redirige de nuevo a la vista de perfil
            return RedirectToAction("Index");
        }
        




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}