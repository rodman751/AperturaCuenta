using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Controllers.ControllersVistas
{
    public class OTPController : Controller
    {
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        public INotyfService _notifyService { get; }

        public OTPController(IRepositorioManager repositoryManager, IServiceManager serviceManager, INotyfService notifyService)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
            _notifyService = notifyService;
        }
        [Authorize]
        public ActionResult Index()
        {
            var datosStep1 = _serviceManager.CookieService.ObtenerDatosCookie<Modelos.OTP>("OTPCookie");

            // Si hay datos guardados, inicializar el modelo con ellos
           
            var model = datosStep1 ?? new Modelos.OTP();
            return View(model);
        }

    }
}
