using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace DatosdeEjemplo
{
    public class DatosDactilaresEjemploService
    {
        private List<DatosDactilaresejemplo> datos;

        public DatosDactilaresEjemploService()
        {
            // Inicializa la lista con algunos datos de ejemplo
            datos = new List<DatosDactilaresejemplo>
        {
            new DatosDactilaresejemplo("123456789", "Codigo123"),
            new DatosDactilaresejemplo("987654321", "Codigo456"),
            new DatosDactilaresejemplo("111222333", "Codigo789")
        };
        }

        public List<DatosDactilaresejemplo> ObtenerDatosEjemplo()
        {
            return datos;
        }

        public bool ExisteDato(DatosDactilares dato)
        {
            return datos.Any(d => d.Identificacion == dato.Identificacion && d.Codigo_Dactilar == dato.Codigo_Dactilar);
        }
    }
}
