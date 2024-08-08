using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.AperturaCuenta
{
    public interface ICookieService
    {
        void GuardarDatosCookie<T>(string cookieName, T data);
        T? ObtenerDatosCookie<T>(string cookieName) where T : class;

        //Task AgregarClaimsAsync(DatosDactilares datos);
        Task SignOutAsync();
        void EliminarCookie(string cookieName);
        void GuardarPasoActual(int paso);
        int ObtenerPasoActual();
    }
}
