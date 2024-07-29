using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.AperturaCuenta;

namespace Presentacion.CuentaApertura
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddControllersWithViews();

            // Registra el servicio de datos dactilares
            builder.Services.AddScoped<IDatosDactilaresService, DatosDactilaresService>();
            // Registra el servicio de cookies
            builder.Services.AddHttpContextAccessor(); // Necesario para IHttpContextAccessor
            builder.Services.AddScoped<ICookieService, CookieService>();

            //servico de cokkie
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(config =>
             {
                 config.Cookie.Name = "AperturaCuenta";
                 config.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                 config.LoginPath = "/DatosDactilares";
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
            app.UseAuthorization();

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
