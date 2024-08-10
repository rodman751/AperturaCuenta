using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface IRegistrosRepository
    {
        Task GuardarAuditora(Entidades.CuentaApertura.RegistrosAuditoria RegistroAuditoria, string estado);
    }
}
