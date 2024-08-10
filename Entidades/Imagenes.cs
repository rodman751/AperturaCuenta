using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.CuentaApertura
{
    public class Imagenes
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string Codigo_Dactilar { get; set; }

        public byte[] Foto { get; set; }
    }
}
