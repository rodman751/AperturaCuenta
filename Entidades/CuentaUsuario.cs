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
        public int Id { get; set; }
        //datos dactilares

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

        //Datos Nuevos

        public double Gasto_de_Transporte { get; set; }
        public double Gasto_de_Educacion { get; set; }


        public bool Creditos { get; set; }
        public bool Tarjetas_de_Credito { get; set; }
        public bool Ninguno { get; set; }


        public bool Casa { get; set; }
        public bool Carro { get; set; }
        public bool Terreno { get; set; }
        //

        public string PaisNacimiento { get; set; }
        public string CiudadNacimiento { get; set; }
        public string NivelDeInstruccion { get; set; }

        // Condición Laboral
        public string CondicionLaboral { get; set; } // Dependiente, NegocioPropio, Ninguno

        // Tipo de Vivienda
        public string TipoVivienda { get; set; } // VivoConFamiliares, Propia, PropiaHipotecada

        // Actividades en otro país
        public bool ActividadesEnOtroPais { get; set; }
        public string? DetallesActividadesEnOtroPais { get; set; }
        public bool AceptoTerminos { get; set; }
        //////////////////////////////////////////////////
        // face scan
        public byte[]? ImageUrl { get; set; }
    }
}
