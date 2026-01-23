using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Zoologico.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //==============================================================
            // Configurar Serilog leyendo desde appsettings.json
            //==============================================================
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            Log.Information("Iniciado el proceso de LOGGER");

            // ... código anterior de Serilog

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ZoologicoAPIContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ZoologicoAPIContext") ?? throw new InvalidOperationException("Connection string 'ZoologicoAPIContext' not found.")));

            // MODIFICACIÓN AQUÍ:
            builder.Services.AddControllersWithViews(); // Cambiado para soportar Vistas
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ... configuraciones de Swagger

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // IMPORTANTE: Agrega esto para cargar CSS y JS en tus vistas
            app.UseAuthorization();

            app.MapControllers();

            // MODIFICACIÓN AQUÍ:
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
