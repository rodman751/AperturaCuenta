using Microsoft.AspNetCore.Mvc;
using Repositorio;
using ServiceManager;

namespace Presentacion.CuentaApertura.Controllers
{
    public class PdfController : Controller
    {
        private readonly IRepositorioManager _repositoryManager;
        private readonly IServiceManager _serviceManager;


        public PdfController(IRepositorioManager repositoryManager, IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _repositoryManager = repositoryManager;
        }

        public async Task<IActionResult> SendPdf()
        {
            // Obtén los datos necesarios del servicio
            var clienteNombre = "Rodman Guerrero";
            var cuentaNumero = "1234567890";
            var correo = "premiunjoel7@gmail.com";
            var cedula = "0450080940";



            var fechaActual = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy");
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templates", "ContractTemplate.html");
            string htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);
            htmlContent = htmlContent.Replace("{{clienteNombre}}", clienteNombre)
                                     .Replace("{{cuentaNumero}}", cuentaNumero)
                                     .Replace("{{cedula}}", cedula)
                                     .Replace("{{LUGAR_FECHA}}", fechaActual);

            byte[] pdfContent = _serviceManager.PdfService.GeneratePdf(htmlContent);

            await _serviceManager.PdfService.SendPdfByEmailAsync(correo, "Contrato de Apertura de Cuenta", "Adjunto encontrará el contrato de apertura de cuenta.", pdfContent);

            return Ok("PDF enviado exitosamente.");
        }
        public async Task<IActionResult> OTP2()
        {
            var clienteNombre = "Rodman Guerrero";
            
            var correo = "premiunjoel7@gmail.com";
           

            // Enviar el OTP por correo y guardar el OTP
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templates", "OTP.html");
            string htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);
            htmlContent = htmlContent.Replace("{{Nombre}}", clienteNombre);
            string otp = await _serviceManager.PdfService.SendOtpByEmailAsync(correo, "Tu código OTP", htmlContent);

            return Ok("PDF enviado exitosamente.");
        }
    }
}
