using Entidades;
using Entidades.CuentaApertura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Interfaces
{
    public interface IUsuarioRepository
    {
        void CreateCombinedData(CombinedData usuario);
        List<DatosDactilares> ObtenerUsuarios();
        Task<List<Usuario>> EjecutarProcedimientoAlmacenado();
    }
}
