using Entidades;
using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentacion.CuentaApertura.Controllers.ControllersVistas
{
    public class UsuarioViewController : Controller
    {
        private readonly ICookieService _cookieService;
        public UsuarioViewController(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        [Authorize]
        public IActionResult Index()
        {

            Modelos.Usuario model = null;
            if (TempData["Usuario"] != null)
            {
                // Obtener y deserializar el modelo almacenado en TempData
                model = JsonConvert.DeserializeObject<Modelos.Usuario>(TempData["Usuario"].ToString());
            }
            // Si el modelo de usuario tiene valores, mapearlos a la vista
            if (model != null )
            {
                return View(model);
            }
            var datosStep1 = _cookieService.ObtenerDatosCookie<Modelos.Usuario>("UsuarioCookie");
            // Si hay datos guardados, inicializar el modelo con ellos
            var model2 = datosStep1 ?? new Modelos.Usuario();
            // Si el modelo de usuario no tiene valores, devolver una vista vacía o con un modelo predeterminado
            return View(model2);
        }


    }
}
