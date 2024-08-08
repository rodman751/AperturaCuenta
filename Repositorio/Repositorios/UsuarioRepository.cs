using Entidades;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class UsuarioRepository : Interfaces.IUsuarioRepository
    {
        private readonly DbContext _context;

        public UsuarioRepository(DbContext context)
        {
            _context = context;
        }

     
        public async Task<List<Usuario>> EjecutarProcedimientoAlmacenado()
        {
            return await _context.EjecutarProcedimientoAlmacenado();


        }
    }
}
