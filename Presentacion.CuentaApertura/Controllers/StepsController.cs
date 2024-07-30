using Entidades.CuentaApertura;
using Entidades;
using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.AperturaCuenta;

namespace Presentacion.CuentaApertura.Controllers
{
    public class StepsController : Controller
    {
        private readonly ICookieService _cookieService;

        public StepsController(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        [HttpPost]
        public IActionResult GuardarUsuCookie(Usuario model)
        {
            if (ModelState.IsValid)
            {
                
                _cookieService.GuardarDatosCookie("UsuarioCookie", model);
                
                return RedirectToAction("Confirm");
            }

            return View(model);
        }



        [HttpPost]
        public IActionResult GuardarDireccionMCookie(DireccionMapa model)
        {
            if (ModelState.IsValid)
            {

                _cookieService.GuardarDatosCookie("DireccionMCokkie", model);

                return RedirectToAction("Confirm");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Confirm()
        {

            var DatosDactilares = _cookieService.ObtenerDatosCookie<Entidades.DatosDactilares>("DatosDactilaresCookie");
            var UsuarioCookie = _cookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie");
            var DireccionMCokkie = _cookieService.ObtenerDatosCookie<DireccionMapa>("DireccionMCokkie");

            var combinedData = new CombinedData
            {
                DatosDactilares = DatosDactilares,
                Usuario = UsuarioCookie,
                DireccionMapa = DireccionMCokkie

            };


            // Aquí puedes combinarlos en un objeto para la vista de confirmación o enviar al endpoint




            //

            return RedirectToAction("Index", "Confirmar", combinedData);
        }


        public IActionResult FinalizarApertura()
        {
            _cookieService.EliminarCookie("DatosDactilaresCookie");
            _cookieService.EliminarCookie("UsuarioCookie");
            _cookieService.EliminarCookie("DireccionMCokkie");
            _cookieService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
