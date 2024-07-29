using Entidades;

namespace Interface.AperturaCuenta
{
    public interface IDatosDactilaresService
    {
        List<DatosDactilares> ObtenerDatos();
        bool ExisteDato(DatosDactilares dato);
    }
}
