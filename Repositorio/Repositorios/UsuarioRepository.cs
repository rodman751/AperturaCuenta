using Entidades;
using Entidades.CuentaApertura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class UsuarioRepository:Interfaces.IUsuarioRepository
    {
        private readonly DbContext _context;

        public UsuarioRepository(DbContext context)
        {
            _context = context;
        }

        public void CreateCombinedData(CombinedData usuario)
        {
            _context.CombinedData.Add(usuario);
        }
        public void ObtenerUsuario()
        {
            _context.DatosDactilares.ToList();
        }
    }
}
