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
    public class ClientesListController: Controller
    {
        private readonly ILogger<ClientesListController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ClientesListController(ApplicationDbContext context,ILogger<ClientesListController> logger,UserManager<IdentityUser> userManager){
            _context = context;
            _logger = logger;
            _userManager= userManager;
        } 
        public async Task<IActionResult> Index(string? searchString){

            var clientes = from o in _context.CLIENTE select o;

            if(!String.IsNullOrEmpty(searchString)){
                clientes =clientes.Where( s => s.nombres.Contains(searchString));
            }
            return View(await clientes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id){
            CLIENTE objCliente = await _context.CLIENTE.FindAsync(id);
            if(objCliente == null){
                return NotFound();
            }
            return View(objCliente);
        }

    }
}