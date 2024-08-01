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
using Repositorio;
using ServiceManager;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace Presentacion.CuentaApertura.Controllers
{
    public class DatosDactilares : Controller
    {

        private readonly ICookieService _cookieService;

        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;

        public DatosDactilares( ICookieService cookieService, IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            
            _cookieService = cookieService;
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
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
        public async Task<IActionResult> ComprobarDatos(Entidades.DatosDactilares model)
        {
            if (ModelState.IsValid)
            {
                
                _cookieService.GuardarDatosCookie("DatosDactilaresCookie", model);

                
                var usuarios = await _repositoryManager.UsuarioRepository.EjecutarProcedimientoAlmacenado();

                
                if (int.TryParse(model.Identificacion, out int identificacionInt))
                {
                    
                    var usuarioExistente = usuarios.FirstOrDefault(u => u.Id == identificacionInt);
                    if (usuarioExistente != null)
                    {
                        
                        Usuario newUsuario = new Usuario
                        {
                            Id = usuarioExistente.Id,
                            Nombre = usuarioExistente.Nombre,
                            Apellido = usuarioExistente.Apellido,
                            Telefono = usuarioExistente.Telefono,
                            Correo = usuarioExistente.Correo
                        };

                        // Guardar el paso actual en una cookie
                        _cookieService.GuardarPasoActual(1);

                        TempData["Usuario"] = JsonConvert.SerializeObject(newUsuario);
                        return RedirectToAction("Index", "UsuarioView");
                    }
                }

                // Si el usuario no existe, guardar el paso actual y redirigir sin el modelo de usuario
                _cookieService.GuardarPasoActual(1);
                return RedirectToAction("Index", "UsuarioView");
            }

            return View(model);
        }





    }
}
