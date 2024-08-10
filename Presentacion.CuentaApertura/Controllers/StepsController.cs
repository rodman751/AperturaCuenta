﻿
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

namespace Presentacion.CuentaApertura.Controllers
{
    public class StepsController : Controller
    {
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

        [HttpPost]
        public IActionResult Camara()
        {
            
            
            _serviceManager.CookieService.GuardarPasoActual(5);


            return RedirectToAction("Index", "Face_scan");
            

            
        }

        [HttpPost]
        public async Task<IActionResult> GuardarFaceScan(Modelos.FaceScan model)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CookieService.GuardarDatosCookie("FaceScanCookie", model);
                _serviceManager.CookieService.GuardarPasoActual(6);
                var usuarioCookie = _serviceManager.CookieService.ObtenerDatosCookie<Modelos.Usuario>("UsuarioCookie");
                ViewBag.Usuario = usuarioCookie.Nombre;
                ViewBag.Apellido = usuarioCookie.Apellido;

                // Enviar el OTP por correo y guardar el OTP
                string otp = await _serviceManager.PdfService.SendOtpByEmailAsync(usuarioCookie.Correo, "Tu código OTP", "Por favor, usa el siguiente código para completar tu proceso de Apertura de Cuenta:");

                // Guardar el OTP en la cookie (o en sesión)
                _serviceManager.CookieService.GuardarDatosCookie("OtpCookie", otp);
                _notifyService.Information("Se ha enviado un código OTP a tu correo electrónico. Por favor, ingrésalo para continuar.");
                return RedirectToAction("Index", "OTP");
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> VerificarOtp(string Codigo ,string Estado , Entidades.CuentaApertura.RegistrosAuditoria RegistrosAuditoria)
        {
            var storedOtp = _serviceManager.CookieService.ObtenerDatosCookie<string>("OtpCookie");

            if (storedOtp == Codigo)
            {
                _serviceManager.CookieService.GuardarPasoActual(7);
                // OTP válido, continuar con el siguiente paso
                _serviceManager.SendPdfService();


                _serviceManager.ObtenerDatosCombinadosparabd();
                //

                var qwe = _serviceManager.ObtenerDatosCombinados();

                RegistrosAuditoria = new Entidades.CuentaApertura.RegistrosAuditoria
                {
                    DireccionIP = "192.168.1.1",
                    DatosNavegador = "Mozilla/5.0",
                    Pais = "Ecuador",
                    Fecha = DateTime.UtcNow,
                    Identificacion = qwe.DatosDactilares.Identificacion,
                    CodigoOTP = _serviceManager.CookieService.ObtenerDatosCookie<string>("OtpCookie"),
                    CodigoDactilar = qwe.DatosDactilares.Codigo_Dactilar,
                };
                Estado = "Valido";

                _repositoryManager.registrosRepository.GuardarAuditora(RegistrosAuditoria, Estado);

                _notifyService.Success("El código OTP ingresado es válido.");
                return RedirectToAction("Index", "Resumen_Final");
            }
            else
            {
                // OTP no válido, mostrar error
                ModelState.AddModelError(string.Empty, "El código OTP ingresado no es válido.");
                _notifyService.Error("El código OTP ingresado no es válido.");

                var qwe = _serviceManager.ObtenerDatosCombinados();

                RegistrosAuditoria = new Entidades.CuentaApertura.RegistrosAuditoria
                {
                    DireccionIP = "192.168.1.1",
                    DatosNavegador = "Mozilla/5.0",
                    Pais = qwe.InformacionPersonal.PaisNacimiento,
                    Fecha = DateTime.UtcNow,
                    Identificacion = qwe.DatosDactilares.Identificacion,
                    CodigoOTP = _serviceManager.CookieService.ObtenerDatosCookie<string>("OtpCookie"),
                    CodigoDactilar = qwe.DatosDactilares.Codigo_Dactilar,
                };
                Estado = "Invalido";

                _repositoryManager.registrosRepository.GuardarAuditora(RegistrosAuditoria, Estado);

                return RedirectToAction("Index", "OTP"); // Mostrar la vista OTP nuevamente
            }
        }



        public async Task<IActionResult> FinalizarApertura()
        {

           
            _serviceManager.borrarCookie();
            _notifyService.Success("Tu cuenta ha sido abierta con éxito. ¡Gracias por elegirnos!");
            return RedirectToAction("Index", "Home");


        }
    }
}
