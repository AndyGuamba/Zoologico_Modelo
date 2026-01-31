using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Zoologico.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuración: appsettings (opcional) + env vars (Render manda por aquí)
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            // Serilog usando la config real del builder
            builder.Host.UseSerilog((ctx, lc) =>
                lc.ReadFrom.Configuration(ctx.Configuration));

            // DB (Npgsql)
            builder.Services.AddDbContext<ZoologicoAPIContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("ZoologicoAPIContext")
                    ?? throw new InvalidOperationException("Connection string 'ZoologicoAPIContext' not found.")
                ));

            // API
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger siempre (para Render)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zoologico API v1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}