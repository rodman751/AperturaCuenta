using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.CuentaApertura
{
    public class CuentaUsuario
    {
        //datos dactilares
        [Key]
        public string Identificacion { get; set; }

        public string Codigo_Dactilar { get; set; }
        // Usuario 
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        //Direccion mapa
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Parroquia { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }

        //Informacion personal ////////////////////////////
        public double Ingresos { get; set; }
        public double Gastos { get; set; }
        public string PaisNacimiento { get; set; }
        public string CiudadNacimiento { get; set; }
        public string NivelDeInstruccion { get; set; }

        // Condición Laboral
        public bool Dependiente { get; set; }
        public bool NegocioPropio { get; set; }
        public bool Ninguno { get; set; }

        // Tipo de Vivienda
        public bool VivoConFamiliares { get; set; }
        public bool Propia { get; set; }
        public bool PropiaHipotecada { get; set; }

        // Actividades en otro país
        public bool ActividadesEnOtroPais { get; set; }
        public string? DetallesActividadesEnOtroPais { get; set; }
        public bool AceptoTerminos { get; set; }
        //////////////////////////////////////////////////
        // face scan
        public string? ImageUrl { get; set; }
    }
}
