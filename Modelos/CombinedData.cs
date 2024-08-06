using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class CombinedData
    {
        public Modelos.DatosDactilares? DatosDactilares { get; set; }
        public Usuario? Usuario { get; set; }
        public DireccionMapa? DireccionMapa { get; set; }
        public Modelos.InformacionPersonal? InformacionPersonal { get; set; }
        public OTP? OTP { get; set; }
        public FaceScan? FaceScan { get; set; }
    }
}
