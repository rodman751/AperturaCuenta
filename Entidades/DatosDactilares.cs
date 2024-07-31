using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class DatosDactilares
    {

        [Key]
        public string Identificacion { get; set; }

        public string Codigo_Dactilar { get; set; }
       
    }
}
