using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Controllers.ControllersVistas
{
    public class Resumen_finalController : Controller
    {
        private readonly ICookieService _cookieService;
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        public Resumen_finalController(ICookieService cookieService, IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            _cookieService = cookieService;
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
        }
        [Authorize]
        public ActionResult Index()
        {
            var datos = _serviceManager.ObtenerDatosCombinados();
            return View(datos);
        }

    }
}
