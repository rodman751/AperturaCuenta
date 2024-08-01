using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Controllers.ControllersVistas
{
    public class DireccionController : Controller
    {
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        public DireccionController(IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
        }
        public ActionResult Index()
        {
            var usuario = _serviceManager.CookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie");
            if (usuario != null)
            {
                ViewBag.Usuario = usuario.Nombre;
                ViewBag.Apellido = usuario.Apellido;
            }
            var datosStep1 = _serviceManager.CookieService.ObtenerDatosCookie<Entidades.DireccionMapa>("DireccionMCokkie");

            // Si hay datos guardados, inicializar el modelo con ellos
            var model = datosStep1 ?? new Entidades.DireccionMapa();
            return View(model);
            
        }

    }
}
