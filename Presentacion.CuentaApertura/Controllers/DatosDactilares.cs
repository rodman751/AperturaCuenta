using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entidades;
using Interface.AperturaCuenta;
using Services.AperturaCuenta;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Entidades.CuentaApertura;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
namespace Presentacion.CuentaApertura.Controllers
{
    public class DatosDactilares : Controller
    {

        private readonly ICookieService _cookieService;
        private readonly IDatosDactilaresService _datosDactilaresService;

        public DatosDactilares(IDatosDactilaresService datosDactilaresService, ICookieService cookieService)
        {
            _datosDactilaresService = datosDactilaresService;
            _cookieService = cookieService;
        }
        
       
        public IActionResult Index()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }

            var datosStep1 = _cookieService.ObtenerDatosCookie<Entidades.DatosDactilares>("DatosDactilaresCookie");

            // Si hay datos guardados, inicializar el modelo con ellos
            var model = datosStep1 ?? new Entidades.DatosDactilares();
            return View(model);
        }

        [HttpPost]
        public IActionResult ComprobarDatos(Entidades.DatosDactilares model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el dato existe
                bool existe = _datosDactilaresService.ExisteDato(model);
               // _cookieService.AgregarClaimsAsync(model);
                _cookieService.GuardarDatosCookie("DatosDactilaresCookie",model);

                if (existe)
                {
                    TempData["Message"] = "Los datos existen.";
                    
                    ViewBag.Identificacion = model.Identificacion; // con la identificación se puede buscar el usuario

                }
                else
                {
                    TempData["Message"] = "Los datos no existen.";
                   
                }

                _cookieService.GuardarPasoActual(1);
                return RedirectToAction("Index", "UsuarioView");
            }
            
       
            return View(model);
        }





    }
}
