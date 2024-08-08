namespace DatosdeEjemplo
{
    public class DatosDactilaresejemplo
    {
        public string Identificacion { get; set; }
        public string Codigo_Dactilar { get; set; }

        public DatosDactilaresejemplo(string identificacion, string codigoDactilar)
        {
            Identificacion = identificacion;
            Codigo_Dactilar = codigoDactilar;
        }
    }
}
