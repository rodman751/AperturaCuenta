using Entidades.CuentaApertura;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interface.AperturaCuenta;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Controllers
{
    public class ConfirmarController : Controller
    {
        private readonly ICookieService _cookieService;
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        public ConfirmarController(ICookieService cookieService ,IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            _cookieService = cookieService;
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
        }


        [HttpGet]
        public ActionResult Index()
        {
            // Obtener todos los datos de las cookies para confirmar
            //var DatosDactilares = _cookieService.ObtenerDatosCookie<Entidades.DatosDactilares>("DatosDactilaresCookie") ?? new Entidades.DatosDactilares();
            //var UsuarioCookie = _cookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie") ?? new Usuario();
            //var DireccionMCokkie = _cookieService.ObtenerDatosCookie<DireccionMapa>("DireccionMCokkie") ?? new DireccionMapa();



             var datos =_serviceManager.ObtenerDatosCombinados();
            // Combinarlos en un objeto para la vista de confirmación
            //var combinedData = new CombinedData
            //{
                
            //    DatosDactilares = DatosDactilares,
            //    Usuario = UsuarioCookie,
            //    DireccionMapa = DireccionMCokkie
            //};

            return View(datos);

        }


    }
}
