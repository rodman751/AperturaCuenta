using Entidades;
using Modelos;

using Interface.AperturaCuenta;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositorio;
using Services.AperturaCuenta;

namespace ServiceManager
{
    public class ServiceManager:IServiceManager
    {
        //private readonly Lazy<ICookieService> _cookieService;

        //public ServiceManager( IRepositorioManager repositorioManager)
        //{
        //    _cookieService = new Lazy<ICookieService>(()=> new CookieService(repositorioManager));

        //}

        //public ICookieService CookieService => _cookieService.Value;


        private readonly ICookieService _cookieService;
        private readonly IPdfService _pdfService;
        public ServiceManager(ICookieService cookieService, IPdfService pdfService)
        {
            _cookieService = cookieService;
            _pdfService = pdfService;
        }

        public ICookieService CookieService => _cookieService;
        public IPdfService PdfService => _pdfService;

        public CombinedData ObtenerDatosCombinados()
        {
            var datosDactilares = _cookieService.ObtenerDatosCookie<DatosDactilares>("DatosDactilaresCookie");
            var usuarioCookie = _cookieService.ObtenerDatosCookie<Modelos.Usuario>("UsuarioCookie");
            var direccionMapa = _cookieService.ObtenerDatosCookie<DireccionMapa>("DireccionMCokkie");
            var DatosAdicionales = _cookieService.ObtenerDatosCookie<InformacionPersonal>("GuardarDatos_Adicionales");
            var faceScan = _cookieService.ObtenerDatosCookie<Modelos.FaceScan>("FaceScanCookie");
            var otp = _cookieService.ObtenerDatosCookie<OTP>("OTPCookie");

            return new CombinedData
            {
                DatosDactilares = datosDactilares,
                Usuario = usuarioCookie,
                DireccionMapa = direccionMapa,
                InformacionPersonal = DatosAdicionales,
                FaceScan = faceScan,
                OTP = otp
            };
        }

        public void borrarCookie()
        {
            _cookieService.EliminarCookie("PasoActualCookie");
            _cookieService.EliminarCookie("DatosDactilaresCookie");
            _cookieService.EliminarCookie("UsuarioCookie");
            _cookieService.EliminarCookie("DireccionMCokkie");
            _cookieService.EliminarCookie("GuardarDatos_Adicionales");
            _cookieService.EliminarCookie("FaceScanCookie");
            _cookieService.EliminarCookie("OTPCookie");
            _cookieService.EliminarCookie("OtpCookie");

        }

        public async Task SendPdfService()
        {
          
            var datos = ObtenerDatosCombinados();

            var nombre = datos?.Usuario?.Nombre ?? "NombreDesconocido";
            var apellido = datos?.Usuario?.Apellido ?? "ApellidoDesconocido";
            var clienteNombre = $"{nombre} {apellido}";
            var cuentaNumero = "1234567890";
            var correo = datos.Usuario.Correo;
            var cedula = datos.DatosDactilares.Identificacion;


            var fechaActual = DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy");
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Templates", "ContractTemplate.html");
            string htmlContent = await System.IO.File.ReadAllTextAsync(templatePath);
            htmlContent = htmlContent.Replace("{{clienteNombre}}", clienteNombre)
                                     .Replace("{{cuentaNumero}}", cuentaNumero)
                                     .Replace("{{cedula}}", cedula)
                                     .Replace("{{LUGAR_FECHA}}", fechaActual);

            byte[] pdfContent = PdfService.GeneratePdf(htmlContent);

            await PdfService.SendPdfByEmailAsync(correo, "Contrato de Apertura de Cuenta", "Adjunto encontrará el contrato de apertura de cuenta.", pdfContent);

            //return Ok("PDF enviado exitosamente.");
        }

    }
}
