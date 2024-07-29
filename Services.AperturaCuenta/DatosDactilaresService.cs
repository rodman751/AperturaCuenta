using Entidades;
using Interface.AperturaCuenta;
namespace Services.AperturaCuenta
{
    public class DatosDactilaresService : IDatosDactilaresService
    {
        private List<DatosDactilares> datos;

        public DatosDactilaresService()
        {
            // Inicializa la lista con algunos datos de ejemplo
            datos = new List<DatosDactilares>
            {
            new DatosDactilares("123456789", "Codigo123"),
            new DatosDactilares("987654321", "Codigo987"),
            new DatosDactilares("111222333", "Codigo111")
            };
        }

        public List<DatosDactilares> ObtenerDatos()
        {
            return datos;
        }

        public bool ExisteDato(DatosDactilares dato)
        {
            return datos.Any(d => d.Identificacion == dato.Identificacion && d.Codigo_Dactilar == dato.Codigo_Dactilar);
        }
    }
}
