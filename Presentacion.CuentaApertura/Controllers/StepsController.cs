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
                ViewBag.Usuario = model.Nombre;
                ViewBag.Apellido = model.Apellido;
                return RedirectToAction("Index","Direccion");
            }

            return View(model);
        }



        [HttpPost]
        public IActionResult GuardarDireccionMCookie(DireccionMapa model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("DireccionMCokkie", model);
                _serviceManager.CookieService.GuardarPasoActual(3);
               
                return RedirectToAction("Index","Datos_Adicionales");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult GuardarDatos_Adicionales(Entidades.InformacionPersonal model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("GuardarDatos_Adicionales", model);
                _serviceManager.CookieService.GuardarPasoActual(4);
                var usuarioCookie = _serviceManager.CookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie");
                ViewBag.Usuario = usuarioCookie.Nombre;
                ViewBag.Apellido = usuarioCookie.Apellido;

                return RedirectToAction("Index","Face_scan");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult GuardarFaceScan(Models.FaceScan model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("FaceScanCookie", model);
                _serviceManager.CookieService.GuardarPasoActual(5);
                var usuarioCookie = _serviceManager.CookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie");
                ViewBag.Usuario = usuarioCookie.Nombre;
                ViewBag.Apellido = usuarioCookie.Apellido;

                return RedirectToAction("Index", "OTP");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult GuardarOTP(Entidades.OTP model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("OTPCookie", model);
                _serviceManager.CookieService.GuardarPasoActual(6);
               

                return RedirectToAction("Index", "Resumen_Final");
            }

            return View(model);
        }

        //[HttpGet]
        //public IActionResult Resumen_Final()
        //{

            
        //    _serviceManager.CookieService.GuardarPasoActual(9);
        //    return RedirectToAction("Index", "Confirmar");
        //}


        public IActionResult FinalizarApertura()
        {

            _serviceManager.borrarCookie();
            
            return RedirectToAction("Index", "Home");
        }
    }
}
