using Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Controllers.ControllersVistas
{
    public class Datos_adicionalesController : Controller
    {
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        public Datos_adicionalesController(IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
        }
        [Authorize]
        public ActionResult Index()
        {
            var usuario = _serviceManager.CookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie");
            if (usuario != null)
            {
                ViewBag.Usuario = usuario.Nombre;
                ViewBag.Apellido = usuario.Apellido;
            }
            var datosStep1 = _serviceManager.CookieService.ObtenerDatosCookie<Modelos.InformacionPersonal>("GuardarDatos_Adicionales");

            // Si hay datos guardados, inicializar el modelo con ellos
            var model = datosStep1 ?? new Modelos.InformacionPersonal();
            return View(model);
        }

    }
}
