
using Entidades;
using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.AperturaCuenta;
using Repositorio;
using ServiceManager;
using AspNetCoreHero.ToastNotification.Abstractions;
using iText.Kernel.Pdf.Canvas.Wmf;
using Entidades.CuentaApertura;
using Modelos;
using Microsoft.IdentityModel.Tokens;

namespace Presentacion.CuentaApertura.Controllers
{
    public class StepsController : Controller
    {
        private static readonly object _lock = new object();
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;
        public INotyfService _notifyService { get; }
        public StepsController(IRepositorioManager repositoryManager, IServiceManager serviceManager, INotyfService notifyService)
        {
            _repositoryManager = repositoryManager;
            _serviceManager = serviceManager;
            _notifyService = notifyService;
        }

        [HttpPost]
        public IActionResult GuardarUsuCookie(Modelos.Usuario model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("UsuarioCookie", model);
                _serviceManager.CookieService.GuardarPasoActual(2);
                //_cookieService.GuardarDatosCookie("UsuarioCookie", model);
                //_cookieService.GuardarPasoActual(2);
                //ViewBag.Usuario = model.Nombre;
                //ViewBag.Apellido = model.Apellido;
                return RedirectToAction("Index","Direccion");
            }

            return View(model);
        }



        [HttpPost]
        public IActionResult GuardarDireccionMCookie(Modelos.DireccionMapa model)
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
        public IActionResult GuardarDatos_Adicionales(Modelos.InformacionPersonal model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("GuardarDatos_Adicionales", model);
                _serviceManager.CookieService.GuardarPasoActual(4);
               

                return RedirectToAction("Index","Face_Check");
            }

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Camara()
        //{
            
        //    //_serviceManager.CookieService.GuardarDatosCookie("FaceScanCookie", data);
        //    var paso= _serviceManager.CookieService.ObtenerPasoActual();
        //    if (paso == 5)
        //    {
        //        ViewBag.Message = "Ya se ha realizado el paso de Face Scan";
        //    }
           

        //    return RedirectToAction("Index", "Face_scan");
        //}

        
        [Consumes("application/json")]
        public async Task<IActionResult> Camara2([FromBody] string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                return BadRequest("No se proporcionó imagen.");
            }

            try
            {
                // Verificar y limpiar la cadena base64
                base64Image = base64Image.Trim();

                // Eliminar el prefijo "data:image/png;base64,"
                string base64Data = base64Image;
                const string base64Prefix = "data:image/png;base64,";
                if (base64Data.StartsWith(base64Prefix))
                {
                    base64Data = base64Data.Substring(base64Prefix.Length);
                }

                // Reemplazar caracteres no válidos
                base64Data = base64Data.Replace(" ", "+");

                // Asegurar que la cadena tenga un padding adecuado
                if (base64Data.Length % 4 != 0)
                {
                    base64Data = base64Data.PadRight(base64Data.Length + (4 - base64Data.Length % 4) % 4, '=');
                }

                var imageBytes = Convert.FromBase64String(base64Data);

                var datosDactilares = _serviceManager.CookieService.ObtenerDatosCookie<DatosDactilares>("DatosDactilaresCookie");

                Imagenes model = new Imagenes
                {
                    Identificacion = datosDactilares.Identificacion,
                    Codigo_Dactilar = datosDactilares.Codigo_Dactilar,
                    Foto = imageBytes // Puedes almacenar el base64 completo si es necesario
                };

                await _repositoryManager.registrosRepository.GuardarImagen(model);
                


                FaceScan model2 = new FaceScan
                {
                    ImageUrl = imageBytes
                };
                // _serviceManager.CookieService.GuardarDatosCookie("FaceScanCookie", model2);
                //_serviceManager.CookieService.GuardarPasoActual(5);

                //return RedirectToAction("Index", "Face_scan");
                return Json(new { success = true, redirectUrl = Url.Action("Index", "OTP") });
            }
            catch (FormatException ex)
            {
                // Registro del error de formato base64
                Console.WriteLine($"Error en la conversión de base64: {ex.Message}");
                return BadRequest("La cadena proporcionada no es una imagen base64 válida.");
            }
            catch (Exception ex)
            {
                // Registro del error general
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return StatusCode(500, "Ocurrió un error en el servidor.");
            }
        }







        [HttpPost]
        public async Task<IActionResult> GuardarFaceScan()
        {
            
                //_serviceManager.CookieService.GuardarDatosCookie("FaceScanCookie", model);
                _serviceManager.CookieService.GuardarPasoActual(5);
                var usuarioCookie = _serviceManager.CookieService.ObtenerDatosCookie<Modelos.Usuario>("UsuarioCookie");
                ViewBag.Usuario = usuarioCookie.Nombre;
                ViewBag.Apellido = usuarioCookie.Apellido;

                // Enviar el OTP por correo y guardar el OTP
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templates", "OTP.html");
                string htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);
                htmlContent = htmlContent.Replace("{{Nombre}}", usuarioCookie.Nombre);
                string otp = await _serviceManager.PdfService.SendOtpByEmailAsync(usuarioCookie.Correo, "Tu código OTP", htmlContent);

                // Guardar el OTP en la cookie (o en sesión)
                _serviceManager.CookieService.GuardarDatosCookie("OtpCookie", otp);
                _notifyService.Information("Se ha enviado un código OTP a tu correo electrónico. Por favor, ingrésalo para continuar.");
                return RedirectToAction("Index", "OTP");
            

            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerificarOtp(string Codigo, string Estado, Entidades.CuentaApertura.RegistrosAuditoria RegistrosAuditoria)
        {
            var storedOtp = _serviceManager.CookieService.ObtenerDatosCookie<string>("OtpCookie");
            var Fecha_ini = _serviceManager.CookieService.ObtenerDatosCookie<Entidades.CuentaApertura.RegistrosAuditoria>("Fecha_inicio");
            string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            string pais = await _serviceManager.ObtenerPaisDesdeIP(ipAddress);
            string userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            var qwe = _serviceManager.ObtenerDatosCombinados();

            // Initialize the RegistrosAuditoria object once
            RegistrosAuditoria = new Entidades.CuentaApertura.RegistrosAuditoria
            {
                DireccionIP = ipAddress,
                DatosNavegador = userAgent,
                Pais = pais,
                Fecha_inicio = Fecha_ini.Fecha_inicio,
                Fecha_Fin = DateTime.Now,
                Correo_envio_OTP = qwe.Usuario.Correo,
                Identificacion = qwe.DatosDactilares.Identificacion,
                CodigoOTP = Codigo,
                CodigoDactilar = qwe.DatosDactilares.Codigo_Dactilar,
            };

            if (storedOtp == Codigo)
            {
                _serviceManager.CookieService.GuardarPasoActual(6);
                Estado = "Valido";

                lock (_lock)
                {
                    _repositoryManager.registrosRepository.GuardarAuditora(RegistrosAuditoria, Estado);

                    CuentaUsuario UsuarioGuardar = new CuentaUsuario();
                    _serviceManager.ObtenerDatosCombinadosParaBD(UsuarioGuardar);

                    _serviceManager.SendPdfService();
                }

                _notifyService.Success("El código OTP ingresado es válido.");
                return RedirectToAction("Index", "Resumen_Final");
            }
            else
            {
                Estado = "Invalido";
                _repositoryManager.registrosRepository.GuardarAuditora(RegistrosAuditoria, Estado);

                ModelState.AddModelError(string.Empty, "El código OTP ingresado no es válido.");
                _notifyService.Error("El código OTP ingresado no es válido.");
                return RedirectToAction("Index", "OTP");
            }
        }



        public async Task<IActionResult> FinalizarApertura()
        {


            _serviceManager.borrarCookie();
            _serviceManager.CookieService.SignOutAsync();
            _notifyService.Success("Tu cuenta ha sido abierta con éxito. ¡Gracias por elegirnos!");
            return RedirectToAction("Index", "Home");


        }
    }
}
