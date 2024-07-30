using Entidades;
using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.CuentaApertura.Controllers
{
    public class UsuarioViewController : Controller
    {
        private readonly ICookieService _cookieService;
        public UsuarioViewController(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }
        public ActionResult Index()
        {
           
            var datosStep1 = _cookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie");

            // Si hay datos guardados, inicializar el modelo con ellos
            var model = datosStep1 ?? new Usuario();

            
            return View(model);
        }


    }
}
