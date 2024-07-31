using Interface.AperturaCuenta;
using Repositorio;
using Services.AperturaCuenta;

namespace ServiceManager
{
    public class ServiceManager:IServiceManager
    {
        //private readonly Lazy<ICookieService> _cookieService;

        //public ServiceManager( IRepositorioManager repositorioManager)
        //{
        //    _cookieService = new Lazy<ICookieService>(()=> new CookieService(repositorioManager));

        //}

        //public ICookieService CookieService => _cookieService.Value;


        private readonly ICookieService _cookieService;

        public ServiceManager(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public ICookieService CookieService => _cookieService;
    }
}
