using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Patitas_Felices.Models;

namespace Patitas_Felices.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<MASCOTAS>? PRODUCTs{get; set;}
    public DbSet<CLIENTE>? CLIENTE{get; set;}
}

