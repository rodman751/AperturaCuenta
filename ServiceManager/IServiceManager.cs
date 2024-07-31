using Interface.AperturaCuenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManager
{
    public interface IServiceManager
    {
        ICookieService CookieService { get; }
    }
}
