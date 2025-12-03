using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zoologico_Modelo;

    public class ZoologicoAPIContext : DbContext
    {
        public ZoologicoAPIContext (DbContextOptions<ZoologicoAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Zoologico_Modelo.Especie> Especie { get; set; } = default!;

public DbSet<Zoologico_Modelo.Raza> Raza { get; set; } = default!;

public DbSet<Zoologico_Modelo.Animal> Animal { get; set; } = default!;
    }
