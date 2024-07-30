using Entidades.CuentaApertura;
using Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interface.AperturaCuenta;

namespace Presentacion.CuentaApertura.Views.Confirm
{
    public class ConfirmarController : Controller
    {
        private readonly ICookieService _cookieService;
        public ConfirmarController(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            // Obtener todos los datos de las cookies para confirmar
            var DatosDactilares = _cookieService.ObtenerDatosCookie<Entidades.DatosDactilares>("DatosDactilaresCookie") ?? new Entidades.DatosDactilares();
            var UsuarioCookie = _cookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie") ?? new Usuario();
            var DireccionMCokkie = _cookieService.ObtenerDatosCookie<DireccionMapa>("DireccionMCokkie") ?? new DireccionMapa();

            // Combinarlos en un objeto para la vista de confirmación
            var combinedData = new CombinedData
            {
                DatosDactilares = DatosDactilares,
                Usuario = UsuarioCookie,
                DireccionMapa = DireccionMCokkie
            };

            return View(combinedData);
            
        }

       
    }
}
