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
            var clienteNombre = "John Doe";
            var cuentaNumero = "1234567890";
            var correo = "ppruebas109@gmail.com";
            // Crea el HTML con los datos dinámicos
            string htmlContent = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    margin: 20px;
                }}
                .contract-header {{
                    text-align: center;
                    font-size: 24px;
                    margin-bottom: 20px;
                }}
                .contract-content {{
                    font-size: 14px;
                }}
            </style>
        </head>
        <body>
            <div class='contract-header'>
                Contrato de Apertura de Cuenta
            </div>
            <div class='contract-content'>
                <p>Estimado Cliente,</p>
                <p>Nos complace informarle que su cuenta ha sido aperturada con éxito.</p>
                <p>Datos de la Cuenta:</p>
                <ul>
                    <li>Nombre: {clienteNombre}</li>
                    <li>Cuenta: {cuentaNumero}</li>
                </ul>
                <p>Gracias por elegirnos.</p>
                <p>Atentamente,</p>
                <p>Su Empresa</p>
            </div>
        </body>
        </html>";

            byte[] pdfContent = _serviceManager.PdfService.GeneratePdf(htmlContent);

            await _serviceManager.PdfService.SendPdfByEmailAsync(correo, "Contrato de Apertura de Cuenta", "Adjunto encontrará el contrato de apertura de cuenta.", pdfContent);

            return Ok("PDF enviado exitosamente.");
        }
    }
}
