using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.CuentaApertura
{
    public class RegistrosAuditoria
    {
        public int Id { get; set; }                  // Identificador único para cada registro de log
        public string DireccionIP { get; set; }      // Dirección IP del usuario
        public string DatosNavegador { get; set; }  // Datos del navegador del usuario
        public string Pais { get; set; }            // País de origen del usuario
        public string Correo_envio_OTP { get; set; }    // Correo_envio_OTP

        public DateTime Fecha_inicio { get; set; }         // Fecha_inicio y hora de la acción
        public DateTime Fecha_Fin { get; set; }         // Fecha_Fin y hora de la acción
        public string Identificacion { get; set; }  // Identificación de la sesión del usuario
        
        public string CodigoDactilar { get; set; }  // Código dactilar del usuario
        public string CodigoOTP { get; set; }        // Código OTP (One-Time Password) validado o enviado
        public string EstadoOTP { get; set; }        // Estado del código OTP (por ejemplo, validado o enviado)

    }
}
