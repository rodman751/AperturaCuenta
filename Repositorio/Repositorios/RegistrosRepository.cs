using Entidades.CuentaApertura;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio.Interfaces;

namespace Repositorio.Repositorios
{
    public class RegistrosRepository : IRegistrosRepository
    {
        private readonly DbContext _context;

        public RegistrosRepository(DbContext context)
        {
            _context = context;
        }

        public async Task GuardarAuditora(Entidades.CuentaApertura.RegistrosAuditoria RegistroAuditoria , string estado)
        {
            RegistroAuditoria.EstadoOTP = estado;
            _context.Add(RegistroAuditoria);
            //_context.SaveChangesAsync();
            _context.SaveChanges();
        }

    }
}
