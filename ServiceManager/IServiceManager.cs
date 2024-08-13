
using Entidades.CuentaApertura;
using Interface.AperturaCuenta;
using Services.AperturaCuenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManager
{
    public interface IServiceManager
    {
        ICookieService CookieService { get; }
        IPdfService PdfService { get; }
        Modelos.CombinedData ObtenerDatosCombinados();
        Task ObtenerDatosCombinadosParaBD(CuentaUsuario UsuarioGuardar);
        void borrarCookie();
        Task SendPdfService();
        Task<string> ObtenerPaisDesdeIP(string ipAddress);
    }
}
