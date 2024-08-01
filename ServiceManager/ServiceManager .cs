using Entidades;
using Entidades.CuentaApertura;
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

        public ServiceManager(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public ICookieService CookieService => _cookieService;
        public CombinedData ObtenerDatosCombinados()
        {
            var datosDactilares = _cookieService.ObtenerDatosCookie<DatosDactilares>("DatosDactilaresCookie");
            var usuarioCookie = _cookieService.ObtenerDatosCookie<Usuario>("UsuarioCookie");
            var direccionMapa = _cookieService.ObtenerDatosCookie<DireccionMapa>("DireccionMCokkie");
            var DatosAdicionales = _cookieService.ObtenerDatosCookie<InformacionPersonal>("GuardarDatos_Adicionales");
            var faceScan = _cookieService.ObtenerDatosCookie<Entidades.FaceScan>("FaceScanCookie");
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
            
        }

    }
}
