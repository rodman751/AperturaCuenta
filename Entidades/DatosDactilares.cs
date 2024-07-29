using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class DatosDactilares
    {
        [Required(ErrorMessage = "La identificación es requerida.")]
        
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El código dactilar es requerido.")]
        public string Codigo_Dactilar { get; set; }
        public DatosDactilares() { }
        public DatosDactilares(string identificacion, string codigoDactilar)
        {
            Identificacion = identificacion;
            Codigo_Dactilar = codigoDactilar;
        }
    }
}
