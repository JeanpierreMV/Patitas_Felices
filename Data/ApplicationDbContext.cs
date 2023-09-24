﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Patitas_Felices.Models;

namespace Patitas_Felices.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<MASCOTAS>? MASCOTAS{get; set;}
    public DbSet<CLIENTE>? CLIENTE{get; set;}
    public DbSet<H_MEDICO>? H_MEDICO{get; set;}
    public DbSet<ADOPCION>? ADOPCION{get; set;}

}

