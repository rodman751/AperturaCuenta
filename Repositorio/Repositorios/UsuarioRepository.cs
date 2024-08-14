using Dapper;
using Entidades;
using Entidades.CuentaApertura;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositorio.Repositorios
{
    public class UsuarioRepository : Interfaces.IUsuarioRepository
    {
        private readonly DbContext _context;
        private readonly string _connectionString;
        public UsuarioRepository(IConfiguration configuration, DbContext context)
        {
            _connectionString = configuration.GetConnectionString("DbContext")
                                ?? throw new InvalidOperationException("Connection string 'DbContext' not found.");
            _context = context;
        }
        
        public async Task<List<Usuario>> EjecutarProcedimientoAlmacenado()
        {
            return await _context.EjecutarProcedimientoAlmacenado();
        }

        public async Task GuardarUsuario(CuentaUsuario cuenta)
        {

            //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
           // {
                try
                {
                    // Usar DbContext para operaciones CRUD
                    _context.Add(cuenta);
                    await _context.SaveChangesAsync();

                    // Usar Dapper para ejecutar el procedimiento almacenado
                    using (IDbConnection dbConnection = new SqlConnection(_connectionString))
                    {
                        await dbConnection.ExecuteAsync("EXEC sp_UpdateImageUrl");
                    }

                    // Commit the transaction
                    //transaction.Complete();
                }
                catch (Exception ex)
                {
                    // Log the exception or rethrow it
                    throw new Exception("Error al guardar el usuario", ex);
                }
           //}
        }
      




    }
}
