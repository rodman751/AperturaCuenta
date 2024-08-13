using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using Presentacion.CuentaApertura.Models;
using System.Diagnostics;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentacion.CuentaApertura.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICookieService _cookieService;
        public HomeController(ILogger<HomeController> logger, ICookieService cookieService)
        {
            _logger = logger;
            _cookieService = cookieService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Inicio()
        {
            _cookieService.AgregarClaimsAsync();
            
            Entidades.CuentaApertura.RegistrosAuditoria data = new Entidades.CuentaApertura.RegistrosAuditoria ();
            data.Fecha_inicio = DateTime.Now;
            _cookieService.GuardarDatosCookie<Entidades.CuentaApertura.RegistrosAuditoria>("Fecha_inicio", data);
            int pasoActual = _cookieService.ObtenerPasoActual();
            switch (pasoActual)
            {
                case 1:
                    return RedirectToAction("Index", "UsuarioView");
                case 2:
                    return RedirectToAction("Index", "Direccion");
                case 3:
                    return RedirectToAction("Index", "Datos_adicionales");
                case 4:
                    return RedirectToAction("Index", "Face_Check");
               
                case 5:
                    return RedirectToAction("Index", "OTP");
                case 6:
                    return RedirectToAction("Index", "Resumen_final");
                default:
                    return RedirectToAction("Index", "DatosDactilares");
            }
        }



    }
}
