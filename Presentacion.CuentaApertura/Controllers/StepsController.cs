using Entidades.CuentaApertura;
using Entidades;
using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.AperturaCuenta;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Controllers
{
    public class StepsController : Controller
    {
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;

        public StepsController(IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
        }

        [HttpPost]
        public IActionResult GuardarUsuCookie(Usuario model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("UsuarioCookie", model);
                _serviceManager.CookieService.GuardarPasoActual(2);
                //_cookieService.GuardarDatosCookie("UsuarioCookie", model);
                //_cookieService.GuardarPasoActual(2);

                return RedirectToAction("Confirm");
            }

            return View(model);
        }



        [HttpPost]
        public IActionResult GuardarDireccionMCookie(DireccionMapa model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("DireccionMCokkie", model);
                //_cookieService.GuardarDatosCookie("DireccionMCokkie", model);

                return RedirectToAction("Confirm");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Confirm()
        {

            //var DatosDactilares = _cookieService.ObtenerDatosCookie<Entidades.DatosDactilares>("DatosDactilaresCookie");
            //var UsuarioCookie = _cookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie");
            //var DireccionMCokkie = _cookieService.ObtenerDatosCookie<DireccionMapa>("DireccionMCokkie");

            //var combinedData = new CombinedData
            //{
            //    DatosDactilares = DatosDactilares,
            //    Usuario = UsuarioCookie,
            //    DireccionMapa = DireccionMCokkie

            //};
            var datos =_serviceManager.ObtenerDatosCombinados();

            // Aquí puedes combinarlos en un objeto para la vista de confirmación o enviar al endpoint




            //
            _serviceManager.CookieService.GuardarPasoActual(3);
            return RedirectToAction("Index", "Confirmar", datos);
        }


        public IActionResult FinalizarApertura()
        {
            //_cookieService.EliminarCookie("PasoActualCookie");
            //_cookieService.EliminarCookie("DatosDactilaresCookie");
            //_cookieService.EliminarCookie("UsuarioCookie");
            //_cookieService.EliminarCookie("DireccionMCokkie");

            _serviceManager.borrarCookie();
            //_cookieService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
