using Entidades;
using Interface.AperturaCuenta;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.AperturaCuenta
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void GuardarDatosCookie<T>(string cookieName, T data)
        {
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(5) // Expiración de la cookie
            };

            // Serializar los datos como JSON para almacenarlos en la cookie
            var jsonData = JsonSerializer.Serialize(data);

            // Almacenar la cookie
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, jsonData, options);
        }

        public T? ObtenerDatosCookie<T>(string cookieName) where T : class
        {
            if (_httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(cookieName, out var jsonData) == true)
            {
                // Deserializar los datos JSON de la cookie
                return JsonSerializer.Deserialize<T>(jsonData);
            }

            return null;
        }

        public async Task AgregarClaimsAsync(DatosDactilares datos)
        {
            var claims = new List<Claim>
            {
                new Claim("Identificaion", datos.Identificacion),
            
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = true // Configura según tus necesidades
            };

            await _httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }


        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void EliminarCookie(string cookieName)
        {
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1) // Establecer una fecha de expiración en el pasado para eliminar la cookie
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, string.Empty, options);
        }
    }
}
