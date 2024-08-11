using Entidades.CuentaApertura;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task GuardarImagen(Imagenes imagen)
        {
            _context.Add(imagen);
            _context.SaveChanges();
        }

        public async Task<List<Imagenes>> ObtenerTodasLasImagenes()
        {
            return await _context.Imagenes.ToListAsync();
        }
        //public async Task ActualizarCuentaUsuarioAsync(CuentaUsuario cuentaUsuario)
        //{
        //    try
        //    {
        //        // Guardar el CuentaUsuario
        //        _context.CuentaUsuario.Update(cuentaUsuario);
        //        await _context.SaveChangesAsync();

        //        // Obtener la imagen correspondiente
        //        var imagen = await _context.Imagenes
        //                                   .FirstOrDefaultAsync(img => img.Identificacion == cuentaUsuario.Identificacion);

        //        if (imagen != null)
        //        {
        //            // Actualizar el campo ImageUrl
        //            cuentaUsuario.ImageUrl = imagen.Foto;

        //            // Necesitamos volver a actualizar el registro en la base de datos
        //            _context.Entry(cuentaUsuario).Property(c => c.ImageUrl).IsModified = true;
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar y registrar la excepción
        //        // Puedes usar logging aquí para obtener más detalles sobre el error
        //        Console.WriteLine($"Se produjo un error: {ex.Message}");
        //        throw;
        //    }
        //}






    }
}
