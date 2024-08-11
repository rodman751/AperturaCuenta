using Entidades.CuentaApertura;
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
        Task GuardarImagen(Imagenes imagen);

        Task<List<Imagenes>> ObtenerTodasLasImagenes();
        //Task ActualizarCuentaUsuarioAsync(CuentaUsuario cuentaUsuario);
    }
}
