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
        
        [HttpGet]
        public IActionResult Index()
        {
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"];
            }
            return View();
        }
        //private async Task AgregarClaimsAsync(Usuario user)
        //{
        //    var claims = new List<Claim>
        //    {

        //        new Claim("Nombre", user.Nombre),
        //        new Claim("Apellido", user.Apellido)

        //    };

        //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    var authProperties = new AuthenticationProperties
        //    {
        //        IsPersistent = true // Configura según tus necesidades
        //    };

        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        //}

      

        [HttpPost]
        public IActionResult ComprobarDatos(Entidades.DatosDactilares model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el dato existe
                bool existe = _datosDactilaresService.ExisteDato(model);

                if (existe)
                {
                    TempData["Message"] = "Los datos existen.";
                    //  recuperar datos del usuario
                    ViewBag.Identificacion = model.Identificacion; // con la identificación se puede buscar el usuario

                }
                else
                {
                    TempData["Message"] = "Los datos no existen.";
                    // crear nuevos datos del usuario
                }

                // Redirigir a la acción vista
                return RedirectToAction("Index", "UsuarioView");
            }

            // Si el modelo no es válido, devolver la misma vista para mostrar errores
            return View(model);
        }

        [HttpGet]
        public IActionResult Step1()
        {
            // Intentar recuperar datos guardados de la cookie
            var datosStep1 =_cookieService.ObtenerDatosCookie<UsuarioViewController>("Step1Cookie");

            // Si hay datos guardados, inicializar el modelo con ellos
            var model = datosStep1 ?? new UsuarioViewController();

            return View(model);
        }

        [HttpPost]
        public IActionResult Step1(Usuario model)
        {
            if (ModelState.IsValid)
            {
                // Guardar datos en la cookie
                _cookieService.GuardarDatosCookie("Step1Cookie", model);
                _cookieService.AgregarClaimsAsync(model);

                // Redirigir al siguiente paso
                return RedirectToAction("Step2");
            }

            // Si el modelo no es válido, mostrar la vista actual con los errores
            return View(model);
        }

        [HttpGet]
        public IActionResult Step2()
        {
            // Intentar recuperar datos guardados de la cookie
            var datosStep2 = _cookieService.ObtenerDatosCookie<DireccionMapa>("Step2Cookie");

            // Si hay datos guardados, inicializar el modelo con ellos
            var model = datosStep2 ?? new DireccionMapa();

            return View(model);
        }

        [HttpPost]
        public IActionResult Step2(DireccionMapa model)
        {
            if (ModelState.IsValid)
            {
                // Guardar datos en la cookie
                _cookieService.GuardarDatosCookie("Step2Cookie", model);

                // Aquí es donde se pueden enviar todos los datos a un endpoint
                return RedirectToAction("Confirm");
            }

            // Si el modelo no es válido, mostrar la vista actual con los errores
            return View(model);
        }

        [HttpGet]
        public  IActionResult Confirm()
        {
            // Obtener todos los datos de las cookies para confirmar
            var datosStep1 = _cookieService.ObtenerDatosCookie<Usuario>("Step1Cookie");
            var datosStep2 = _cookieService.ObtenerDatosCookie<DireccionMapa>("Step2Cookie");

            // Aquí puedes combinarlos en un objeto para la vista de confirmación o enviar al endpoint
            var combinedData = new CombinedData
            {
                Usuario = datosStep1,
                DireccionMapa = datosStep2
            };

            // Opcionalmente, podrías enviar estos datos a un endpoint usando un servicio HTTP



            // Limpiar las cookies
            _cookieService.SignOutAsync();

            return View(combinedData);
        }

       
    }
}
