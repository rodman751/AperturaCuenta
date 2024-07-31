using Repositorio;
using ServiceManager;
using Services.AperturaCuenta;
using Interface.AperturaCuenta;

namespace Presentacion.CuentaApertura.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioManager, RepositorioManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager.ServiceManager>();
        }

        public static void ConfigureCookieService(this IServiceCollection services)
        {
            services.AddScoped<ICookieService, CookieService>();
        }
    }
}
