using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InformacionPersonal
    {
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
    }

}
