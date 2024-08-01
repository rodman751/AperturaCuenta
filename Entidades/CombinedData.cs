
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.CuentaApertura
{
    public class CombinedData
    {
        public int id { get; set; }
        public DatosDactilares ?DatosDactilares { get; set; }
        public Usuario ?Usuario { get; set; }
        public DireccionMapa ?DireccionMapa { get; set; }
        public InformacionPersonal ?InformacionPersonal { get; set; }
        public OTP ?OTP { get; set; }
        public FaceScan? FaceScan { get; set; }

    }
}
