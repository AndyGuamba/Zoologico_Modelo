using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoologico_Modelo;


namespace Zoologico.API
{
    public class ZoologicoAPIContext : DbContext
    {
        public ZoologicoAPIContext(DbContextOptions<ZoologicoAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Zoologico_Modelo.Especie> Especies { get; set; } = default!;
        public DbSet<Zoologico_Modelo.Raza> Razas { get; set; } = default!;
        public DbSet<Zoologico_Modelo.Animal> Animales { get; set; } = default!;
    }

    // ESTA CLASE ES LA QUE SOLUCIONA EL ERROR DE SCAFFOLDING
    public class ZoologicoAPIContextFactory : IDesignTimeDbContextFactory<ZoologicoAPIContext>
    {
        public ZoologicoAPIContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ZoologicoAPIContext>();

            // Usamos la misma cadena de conexión de tu JSON
            optionsBuilder.UseNpgsql("Host=dpg-d4nqpbshg0os739jstv0-a.oregon-postgres.render.com;Port=5432;Database=zoologicoapi;Username=admin;Password=FdcsLQmerRgylwouTaHcf86MgASGfeo5;Ssl Mode=Require;Trust Server Certificate=true;");

            return new ZoologicoAPIContext(optionsBuilder.Options);
        }
    }
}