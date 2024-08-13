using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.AperturaCuenta;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositorio;
using ServiceManager;
using Presentacion.CuentaApertura.Extensions;
using Microsoft.Extensions.FileProviders;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;

namespace Presentacion.CuentaApertura
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext") ?? throw new InvalidOperationException("Connection string 'DbContext' not found.")));


            //imagens de la base de datos

            builder.WebHost.ConfigureKestrel(options =>
            {
                // Establece el tama�o m�ximo del cuerpo de solicitud (por ejemplo, 50 MB)
                options.Limits.MaxRequestBodySize = 50 * 1024 * 1024; // 50 MB en bytes
            });
            builder.Services.AddControllers();
            // Add services to the container.
            builder.Services.AddControllersWithViews();


            // Registra el servicio de datos dactilares
            
            // Registra el servicio de cookies
            builder.Services.AddHttpContextAccessor();
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureCookieService();
            builder.Services.AddDataProtection();
            builder.Services.ConfigurePdfService();
            builder.Services.ConfigureGuardarAuditoria();
            // notificaciones
            builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopCenter; });

            // Configura los servicios de autenticaci�n y autorizaci�n
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home"; // Ruta para la p�gina de inicio de sesi�n
                 
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(18); // Tiempo de expiraci�n de la cookie
                    options.SlidingExpiration = true; // Renueva la cookie autom�ticamente si el usuario est� activo
                });


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //cokkie
            app.UseAuthentication();

            app.MapControllers();
            //notificaiones
            app.UseNotyf();
            //
            app.UseStaticFiles(); // Esta l�nea sirve archivos est�ticos de wwwroot por defecto

            // Configura para servir archivos est�ticos desde lib/weights
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "weights")),
                RequestPath = "/lib/weights",
                ServeUnknownFileTypes = true // Permite servir archivos sin extensi�n
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
