using Entidades;
using Modelos;

using Interface.AperturaCuenta;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositorio;
using Services.AperturaCuenta;
using Entidades.CuentaApertura;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;


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
        private readonly IRepositorioManager _repositorioManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ServiceManager(ICookieService cookieService, IPdfService pdfService, IRepositorioManager repositorioManager, IHttpContextAccessor httpContextAccessor)
        {
            _cookieService = cookieService;
            _pdfService = pdfService;
            _repositorioManager = repositorioManager;
            _httpContextAccessor = httpContextAccessor;
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
        public async Task ObtenerDatosCombinadosParaBD(CuentaUsuario UsuarioGuardar)
        {
            // Obtener los datos de las cookies
            var datosDactilares = _cookieService.ObtenerDatosCookie<DatosDactilares>("DatosDactilaresCookie");
            var usuarioCookie = _cookieService.ObtenerDatosCookie<Modelos.Usuario>("UsuarioCookie");
            var direccionMapa = _cookieService.ObtenerDatosCookie<DireccionMapa>("DireccionMCokkie");
            var datosAdicionales = _cookieService.ObtenerDatosCookie<InformacionPersonal>("GuardarDatos_Adicionales");

            // Asignar los valores a la instancia UsuarioGuardar
            UsuarioGuardar.Identificacion = datosDactilares.Identificacion;
            UsuarioGuardar.Codigo_Dactilar = datosDactilares.Codigo_Dactilar;
            UsuarioGuardar.Nombre = usuarioCookie.Nombre;
            UsuarioGuardar.Apellido = usuarioCookie.Apellido;
            UsuarioGuardar.Telefono = usuarioCookie.Telefono;
            UsuarioGuardar.Correo = usuarioCookie.Correo;
            UsuarioGuardar.Provincia = direccionMapa.Provincia;
            UsuarioGuardar.Canton = direccionMapa.Canton;
            UsuarioGuardar.Parroquia = direccionMapa.Parroquia;
            UsuarioGuardar.Direccion = direccionMapa.Direccion;
            UsuarioGuardar.Referencia = direccionMapa.Referencia;
            UsuarioGuardar.Ingresos = datosAdicionales.Ingresos;
            UsuarioGuardar.Gastos = datosAdicionales.Gastos;
            UsuarioGuardar.PaisNacimiento = datosAdicionales.PaisNacimiento;
            UsuarioGuardar.CiudadNacimiento = datosAdicionales.CiudadNacimiento;
            UsuarioGuardar.NivelDeInstruccion = datosAdicionales.NivelDeInstruccion;
            UsuarioGuardar.CondicionLaboral = datosAdicionales.CondicionLaboral;
            UsuarioGuardar.TipoVivienda = datosAdicionales.TipoVivienda;
            UsuarioGuardar.ActividadesEnOtroPais = datosAdicionales.ActividadesEnOtroPais;
            UsuarioGuardar.DetallesActividadesEnOtroPais = datosAdicionales.DetallesActividadesEnOtroPais;
            UsuarioGuardar.AceptoTerminos = datosAdicionales.AceptoTerminos;

            UsuarioGuardar.Gasto_de_Transporte = datosAdicionales.Gasto_de_Transporte;
            UsuarioGuardar.Gasto_de_Educacion = datosAdicionales.Gasto_de_Educacion;

            UsuarioGuardar.Creditos = datosAdicionales.Creditos;
            UsuarioGuardar.Tarjetas_de_Credito = datosAdicionales.Tarjetas_de_Credito;
            UsuarioGuardar.Ninguno = datosAdicionales.Ninguno;

            UsuarioGuardar.Casa = datosAdicionales.Casa;
            UsuarioGuardar.Carro = datosAdicionales.Carro;
            UsuarioGuardar.Terreno = datosAdicionales.Terreno;


            // Guardar el usuario en el repositorio
            await _repositorioManager.UsuarioRepository.GuardarUsuario(UsuarioGuardar);
            //await _repositorioManager.UsuarioRepository.EjecutarPA_obtenerURLimage();
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
            _cookieService.EliminarCookie("Fecha_inicio");
        }
        public async Task borrarCookie2()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                context.Response.Cookies.Delete("PasoActualCookie");
                context.Response.Cookies.Delete("DatosDactilaresCookie");
                context.Response.Cookies.Delete("UsuarioCookie");
                context.Response.Cookies.Delete("DireccionMCokkie");
                context.Response.Cookies.Delete("GuardarDatos_Adicionales");
                context.Response.Cookies.Delete("FaceScanCookie");
                context.Response.Cookies.Delete("OTPCookie");
                context.Response.Cookies.Delete("OtpCookie");
                context.Response.Cookies.Delete("Fecha_inicio");
                // Añade más cookies si es necesario
            }
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

        public async Task<string> ObtenerPaisDesdeIP(string ipAddress)
        {
            // Endpoint de ip-api.com para obtener información de la IP
            string url = $"http://ip-api.com/json/{ipAddress}?fields=country";

            using (HttpClient client = new HttpClient())
            {
                // Llamada al servicio para obtener la información de la IP
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Leer la respuesta y convertirla a JSON
                    string result = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(result);

                    // Obtener el país desde la respuesta
                    string pais = json["country"]?.ToString();
                    return pais;
                }
                else
                {
                    // Manejar el error de manera apropiada
                    return "Desconocido";
                }
            }
        }

    }
}
